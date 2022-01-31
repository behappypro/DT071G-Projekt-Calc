using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        Boolean calculate = false;
        Boolean Operator = false;
        Boolean Decimal = false;
        Boolean blockOperator = false;
        char[] operators = { '+', '×', '÷', '-' };
        
        public MainPage()
        {
            InitializeComponent();
        }
        
        private void selectNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (txtExpression.Text == "0") txtExpression.Text = "";
            
            if (calculate == true)
            {
                txtExpression.Text = "";
                txtExpression.Text += button.Text;
                calculate = false;
            }

            else
            {
                if (txtExpression.Text.Length > 0 && txtExpression.Text.ElementAt(txtExpression.Text.Length - 1) == ')')
                {
                    txtExpression.Text += "×";
                }

               txtExpression.Text += button.Text;
            }
            Operator = false;
            Decimal = false;
            blockOperator = false;
        }
        
        private void selectOperator(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int operatorCounter = 0;
            Operator = false;

            if (Operator == true && button.Text != "-" && button.Text != "(")
            {
                txtExpression.Text = txtExpression.Text.Remove(txtExpression.Text.Length - 1, 1);
                txtExpression.Text += button.Text;
            }

            if (txtExpression.Text == "Felaktigt uttryck")
            {
                txtExpression.Text = "0";
                calculate = false;
            }

            if (blockOperator == true && button.Text != "-" && button.Text != "(")
            {
                return;
            }

            if (button.Text == ".")
            {
                if (!Char.IsDigit(txtExpression.Text.ElementAt(txtExpression.Text.Length - 1)) &&
                    txtExpression.Text.ElementAt(txtExpression.Text.Length - 1) != '.')
                {
                    txtExpression.Text += "0";
                }
                Operator = false;
                Decimal = true;
            }


            if (button.Text == "(" || button.Text == "-" || Operator == false|| Decimal == false)
            {
            
                if (txtExpression.Text == "0"){

                    if (button.Text == "(" || button.Text == "√")
                    {
                        txtExpression.Text = button.Text;
                    }

                    else
                    {
                        txtExpression.Text += button.Text;
                    }

                    if (button.Text == ")")
                    {
                        txtExpression.Text = "0";
                    }
                }

                else if (button.Text == "(")
                {
                  
                    for (int i = 0; i < operators.Length; i++)
                    {
                        if (txtExpression.Text.ElementAt(txtExpression.Text.Length - 1) == operators[i])
                        {
                            operatorCounter++;
                        }
                    }

                    if (operatorCounter > 0)
                    {
                        txtExpression.Text += button.Text;
                    }
                    else txtExpression.Text += "×(";
                }

                else if (button.Text == "-")
                {
                    txtExpression.Text += button.Text;
                    Operator = false;
                    blockOperator = true;
                }

                else{

                    if (button.Text == ")")
                    {
                        Operator = false;
                        blockOperator = false;
                    }
                    else
                    {
                        Operator = true;
                        Decimal = true;
                    }

                    blockOperator = false;
                    txtExpression.Text += button.Text;
                    
                }                
            }
            calculate = false;
            
        }

        private void btnAllClear_Clicked(object sender, EventArgs e)
        {
            txtExpression.Text = "0";
            Operator = false;
            blockOperator = false;

        }

        private void btnClear_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            char lastElement = txtExpression.Text.ElementAt(txtExpression.Text.Length - 1);

            if (txtExpression.Text == "0") return;
            txtExpression.Text = txtExpression.Text.Remove(txtExpression.Text.Length - 1, 1);

            if (lastElement == '+'|| lastElement == '×' || lastElement == '÷' || lastElement == '-')
            {
                Operator = false;
                blockOperator = false;
            }
            else Operator = true;

            if (string.IsNullOrEmpty(txtExpression.Text)) txtExpression.Text = "0";
        }


        private void btnCalculate_Clicked(object sender, EventArgs e)
        {
            calculate = true;
            string expression = txtExpression.Text;
            currentNumber.Text = expression;
            if (expression == "Felaktigt uttryck")
            {
                txtExpression.Text = "0";
                currentNumber.Text ="";
            }
            else
            {
                double result = Calculator.Calculate(expression);

                if (double.IsNaN(result))
                {
                    txtExpression.Text = "Felaktigt uttryck";
                }
                else
                {
                    txtExpression.Text = result.ToString();
                }
            }

            
            
        }
    }
}
