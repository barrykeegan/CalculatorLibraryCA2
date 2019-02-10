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
    delegate double SingleOperand(double x);
    delegate double TwoOperands(double x, double y);

    public partial class frmCalculator : Form
    {
        //string used to store the operator chosen by user, will be used to control switch
        string chosenOperator = null;
        //used to specify whether chosen operation will be applied to one value only
        bool singletonOperator = false;
        //used to flag that an calculation has taken place or a command button was pressed.
        //On starting up calculator, it behaves as if an operation has occurred (turning on is an operation)
        //so that number entry can be processed correctly.
        //when number buttons pressed when this flag is set it will clear the display and set it to false.
        //the value in the display postOperation can be used as a value for subsequent operations
        bool postOperation = true;
        //used to store the values to work with
        double firstOperand = 0.0;
        double secondOperand = 0.0;


        public frmCalculator()
        {
            InitializeComponent();
        }

        private void ResetAllDefaults()
        {
            postOperation = true;
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
            //When in this function, the maindefaults can be reset as a new calculation
            //can now take place, either using the result being displayed or completely
            //new numbers at the user's choice, also if result being display then it's
            //postOperation is set to true
            ResetMainDefaults();
            postOperation = true;

            //strResult variable used to test value to be displayed so it can be formatted
            //appropriately if needs be
            string strResult = result.ToString();
            //display space is limited, make sure result can be displayed
            if (strResult.Contains("E") || strResult.Length > 11)
            {
                lblDisplay.Text = result.ToString("0.######E+0", CultureInfo.InvariantCulture);
            }
            //below two ifs deal with special double values of infinity and NaN being the outcome
            //of calculation. if either received use messagebox to display result and Reset All Defaults
            //as if the calculator has just been started up again as none of these values can be used
            //in subsequent calculations.
            else if (result == double.PositiveInfinity || result == double.NegativeInfinity)
            {
                MessageBox.Show("Your calculation resulted in infinity");
                ResetAllDefaults();
            }
            else if (result == double.NaN)
            {
                MessageBox.Show("Your calculation resulted as Not a Number");
                ResetAllDefaults();
            }
            else
            {
                lblDisplay.Text = result.ToString();
            }
        }
        
        //singleton operations, in one place, to be delegatised
        private void ProcessInvert()
        {
            DisplayResult(Calculator.Invert(firstOperand));          
        }

        private void ProcessSquare()
        {
            DisplayResult(Calculator.Square(firstOperand));
        }

        private void ProcessSquareRoot()
        {
            DisplayResult(Calculator.SquareRoot(firstOperand));
        }

        private void ProcessCube()
        {
            DisplayResult(Calculator.Cube(firstOperand));
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
                }
            }
            else
            {
                MessageBox.Show("The Factorial operation requires an integer value");
            }
            ResetMainDefaults();
        }

        //two operand methods in one place, to be delegatised
        private void ProcessTwoOperandOperation(TwoOperands operation)
        {
            DisplayResult(operation(firstOperand, secondOperand));
        }

        private void ProcessAdd()
        {
            DisplayResult(Calculator.Add(firstOperand, secondOperand));
        }

        private void ProcessSub()
        {
            DisplayResult(Calculator.Subtract(firstOperand, secondOperand));
        }

        private void ProcessMul()
        {
            DisplayResult(Calculator.Multiply(firstOperand, secondOperand));
        }

        private void ProcessDiv()
        {
            DisplayResult(Calculator.Divide(firstOperand, secondOperand));
        }

        private void ProcessExp()
        {
            DisplayResult(Calculator.BaseToExponent(firstOperand, secondOperand));
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
                        ProcessTwoOperandOperation(Calculator.Add);
                        break;
                    case "sub":
                        ProcessTwoOperandOperation(Calculator.Subtract);
                        break;
                    case "mul":
                        ProcessTwoOperandOperation(Calculator.Multiply);
                        break;
                    case "div":
                        ProcessTwoOperandOperation(Calculator.Divide);
                        break;
                    case "exp":
                        ProcessTwoOperandOperation(Calculator.BaseToExponent);
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

        //the plusminus operation is so simple that it can be processed on its own
        private void btnPlusMinus_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text != "")
            {
                lblDisplay.Text = Calculator.PlusMinus(double.Parse(lblDisplay.Text)).ToString();
                postOperation = true;
            }
        }

        //logic for each TwoOperandButtonClick was the same in each case
        //so was moved to a general purpose processor
        private void ProcessTwoOperandButtonClick(string operation)
        {
            if (lblDisplay.Text != "")
            {
                if (!singletonOperator && chosenOperator != null)
                {
                    ExecuteEqualsAction();
                }

                singletonOperator = false;
                chosenOperator = operation;
                SetFirstOperand();
                postOperation = true;
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            ProcessTwoOperandButtonClick("add");
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            ProcessTwoOperandButtonClick("sub");
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            ProcessTwoOperandButtonClick("div");
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            ProcessTwoOperandButtonClick("mul");
        }

        private void btnPow_Click(object sender, EventArgs e)
        {
            ProcessTwoOperandButtonClick("exp");
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
                //special case, where 0 has already been entered, ignore subsequent 0
                //presses so you dont have bunch of superfluous leading 0s
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
