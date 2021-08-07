using System;
using System.Text;
using System.Collections.Generic;

namespace CuteS.LexicalAnalyser
{
    public class LexerError : Exception
    {
        public LexerError(string message, int line) : base($"Lexer error: [line: {line}] {message}\n") { }

        public LexerError(List<char> bufferedLine, string message, int line) : base(ReadableError(bufferedLine, message, line)) { }

        private static string ReadableError(List<char> bufferedLine, string message, int line)
        {
            StringBuilder errorMessage = new($"Lexer error: [line: {line}] {message}\n");
            foreach (char symbol in bufferedLine) errorMessage.Append(symbol);
            return errorMessage.Append(new string(' ', bufferedLine.Count - 1) + '^').ToString();
        }
    }
}
