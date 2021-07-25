using System.Collections.Generic;

namespace CuteS.SyntaxAnalyser.AstNodes
{
    public class ClassBodyNode
    {
        public List<DeclarationNode> Fields { get; private set; }

        public List<FunctionNode> Functions { get; private set; }

        public ClassBodyNode()
        {
            Fields = new();
            Functions = new();
        }

        public void AddField(DeclarationNode field) => Fields.Add(field);

        public void AddFunction(FunctionNode function) => Functions.Add(function);
    }
}
