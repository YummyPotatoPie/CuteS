using System.Collections.Generic;

using CuteS.LexicalAnalyser;
using CuteS.LexicalAnalyser.Tokens;

using CuteS.SyntaxAnalyser.AstNodes;
using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;

namespace CuteS.SyntaxAnalyser
{
    public sealed class Parser : AbstractParser<Program>
    {
        public Parser(ref Lexer lex) : base(ref lex) => Lex.NextToken();

        public override Program Parse() 
        {
            List<Import> imports = new();

            while (Lex.CurrentToken.Tag == TokenAttributes.Import) imports.Add(Import());

            if (Lex.CurrentToken.Tag != TokenAttributes.Namespace) throw new SyntaxError("Expected namespace body", Lex.Line);
            else return new Program(imports.ToArray(), Namespace(), Lex.Line);
        }

        private Import Import()
        {
            Import import;
            Match(TokenAttributes.Import);

            if (Lex.CurrentToken.Tag == TokenAttributes.Lower)
            {
                Match(TokenAttributes.Lower); 
                if (Lex.CurrentToken is WordToken token && token.Tag == TokenAttributes.Identifier) import = new(new Identifier(token, Lex.Line), true, Lex.Line);
                else throw new SyntaxError("Expected identifier", Lex.Line);
                Match(Lex.CurrentToken.Tag);
                Match(TokenAttributes.Greater);
            }
            else 
            {
                if (Lex.CurrentToken is WordToken token && token.Tag == TokenAttributes.String) import = new(new Identifier(token, Lex.Line), false, Lex.Line);
                else throw new SyntaxError("Expected literal", Lex.Line);
                Match(Lex.CurrentToken.Tag);
            }
            Match(';');
            return import;
        }

        private Namespace Namespace()
        {
            Identifier namespaceName;
            List<Class> namespaceClasses = new();
            Match(TokenAttributes.Namespace);

            if (Lex.CurrentToken is WordToken token && token.Tag == TokenAttributes.Identifier) namespaceName = new(token, Lex.Line);
            else throw new SyntaxError("Expected identifier", Lex.Line);
            Match(Lex.CurrentToken.Tag);

            Match('{');
            while (Lex.CurrentToken.Tag != '}') namespaceClasses.Add(Class());
            Match('}');

            if (namespaceClasses.Count == 0) throw new SyntaxError("Namespace must contains at least 1 class", Lex.Line);
            return new Namespace(namespaceName, namespaceClasses.ToArray(), Lex.Line);
        }

        private Class Class() => new ClassParser(ref Lex).Parse();
    }
}