namespace Basic
{
    public class Evaluate
    {
        private System.Collections.Generic.Stack<double> _operands = new System.Collections.Generic.Stack<double>();
        private System.Collections.Generic.Stack<string> _operators = new System.Collections.Generic.Stack<string>();

        public double Eval(string input)
        {
            string[] expressions = Parse(input);
            double result = Eval(expressions);
            return result;
        }

        public double Eval(string[] expressions)
        {
            foreach (var exp in expressions)
            {
                double operand;
                if (exp == "+" || exp == "-" || exp == "*" || exp == "/")
                {
                    _operators.Push(exp);
                }
                else if (exp == "(")
                {

                }
                else if (exp == ")")
                {
                    while (_operators.Count > 0 && _operands.Count > 1)
                    {
                        Calculate();
                    }
                }
                else if (double.TryParse(exp.ToString(), out operand))
                {
                    _operands.Push(operand);
                }
            }
            while (_operands.Count > 1 && _operators.Count > 0)
            {
                Calculate();
            }
            return _operands.Pop();
        }

        public string[] Parse(string input)
        {
            System.Collections.Generic.List<string> expressions = new System.Collections.Generic.List<string>();
            string operandStr = string.Empty;
            foreach (char c in input)
            {
                if (c != ' ')
                {
                    double operand;
                    if (double.TryParse(c.ToString(), out operand))
                    {
                        if (expressions.Count > 0)
                        {
                            if (double.TryParse(expressions[expressions.Count - 1], out operand))
                            {
                                expressions[expressions.Count - 1] += c;
                            }
                            else
                            {
                                expressions.Add(c.ToString());
                            }
                        }
                        else
                        {
                            expressions.Add(c.ToString());
                        }
                    }
                    else
                    {
                        expressions.Add(c.ToString());
                        //if(operandStr != string.Empty)
                        //{
                        //    expressions.Add(operandStr);
                        //    operandStr = string.Empty;
                        //}
                    }
                }
            }
            return expressions.ToArray();
        }

        private void Calculate()
        {
            var opera = _operators.Pop();
            var first = _operands.Pop();
            var second = _operands.Pop();
            if (opera == "+")
            {
                _operands.Push(first + second);
            }
            else if (opera == "-")
            {
                _operands.Push(second - first);
            }
            else if (opera == "*")
            {
                _operands.Push(first * second);
            }
            else if (opera == "/")
            {
                _operands.Push(second / first);
            }
        }
    }
}