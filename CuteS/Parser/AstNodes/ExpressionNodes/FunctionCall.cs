using System.Text;

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

        public override string Emit() 
        {
            StringBuilder argumentsValues = new();

            foreach (Expression argumentValue in ArgumentsValues) argumentsValues.Append(argumentValue.Emit()).Append(", ");

            string values = "";
            if (ArgumentsValues.Length != 0) values = argumentsValues.ToString().Substring(0, argumentsValues.Length - 2);

            return $"{FunctionName.Emit()}({values})";
        }

        public override string ToString()
        {
            StringBuilder argumentsValues = new();

            foreach (Expression argumentValue in ArgumentsValues) argumentsValues.Append(argumentValue.ToString());

            return $"FunctionCall(FunctionName({FunctionName});ArgumentsValues({argumentsValues}););";
        }
    }
}