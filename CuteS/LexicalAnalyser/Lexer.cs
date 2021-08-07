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
        }

        private void ReserveWord(WordToken word) => IdentifiersTable.Add(word.Lexeme, word);

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

        private bool IsOperatorSymbol(char symbol) => "~!%^&*-+=|/<>:".IndexOf(symbol) > -1;

        private Token NextOperator()
        {
            // TODO: Implementation 
            throw new NotImplementedException();
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

                WordToken token = (WordToken) IdentifiersTable[buffer.ToString()];
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
            while (_position != _stream.Length && char.IsDigit(_stream[_position]))
            {
                currentLineBuffer.Add(_stream[_position]);
                value = value * 10 + _stream[_position] - '0';
                _position++;
            }

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
