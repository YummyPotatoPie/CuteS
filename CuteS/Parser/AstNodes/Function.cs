using System.Text;

using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;
using CuteS.SyntaxAnalyser.AstNodes.StatementsNodes;

namespace CuteS.SyntaxAnalyser.AstNodes
{
    public class Function : Node 
    {
        public Identifier FunctionName { get; private set; }

        public Type ReturnType { get; private set; }

        public Argument[] Arguments { get; private set; }

        public Statement[] Statements { get; private set; }

        public Function(Identifier functionName, Type returnType, Argument[] arguments, Statement[] statements, int line) : base(line)
        {
            FunctionName = functionName;
            ReturnType = returnType;
            Arguments = arguments;
            Statements = statements;
        }

        public override string Emit()
        {
            StringBuilder arguments = new(), statements = new();

            foreach (Argument arg in Arguments) arguments.Append(arg.Emit()).Append(", ");
            foreach (Statement statement in Statements) statements.Append(statement.Emit()).Append('\n');

            string args = "";
            if (Arguments.Length != 0) args = arguments.ToString().Substring(0, arguments.Length - 2);
            
            return $"{ReturnType.Emit()} {FunctionName.Emit()}({args})\n{{\n{statements}}}\n";
        }

        public override string ToString()
        {
            StringBuilder arguments = new(), statements = new();

            foreach (Argument arg in Arguments) arguments.Append(arg.ToString());
            foreach (Statement statement in Statements) statements.Append(statement.ToString());

            return $"Function(FunctionName({FunctionName});ReturnType({ReturnType});Arguments({arguments});Statements({statements}););";
        }
    }
}