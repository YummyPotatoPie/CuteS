using System.Text;

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

        public override string Emit()
        {
            StringBuilder classes = new();

            foreach (Class clazz in Classes) classes.Append(clazz.Emit()).Append('\n');

            return $"namespace {NamespaceName.Emit()}\n{{\n\n{classes}\n}}";
        }

        public override string ToString()
        {
            StringBuilder classes = new();

            foreach (Class clazz in Classes) classes.Append(clazz.ToString());

            return $"Namespace(NamespaceName({NamespaceName});Classes({classes});";
        }
    }
}