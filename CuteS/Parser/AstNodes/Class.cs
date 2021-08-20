using System.Text;

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

        public override string Emit()
        {
            StringBuilder fields = new(), functions = new();

            foreach (Declaration field in Fields) fields.Append("public ").Append(field.Emit()).Append('\n');
            foreach (Function function in Functions) functions.Append("public ").Append(function.Emit()).Append('\n');

            return $"public class {ClassName.Emit()}\n{{\n{fields}\n{functions}}}";
        }

        public override string ToString()
        {
            StringBuilder fields = new(), functions = new();

            foreach (Declaration field in Fields) fields.Append(field.ToString());
            foreach (Function function in Functions) functions.Append(function.ToString());

            return $"Class(ClassName({ClassName});Fields({fields});Functions({functions}););";
        }
    }
}