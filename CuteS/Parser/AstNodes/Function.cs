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
    }
}