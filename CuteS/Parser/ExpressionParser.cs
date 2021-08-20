using System.Collections.Generic;

using CuteS.LexicalAnalyser;
using CuteS.LexicalAnalyser.Tokens;

using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;

namespace CuteS.SyntaxAnalyser
{
    public class ExpressionParser : AbstractParser<Expression>
    {
        private List<WordToken> _assignOperators = new List<WordToken>
        {
            WordToken.AddAssign,
            WordToken.Assign,
            WordToken.BitAndAssign,
            WordToken.BitOrAssign,
            WordToken.DivAssign,
            WordToken.LShiftAssign,
            WordToken.ModAssign,
            WordToken.MulAssign,
            WordToken.RShiftAssign,
            WordToken.SubAssign,
            WordToken.XorAssign
        };

        public ExpressionParser(ref Lexer lex) : base(ref lex) { }

        public override Expression Parse()
        {
            Expression expression = BoolOrTerm();

            if (Lex.CurrentToken is WordToken token && _assignOperators.Contains(token))
            {
                Match(Lex.CurrentToken.Tag);
                expression = new BinaryOperator(expression, BoolOrTerm(), token, Lex.Line);
            }
            return expression;
        }

        private Expression BoolOrTerm()
        {
            Expression expression = BoolAndTerm();

            while (Lex.CurrentToken is WordToken token && token.Tag == TokenAttributes.BoolOr)
            {
                Match(Lex.CurrentToken.Tag);
                expression = new BinaryOperator(expression, BoolOrTerm(), token, Lex.Line);
            }
            return expression;
        }

        private Expression BoolAndTerm()
        {
            Expression expression = BitOrTerm();

            while (Lex.CurrentToken is WordToken token && token.Tag == TokenAttributes.BoolAnd)
            {
                Match(Lex.CurrentToken.Tag);
                expression = new BinaryOperator(expression, BoolAndTerm(), token, Lex.Line);
            }
            return expression;
        }

        private Expression BitOrTerm()
        {
            Expression expression = XorTerm();

            while (Lex.CurrentToken is WordToken token && token.Tag == TokenAttributes.BitOr)
            {
                Match(Lex.CurrentToken.Tag);
                expression = new BinaryOperator(expression, BitOrTerm(), token, Lex.Line);
            }
            return expression;
        }

        private Expression XorTerm()
        {
            Expression expression = BitAndTerm();

            while (Lex.CurrentToken is WordToken token && token.Tag == TokenAttributes.Xor)
            {
                Match(Lex.CurrentToken.Tag);
                expression = new BinaryOperator(expression, XorTerm(), token, Lex.Line);
            }
            return expression;
        }

        private Expression BitAndTerm()
        {
            Expression expression = RelationTerm();

            while (Lex.CurrentToken is WordToken token && token.Tag == TokenAttributes.BitAnd)
            {
                Match(Lex.CurrentToken.Tag);
                expression = new BinaryOperator(expression, BitAndTerm(), token, Lex.Line);
            }
            return expression;
        }

        private Expression RelationTerm()
        {
            Expression expression = RelationEqTerm();

            while (Lex.CurrentToken is WordToken token && (token.Tag == TokenAttributes.NotEq || token.Tag == TokenAttributes.Eq))
            {
                Match(Lex.CurrentToken.Tag);
                expression = new BinaryOperator(expression, RelationTerm(), token, Lex.Line);
            }
            return expression;
        }

        private Expression RelationEqTerm()
        {
            Expression expression = ShiftTerm();

            while (Lex.CurrentToken is WordToken token && 
            (token.Tag == TokenAttributes.Lower || token.Tag == TokenAttributes.Greater || token.Tag == TokenAttributes.LowerEq || token.Tag == TokenAttributes.GreaterEq))
            {
                Match(Lex.CurrentToken.Tag);
                expression = new BinaryOperator(expression, RelationEqTerm(), token, Lex.Line);
            }
            return expression;
        }

        private Expression ShiftTerm()
        {
            Expression expression = AddTerm();

            while (Lex.CurrentToken is WordToken token && (token.Tag == TokenAttributes.RShift || token.Tag == TokenAttributes.LShift))
            {
                Match(Lex.CurrentToken.Tag);
                expression = new BinaryOperator(expression, ShiftTerm(), token, Lex.Line);
            }
            return expression;
        }

        private Expression AddTerm()
        {
            Expression expression = MultTerm();

            while (Lex.CurrentToken is WordToken token && (token.Tag == TokenAttributes.Add || token.Tag == TokenAttributes.Sub))
            {
                Match(Lex.CurrentToken.Tag);
                expression = new BinaryOperator(expression, AddTerm(), token, Lex.Line);
            }
            return expression;
        }

        private Expression MultTerm()
        {
            Expression expression = UnaryTerm();

            while (Lex.CurrentToken is WordToken token && 
            (token.Tag == TokenAttributes.Mul || token.Tag == TokenAttributes.Div || token.Tag == TokenAttributes.Mod))
            {
                Match(Lex.CurrentToken.Tag);
                expression = new BinaryOperator(expression, MultTerm(), token, Lex.Line);
            }
            return expression;
        }

        private Expression UnaryTerm()
        {
            if (Lex.CurrentToken is WordToken token &&
            (token.Tag == TokenAttributes.Add || token.Tag == TokenAttributes.Sub || token.Tag == TokenAttributes.BoolNot || token.Tag == TokenAttributes.BitNot))
            {
                Match(token.Tag);
                return new UnaryOperator(UnaryTerm(), token, Lex.Line);
            }
            else if (Lex.CurrentToken is WordToken wordToken && wordToken.Tag == TokenAttributes.New)
            {
                Match(wordToken.Tag);
                return new UnaryOperator(IdentifierCall(), wordToken, Lex.Line);
            }
            else return FactorTerm();
        }

        private Expression FactorTerm()
        {
            Expression expression;

            if (Lex.CurrentToken.Tag == '(')
            {
                Match('('); expression = BoolOrTerm(); Match(')');
                return expression;
            }
            else if (Lex.CurrentToken is WordToken token)
            {
                if (token.Tag == TokenAttributes.Identifier) return IdentifierCall();
                else if (token.Tag == TokenAttributes.True || token.Tag == TokenAttributes.False || token.Tag == TokenAttributes.String)
                {
                    Match(Lex.CurrentToken.Tag);
                    return new Constant(token, Lex.Line);
                }
                else throw new SyntaxError("Unknown primitive type", Lex.Line); // idk what is this
            }
            else if (Lex.CurrentToken is NumberToken numberToken)
            {
                Match(Lex.CurrentToken.Tag);
                return new Constant(numberToken, Lex.Line);
            }
            else throw new SyntaxError("Expected expression term", Lex.Line);
        }

        private Expression IdentifierCall()
        {
            Expression expression = IdentifierCallTerm();

            while (Lex.CurrentToken is WordToken token && Lex.CurrentToken.Tag == TokenAttributes.DotOp)
            {
                
                Match(Lex.CurrentToken.Tag);
                expression = new BinaryOperator(expression, IdentifierCall(), token, Lex.Line);
            }
            return expression;
        }

        private Expression IdentifierCallTerm()
        {
            Identifier identifier;
            if (Lex.CurrentToken is WordToken token && token.Tag == TokenAttributes.Identifier)
            {
                Match(Lex.CurrentToken.Tag);
                identifier = new(token, Lex.Line);

                if (Lex.CurrentToken.Tag == '(') 
                {
                    Match('(');
                    FunctionCall functionCall = new(identifier, ArgumentsValues(), Lex.Line);
                    Match(')');
                    return functionCall;
                }
                else return identifier;
            }
            else throw new SyntaxError("Expected identifier or function call", Lex.Line);
        }

        private Expression[] ArgumentsValues()
        {
            List<Expression> argumentsValues = new();

            while (Lex.CurrentToken.Tag != ')')
            {
                argumentsValues.Add(BoolOrTerm());

                if (Lex.CurrentToken.Tag == ',') Match(',');
                else break;
            }

            return argumentsValues.ToArray();
        }
    }
}