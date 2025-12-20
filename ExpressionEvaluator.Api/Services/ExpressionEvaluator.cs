namespace ExpressionEvaluator.Api.Services
{
    public class ExpressionEvaluatorService
    {
        private static bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }
        private static double Calculate(string s) 
        {
            Stack<double> stack = new Stack<double>();
            int num = 0;
            char op = '+';

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                // build the number
                if (char.IsDigit(c))
                {
                    num = num * 10 + (c - '0');
                }

                // if current char is an operator OR end of string, apply previous op
                if (IsOperator(c) || i == s.Length - 1)
                {
                    switch (op)
                    {
                        case '+':
                            stack.Push(num);
                            break;
                        case '-':
                            stack.Push(-num);
                            break;
                        case '*':
                            stack.Push(stack.Pop() * num);
                            break;
                        case '/':
                            if (num == 0)
                                throw new Exception("Cannot divide by zero");
                            stack.Push(stack.Pop() / num);
                            break;
                    }

                    op = c;   // update the operation
                    num = 0;  // reset number
                }
            }

            // sum the stack
            double result = 0;
            while (stack.Count > 0)
            {
                result += stack.Pop();
            }
            return result;
        }
        
        private static bool ValidateExpression(string expression)
        {
            // check first character
            if (!char.IsDigit(expression[0]))
                return false;
            
            // check last character
            if (!char.IsDigit(expression[^1]))
                return false;
            
            // check for consecutive operators
            for (int i = 1; i < expression.Length; i++)
            {
                if (IsOperator(expression[i]) && IsOperator(expression[i - 1]))
                    return false;
            }

            // check for invalid characters
            foreach (char c in expression)
            {
                if (!char.IsDigit(c) && !IsOperator(c))
                    return false;
            }

            return true;
        }
        
        public string Evaluate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new Exception("Expression cannot be empty");

            // remove whitespaces
            expression = expression.Replace(" ", "");

            try
            {
                bool isValid = ValidateExpression(expression);
                if (!isValid)
                    throw new Exception("Expression format is invalid");

                double result = Calculate(expression);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid expression: " + ex.Message);
            }
        }
    }
}