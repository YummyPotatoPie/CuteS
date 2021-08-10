using System.Collections.Generic;

using CuteS.LexicalAnalyser;
using CuteS.LexicalAnalyser.Tokens;

using CuteS.Parser.AST.Nodes;

namespace CuteS.Parser
{
    public class ClassesParser : AbstractParser<ClassesNode>
    {
        public ClassesParser(Lexer lexer) : base(lexer) { }

        public override ClassesNode Parse()
        {
            List<ClassNode> classes = new();
            ClassNode classNode = Class();

            while (classNode != null)
            {
                classes.Add(classNode);
                classNode = Class();
            }

            if (classes.Count == 0) throw new SyntaxError();
            else return new ClassesNode(classes.ToArray(), Lex.Line);
        }

        private ClassNode Class()
        {
            if (CurrentToken.Tag != TokenAttributes.Class) return null;
            else 
            {
                IdentifierNode className;
                Match(TokenAttributes.Class); className = ClassName(); Match('{'); /*ClassBody*/ Match('}'); 
                return new ClassNode(className, Lex.Line);
            }
        }

        private IdentifierNode ClassName()
        {
            if (CurrentToken is WordToken && CurrentToken.Tag == TokenAttributes.Identifier)
            {
                string identifier = ((WordToken) CurrentToken).Lexeme;
                Match(TokenAttributes.Identifier);
                return new IdentifierNode(identifier, Lex.Line);
            }
            else throw new SyntaxError();
        }
    }
}