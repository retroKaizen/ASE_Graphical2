using DrawWithCommandline;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace DrawTests
{
    [TestClass]
    public class DrawingTests
    {

        [TestMethod]

        //Checking if this function validates if endif case or not

        public void checkLoopAndIfValidation()
        {
            String input;
            TextBox textbox = new TextBox();
            input = "counter = 5 \r\n If counter = 5 then \r\n radius + 25 \r\n Circle 5 \r\n EndIf";
            textbox.Text = input;
            Validation validation = new Validation(textbox);
            Boolean expectedOutcome;
            Boolean realOutcome;
            expectedOutcome = true;
            validation.checkLoopAndIfValidation();
            realOutcome = validation.isValidCommand;
            Assert.AreEqual(expectedOutcome, realOutcome);
        }

        //Testing if it validates a line of command

        [TestMethod()]
        public void CheckLineValidationTest()
        {
            String input;
            TextBox textbox = new TextBox();
            input = "Rectangle 100, 100";

            textbox.Text = input;
            Validation validation = new Validation(textbox);

            Boolean expectedOutcome;
            Boolean realOutcome;
            expectedOutcome = true;
            validation.checkLineValidation(input);
            realOutcome = validation.isValidCommand;
            Assert.AreEqual(expectedOutcome, realOutcome);
        }

        //Testing if it validates a variable

        [TestMethod()]

        public void checkIfVariableDefinedTest()
        {
            String input;
            TextBox textbox = new TextBox();
            input = "Radius = 20 \r\n Circle Radius";

            textbox.Text = input;
            Validation validation = new Validation(textbox);

            Boolean expectedOutcome;
            Boolean realOutcome;
            expectedOutcome = true;
            validation.checkIfVariableDefined("radius");
            realOutcome = validation.isValidCommand;
            Assert.AreEqual(expectedOutcome, realOutcome);
        }
    }
}