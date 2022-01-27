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
                txtExpression.Text += button.Text;
            }
      
        }
        
        private void selectOperator(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (txtExpression.Text == "0")
            {
                if (button.Text == "(")
                {
                    txtExpression.Text = button.Text;
                    calculate = false;
                }

                else
                {
                    txtExpression.Text += button.Text;
                    calculate = false;
                }
            }

            else
            {
                txtExpression.Text += button.Text;
                calculate = false;
            }
        }

        private void btnAllClear_Clicked(object sender, EventArgs e)
        {
            txtExpression.Text = "0";
            currentNumber.Text = "";
        }

        private void btnClear_Clicked(object sender, EventArgs e)
        {
            if (txtExpression.Text == "0") return;
            txtExpression.Text = txtExpression.Text.Remove(txtExpression.Text.Length - 1, 1);
            if (string.IsNullOrEmpty(txtExpression.Text)) txtExpression.Text = "0";
        }


        private void btnCalculate_Clicked(object sender, EventArgs e)
        {
            calculate = true;
            string expression = txtExpression.Text;
            currentNumber.Text = expression;
            double result = Calculator.Calculate(expression);
            txtExpression.Text = result.ToString();  
        }
    }
}
