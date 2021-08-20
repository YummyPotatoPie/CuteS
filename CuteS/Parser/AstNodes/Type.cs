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

        public override string Emit()
        {
            string namespaceName = NamespaceName == null ? "" : NamespaceName.Emit();
            string typeName = TypeName.Emit();

            if (namespaceName != "") return $"{namespaceName}.{typeName}";
            return typeName;
        }

        public override string ToString() 
        {
            string namespaceName = NamespaceName == null ? "NamespaceName(null);" : $"NamespaceName({NamespaceName});";
            string typeName = TypeName.ToString();

            return $"Type({namespaceName}{typeName});";
        } 
    }
}