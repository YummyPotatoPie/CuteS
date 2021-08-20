using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;

namespace CuteS.SyntaxAnalyser.AstNodes
{
    public class Argument : Node 
    {
        public Identifier ArgumentName { get; private set; }

        public Type ArgumentType { get; private set; }

        public Argument(Identifier argumentName, Type argumentType, int line) : base(line)
        {
            ArgumentName = argumentName;
            ArgumentType = argumentType;
        }
    }
}