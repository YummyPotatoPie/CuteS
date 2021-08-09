using System.Collections.Generic;

using CuteS.LexicalAnalyser;
using CuteS.LexicalAnalyser.Tokens;

using CuteS.Parser.AST;
using CuteS.Parser.AST.Nodes;

namespace CuteS.Parser
{
    // Dimitte mihi, quoniam ego sum optimus - (C) Descensus recursive 
    public class Parser
    {
        private Lexer _lexer;

        private Token _currentToken;

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            _currentToken = _lexer.NextToken();
        }

        private void Match(int tokenTag)
        {
            if (_currentToken == null) throw new SyntaxError();

            if (tokenTag == _currentToken.Tag)
            {
                _currentToken = _lexer.NextToken();
            }
            else throw new SyntaxError();
        }

        public Ast Parse() => ParseProgram();

        public Ast Parse(string sourceStream, string filename)
        {
            _lexer.SetNewStream(sourceStream, filename);
            return Parse();
        }

        private Ast ParseProgram()
        {
            ProgramNode program = Program();
            throw new System.NotImplementedException();
        }

        private ProgramNode Program() => new ProgramNode(Imports(), 0);

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
            if (_currentToken.Tag == TokenAttributes.Import)
            {
                ImportNode import;
                Match(TokenAttributes.Import); import = new(NamespaceId(), _lexer.Line); Match(';');
                return import;
            }
            else return null;
        }

        private IdExpression NamespaceId()
        {
            if (_currentToken.Tag != TokenAttributes.Identifier) throw new SyntaxError();
            else 
            {
                List<IdentifierNode> identifiers = new();
                while (_currentToken.Tag != ';')
                {
                    if (_currentToken is WordToken)
                    {
                        string identifier = ((WordToken) _currentToken).Lexeme;
                        identifiers.Add(new IdentifierNode(identifier, _lexer.Line));
                        Match(TokenAttributes.Identifier);

                        if (_currentToken.Tag == TokenAttributes.DotOp) Match(TokenAttributes.DotOp);
                    }
                    else throw new SyntaxError();
                }

                return new IdExpression(identifiers.ToArray(), _lexer.Line);
            }
        }
    }
}