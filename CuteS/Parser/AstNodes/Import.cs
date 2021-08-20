using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;

namespace CuteS.SyntaxAnalyser.AstNodes
{
    public class Import : Node 
    {
        public bool IsSpecialImport { get; private set; }

        public Identifier ImportName { get; private set; }

        public Import(Identifier importName, bool isSpecialImport, int line) : base(line) 
        {
            IsSpecialImport = isSpecialImport;
            ImportName = importName;
        }

        public override string Emit() => $"using {ImportName.Emit()};";

        public override string ToString() => $"Import({ImportName}IsSpecialImport({IsSpecialImport}););";
    }
}