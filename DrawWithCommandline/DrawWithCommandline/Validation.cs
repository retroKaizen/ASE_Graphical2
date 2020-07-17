using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DrawWithCommandline
{
    public class Validation
    {
        private TextBox txtCommand;
        public Boolean isValidCommand = true;
        public Boolean isSomethingInvalid = false;
        public int Raduis = 0;
        public int Width = 0;
        public int Height = 0;
        public int counter = 0;
        public int LoopCounter = 0;
        public int lineNumber = 0;

        public Boolean hasLoop = false;
        public Boolean hasEndLoop = false;
        public Boolean hasIf = false;
        public Boolean hasEndif = false;

        public int loopLineNo = 0, endLoopLineNo = 0, ifLineNo = 0, endIfLineNo = 0;
        public Validation(TextBox txtCommand)
        {
            this.txtCommand = txtCommand;

            int numberOfLines = txtCommand.Lines.Length;
            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txtCommand.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (!oneLineCommand.Equals(""))
                {
                    checkLineValidation(oneLineCommand);
                    lineNumber = (i + 1);
                    if (!isValidCommand)
                    {
                        MessageBox.Show("Error in line " + lineNumber);
                        isValidCommand = true;
                    }
                }

            }
            checkLoopAndIfValidation();
            if (!isValidCommand)
            {
                isSomethingInvalid = true;
            }
        }
        
    }
}
