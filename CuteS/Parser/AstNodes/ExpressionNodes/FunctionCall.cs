namespace CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes
{
    public class FunctionCall : Expression
    {
        public Identifier FunctionName { get; private set; }

        public Expression[] ArgumentsValues { get; private set; }

        public FunctionCall(Identifier functionName, Expression[] argumentsValues, int line) : base(line)
        {
            FunctionName = functionName;
            ArgumentsValues = argumentsValues;
        }
    }
}