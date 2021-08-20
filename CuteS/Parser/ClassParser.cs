using System.Collections.Generic;

using CuteS.SyntaxAnalyser.AstNodes;
using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;
using CuteS.SyntaxAnalyser.AstNodes.StatementsNodes;

using CuteS.LexicalAnalyser;
using CuteS.LexicalAnalyser.Tokens;

namespace CuteS.SyntaxAnalyser
{
    public class ClassParser : AbstractParser<Class>
    {
        public ClassParser(ref Lexer lex) : base(ref lex) { }

        public override Class Parse()
        {
            Identifier className;
            List<Declaration> fields = new();
            List<Function> functions = new();

            Match(TokenAttributes.Class);
            if (Lex.CurrentToken is WordToken token && token.Tag == TokenAttributes.Identifier) className = new(token, Lex.Line);
            else throw new SyntaxError("Expected identifier", Lex.Line);
            Match(Lex.CurrentToken.Tag);

            Match('{');
            while (Lex.CurrentToken.Tag != '}')
            {
                if (Lex.CurrentToken.Tag == TokenAttributes.Let) fields.Add(Field());
                else if (Lex.CurrentToken.Tag == TokenAttributes.Function) functions.Add(Function());
                else throw new SyntaxError("Invalid expression in class body", Lex.Line);
            }
            Match('}');
            return new Class(className, fields.ToArray(), functions.ToArray(), Lex.Line);
        }

        public Declaration Field()
        {
            Type fieldType;
            Expression assignExpression;
            Match(TokenAttributes.Let);

            fieldType = Type();
            Match(TokenAttributes.TypeOp);

            assignExpression = new ExpressionParser(ref Lex).Parse();
            Match(';');
            return new Declaration(fieldType, assignExpression, Lex.Line);
        }

        private Function Function()
        {
            Type returnType;
            Identifier functionName;
            List<Argument> arguments = new();
            List<Statement> statements = new();
            Match(TokenAttributes.Function);

            returnType = Type();
            Match(TokenAttributes.TypeOp);

            if (Lex.CurrentToken is WordToken token && token.Tag == TokenAttributes.Identifier) functionName = new(token, Lex.Line);
            else throw new SyntaxError("Expected identifier", Lex.Line);
            Match(Lex.CurrentToken.Tag);

            Match('(');
            while (Lex.CurrentToken.Tag != ')')
            {
                arguments.Add(Argument());

                if (Lex.CurrentToken.Tag == ',') 
                {
                    Match(',');
                    continue;
                }
            }
            Match(')');

            Match('{');
            while (Lex.CurrentToken.Tag != '}')
            {
                statements.Add(new StatementParser(ref Lex).Parse());
            }
            Match('}');

            return new Function(functionName, returnType, arguments.ToArray(), statements.ToArray(), Lex.Line);
        }

        private Argument Argument()
        {
            Type argumentType = Type();
            Identifier argumentName;

            Match(TokenAttributes.TypeOp);
            if (Lex.CurrentToken is WordToken token && token.Tag == TokenAttributes.Identifier) argumentName = new(token, Lex.Line);
            else throw new SyntaxError("Expected identifier", Lex.Line);
            Match(Lex.CurrentToken.Tag);
            return new Argument(argumentName, argumentType, Lex.Line);
        }

        private  Type Type()
        {
            List<Identifier> typeIdentifiers = new();

            while (Lex.CurrentToken is WordToken token)
            {
                if (new List<int>() { TokenAttributes.Int, TokenAttributes.Float, TokenAttributes.String, TokenAttributes.Bool, TokenAttributes.Void}.Contains(token.Tag))
                {
                    Match(Lex.CurrentToken.Tag);
                    return new Type(null, new Identifier(token, Lex.Line), Lex.Line);
                }
                else 
                {
                    if (token.Tag != TokenAttributes.Identifier) throw new SyntaxError("Expected identifier", Lex.Line);
                    else typeIdentifiers.Add(new Identifier(token, Lex.Line));
                    Match(Lex.CurrentToken.Tag);

                    if (Lex.CurrentToken.Tag != TokenAttributes.DotOp) break;
                    else Match(TokenAttributes.DotOp);
                }
            }

            if (typeIdentifiers.Count > 2 ) throw new SyntaxError("Invalid type expression", Lex.Line);
            else if (typeIdentifiers.Count == 0) throw new SyntaxError("Expected type expression", Lex.Line);
            else if (typeIdentifiers.Count == 1) return new Type(null, typeIdentifiers[0], Lex.Line);
            else return new Type(typeIdentifiers[0], typeIdentifiers[1], Lex.Line);
        }
    }
}