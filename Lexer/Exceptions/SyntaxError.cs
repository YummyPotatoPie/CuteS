namespace Lexer.Exceptions
{
    /// <summary>
    /// Exception thrown if lexer found syntax error
    /// </summary>
    public class SyntaxError : System.Exception
    {
        /// <summary>
        /// Base constructor of SyntaxError 
        /// </summary>
        public SyntaxError() : base() { }

        /// <summary>
        /// Constructor that define error message
        /// </summary>
        /// <param name="message">Message that describe the error</param>
        public SyntaxError(string message) : base(message) { }
    }
}
