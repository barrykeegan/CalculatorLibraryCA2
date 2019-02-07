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
        //used to specify whether chosen operation will be applied to one value only
        bool singletonOperator = false;
        //used to flag that a calculation has taken place and what appears in the display
        //is the result, certain buttons pressed when this flag is set will clear the display
        //the value in the display postOperation can be used as a value for subsequent operations
        bool postOperation = false;
        //used to store the values to work with
        double firstOperand = 0.0;
        double secondOperand = 0.0;


        public frmCalculator()
        {
            InitializeComponent();
        }

        private void ResetAllDefaults()
        {

            postOperation = false;
            lblDisplay.Text = "";
            ResetMainDefaults();
        }

        private void ResetMainDefaults()
        {
            chosenOperator = null;
            singletonOperator = false;
            firstOperand = 0.0;
            secondOperand = 0.0;
        }

        private void SetFirstOperand()
        {
            firstOperand = double.Parse(lblDisplay.Text);
        }

        private void ProcessInvert()
        {
            double result = Calculator.Invert(firstOperand);
            postOperation = true;
            if (result == double.PositiveInfinity)
            {
                MessageBox.Show("Division by 0 results in infinity!");
                ResetAllDefaults();
            }
            else
            {
                lblDisplay.Text = result.ToString();
                ResetMainDefaults();
            }            
        }

        private void ProcessFactorial()
        {
            if (firstOperand % 1 == 0)
            {
                if (firstOperand < 0)
                {
                    MessageBox.Show("The Factorial operation can only be run on non-negative values");
                    chosenOperator = null;
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
            ResetMainDefaults();
        }
        
        private void ProcessSquare()
        {
            lblDisplay.Text = Calculator.Square(firstOperand).ToString();
            ResetMainDefaults();
        }

        private void ProcessSquareRoot()
        {
            lblDisplay.Text = Calculator.SquareRoot(firstOperand).ToString();
        }

        private void ProcessCube()
        {
            lblDisplay.Text = Calculator.Cube(firstOperand).ToString();
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text.Length != 0)
            {
                if (chosenOperator != null)
                {
                    if (singletonOperator)
                    {
                        SetFirstOperand();
                        switch (chosenOperator)
                        {
                            case "sqrt":
                                ProcessSquareRoot();
                                //lblDisplay.Text = Calculator.SquareRoot(firstOperand).ToString();
                                break;
                            case "fact":
                                ProcessFactorial();
                                break;
                            case "square":
                                ProcessSquare();
                                break;
                            case "cube":
                                ProcessCube();
                                //lblDisplay.Text = Calculator.Cube(firstOperand).ToString();
                                break;
                            case "invert":
                                ProcessInvert();
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        throw new NotImplementedException("Haven't implement non-singleton operators yet, execution shouldn't reach here until I do.");
                    }
                }
            }
        }

        private void btnFactorial_Click(object sender, EventArgs e)
        {
            if(lblDisplay.Text == "")
            {
                singletonOperator = true;
                chosenOperator = "fact";
            }
            else
            {
                SetFirstOperand();
                ProcessFactorial();
            }
        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text == "")
            {
                singletonOperator = true;
                chosenOperator = "invert";
            }
            else
            {
                SetFirstOperand();
                ProcessInvert();
            }
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text == "")
            {
                singletonOperator = true;
                chosenOperator = "square";
            }
            else
            {
                SetFirstOperand();
                ProcessSquare();
            }
        }

        private void btnSquareRoot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text == "")
            {
                singletonOperator = true;
                chosenOperator = "sqrt";
            }
            else
            {
                SetFirstOperand();
                ProcessSquareRoot();
            }
        }

        private void btnClearDisplay_Click(object sender, EventArgs e)
        {
            ResetAllDefaults();
        }

        private void NumberButtonClick(int number)
        {
            if (postOperation)
            {
                ResetAllDefaults();
                lblDisplay.Text = number.ToString();
            }
            else
            {
                if (lblDisplay.Text == "")
                {
                    lblDisplay.Text = number.ToString();
                }
                else if (lblDisplay.Text == "0")
                {
                    if (number != 0)
                    {
                        lblDisplay.Text = number.ToString();
                    }
                }
                else if (lblDisplay.Text.Length < 8)
                {
                    lblDisplay.Text += number.ToString();
                }
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
            if (!lblDisplay.Text.Contains("."))
            {
                if (lblDisplay.Text.Length == 0)
                {
                    lblDisplay.Text = "0.";
                }
                else if (lblDisplay.Text.Length < 7)
                {
                    lblDisplay.Text += ".";
                }
            }
        }
    }
}
