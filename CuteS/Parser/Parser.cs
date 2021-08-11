using System.Collections.Generic;

using CuteS.LexicalAnalyser;
using CuteS.LexicalAnalyser.Tokens;

using CuteS.Parser.AstNodes;

namespace CuteS.Parser
{
    // Dimitte mihi, quoniam ego sum optimus - (C) Descensus recursive 
    public class Parser : AbstractParser<ProgramNode>
    {
        public Parser(Lexer lexer) : base(ref lexer) => Lex.NextToken();

        public override ProgramNode Parse() => ParseProgram();

        public ProgramNode Parse(string sourceStream, string filename)
        {
            Lex.SetNewStream(sourceStream, filename);
            return Parse();
        }

        private ProgramNode ParseProgram()
        {
            return Program();
            throw new System.NotImplementedException();
        }

        private ProgramNode Program() => new ProgramNode(Imports(), Namespace(), 0);

        private ImportNode[] Imports()
        {
            List<ImportNode> imports = new();
            ImportNode import = Import();

            while (import != null)
            {
                imports.Add(import);
                import = Import();
            }

            return imports.ToArray();
        }

        private ImportNode Import()
        {
            if (Lex.CurrentToken.Tag == TokenAttributes.Import)
            {
                ImportNode import;
                Match(TokenAttributes.Import); import = new(NamespaceId(), Lex.Line); Match(';');
                return import;
            }
            else return null;
        }

        private NamespaceNode Namespace()
        {
            if (Lex.CurrentToken.Tag != TokenAttributes.Namespace) throw new SyntaxError();
            else 
            {
                IdExpression namespaceName;
                ClassesNode classes;
                Match(TokenAttributes.Namespace);  namespaceName = NamespaceId(); Match('{'); classes = new ClassesParser(ref Lex).Parse(); Match('}');
                return new NamespaceNode(namespaceName, classes, Lex.Line);
            }
        }

        private IdExpression NamespaceId()
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