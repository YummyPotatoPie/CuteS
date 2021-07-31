namespace CuteS.LexicalAnalyser.Tokens
{
    public class IntegerNumberToken : Token
    {
        public int Value { get; private set; }

        public IntegerNumberToken(int value) : base(TokenAttributes.Int) => Value = value;
    }
}