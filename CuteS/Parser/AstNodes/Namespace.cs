using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;

namespace CuteS.SyntaxAnalyser.AstNodes
{
    public class Namespace : Node 
    {
        public Identifier NamespaceName { get; private set; }

        public Class[] Classes { get; private set; }

        public Namespace(Identifier namespaceName, Class[] classes, int line) : base(line)
        {
            NamespaceName = namespaceName;
            Classes = classes;
        }
    }
}