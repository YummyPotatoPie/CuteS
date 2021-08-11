using System.Collections.Generic;

using CuteS.LexicalAnalyser;
using CuteS.LexicalAnalyser.Tokens;

using CuteS.Parser.AstNodes;
using CuteS.Parser.AstNodes.ExpressionsNodes;
using CuteS.Parser.AstNodes.StatementsNodes;

namespace CuteS.Parser
{
    public class ClassesParser : AbstractParser<ClassesNode>
    {
        public ClassesParser(ref Lexer lexer) : base(ref lexer) { }

        public override ClassesNode Parse()
        {
            List<ClassNode> classes = new();

            while (Lex.CurrentToken.Tag != '}')
            {
                ClassNode classNode = Class();
                if (classNode != null) classes.Add(classNode);
            }

            if (classes.Count == 0) throw new SyntaxError();
            else return new ClassesNode(classes.ToArray(), Lex.Line);
        }

        private ClassNode Class()
        {
            if (Lex.CurrentToken.Tag != TokenAttributes.Class) return null;
            else 
            {
                IdentifierNode className;
                ClassBodyNode classBody;
                Match(TokenAttributes.Class); className = ClassName(); Match('{'); classBody = ClassBody(); Match('}'); 
                return new ClassNode(className, classBody, Lex.Line);
            }
        }

        private IdentifierNode ClassName()
        {
            if (Lex.CurrentToken is WordToken token && Lex.CurrentToken.Tag == TokenAttributes.Identifier)
            {
                string identifier = token.Lexeme;
                Match(TokenAttributes.Identifier);
                return new IdentifierNode(identifier, Lex.Line);
            }
            else throw new SyntaxError();
        }

        private ClassBodyNode ClassBody()
        {
            List<DeclarationNode> fields = new();
            List<FunctionNode> functions = new();

            while (Lex.CurrentToken.Tag != '}')
            {
                if (Lex.CurrentToken.Tag == TokenAttributes.Let) fields.Add(FieldDeclaration());
                else if (Lex.CurrentToken.Tag == TokenAttributes.Function) functions.Add(FunctionDeclaration());
                else throw new SyntaxError();
            }

            return new ClassBodyNode(fields.ToArray(), functions.ToArray(), Lex.Line);
        }

        private FunctionNode FunctionDeclaration()
        {
            Match(TokenAttributes.Function);

            IdentifierNode functionName;
            ArgumentNode[] arguments;
            TypeNode returnType;
            StatementsNode statements;
            if (Lex.CurrentToken is WordToken token && Lex.CurrentToken.Tag == TokenAttributes.Identifier)
            {
                functionName = new(token.Lexeme, Lex.Line);
                Match(TokenAttributes.Identifier);
            }
            else throw new SyntaxError();

            Match('(');
            arguments = Arguments();
            Match(')');

            Match(TokenAttributes.TypeOp);
            returnType = new(TypeName(), Lex.Line);

            Match('{');
            statements = new StatementsParser(ref Lex).Parse();
            Match('}');

            return new FunctionNode(functionName, arguments, returnType, statements, Lex.Line);
        }

        private ArgumentNode[] Arguments()
        {
            List<ArgumentNode> arguments = new();
            while (true)
            {
                IdentifierNode identifier;
                if (Lex.CurrentToken is WordToken token && Lex.CurrentToken.Tag == TokenAttributes.Identifier) 
                {
                    identifier = new(token.Lexeme, Lex.Line);
                    Match(TokenAttributes.Identifier);
                }
                else throw new SyntaxError();

                Match(TokenAttributes.TypeOp);
                TypeNode typeName = new(TypeName(), Lex.Line);

                arguments.Add(new ArgumentNode(identifier, typeName, Lex.Line));

                if (Lex.CurrentToken.Tag == ',') Match(',');
                else break;
            }

            return arguments.ToArray();
        }

        private DeclarationNode FieldDeclaration()
        {
            IdentifierNode identifier;
            TypeNode fieldType;
            ExpressionNode expression = null;

            Match(TokenAttributes.Let);
            if (Lex.CurrentToken is WordToken token && Lex.CurrentToken.Tag == TokenAttributes.Identifier) 
            {
                identifier = new(token.Lexeme, Lex.Line);
                Match(TokenAttributes.Identifier);
            }
            else throw new SyntaxError();

            Match(TokenAttributes.TypeOp);
            fieldType = new(TypeName(), Lex.Line);

            if (Lex.CurrentToken.Tag == TokenAttributes.Assign)
            {
                Match(TokenAttributes.Assign);
                expression = new ExpressionParser(ref Lex).Parse();
            }
            Match(';');

            return new DeclarationNode(identifier, fieldType, expression, Lex.Line);
        }

        private IdExpression TypeName()
        {
            if (Lex.CurrentToken.Tag != TokenAttributes.Identifier) throw new SyntaxError();
            else 
            {
                List<IdentifierNode> identifiers = new();
                while (true)
                {
                    if (Lex.CurrentToken is WordToken token && Lex.CurrentToken.Tag == TokenAttributes.Identifier)
                    {
                        string identifier = token.Lexeme;
                        identifiers.Add(new IdentifierNode(identifier, Lex.Line));
                        Match(TokenAttributes.Identifier);

                        if (Lex.CurrentToken.Tag == TokenAttributes.DotOp) Match(TokenAttributes.DotOp);
                        else break;
                    }
                    else throw new SyntaxError();
                }

                return new IdExpression(identifiers.ToArray(), Lex.Line);
            }
        }
    }
}