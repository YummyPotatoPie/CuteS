using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using CuteS.LexicalAnalyser.Tokens;

namespace CuteS.LexicalAnalyser
{
    public class Lexer
    {
        private string _stream;

        private int _position = 0;

        private Hashtable _reservedWordsTable = new();

        private List<char> _currentLineBuffer = new();

        public int Line { get; private set; } = 1;

        public Token CurrentToken { get; private set; }

        public Lexer(string stream)
        {
            _stream = stream;

            // Reserve keywords
            ReserveWord(WordToken.Namespace);
            ReserveWord(WordToken.Class);
            ReserveWord(WordToken.Import);
            ReserveWord(WordToken.Function);
            ReserveWord(WordToken.Let);
            ReserveWord(WordToken.For);
            ReserveWord(WordToken.While);
            ReserveWord(WordToken.If);
            ReserveWord(WordToken.Else);
            ReserveWord(WordToken.Return);

            // Reserve special words
            ReserveWord(WordToken.Int);
            ReserveWord(WordToken.Float);
            ReserveWord(WordToken.String);
            ReserveWord(WordToken.Bool);
            ReserveWord(WordToken.Void);
            ReserveWord(WordToken.True);
            ReserveWord(WordToken.False);

            // Reserve operators
            ReserveWord(WordToken.Add);
            ReserveWord(WordToken.AddAssign);
            ReserveWord(WordToken.Assign);
            ReserveWord(WordToken.BitAnd);
            ReserveWord(WordToken.BitAndAssign);
            ReserveWord(WordToken.BitNot);
            ReserveWord(WordToken.BitOr);
            ReserveWord(WordToken.BitOrAssign);
            ReserveWord(WordToken.BoolAnd);
            ReserveWord(WordToken.BoolNot);
            ReserveWord(WordToken.BoolOr);
            ReserveWord(WordToken.Div);
            ReserveWord(WordToken.DivAssign);
            ReserveWord(WordToken.DotOp);
            ReserveWord(WordToken.Eq);
            ReserveWord(WordToken.Greater);
            ReserveWord(WordToken.GreaterEq);
            ReserveWord(WordToken.Lower);
            ReserveWord(WordToken.LowerEq);
            ReserveWord(WordToken.LShift);
            ReserveWord(WordToken.LShiftAssign);
            ReserveWord(WordToken.Mod);
            ReserveWord(WordToken.ModAssign);
            ReserveWord(WordToken.Mul);
            ReserveWord(WordToken.NotEq);
            ReserveWord(WordToken.RShift);
            ReserveWord(WordToken.RShiftAssign);
            ReserveWord(WordToken.Sub);
            ReserveWord(WordToken.SubAssign);
            ReserveWord(WordToken.TypeOp);
            ReserveWord(WordToken.Xor);
            ReserveWord(WordToken.XorAssign);
        }

        private void ReserveWord(WordToken word) => _reservedWordsTable.Add(word.Lexeme, word);

        private void SkipWhiteSpaces()
        {
            while (_position != _stream.Length && char.IsWhiteSpace(_stream[_position]))
            {
                if (_stream[_position] == '\n')
                {
                    Line++;
                    _currentLineBuffer.Clear();
                    _position++;
                    continue;
                }
                if (_stream[_position] == '\t')
                {
                    for (int i = 0; i < 4; i++) _currentLineBuffer.Add(' ');
                    _position++;
                    continue;
                }
                _currentLineBuffer.Add(_stream[_position]);
                _position++;
            }
        }

        private static bool IsOperatorSymbol(char symbol) => "~!%^&*-+=|/<>:.".IndexOf(symbol) > -1;

        private Token NextOperator()
        {
            StringBuilder buffer = new();

            while (_position != _stream.Length && IsOperatorSymbol(_stream[_position]))
            {
                _currentLineBuffer.Add(_stream[_position]);
                buffer.Append(_stream[_position]);
                _position++;
            }

            WordToken op = (WordToken) _reservedWordsTable[buffer.ToString()];
            if (op == null) throw new LexerError(_currentLineBuffer, "Invalid operator", Line);
            return op;
        }

        // TODO: NextLiteral() method for tokeniza lexemes like this "str" or "rtsdfFERGERGsdFQWRGERG2342ERDE2"
        private WordToken NextLiteral()
        {
            StringBuilder buffer = new();

            _position++;
            while (_position != _stream.Length && _stream[_position] != '"')
            {
                buffer.Append(_stream[_position]);
                _position++;
            }

            if (_position == _stream.Length) throw new LexerError("Unexpected end of file", Line);
            else 
            {
                _position++;
                return new WordToken(buffer.ToString(), TokenAttributes.String);
            }
        }

        private WordToken NextWord()
        {
            StringBuilder buffer = new(256);

            try
            {
                while (_position != _stream.Length && (char.IsLetterOrDigit(_stream[_position]) || _stream[_position] == '_'))
                {
                    buffer.Append(_stream[_position]);
                    _currentLineBuffer.Add(_stream[_position]);
                    _position++;
                }
                string identifier = buffer.ToString();

                WordToken token = (WordToken) _reservedWordsTable[identifier];
                if (token != null) return token;
                return new WordToken(identifier);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new LexerError("Identifier length cannot be more then 256 symbols", Line);
            }
        }

        private Token NextNumber()
        {
            int value = 0;
            if (_stream[_position] - '0' == 0) 
            {
                _currentLineBuffer.Add(_stream[_position]);
                _position++;
            }
            else {
                while (_position != _stream.Length && char.IsDigit(_stream[_position]))
                {
                    _currentLineBuffer.Add(_stream[_position]);
                    value = value * 10 + _stream[_position] - '0';
                    _position++;
                }
            }

            if (_position != _stream.Length && value == 0 && char.IsDigit(_stream[_position])) throw new LexerError(_currentLineBuffer, "Invalid number entry", Line);

            if (_position != _stream.Length && _stream[_position] == '.')
            {
                double floatValue = value;
                int divisor = 10;

                _currentLineBuffer.Add(_stream[_position]);
                _position++;
                while (_position != _stream.Length && char.IsDigit(_stream[_position]))
                {
                    _currentLineBuffer.Add(_stream[_position]);
                    floatValue += (_stream[_position] - '0') / divisor;
                    divisor *= 10;
                    _position++;
                }

                if (divisor == 10) throw new LexerError(_currentLineBuffer, "Expected number", Line);
                return new NumberToken(floatValue, TokenAttributes.Float); 

            }
            return new NumberToken(value, TokenAttributes.Int); 
        }

        /// <summary>
        /// Enables if lexer finds error at input stream
        /// </summary>
        /// <returns>Errors messages</returns>
        private string PanicMode()
        {
            StringBuilder errorMessages = new();

            Token token = null;
            do
            {
                try
                {
                    token = ReadNextToken();
                }
                catch (Exception ex)
                {
                    errorMessages.Append(ex.Message).Append("\n\n");
                }
            } while (token != null);

            return errorMessages.ToString();
        }

        private Token ReadNextToken()
        {
            SkipWhiteSpaces();
            if (_position == _stream.Length) return null;

            char currentSymbol = _stream[_position];
            if (char.IsLetter(currentSymbol) || currentSymbol == '_') return NextWord();
            if (char.IsDigit(currentSymbol)) return NextNumber();
            if (IsOperatorSymbol(currentSymbol)) return NextOperator();
            if (currentSymbol == '"') return NextLiteral();

            _currentLineBuffer.Add(_stream[_position]);
            _position++;
            return new Token(currentSymbol);
        }

        public void NextToken()
        {
            try
            {
                CurrentToken = ReadNextToken();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Compilation error:\n{ex.Message}\n\n{PanicMode()}");
                Environment.Exit(1);
            }
        }
    }
}
