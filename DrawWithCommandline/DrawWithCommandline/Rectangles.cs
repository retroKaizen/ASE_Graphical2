using DrawWithCommandline;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    [Serializable]
    class Rectangles : Shape
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public bool isFilled { get; set; }
        public Color color { get; set; }

        /// <summary>
        /// draw rectangle
        /// </summary>
        /// <param name="g"></param>

        public void Draw(Graphics g)
        {
            if (isFilled)
            {
                Brush br = new SolidBrush(color);
                g.FillRectangle(br, new Rectangle(x, y, width, height));
                br.Dispose();
            }
            else
            {
                Pen pen = new Pen(color, 1);
                g.DrawRectangle(pen, new Rectangle(x, y, width, height));
                pen.Dispose();
            }
        }

        /// <summary>
        /// parameter for rectangle with color
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>

        public void SetParam(int x, int y, int width, int height, Color color)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;
            this.isFilled = isFilled;
        }
    }
}
