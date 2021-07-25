using System.Text.RegularExpressions;
using CuteS.Lexer.Tokens;
using CuteS.Lexer;
using CuteS.SyntaxAnalyser.AstNodes;

namespace CuteS.SyntaxAnalyser
{
    public class Parser
    {
        private Token _currentToken;

        private readonly LexAnalyser _lexer;

        public Parser(LexAnalyser lexer)
        {
            _lexer = lexer;
            _currentToken = _lexer.NextToken();
        }

        private void Error(string message) => throw new SyntaxError($"{message} at line {_lexer.Line}");

        private void Match(int tag)
        {
            if (_currentToken == null) Error("Unexpeced end of file");
            if (_currentToken.Tag == tag) _currentToken = _lexer.NextToken();
            else Error("Syntax error");
        }

        public Program Parse()
        {
            return new Program(Usings(), Namespace());
        }

        private UsingsNode Usings() => null;

        private NamespaceNode Namespace()
        {
            IdentifierNode namespaceIdentifier;
            ClassesNode namespaceClasses;
            Match((int)Attribute.Namespace); namespaceIdentifier = Identifier(); Match('{'); namespaceClasses = ClassesDeclarations(); Match('}');
            return new NamespaceNode(namespaceIdentifier, namespaceClasses);
        }

        private ClassesNode ClassesDeclarations()
        {
            ClassesNode classesDeclarations = new();
            ClassNode classDeclaration = ClassDeclaration();
            while (classDeclaration != null)
            {
                classesDeclarations.AddClassNode(classDeclaration);
                classDeclaration = ClassDeclaration();
            }
            return classesDeclarations;
        }

        private ClassNode ClassDeclaration()
        {
            if (_currentToken.Tag != (int)Attribute.Class) return null;

            IdentifierNode className;
            ClassBodyNode classBody;
            Match((int)Attribute.Class); className = Identifier(); Match('{'); classBody = ClassBody(); Match('}');
            return new ClassNode(className, classBody);
        }

        private ClassBodyNode ClassBody()
        {
            if (_currentToken.Tag == '}') return null;

            ClassBodyNode classBody = new();
            while (_currentToken.Tag != '}')
            {
                if (_currentToken.Tag == (int)Attribute.Function) classBody.AddFunction(Function());
            }
            return classBody;
        }

        private FunctionNode Function()
        {
            Match((int)Attribute.Function); Identifier(); Identifier(); Match('('); Match(')'); Match('{'); Match('}');
            return null;
        }

        private IdentifierNode Identifier()
        {
            if (_currentToken.Tag == (int)Attribute.Identificator && _currentToken is WordToken)
            {
                string lexeme = ((WordToken)_currentToken).Lexeme;
                Regex idRegex = new("^[a-zA-Z_$][a-zA-Z_$0-9]*$");
                if (idRegex.IsMatch(lexeme))
                {
                    Match((int)Attribute.Identificator);
                    return new IdentifierNode(lexeme);
                }
                else Error("");
            }
            else Error("");
            return new("");
        }
    }
}
