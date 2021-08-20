using CuteS.LexicalAnalyser.Tokens;

namespace CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes
{
    public class Constant : Expression
    {
        public Token Value { get; private set; }

        public Constant(Token value, int line) : base(line) => Value = value;

        public override string Emit() 
        {
            if (Value is WordToken token)
            {
                if (token.Tag == TokenAttributes.String) return '"' + token.Lexeme + '"';
                else return token.Lexeme;
            }
            else if (Value is NumberToken numberToken)
            {
                if (numberToken.Tag == TokenAttributes.Int) return ((int) numberToken.Value).ToString();
                else return ((double) numberToken.Value).ToString();
            }
            else return "";
        }

        public override string ToString() => Value is NumberToken token ? $"Constant({token});" : 
                                             Value is WordToken wordToken && wordToken.Tag == TokenAttributes.String ? $"Constant({wordToken});" : $"Constant({Value});";
    }
}