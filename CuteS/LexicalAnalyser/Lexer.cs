using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using CuteS.LexicalAnalyser.Tokens;

namespace CuteS.LexicalAnalyser
{
    public class Lexer
    {
        private readonly string _stream;

        private int _position = 0;

        public Hashtable _reservedWordsTable { get; private set; } = new();

        private List<char> currentLineBuffer = new();

        public int Line { get; private set; } = 1;

        public Hashtable IdentifiersTable { get; private set; } = new();

        public Lexer(string stream)
        {
            _stream = stream;

            // Reserve keywords
            ReserveWord(WordToken.Namespace);
            ReserveWord(WordToken.Class);
            ReserveWord(WordToken.Import);

            // Reserve special words
            // Nothing here for now

            // Reserve operators
            ReserveWord(WordToken.Add);
            ReserveWord(WordToken.AddAssign);
            ReserveWord(WordToken.Assign);
            ReserveWord(WordToken.BitAnd);
            ReserveWord(WordToken.BitAndAssign);
            ReserveWord(WordToken.BitNot);
            ReserveWord(WordToken.BitNotAssign);
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
                    currentLineBuffer.Clear();
                    _position++;
                    continue;
                }
                if (_stream[_position] == '\t')
                {
                    for (int i = 0; i < 4; i++) currentLineBuffer.Add(' ');
                    _position++;
                    continue;
                }
                currentLineBuffer.Add(_stream[_position]);
                _position++;
            }
        }

        private static bool IsOperatorSymbol(char symbol) => "~!%^&*-+=|/<>:.".IndexOf(symbol) > -1;

        // This method is a collection of garbage and shit code (for my opinion), but idk how to write it another way
        private Token NextOperator()
        {
            StringBuilder buffer = new();

            while (_position != _stream.Length && IsOperatorSymbol(_stream[_position]))
            {
                buffer.Append(_stream[_position]);
                _position++;
            }

            WordToken op = (WordToken) IdentifiersTable[buffer.ToString()];
            if (op == null) throw new LexerError(currentLineBuffer, "Invalid operator", Line);
            return op;
        }

        private WordToken NextWord()
        {
            StringBuilder buffer = new(256);

            try
            {
                while (_position != _stream.Length && (char.IsLetterOrDigit(_stream[_position]) || _stream[_position] == '_'))
                {
                    buffer.Append(_stream[_position]);
                    currentLineBuffer.Add(_stream[_position]);
                    _position++;
                }

                WordToken token = (WordToken) _reservedWordsTable[buffer.ToString()];
                if (token != null) return token;

                token = new(buffer.ToString());
                IdentifiersTable.Add(token.Lexeme, token);
                return token;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new LexerError("Identifier length cannot be more then 256 symbols", Line);
            }
        }

        private Token NextNumber()
        {
            int value = 0;
            if (_stream[_position] - '0' == 0) _position++;
            else {
                while (_position != _stream.Length && char.IsDigit(_stream[_position]))
                {
                    currentLineBuffer.Add(_stream[_position]);
                    value = value * 10 + _stream[_position] - '0';
                    _position++;
                }
            }

            if (_position != _stream.Length && value == 0 && char.IsDigit(_stream[_position])) throw new LexerError(currentLineBuffer, "Invalid number entry", Line);

            if (_position != _stream.Length && _stream[_position] == '.')
            {
                float floatValue = value;
                int divisor = 10;

                currentLineBuffer.Add(_stream[_position]);
                _position++;
                while (_position != _stream.Length && char.IsDigit(_stream[_position]))
                {
                    currentLineBuffer.Add(_stream[_position]);
                    floatValue += (_stream[_position] - '0') / divisor;
                    divisor *= 10;
                    _position++;
                }

                if (divisor == 10) throw new LexerError(currentLineBuffer, "Expected number", Line);
                return new Token((int)floatValue);

            }
            return new Token(value);
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

            currentLineBuffer.Add(_stream[_position]);
            _position++;
            return new Token(currentSymbol);
        }

        public Token NextToken()
        {
            try
            {
                return ReadNextToken();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Compilation error:\n{ex.Message}\n\n{PanicMode()}");
                Environment.Exit(1);
                
                // Useless return statements just needed to compile this without errors
                return null;
            }
        }
    }
}
