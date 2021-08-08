namespace CuteS.LexicalAnalyser.Tokens
{
    public class WordToken : Token
    {
        public string Lexeme { get; private set; }

        // Reserved words 
        public readonly static WordToken
            Namespace   = new("namespace", TokenAttributes.Namespace),
            Class       = new("class", TokenAttributes.Class),
            Import      = new("import", TokenAttributes.Import),
            Function    = new("fn", TokenAttributes.Function),
            Let         = new("let", TokenAttributes.Let),
            For         = new("for", TokenAttributes.For),
            While       = new("while", TokenAttributes.While),
            If          = new("if", TokenAttributes.If),
            Else        = new("else", TokenAttributes.Else),
            Return      = new("return", TokenAttributes.Return);

        // Primitive types
        public readonly static WordToken
            Int         = new("Int", TokenAttributes.Int),
            Float       = new("Float", TokenAttributes.Float),
            Bool        = new("Bool", TokenAttributes.Bool),
            String      = new("String", TokenAttributes.String);

        // Operators
        public readonly static WordToken
            AddAssign       = new("+=", TokenAttributes.AddAssign),
            SubAssign       = new("-=", TokenAttributes.SubAssign),
            DivAssign       = new("/=", TokenAttributes.DivAssign),
            MulAssign       = new("*=", TokenAttributes.MulAssign),
            ModAssign       = new("%=", TokenAttributes.ModAssign),
            RShiftAssign    = new(">>=", TokenAttributes.RShiftAssign),
            LShiftAssign    = new("<<=", TokenAttributes.LShiftAssign),
            BitAndAssign    = new("&=", TokenAttributes.BitAndAssign),
            BitOrAssign     = new("|=", TokenAttributes.BitOrAssign),
            XorAssign       = new("^=", TokenAttributes.XorAssign),
            BitNotAssign    = new("~=", TokenAttributes.BitNotAssign),
            NotEq           = new("!=", TokenAttributes.NotEq),
            Eq              = new("==", TokenAttributes.Eq),
            GreaterEq       = new(">=", TokenAttributes.GreaterEq),
            LowerEq         = new("<=", TokenAttributes.LowerEq),
            Assign          = new("=", TokenAttributes.Assign),
            Add             = new("+", TokenAttributes.Add),
            Sub             = new("-", TokenAttributes.Sub),
            Div             = new("/", TokenAttributes.Div),
            Mul             = new("*", TokenAttributes.Mul),
            Mod             = new("%", TokenAttributes.Mod),
            RShift          = new(">>", TokenAttributes.RShift),
            LShift          = new("<<", TokenAttributes.LShift),
            BitOr           = new("|", TokenAttributes.BitAnd),
            BoolOr          = new("||", TokenAttributes.BoolOr),
            BitAnd          = new("&", TokenAttributes.BitAnd),
            BoolAnd         = new("&&", TokenAttributes.BoolAnd),
            Xor             = new("^", TokenAttributes.Xor),
            BitNot          = new("~", TokenAttributes.BitNot),
            BoolNot         = new("!", TokenAttributes.BoolNot),
            TypeOp          = new(":", TokenAttributes.TypeOp),
            DotOp           = new(".", TokenAttributes.DotOp),
            Greater         = new(">", TokenAttributes.Greater),
            Lower           = new("<", TokenAttributes.Lower);

        /// <summary>
        /// WordToken base constructor
        /// </summary>
        /// <param name="lexeme">Lexeme that represents token</param>
        /// <param name="tag">Default value is 256 that equal TokenAttribute.Identifier</param>
        public WordToken(string lexeme, int tag = 256) : base(tag) => Lexeme = lexeme;
    }
}