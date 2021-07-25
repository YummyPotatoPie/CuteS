namespace CuteS.Lexer.Tokens
{
    public class WordToken : Token
    {
        public string Lexeme { get; private set; }

        public readonly static WordToken
            Using = new((int)Attribute.Using, "using"),
            Namespace = new((int)Attribute.Namespace, "namespace"),
            Function = new((int)Attribute.Function, "function"),
            Class = new((int)Attribute.Class, "class"),
            Integer = new((int)Attribute.BasicType, "int"),
            Float = new((int)Attribute.BasicType, "float"),
            Char = new((int)Attribute.BasicType, "char"),
            Bool = new((int)Attribute.BasicType, "bool");

        public WordToken(int tag, string lexeme) : base(tag) => Lexeme = lexeme;
    }
}
