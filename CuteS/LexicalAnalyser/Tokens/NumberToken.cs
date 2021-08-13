namespace CuteS.LexicalAnalyser.Tokens
{
    public class NumberToken : Token
    {
        public double Value { get; private set; }

        public NumberToken(double value, int type) : base(type) => Value = value;
    }
}