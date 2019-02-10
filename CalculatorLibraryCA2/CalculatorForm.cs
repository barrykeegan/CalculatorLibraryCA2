using System;
using System.Globalization;
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

        private void SetSecondOperand()
        {
            secondOperand = double.Parse(lblDisplay.Text);
        }

        private void DisplayResult(double result)
        {
            string strResult = result.ToString();
            if (strResult.Contains("E") || strResult.Length > 11)
            {
                lblDisplay.Text = result.ToString("0.#####E+0", CultureInfo.InvariantCulture);
            }
            else
            {
                lblDisplay.Text = result.ToString();
            }
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
                DisplayResult(result);
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
                }
                else
                {
                    DisplayResult(Calculator.Factorial(firstOperand));
                    postOperation = true;
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
            DisplayResult(Calculator.Square(firstOperand));
            postOperation = true;
            ResetMainDefaults();
        }

        private void ProcessSquareRoot()
        {
            double result = Calculator.SquareRoot(firstOperand);
            if (result == double.NaN)
            {
                MessageBox.Show("Result was Not a Number. Were you trying to get the Square Root of a negative number?");
                ResetAllDefaults();
            }
            else
            {
                DisplayResult(result);
                postOperation = true;
                ResetMainDefaults();
            }            
        }

        private void ProcessCube()
        {
            DisplayResult(Calculator.Cube(firstOperand));
            postOperation = true;
            ResetMainDefaults();
        }

        private void ProcessAdd()
        {
            DisplayResult(Calculator.Add(firstOperand, secondOperand));
            postOperation = true;
            ResetMainDefaults();
        }

        private void ProcessSub()
        {
            DisplayResult(Calculator.Subtract(firstOperand, secondOperand));
            postOperation = true;
            ResetMainDefaults();
        }

        private void ProcessMul()
        {
            DisplayResult(Calculator.Multiply(firstOperand, secondOperand));
            postOperation = true;
            ResetMainDefaults();
        }

        private void ProcessDiv()
        {
            DisplayResult(Calculator.Divide(firstOperand, secondOperand));
            postOperation = true;
            ResetMainDefaults();
        }

        private void ProcessExp()
        {
            DisplayResult(Calculator.BaseToExponent(firstOperand, secondOperand));
            postOperation = true;
            ResetMainDefaults();
        }

        private void ExecuteEqualsAction()
        {
            if (singletonOperator)
            {
                SetFirstOperand();
                switch (chosenOperator)
                {
                    case "sqrt":
                        ProcessSquareRoot();
                        break;
                    case "fact":
                        ProcessFactorial();
                        break;
                    case "square":
                        ProcessSquare();
                        break;
                    case "cube":
                        ProcessCube();
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
                SetSecondOperand();
                switch (chosenOperator)
                {
                    case "add":
                        ProcessAdd();
                        break;
                    case "sub":
                        ProcessSub();
                        break;
                    case "mul":
                        ProcessMul();
                        break;
                    case "div":
                        ProcessDiv();
                        break;
                    case "exp":
                        ProcessExp();
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text != "" && chosenOperator != null)
            {
                ExecuteEqualsAction();   
            }
        }

        private void btnCube_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text == "")
            {
                singletonOperator = true;
                chosenOperator = "cube";
            }
            else
            {
                SetFirstOperand();
                ProcessCube();
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

        private void btnPlusMinus_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text != "")
            {
                lblDisplay.Text = Calculator.PlusMinus(double.Parse(lblDisplay.Text)).ToString();
                postOperation = true;
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text != "")
            {
                if( !singletonOperator && chosenOperator != null)
                {
                    ExecuteEqualsAction();
                }
                singletonOperator = false;
                chosenOperator = "add";
                SetFirstOperand();
                postOperation = true;
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text != "")
            {
                if (!singletonOperator && chosenOperator != null)
                {
                    ExecuteEqualsAction();
                }
                singletonOperator = false;
                chosenOperator = "sub";
                SetFirstOperand();
                postOperation = true;
            }
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text != "")
            {
                if (!singletonOperator && chosenOperator != null)
                {
                    ExecuteEqualsAction();
                }
                singletonOperator = false;
                chosenOperator = "div";
                SetFirstOperand();
                postOperation = true;
            }
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text != "")
            {
                if (!singletonOperator && chosenOperator != null)
                {
                    ExecuteEqualsAction();
                }
                singletonOperator = false;
                chosenOperator = "mul";
                SetFirstOperand();
                postOperation = true;
            }
        }

        private void btnPow_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text != "")
            {
                if (!singletonOperator && chosenOperator != null)
                {
                    ExecuteEqualsAction();
                }
                singletonOperator = false;
                chosenOperator = "exp";
                SetFirstOperand();
                postOperation = true;
            }
        }

        private void btnClearDisplay_Click(object sender, EventArgs e)
        {
            ResetAllDefaults();
        }

        private void NumberButtonClick(int number)
        {
            //After an operation button is pressed, we need to re-initialise
            //the lblDisplay with the new number pressed
            if (postOperation)
            {
                //if -1 passed in as number, changed display to "0." as dot has been clicked
                lblDisplay.Text = number == -1 ? "0." : number.ToString();
                //change to false as last button pressed not a command button
                postOperation = false;
            }
            else
            {
                if (lblDisplay.Text == "0")
                {
                    if (number == -1)
                    {
                        lblDisplay.Text += ".";
                    }
                    else if (number != 0)
                    {
                        lblDisplay.Text = number.ToString();
                    }
                }
                else if (lblDisplay.Text.Length < 8)
                {
                    lblDisplay.Text += number == -1 ? "." : number.ToString();
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

        // Treating a decimal point as a number
        private void btnPoint_Click(object sender, EventArgs e)
        {
            NumberButtonClick(-1);            
        }        
    }
}
