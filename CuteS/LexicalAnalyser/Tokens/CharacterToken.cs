namespace CuteS.LexicalAnalyser.Tokens
{
    public class CharacterToken : Token
    {
        public char Character { get; private set; }

        public CharacterToken(char character) : base(TokenAttributes.Char) => Character = character;
    }
}
