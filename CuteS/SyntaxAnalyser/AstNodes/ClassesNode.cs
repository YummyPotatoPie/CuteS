using System.Collections.Generic;

namespace CuteS.SyntaxAnalyser.AstNodes
{
    public class ClassesNode
    {
        public List<ClassNode> NamespaceClasses { get; private set; }

        public ClassesNode() => NamespaceClasses = new();

        public void AddClassNode(ClassNode classNode) => NamespaceClasses.Add(classNode);
    }
}
