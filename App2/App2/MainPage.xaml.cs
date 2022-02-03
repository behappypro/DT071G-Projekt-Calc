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
        
        // Funktion för att lägga till nummer i uttrycket
        private void selectNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            // Om uttrycket är nollställd tömts det
            if (txtExpression.Text == "0") txtExpression.Text = "";
            
            // Om vi har gjort en kalkylation tidigare så tömmer vi uttrycket och börjar på nytt
            if (calculate == true)
            {
                txtExpression.Text = "";
                txtExpression.Text += button.Text;
                calculate = false;
            }

            else
            {
                // Om sista tecknet är en parentes och vi lägger till ett nummer, sätt gongertecken före
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
        
        // Funktion för att välja operatör
        private void selectOperator(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int operatorCounter = 0;
            Operator = false;

            // Om operatör redan finns i uttrycket och vi skriver ny operatör så ersätts den
            if (Operator == true && button.Text != "-" && button.Text != "(")
            {
                txtExpression.Text = txtExpression.Text.Remove(txtExpression.Text.Length - 1, 1);
                txtExpression.Text += button.Text;
            }
            // Om uttrycket är felaktigt och vi lägger till operatör så nollställs den
            if (txtExpression.Text == "Felaktigt uttryck")
            {
                txtExpression.Text = "0";
                calculate = false;
            }

            if (blockOperator == true && button.Text != "-" && button.Text != "(")
            {
                return;
            }

            // Om vi skriver en punkt och sista tecknet inte är en siffta så läggs noll innan
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

            // Om knappen är en parentes eller minustecken eller boolean är false
            if (button.Text == "(" || button.Text == "-" || Operator == false|| Decimal == false)
            {
            
                // Ät uttrycket nollställt?
                if (txtExpression.Text == "0"){

                    // Ät knappen en parentes eller roten ur, ersätt med värdet
                    if (button.Text == "(" || button.Text == "√")
                    {
                        txtExpression.Text = button.Text;
                    }
                    // Annars lägg till värde på befintligt
                    else
                    {
                        txtExpression.Text += button.Text;
                    }
                    // Är knappen slutparentes och uttrycket är noll, Lämna det noll 
                    if (button.Text == ")")
                    {
                        txtExpression.Text = "0";
                    }
                }

                else if (button.Text == "(")
                {
                  
                    // Loopar igenom alla operatörer
                    for (int i = 0; i < operators.Length; i++)
                    {
                        // Är sista tecknet en operatör, ökar räknaren med ett
                        if (txtExpression.Text.ElementAt(txtExpression.Text.Length - 1) == operators[i])
                        {
                            operatorCounter++;
                        }
                    }
                    // Är räknaren större än 1 läggs värdet till
                    if (operatorCounter > 0)
                    {
                        txtExpression.Text += button.Text;
                    }
                    // Annars läggs nedanstående värde
                    else txtExpression.Text += "×(";
                }

                else if (button.Text == "-")
                {
                    txtExpression.Text += button.Text;
                    Operator = false;
                    blockOperator = true;
                }

                else{
                    // Om det är slutparentes sätts boolean till false
                    if (button.Text == ")")
                    {
                        Operator = false;
                        blockOperator = false;
                    }
                    // Annars sätter vi de till true så att vi inte kan skriva dubbel operatör
                    else
                    {
                        Operator = true;
                        Decimal = true;
                    }
                    // Lägger till knappens operatör till uttrycket
                    blockOperator = false;
                    txtExpression.Text += button.Text;
                    
                }                
            }
            calculate = false;
            
        }
        // Funktion för att rensa hela uttrycket
        private void btnAllClear_Clicked(object sender, EventArgs e)
        {
            // Återställer texten och booleans till false
            txtExpression.Text = "0";
            Operator = false;
            blockOperator = false;

        }

        // Funktion för att radera senaste tecknet
        private void btnClear_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            // Hämtar senaste tecknet
            char lastElement = txtExpression.Text.ElementAt(txtExpression.Text.Length - 1);

            if (txtExpression.Text == "0") return;
            // Raderar sista tecknet
            txtExpression.Text = txtExpression.Text.Remove(txtExpression.Text.Length - 1, 1);

            // Om tecknet är en operatör så sätts booleans till false
            if (lastElement == '+'|| lastElement == '×' || lastElement == '÷' || lastElement == '-')
            {
                Operator = false;
                blockOperator = false;
            }
            else Operator = true;

            // Är fältet tomt så nollställs det
            if (string.IsNullOrEmpty(txtExpression.Text)) txtExpression.Text = "0";
        }

        // Funktion för att räkna ut ett uttryck
        private void btnCalculate_Clicked(object sender, EventArgs e)
        {
            calculate = true;
            // Hämtar värdet av uttrycket
            string expression = txtExpression.Text;
            currentNumber.Text = expression;
            // Om uttrycket är felaktikt och använder trycker på = så nollställs det
            if (expression == "Felaktigt uttryck")
            {
                txtExpression.Text = "0";
                currentNumber.Text ="";
            }
            else
            {
                // Skickar vårat uttryck som argument till klassen och funktionen
                double result = Calculator.Calculate(expression);

                // Är uttrycket inte ett nummer, då skrivs felmeddelande ut
                if (double.IsNaN(result))
                {
                    txtExpression.Text = "Felaktigt uttryck";
                }
                // Ananrs skrivs svaret ut
                else
                {
                    txtExpression.Text = result.ToString();
                }
            }

            
            
        }
    }
}
