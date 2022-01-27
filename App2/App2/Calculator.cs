using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public class Calculator
    {
        public static double Calculate(string expression)
        {
            int bracketCounter = 0;
            int operatorIndex = -1;

            // Loopar för att hitta index på sista operatören
            // Exempel från uttrycket 89-87+2 kommer det hämta index av + operatören 
            // Räknar även alla paranteser
            // Vi delar upp uttrycket i flera delar
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression.ElementAt(i);
                if (c == '(') bracketCounter++;
                else if (c == ')') bracketCounter--;
                else if ((c == '+' || c == '-' || c == '%') && i!=0 && bracketCounter == 0)//+, - och % operatör har första prioritet i uttrycket 
                {
                    // Kollar om sista karakäteren är en operatör isåfall ignorerar den det. Detta används för negativa tal
                    if (operatorIndex != i - 1)
                    {
                        operatorIndex = i;
                    }
                }
                else if ((c == '×' || c == '÷') && i != 0 && bracketCounter == 0 && operatorIndex < 0)//× och ÷ operatörer är andra i prioritet på uttrycket
                {
                    
                    operatorIndex = i;
                }
                else if ((c == '^' || c=='√') && bracketCounter == 0 && operatorIndex < 0)//^ och √ operatörer har sista prioritet på uttrycket
                {
                    operatorIndex = i;
                }
            }
            if (operatorIndex < 0)// Kollar om det inte finns någon operatör i uttrycket
            {
                expression = expression.Trim();
                if (expression.ElementAt(0) == '(' && expression.ElementAt(expression.Length - 1) == ')')
                    return Calculate(expression.Substring(1, expression.Length - 2));//Rekursivt anrop för funktionen calculate om uttrycket har parantes. Exempelvis (2+2)*(3+3)
                else
                    return Convert.ToDouble(expression);//Returnera resultatet av uttrycket eller deluttrycket 
            }
            else
            {
                //Switch för de olika matematiska funktionerna utifrån operatör
                //Funktionen Calculate kallas rekursivt för deluttrycket
                //Exempelvis för uttrycket 89-87-2 kan vi tag två deluttryck och för båda kalla funktionen calculate separat och på slutet räkna ut resultatet
                switch (expression.ElementAt(operatorIndex))
                {
                    case '+':
                        return Calculate(expression.Substring(0, operatorIndex)) + Calculate(expression.Substring(operatorIndex + 1));
                    case '-':
                        return Calculate(expression.Substring(0, operatorIndex)) - Calculate(expression.Substring(operatorIndex + 1));
                    case '×':
                        return Calculate(expression.Substring(0, operatorIndex)) * Calculate(expression.Substring(operatorIndex + 1));
                    case '÷':
                        return Calculate(expression.Substring(0, operatorIndex)) / Calculate(expression.Substring(operatorIndex + 1));
                    case '%':
                        return Calculate(expression.Substring(0, operatorIndex)) % Calculate(expression.Substring(operatorIndex + 1));
                    case '^':
                        return Math.Pow(Calculate(expression.Substring(0, operatorIndex)), Calculate(expression.Substring(operatorIndex + 1)));
                    case '√':
                        return Math.Sqrt(Calculate(expression.Substring(1, expression.Length - 1)));
                }
            }
            return 0;
        }
    }
}