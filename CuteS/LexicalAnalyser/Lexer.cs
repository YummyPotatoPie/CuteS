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

        private Hashtable _keywords = new();

        private List<char> currentLineBuffer = new();

        public int Line { get; private set; } = 1;

        public Lexer(string stream)
        {
            _stream = stream;

            // Reserve keywords
            ReserveWord(WordToken.Namespace);
            ReserveWord(WordToken.Class);
            ReserveWord(WordToken.Import);
        }

        private void ReserveWord(WordToken word) => _keywords.Add(word.Lexeme, word);

        private void SkipWhiteSpaces()
        {
            while (_position != _stream.Length && char.IsWhiteSpace(_stream[_position]))
            {
                currentLineBuffer.Add(_stream[_position]);
                if (_stream[_position] == '\n')
                {
                    Line++;
                    currentLineBuffer.Clear();
                }
                _position++;
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
                    currentLineBuffer.Add(_stream[_position]);
                    _position++;
                }
                return new WordToken(buffer.ToString());
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new LexerError("Identifier length cannot be more then 256 symbols", Line);
            }
        }

        public Token NextToken()
        {
            SkipWhiteSpaces();
            if (_position == _stream.Length) return null;

            char currentSymbol = _stream[_position];
            if (char.IsLetter(currentSymbol) || currentSymbol == '_') return NextWord();
            return null;
        }
    }
}
