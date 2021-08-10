using System.Collections.Generic;

using CuteS.LexicalAnalyser;
using CuteS.LexicalAnalyser.Tokens;

using CuteS.Parser.AST;
using CuteS.Parser.AST.Nodes;

namespace CuteS.Parser
{
    // Dimitte mihi, quoniam ego sum optimus - (C) Descensus recursive 
    public class Parser : AbstractParser<Ast>
    {
        public Parser(Lexer lexer) : base(lexer) { }

        public override Ast Parse() => ParseProgram();

        public Ast Parse(string sourceStream, string filename)
        {
            Lex.SetNewStream(sourceStream, filename);
            return Parse();
        }

        private Ast ParseProgram()
        {
            ProgramNode program = Program();
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
            if (CurrentToken.Tag == TokenAttributes.Import)
            {
                ImportNode import;
                Match(TokenAttributes.Import); import = new(NamespaceId(), Lex.Line); Match(';');
                return import;
            }
            else return null;
        }

        private NamespaceNode Namespace()
        {
            if (CurrentToken.Tag != TokenAttributes.Namespace) throw new SyntaxError();
            else 
            {
                NamespaceNode namespaceNode;
                Match(TokenAttributes.Namespace);  namespaceNode = new(NamespaceId(), Lex.Line); Match('{'); /* Parse classes */ Match('}');
                return namespaceNode;
            }
        }

        private IdExpression NamespaceId()
        {
            if (CurrentToken.Tag != TokenAttributes.Identifier) throw new SyntaxError();
            else 
            {
                List<IdentifierNode> identifiers = new();
                while (true)
                {
                    if (CurrentToken is WordToken && CurrentToken.Tag == TokenAttributes.Identifier)
                    {
                        string identifier = ((WordToken) CurrentToken).Lexeme;
                        identifiers.Add(new IdentifierNode(identifier, Lex.Line));
                        Match(TokenAttributes.Identifier);

                        if (CurrentToken.Tag == TokenAttributes.DotOp) Match(TokenAttributes.DotOp);
                        else break;
                    }
                    else throw new SyntaxError();
                }

                return new IdExpression(identifiers.ToArray(), Lex.Line);
            }
        }
    }
}