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
    delegate double SingleOperandCalculationFunctionDelegate(double x);
    delegate double TwoOperandsCalculationFunctionDelegate(double x, double y);
    delegate void ProcessSingletonDelegate(SingleOperandCalculationFunctionDelegate operation);
    

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
            btnEquals.Focus();
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
                lblDisplay.Text = result.ToString("0.####E+0", CultureInfo.InvariantCulture);
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

        //singleton operations delegatised
        private void ProcessOneOperandOperation(SingleOperandCalculationFunctionDelegate operation)
        {
            DisplayResult(operation(firstOperand));
        }

        //logic for OneOperandButtonClick is different depending on if the user is operating the calculator
        //as: [singleton-operation]-[number]-[equals], or just: [number]-[singleton-operation]
        private void ProcessOneOperandButtonClick(string operation, SingleOperandCalculationFunctionDelegate calcFunction)
        {
            if (lblDisplay.Text == "")
            {
                singletonOperator = true;
                chosenOperator = operation;
            }
            else
            {
                //a previous two operand calculation had been entered, the result of which will
                //be used for this singletonoperator calculation
                if (!singletonOperator && chosenOperator != null)
                {
                    ExecuteEqualsAction();
                }

                //to handle cases where equalsaction resulted in infinity or NaN
                if (lblDisplay.Text != "")
                {
                    SetFirstOperand();
                    ProcessOneOperandOperation(calcFunction);
                }
            }
        }

        private void btnCube_Click(object sender, EventArgs e)
        {
            ProcessOneOperandButtonClick("cube",  Calculator.Cube);
            btnFocusStealer.Focus();
        }        

        private void btnInvert_Click(object sender, EventArgs e)
        {
            ProcessOneOperandButtonClick("invert", Calculator.Invert);
            btnFocusStealer.Focus();
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            ProcessOneOperandButtonClick("square", Calculator.Square);
            btnFocusStealer.Focus();
        }

        private void btnSquareRoot_Click(object sender, EventArgs e)
        {
            ProcessOneOperandButtonClick("sqrt", Calculator.SquareRoot);
            btnFocusStealer.Focus();
        }

        //Factorial is a special case in so far as it has so many restrictions as to valid inputs:
        //integer only, non-negative
        //Cannot easily be delgatised with the other singleton operations.
        private void ProcessFactorial()
        {
            if (lblDisplay.Text == "")
            {
                singletonOperator = true;
                chosenOperator = "fact";
            }
            else
            {
                //a previous two operand calculation had been entered, the result of which will
                //be used for this factorial calculation
                if (!singletonOperator && chosenOperator != null)
                {
                    ExecuteEqualsAction();
                }

                //to handle cases where equals action resulted in infinity or NaN
                if (lblDisplay.Text != "")
                {
                    SetFirstOperand();
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
            }
            
        }

        private void btnFactorial_Click(object sender, EventArgs e)
        {
            ProcessFactorial();
            btnFocusStealer.Focus();
        }

        //the plusminus operation is so simple that it can be processed on its own
        private void btnPlusMinus_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text != "")
            {
                DisplayResult(Calculator.PlusMinus(double.Parse(lblDisplay.Text)));
                postOperation = true;
            }
            btnFocusStealer.Focus();
        }

        //two operand methods have been delegatised
        private void ProcessTwoOperandOperation(TwoOperandsCalculationFunctionDelegate operation)
        {
            DisplayResult(operation(firstOperand, secondOperand));
        }

        //logic for each TwoOperandButtonClick was the same in each case
        //so was moved to a general purpose processor
        private void ProcessTwoOperandButtonClick(string operation)
        {
            if (lblDisplay.Text != "")
            {
                //a previous two operand calculation had been entered, the result of which will
                //be used for the next calculation
                if (!singletonOperator && chosenOperator != null)
                {
                    ExecuteEqualsAction();
                }
                
                //to handle cases where equals action resulted in infinity or NaN
                if (lblDisplay.Text != "")
                {
                    singletonOperator = false;
                    chosenOperator = operation;
                    SetFirstOperand();
                    postOperation = true;
                }
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            ProcessTwoOperandButtonClick("add");
            btnEquals.Focus();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            ProcessTwoOperandButtonClick("sub");
            btnEquals.Focus();
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            ProcessTwoOperandButtonClick("div");
            btnEquals.Focus();
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            ProcessTwoOperandButtonClick("mul");
            btnEquals.Focus();
        }

        private void btnPow_Click(object sender, EventArgs e)
        {
            ProcessTwoOperandButtonClick("exp");
            btnEquals.Focus();
        }

        private void ExecuteEqualsAction()
        {
            if (singletonOperator)
            {
                SetFirstOperand();
                switch (chosenOperator)
                {
                    case "sqrt":
                        ProcessOneOperandOperation(Calculator.SquareRoot);
                        break;
                    case "fact":
                        ProcessFactorial();
                        break;
                    case "square":
                        ProcessOneOperandOperation(Calculator.Square);
                        break;
                    case "cube":
                        ProcessOneOperandOperation(Calculator.Cube);
                        break;
                    case "invert":
                        ProcessOneOperandOperation(Calculator.Invert);
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
            btnEquals.Focus();
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

        // Treating a decimal point as a number
        private void btnPoint_Click(object sender, EventArgs e)
        {
            NumberButtonClick(-1);
            btnEquals.Focus();
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            NumberButtonClick(0);
            btnEquals.Focus();
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            NumberButtonClick(1);
            btnEquals.Focus();
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            NumberButtonClick(2);
            btnEquals.Focus();
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            NumberButtonClick(3);
            btnEquals.Focus();
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            NumberButtonClick(4);
            btnEquals.Focus();
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            NumberButtonClick(5);
            btnEquals.Focus();
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            NumberButtonClick(6);
            btnEquals.Focus();
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            NumberButtonClick(7);
            btnEquals.Focus();
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            NumberButtonClick(8);
            btnEquals.Focus();
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            NumberButtonClick(9);
            btnEquals.Focus();
        }

        private void btnClearDisplay_Click(object sender, EventArgs e)
        {
            ResetAllDefaults();
            btnEquals.Focus();
        }
        
        private void frmCalculator_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '0':
                    NumberButtonClick(0);
                    break;
                case '1':
                    NumberButtonClick(1);
                    break;
                case '2':
                    NumberButtonClick(2);
                    break;
                case '3':
                    NumberButtonClick(3);
                    break;
                case '4':
                    NumberButtonClick(4);
                    break;
                case '5':
                    NumberButtonClick(5);
                    break;
                case '6':
                    NumberButtonClick(6);
                    break;
                case '7':
                    NumberButtonClick(7);
                    break;
                case '8':
                    NumberButtonClick(8);
                    break;
                case '9':
                    NumberButtonClick(9);
                    break;
                case '.':
                    NumberButtonClick(-1);
                    break;
                case '+':
                    ProcessTwoOperandButtonClick("add");
                    break;
                case '-':
                    ProcessTwoOperandButtonClick("sub");
                    break;
                case '*':
                    ProcessTwoOperandButtonClick("mul");
                    break;
                case '/':
                    ProcessTwoOperandButtonClick("div");
                    break;
                default:
                    break;
            }
        }
        
    }
}
