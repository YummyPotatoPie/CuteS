namespace CuteS.SyntaxAnalyser.AstNodes
{
    public class ClassNode
    {
        public IdentifierNode ClassName { get; private set; }

        public ClassBodyNode ClassBody { get; private set; }

        public ClassNode(IdentifierNode className, ClassBodyNode classBody)
        {
            ClassName = className;
            ClassBody = classBody;
        }
    }
}
