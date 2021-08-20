using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;

using CuteS.SyntaxAnalyser.AstNodes.StatementsNodes;

namespace CuteS.SyntaxAnalyser.AstNodes
{
    public class Class : Node 
    {
        public Identifier ClassName { get; private set; } 

        public Declaration[] Fields { get; private set; }

        public Function[] Functions { get; private set ;}

        public Class(Identifier className, Declaration[] fields, Function[] functions, int line) : base(line)
        {
            ClassName = className;
            Fields = fields;
            Functions = functions;
        }
    }
}