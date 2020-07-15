using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Shapes;

namespace DrawWithCommandline
{
    public partial class Draw : Form
    {
        Color btnBorderColor = Color.FromArgb(104, 162, 255);
        Color mainColor = Color.Black;
        int size = 2;
        Graphics g;
        int x, y = -1;
        int mouseX, mouseY = 0;
        Boolean moving = false;
        Pen pen;
        String active = "pen";
        OpenFileDialog openFile = new OpenFileDialog();
        String line = "";
        Validation validate;
        int loopCounter = 0;
        Boolean hasDrawOrMoveValue = false;

        public int radius = 0;
        public int width = 0;
        public int height = 0;
        public int dSize = 0;
        public int counter = 0;

        string shape;
        ShapeFactory shapeFactory = new ShapeFactory();
        Shape shapes;
        public Draw()
        {
            InitializeComponent();
            g = panelPaint.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen = new Pen(mainColor, size);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            addPenSizes();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F10:
                    btnRun.PerformClick();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void addPenSizes()
        {
            cboSize.Items.Clear();
            cboSize.Items.AddRange(new String[] { "1", "2", "4", "6", "8" });
            cboSize.SelectedIndex = 0;
        }

        private void addDrawingSizes()
        {
            cboSize.Items.Clear();
            cboSize.Items.AddRange(new String[] { "10", "20", "40", "50", "100" });
            cboSize.SelectedIndex = 0;
        }

        private void Draw_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            setAllBorderColorForButtons();
            btnPen.FlatAppearance.BorderSize = 1;
            panelPaint.Cursor = new Cursor(Properties.Resources.pen.GetHicon());
        }

        private void btnChooseColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() != DialogResult.Cancel)
            {
                btnChooseColor.BackColor = colorDialog1.Color;
                mainColor = colorDialog1.Color;
            }
            pen = new Pen(mainColor, size);
        }

        private void btnPen_Click(object sender, EventArgs e)
        {
            cboSize.Enabled = true;
            active = "pen";
            removeAllBorderFromButtons();
            btnPen.FlatAppearance.BorderSize = 1;
            panelPaint.Cursor = new Cursor(Properties.Resources.pen.GetHicon());
            addPenSizes();

        }

        private void btnEraser_Click(object sender, EventArgs e)
        {
            active = "eraser";
            removeAllBorderFromButtons();
            btnEraser.FlatAppearance.BorderSize = 1;
            panelPaint.Cursor = new Cursor(Properties.Resources.eraser.GetHicon());
            cboSize.Enabled = false;

        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            active = "rectangle";
            removeAllBorderFromButtons();
            btnRectangle.FlatAppearance.BorderSize = 1;
            panelPaint.Cursor = Cursors.Default;
            addDrawingSizes();
            cboSize.Enabled = true;
        }

        private void btnTriangle_Click(object sender, EventArgs e)
        {
            active = "triangle";
            removeAllBorderFromButtons();
            btnTriangle.FlatAppearance.BorderSize = 1;
            panelPaint.Cursor = Cursors.Default;
            addDrawingSizes();
            cboSize.Enabled = true;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            active = "circle";
            removeAllBorderFromButtons();
            btnCircle.FlatAppearance.BorderSize = 1;
            panelPaint.Cursor = Cursors.Default;
            addDrawingSizes();
            cboSize.Enabled = true;
        }
       
        private void setAllBorderColorForButtons()
        {
            btnPen.FlatAppearance.BorderColor = btnBorderColor;
            btnEraser.FlatAppearance.BorderColor = btnBorderColor;
            btnRectangle.FlatAppearance.BorderColor = btnBorderColor;
            btnTriangle.FlatAppearance.BorderColor = btnBorderColor;
            btnCircle.FlatAppearance.BorderColor = btnBorderColor;
        }

        private void removeAllBorderFromButtons()
        {
            btnPen.FlatAppearance.BorderSize = 0;
            btnEraser.FlatAppearance.BorderSize = 0;
            btnRectangle.FlatAppearance.BorderSize = 0;
            btnTriangle.FlatAppearance.BorderSize = 0;
            btnCircle.FlatAppearance.BorderSize = 0;
        }
        private void panelPaint_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            x = e.X;
            y = e.Y;
        }

        private void panelPaint_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
            x = -1;
            y = -1;
        }

        private void cboSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            size = Convert.ToInt32(cboSize.SelectedItem.ToString());
            pen = new Pen(mainColor, size);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            panelPaint.Refresh();
        }

        private void panelPaint_MouseClick(object sender, MouseEventArgs e)
        {
            if (active.Equals("pen"))
            {
                g.DrawLine(pen, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;
            }
            else if (active.Equals("eraser"))
            {
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                g.FillRectangle(myBrush, e.X, e.Y, 24, 24);
            }
            else if (active.Equals("rectangle"))
            {
                shapes = shapeFactory.getShape("RECTANGLE");
                shapes.SetParam(e.X, e.Y , size, size, mainColor);

                SolidBrush myBrush = new SolidBrush(mainColor);
                g.FillRectangle(myBrush, e.X , e.Y , size, size);
            }
            else if (active.Equals("triangle"))
            {
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(mainColor);
                Point[] pnt = new Point[3];

                pnt[0].X = e.X;
                pnt[0].Y = e.Y - size;

                pnt[1].X = e.X + size;
                pnt[1].Y = e.Y + size;

                pnt[2].X = e.X - size;
                pnt[2].Y = e.Y + size;
                g.FillPolygon(myBrush, pnt);
            }
            else if (active.Equals("circle"))
            {
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(mainColor);
                g.FillEllipse(myBrush, e.X - size / 2, e.Y - size / 2, size, size);

            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                txtCommand.Clear();
                line = "";
                StreamReader sr = new StreamReader(openFile.FileName);
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        txtCommand.Text += line;
                        txtCommand.Text += "\r\n";
                    }
                }
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtCommand.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File | *.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(saveFileDialog.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(txtCommand.Text);
                }
                MessageBox.Show("Your File has been saved Sucessfully");
            }

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            hasDrawOrMoveValue = false;
            if (txtCommand.Text != null && txtCommand.Text != "")
            {
                validate = new Validation(txtCommand);
                if (!validate.isSomethingInvalid)
                {
                    /*MessageBox.Show("Everything is working fine");*/
                    loadCommand();
                }

            }
        }

        private void panelPaint_MouseMove(object sender, MouseEventArgs e)
        {
            if (!hasDrawOrMoveValue)
            {
                mouseX = e.Location.X-50;
                mouseY = e.Location.Y-50;
            }
            if (moving && x != -1 && y != -1)
            {
                if (active.Equals("pen"))
                {
                    g.DrawLine(pen, new Point(x, y), e.Location);
                    x = e.X;
                    y = e.Y;
                }
                else if (active.Equals("eraser"))
                {
                    //g.DrawRectangle(pen, e.X, e.Y, 24, 24);

                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                    g.FillRectangle(myBrush, e.X, e.Y, 24, 24);
                }
            }


        }
        private void loadCommand()
        {
            int numberOfLines = txtCommand.Lines.Length;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txtCommand.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (!oneLineCommand.Equals(""))
                {
                    Boolean hasDrawto = Regex.IsMatch(oneLineCommand.ToLower(), @"\bdrawto\b");
                    Boolean hasMoveto = Regex.IsMatch(oneLineCommand.ToLower(), @"\bmoveto\b");
                    if (hasDrawto || hasMoveto)
                    {
                        String args = oneLineCommand.Substring(6, (oneLineCommand.Length - 6));
                        String[] parms = args.Split(',');
                        for (int j = 0; j < parms.Length; j++)
                        {
                            parms[j] = parms[j].Trim();
                        }
                        mouseX = int.Parse(parms[0]);
                        mouseY = int.Parse(parms[1]);

                        hasDrawOrMoveValue = true;
                    }
                    else
                    {
                        hasDrawOrMoveValue = false;
                    }
                    if (hasMoveto)
                    {
                        panelPaint.Refresh();
                    }
                }
            }

            for (loopCounter = 0; loopCounter < numberOfLines; loopCounter++)
            {
                String oneLineCommand = txtCommand.Lines[loopCounter];
                oneLineCommand = oneLineCommand.Trim();
                if (!oneLineCommand.Equals(""))
                {
                    RunCommand(oneLineCommand);
                }

            }
        }
        private void RunCommand(String oneLineCommand)
        {

            Boolean hasPlus = oneLineCommand.Contains('+');
            Boolean hasEquals = oneLineCommand.Contains("=");
            if (hasEquals)
            {
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] words = oneLineCommand.Split(' ');
                //removing white spaces in between words
                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = words[i].Trim();
                }
                String firstWord = words[0].ToLower();
                if (firstWord.Equals("if"))
                {
                    Boolean loop = false;
                    if (words[1].ToLower().Equals("radius"))
                    {
                        if (radius == int.Parse(words[3]))
                        {
                            loop = true;
                        }
                    }
                    else if (words[1].ToLower().Equals("width"))
                    {
                        if (width == int.Parse(words[3]))
                        {
                            loop = true;
                        }
                    }
                    else if (words[1].ToLower().Equals("height"))
                    {
                        if (height == int.Parse(words[3]))
                        {
                            loop = true;
                        }

                    }
                    else if (words[1].ToLower().Equals("counter"))
                    {
                        if (counter == int.Parse(words[3]))
                        {
                            loop = true;
                        }
                    }
                    int ifStartLine = (GetIfStartLineNumber());
                    int ifEndLine = (GetEndifEndLineNumber() - 1);
                    loopCounter = ifEndLine;
                    if (loop)
                    {
                        for (int j = ifStartLine; j <= ifEndLine; j++)
                        {
                            string oneLineCommand1 = txtCommand.Lines[j];
                            oneLineCommand1 = oneLineCommand1.Trim();
                            if (!oneLineCommand1.Equals(""))
                            {
                                RunCommand(oneLineCommand1);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("If Statement is false");
                    }
                }
                else
                {
                    string[] words2 = oneLineCommand.Split('=');
                    for (int j = 0; j < words2.Length; j++)
                    {
                        words2[j] = words2[j].Trim();
                    }
                    if (words2[0].ToLower().Equals("radius"))
                    {
                        radius = int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("width"))
                    {
                        width = int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("height"))
                    {
                        height = int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("counter"))
                    {
                        counter = int.Parse(words2[1]);
                    }
                }
            }
            else if (hasPlus)
            {
                oneLineCommand = System.Text.RegularExpressions.Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] words = oneLineCommand.Split(' ');
                if (words[0].ToLower().Equals("repeat"))
                {
                    counter = int.Parse(words[1]);
                    if (words[2].ToLower().Equals("circle"))
                    {
                        int increaseValue = GetSize(oneLineCommand);
                        radius = increaseValue;
                        for (int j = 0; j < counter; j++)
                        {
                            DrawCircle(radius);
                            radius += increaseValue;
                        }
                    }
                    else if (words[2].ToLower().Equals("rectangle"))
                    {
                        int increaseValue = GetSize(oneLineCommand);
                        dSize = increaseValue;
                        for (int j = 0; j < counter; j++)
                        {
                            DrawRectangle(dSize, dSize);
                            dSize += increaseValue;
                        }
                    }
                    else if (words[2].ToLower().Equals("triangle"))
                    {
                        int increaseValue = GetSize(oneLineCommand);
                        dSize = increaseValue;
                        for (int j = 0; j < counter; j++)
                        {
                            DrawTriangle(dSize, dSize, dSize);
                            dSize += increaseValue;
                        }
                    }

                }
                else
                {
                    string[] words2 = oneLineCommand.Split('+');
                    for (int j = 0; j < words2.Length; j++)
                    {
                        words2[j] = words2[j].Trim();
                    }
                    if (words2[0].ToLower().Equals("radius"))
                    {
                        radius += int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("width"))
                    {
                        width += int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("height"))
                    {
                        height += int.Parse(words2[1]);
                    }
                }
            }
            else
            {
                sendDrawCommand(oneLineCommand);
            }


        }

        private int GetSize(string lineCommand)
        {
            int value = 0;
            if (lineCommand.ToLower().Contains("radius"))
            {
                int pos = (lineCommand.IndexOf("radius") + 6);
                int size = lineCommand.Length;
                String tempLine = lineCommand.Substring(pos, (size - pos));
                tempLine = tempLine.Trim();
                String newTempLine = tempLine.Substring(1, (tempLine.Length - 1));
                newTempLine = newTempLine.Trim();
                value = int.Parse(newTempLine);

            }
            else if (lineCommand.ToLower().Contains("size"))
            {
                int pos = (lineCommand.IndexOf("size") + 4);
                int size = lineCommand.Length;
                String tempLine = lineCommand.Substring(pos, (size - pos));
                tempLine = tempLine.Trim();
                String newTempLine = tempLine.Substring(1, (tempLine.Length - 1));
                newTempLine = newTempLine.Trim();
                value = int.Parse(newTempLine);
            }
            return value;
        }

        private void sendDrawCommand(string lineOfCommand)
        {
            String[] shapes = { "circle", "rectangle", "triangle", "polygon" };
            String[] variable = { "radius", "width", "height", "counter", "size" };

            lineOfCommand = System.Text.RegularExpressions.Regex.Replace(lineOfCommand, @"\s+", " ");
            string[] words = lineOfCommand.Split(' ');

            //removing white spaces in between words

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Trim();
            }
            String firstWord = words[0].ToLower();
            Boolean firstWordShape = shapes.Contains(firstWord);
            if (firstWordShape)
            {

                if (firstWord.Equals("circle"))
                {
                    Boolean secondWordIsVariable = variable.Contains(words[1].ToLower());
                    if (secondWordIsVariable)
                    {
                        if (words[1].ToLower().Equals("radius"))
                        {
                            DrawCircle(radius);
                        }
                    }
                    else
                    {
                        DrawCircle(Int32.Parse(words[1]));
                    }

                }
                else if (firstWord.Equals("rectangle"))
                {
                    String args = lineOfCommand.Substring(9, (lineOfCommand.Length - 9));
                    String[] parms = args.Split(',');
                    for (int i = 0; i < parms.Length; i++)
                    {
                        parms[i] = parms[i].Trim();
                    }
                    Boolean secondWordIsVariable = variable.Contains(parms[0].ToLower());
                    Boolean thirdWordIsVariable = variable.Contains(parms[1].ToLower());
                    if (secondWordIsVariable)
                    {
                        if (thirdWordIsVariable)
                        {
                            DrawRectangle(width, height);
                        }
                        else
                        {
                            DrawRectangle(width, Int32.Parse(parms[1]));
                        }

                    }
                    else
                    {
                        if (thirdWordIsVariable)
                        {
                            DrawRectangle(Int32.Parse(parms[0]), height);
                        }
                        else
                        {
                            DrawRectangle(Int32.Parse(parms[0]), Int32.Parse(parms[1]));
                        }
                    }
                }
                else if (firstWord.Equals("triangle"))
                {
                    String args = lineOfCommand.Substring(8, (lineOfCommand.Length - 8));
                    String[] parms = args.Split(',');
                    for (int i = 0; i < parms.Length; i++)
                    {
                        parms[i] = parms[i].Trim();
                    }
                    DrawTriangle(Int32.Parse(parms[0]), Int32.Parse(parms[1]), Int32.Parse(parms[2]));
                }
               

            }
            else
            {
                if (firstWord.Equals("loop"))
                {
                    counter = int.Parse(words[1]);
                    int loopStartLine = (GetLoopStartLineNumber());
                    int loopEndLine = (GetLoopEndLineNumber() - 1);
                    loopCounter = loopEndLine;
                    for (int i = 0; i < counter; i++)
                    {
                        for (int j = loopStartLine; j <= loopEndLine; j++)
                        {
                            String oneLineCommand = txtCommand.Lines[j];
                            oneLineCommand = oneLineCommand.Trim();
                            if (!oneLineCommand.Equals(""))
                            {
                                RunCommand(oneLineCommand);
                            }
                        }
                    }
                }
                else if (firstWord.Equals("if"))
                {
                    Boolean loop = false;
                    if (words[1].ToLower().Equals("radius"))
                    {
                        if (radius == int.Parse(words[1]))
                        {
                            loop = true;
                        }
                    }
                    else if (words[1].ToLower().Equals("width"))
                    {
                        if (width == int.Parse(words[1]))
                        {
                            loop = true;
                        }
                    }
                    else if (words[1].ToLower().Equals("height"))
                    {
                        if (height == int.Parse(words[1]))
                        {
                            loop = true;
                        }

                    }
                    else if (words[1].ToLower().Equals("counter"))
                    {
                        if (counter == int.Parse(words[1]))
                        {
                            loop = true;
                        }
                    }
                    int ifStartLine = (GetIfStartLineNumber());
                    int ifEndLine = (GetEndifEndLineNumber() - 1);
                    loopCounter = ifEndLine;
                    if (loop)
                    {
                        for (int j = ifStartLine; j <= ifEndLine; j++)
                        {
                            String oneLineCommand = txtCommand.Lines[j];
                            oneLineCommand = oneLineCommand.Trim();
                            if (!oneLineCommand.Equals(""))
                            {
                                RunCommand(oneLineCommand);
                            }
                        }
                    }
                }
            }
        }

        private int GetEndifEndLineNumber()
        {
            int numberOfLines = txtCommand.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txtCommand.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (oneLineCommand.ToLower().Equals("endif"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }

        private int GetIfStartLineNumber()
        {
            int numberOfLines = txtCommand.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txtCommand.Lines[i];
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] words = oneLineCommand.Split(' ');

                //removing white spaces in between words

                for (int j = 0; j < words.Length; j++)
                {
                    words[j] = words[j].Trim();
                }
                String firstWord = words[0].ToLower();
                oneLineCommand = oneLineCommand.Trim();
                if (firstWord.Equals("if"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }

        private int GetLoopEndLineNumber()
        {
            int numberOfLines = txtCommand.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txtCommand.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (oneLineCommand.ToLower().Equals("endloop"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }

        private int GetLoopStartLineNumber()
        {
            int numberOfLines = txtCommand.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txtCommand.Lines[i];
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] words = oneLineCommand.Split(' ');

                //removing white spaces in between words

                for (int j = 0; j < words.Length; j++)
                {
                    words[j] = words[j].Trim();
                }
                String firstWord = words[0].ToLower();
                oneLineCommand = oneLineCommand.Trim();
                if (firstWord.Equals("loop"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;

        }

        private void DrawPolygon(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8)
        {
            Pen myPen = new Pen(mainColor);
            Point[] pnt = new Point[5];

            pnt[0].X = mouseX;
            pnt[0].Y = mouseY;

            pnt[1].X = mouseX - v1;
            pnt[1].Y = mouseY - v2;

            pnt[2].X = mouseX - v3;
            pnt[2].Y = mouseY - v4;

            pnt[3].X = mouseX - v5;
            pnt[3].Y = mouseY - v6;

            pnt[4].X = mouseX - v7;
            pnt[4].Y = mouseY - v8;

            g.DrawPolygon(myPen, pnt);
        }
        private void DrawPolygon(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10)
        {
            Pen myPen = new Pen(mainColor);
            Point[] pnt = new Point[6];

            pnt[0].X = mouseX;
            pnt[0].Y = mouseY;

            pnt[1].X = mouseX - v1;
            pnt[1].Y = mouseY - v2;

            pnt[2].X = mouseX - v3;
            pnt[2].Y = mouseY - v4;

            pnt[3].X = mouseX - v5;
            pnt[3].Y = mouseY - v6;

            pnt[4].X = mouseX - v7;
            pnt[4].Y = mouseY - v8;

            pnt[5].X = mouseX - v9;
            pnt[5].Y = mouseY - v10;
            g.DrawPolygon(myPen, pnt);
        }

        private void DrawTriangle(int rBase, int adj, int hyp)
        {
            Pen myPen = new Pen(mainColor);
            Point[] pnt = new Point[3];

            pnt[0].X = mouseX;
            pnt[0].Y = mouseY;

            pnt[1].X = mouseX - rBase;
            pnt[1].Y = mouseY;

            pnt[2].X = mouseX;
            pnt[2].Y = mouseY - adj;
            g.DrawPolygon(myPen, pnt);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnOpen.PerformClick();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSave.PerformClick();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCommand_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelPaint_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DrawRectangle(int width, int height)
        {
            Pen myPen = new Pen(mainColor);
            g.DrawRectangle(myPen, mouseX - radius/3, mouseY - radius/4, width, height);
        }

        private void DrawCircle(int radius)
        {
            Pen myPen = new Pen(mainColor);
            g.DrawEllipse(myPen, mouseX - radius, mouseY - radius, radius * 2, radius * 2);
        }
    }
}