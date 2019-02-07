using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorLibraryCA2
{
    public partial class frmCalculator : Form
    {
        //string used to store the operator chosen by user, will be used to control switch
        string chosenOperator = null;
        bool singletonOperator = false;
        bool postOperation = false;
        double firstOperand = 0.0;
        double secondOperand = 0.0;


        public frmCalculator()
        {
            InitializeComponent();
        }

        private void ResetDefaults()
        {
            chosenOperator = null;
            singletonOperator = false;
            firstOperand = 0.0;
            secondOperand = 0.0;
        }

        private void ProcessInvert()
        {
            double result = Calculator.Invert(firstOperand);
            if (result == double.PositiveInfinity)
            {
                MessageBox.Show("Division by 0 results in infinity!");
                lblDisplay.Text = "0";
            }
            else
            {
                lblDisplay.Text = result.ToString();
            }
            ResetDefaults();
        }

        private void ProcessFactorial()
        {
            if (firstOperand % 1 == 0)
            {
                if (firstOperand < 0)
                {
                    MessageBox.Show("The Factorial operation can only be run on positive values");
                }
                else
                {
                    lblDisplay.Text = Calculator.Factorial((int)firstOperand).ToString();
                }
            }
            else
            {
                MessageBox.Show("The Factorial operation requires an integer value");
            }
            ResetDefaults();
        }
        
        private void ProcessSquare()
        {
            lblDisplay.Text = Calculator.Square(firstOperand).ToString();
            ResetDefaults();
        }
        private void NumberButtonClick(int number)
        {
            string currLabelText = lblDisplay.Text;
            if(number == 0 )
            {
                if(currLabelText != "0" && currLabelText.Length < 8)
                {
                    lblDisplay.Text = currLabelText + number.ToString();
                }
            }
            if ( currLabelText == "0")
            {
                lblDisplay.Text = number.ToString();
            }
            else if (currLabelText.Length < 8)
            {
                lblDisplay.Text = currLabelText + number.ToString();
            }
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            NumberButtonClick(0);
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            NumberButtonClick(1);
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            NumberButtonClick(2);
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            NumberButtonClick(3);
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            NumberButtonClick(4);
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            NumberButtonClick(5);
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            NumberButtonClick(6);
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            NumberButtonClick(7);
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            NumberButtonClick(8);
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            NumberButtonClick(9);
        }

        private void btnPoint_Click(object sender, EventArgs e)
        {
            string currLabelText = lblDisplay.Text;
            if (!currLabelText.Contains("."))
            {
                if(currLabelText.Length==0)
                {
                    lblDisplay.Text = "0.";
                }
                else if(currLabelText.Length < 7)
                {
                    lblDisplay.Text = currLabelText + ".";
                }
            }
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            string currLabelText = lblDisplay.Text;
            if (chosenOperator !=null)
            {
                if (singletonOperator)
                {
                    firstOperand = double.Parse(lblDisplay.Text);
                    switch (chosenOperator)
                    {
                        case "sqrt":
                            lblDisplay.Text = Calculator.SquareRoot(firstOperand).ToString();
                            break;
                        case "fact":
                            ProcessFactorial();
                            break;
                        case "square":
                            ProcessSquare();
                            break;
                        case "cube":
                            lblDisplay.Text = Calculator.Cube(firstOperand).ToString();
                            break;
                        case "invert":
                            ProcessInvert();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void btnClearDisplay_Click(object sender, EventArgs e)
        {
            ResetDefaults();
            lblDisplay.Text = "";
        }

        private void btnFactorial_Click(object sender, EventArgs e)
        {
            if(lblDisplay.Text == "0")
            {
                singletonOperator = true;
                chosenOperator = "fact";
            }
            else
            {
                firstOperand = double.Parse(lblDisplay.Text);
                ProcessFactorial();
            }
        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text == "0")
            {
                singletonOperator = true;
                chosenOperator = "invert";
            }
            else
            {
                firstOperand = double.Parse(lblDisplay.Text);
                ProcessInvert();
            }
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text == "0")
            {
                singletonOperator = true;
                chosenOperator = "square";
            }
            else
            {
                firstOperand = double.Parse(lblDisplay.Text);
                ProcessSquare();
            }
        }
    }
}
