using System;

namespace CuteS.SyntaxAnalyser
{
    public class SyntaxError : Exception
    {
        public SyntaxError(string message, int line) : base($"Syntax error at line {line}: {message}") { }
    }
}