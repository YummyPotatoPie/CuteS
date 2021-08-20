using System.Text;

namespace CuteS.SyntaxAnalyser.AstNodes
{
    public class Program : Node 
    {
        public Import[] Imports { get; private set; }

        public Namespace NamespaceNode { get; private set; }

        public Program(Import[] imports, Namespace namespaceNode, int line) : base(line) 
        {
            Imports = imports;
            NamespaceNode = namespaceNode;
        }

        public override string Emit()
        {
            StringBuilder imports = new();

            foreach (Import import in Imports) imports.Append(import.Emit()).Append('\n');

            return $"{imports}\n{NamespaceNode.Emit()}";
        }

        public override string ToString() 
        {
            StringBuilder importsString = new();

            foreach (Import import in Imports) importsString.Append(import.ToString());

            return $"Program(Imports({importsString});{NamespaceNode});";
        }
    }
}