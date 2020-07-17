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
        
        public void checkLoopAndIfValidation()
        {
            int numberOfLines = txtCommand.Lines.Length;


            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txtCommand.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (!oneLineCommand.Equals(""))
                {
                    hasLoop = Regex.IsMatch(oneLineCommand.ToLower(), @"\bloop\b");
                    if (hasLoop)
                    {
                        loopLineNo = (i + 1);
                    }
                    hasEndLoop = oneLineCommand.ToLower().Contains("endloop");
                    if (hasEndLoop)
                    {
                        endLoopLineNo = (i + 1);
                    }
                    hasIf = Regex.IsMatch(oneLineCommand.ToLower(), @"\bif\b");
                    if (hasIf)
                    {
                        ifLineNo = (i + 1);
                    }
                    hasEndif = oneLineCommand.ToLower().Contains("endif");
                    if (hasEndif)
                    {
                        endIfLineNo = (i + 1);
                    }
                }
            }
            if (loopLineNo > 0)
            {
                hasLoop = true;
            }
            if (endLoopLineNo > 0)
            {
                hasEndLoop = true;
            }
            if (ifLineNo > 0)
            {
                hasIf = true;
            }
            if (endIfLineNo > 0)
            {
                hasEndif = true;
            }
            if (hasLoop)
            {
                if (hasEndLoop)
                {
                    if (loopLineNo < endLoopLineNo)
                    {

                    }
                    else
                    {
                        isValidCommand = false;
                        MessageBox.Show("'ENDLOOP' must be after loop start");
                    }
                }
                else
                {
                    isValidCommand = false;
                    MessageBox.Show("Loop Not Ended with 'ENDLOOP'");
                }
            }
            if (hasIf)
            {
                if (hasEndif)
                {
                    if (ifLineNo < endIfLineNo)
                    {

                    }
                    else
                    {
                        isValidCommand = false;
                        MessageBox.Show("'ENDIF' must be after IF");
                    }
                }
                else
                {
                    isValidCommand = false;
                    MessageBox.Show("IF Not Ended with 'ENDIF'");
                }
            }
        }
        
    }
}
