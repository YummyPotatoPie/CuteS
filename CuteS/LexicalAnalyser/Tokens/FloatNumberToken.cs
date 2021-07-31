namespace CuteS.LexicalAnalyser.Tokens
{
    public class FloatNumberToken : Token
    {
        public float Value { get; private set; }

        public FloatNumberToken(float value) : base(TokenAttributes.Float) => Value = value;
    }
}
