namespace ExpressionEvaluator.Api.Services
{
    public class ExpressionEvaluatorService
    {
        private double Calculate(string s) 
        {
            Stack<double> stack = new Stack<double>();
            int num = 0;
            char op = '+';

            for (int i = 0; i < s.Length; i++) 
            {
                char c = s[i];

                if (char.IsDigit(c)) 
                {
                    num = num * 10 + (c - '0');
                }

                if ((!char.IsDigit(c) && c != ' ') || i == s.Length - 1) 
                {
                    if (op == '+') 
                        stack.Push(num);
                    else if (op == '-') 
                        stack.Push(-num);
                    else if (op == '*') 
                        stack.Push(stack.Pop() * num);
                    else if (op == '/') 
                        stack.Push(stack.Pop() / num);

                    op = c;
                    num = 0;
                }
            }

            double result = 0;
            while (stack.Count > 0) {
                result += stack.Pop();
            }
            return result;
        }
        public string Evaluate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            throw new Exception("Expression cannot be empty");

            // remove whitespaces
            expression = expression.Replace(" ", "");

            try
            {
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