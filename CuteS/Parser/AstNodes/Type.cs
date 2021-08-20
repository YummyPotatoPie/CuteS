using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;

namespace CuteS.SyntaxAnalyser.AstNodes
{
    public class Type : Node 
    {
        public Identifier NamespaceName { get; private set; } = null;

        public Identifier TypeName { get; private set; } = null;

        public Type(Identifier namespaceName, Identifier typeName, int line) : base(line)
        {
            NamespaceName = namespaceName;
            TypeName = typeName;
        }
    }
}