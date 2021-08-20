using System.Collections.Generic;

using CuteS.LexicalAnalyser;
using CuteS.LexicalAnalyser.Tokens;

using CuteS.SyntaxAnalyser.AstNodes;
using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;
using CuteS.SyntaxAnalyser.AstNodes.StatementsNodes;

namespace CuteS.SyntaxAnalyser
{
    public class StatementParser : AbstractParser<Statement>
    {
        public StatementParser(ref Lexer lex) : base(ref lex) { }

        public override Statement Parse()
        {
            if (Lex.CurrentToken.Tag == TokenAttributes.Else) throw new SyntaxError("Unexpected else statement", Lex.Line);
            else if (Lex.CurrentToken.Tag == TokenAttributes.If) return IfStatement();
            else if (Lex.CurrentToken.Tag == TokenAttributes.While) return WhileStatement();
            else if (Lex.CurrentToken.Tag == TokenAttributes.For) return ForStatement();
            else if (Lex.CurrentToken.Tag == TokenAttributes.Return) 
            {
                ReturnStatement returnStatement = ReturnStatement();
                Match(';');
                return returnStatement;
            }
            else if (Lex.CurrentToken.Tag == TokenAttributes.Let) return Declaration();
            else 
            {
                ExpressionStatement expression = new (new ExpressionParser(ref Lex).Parse(), Lex.Line);
                Match(';');
                return expression;
            }
        }

        private IfStatement IfStatement()
        {
            Expression condition;
            List<Statement> statements = new();
            ElseStatement elseStatement = null;
            Match(TokenAttributes.If);

            Match('(');
            condition = new ExpressionParser(ref Lex).Parse();
            Match(')');

            Match('{');
            while (Lex.CurrentToken.Tag != '}') statements.Add(Parse());
            Match('}');

            if (Lex.CurrentToken.Tag == TokenAttributes.Else) elseStatement = ElseStatement();
            
            return new IfStatement(condition, statements.ToArray(), elseStatement, Lex.Line);
        }

        private ElseStatement ElseStatement()
        {
            Match(TokenAttributes.Else);

            if (Lex.CurrentToken.Tag == TokenAttributes.If) return new ElseStatement(new Statement[] { IfStatement() }, Lex.Line);
            else 
            {
                List<Statement> statements = new();
                Match('{');
                while (Lex.CurrentToken.Tag != '}') statements.Add(Parse());
                Match('}');
                return new ElseStatement(statements.ToArray(), Lex.Line);
            }
        }

        private WhileStatement WhileStatement()
        {
            Expression condition;
            List<Statement> statements = new();
            Match(TokenAttributes.While);

            Match('(');
            condition = new ExpressionParser(ref Lex).Parse();
            Match(')');

            Match('{');
            while (Lex.CurrentToken.Tag != '}') statements.Add(Parse());
            Match('}');

            return new WhileStatement(condition, statements.ToArray(), Lex.Line);
        }

        private ForStatement ForStatement()
        {
            Node iteratorExpression = null;
            Expression conditionExpression = null, cycleExpression = null;
            List<Statement> statements = new();

            Match(TokenAttributes.For);

            // idk how to rewtite it another way
            Match('(');
            if (Lex.CurrentToken.Tag == ';') Match(';');
            else 
            {
                if (Lex.CurrentToken.Tag == TokenAttributes.Let) iteratorExpression = Declaration();
                else 
                {
                    iteratorExpression = new ExpressionParser(ref Lex).Parse();
                    Match(';');
                }
            }
            if (Lex.CurrentToken.Tag == ';') Match(';');
            else 
            {
                conditionExpression = new ExpressionParser(ref Lex).Parse();
                Match(';');
            }
            if (Lex.CurrentToken.Tag != ')') cycleExpression = new ExpressionParser(ref Lex).Parse();
            Match(')');

            Match('{');
            while (Lex.CurrentToken.Tag != '}') statements.Add(Parse());
            Match('}');
            return new ForStatement(iteratorExpression, conditionExpression, cycleExpression, statements.ToArray(), Lex.Line);
        }

        private ReturnStatement ReturnStatement()
        {
            Match(TokenAttributes.Return);
            if (Lex.CurrentToken.Tag == ';') return new ReturnStatement(null, Lex.Line);
            else return new ReturnStatement(new ExpressionParser(ref Lex).Parse(), Lex.Line);
        }

        // Copy-paste from ClassParser
        private Declaration Declaration()
        {
            Type variableType;
            Expression assignExpression;
            Match(TokenAttributes.Let);

            variableType = Type();
            Match(TokenAttributes.TypeOp);

            assignExpression = new ExpressionParser(ref Lex).Parse();
            Match(';');
            return new Declaration(variableType, assignExpression, Lex.Line);
        }

        // Copy-paste from ClassParser
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

                    if (Lex.CurrentToken.Tag != '.') break;
                }
            }

            if (typeIdentifiers.Count > 2 ) throw new SyntaxError("Invalid type expression", Lex.Line);
            else if (typeIdentifiers.Count == 0) throw new SyntaxError("Expected type expression", Lex.Line);
            else if (typeIdentifiers.Count == 1) return new Type(null, typeIdentifiers[0], Lex.Line);
            else return new Type(typeIdentifiers[0], typeIdentifiers[1], Lex.Line);
        }
    }
}