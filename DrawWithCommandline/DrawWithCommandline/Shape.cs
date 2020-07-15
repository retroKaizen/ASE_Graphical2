using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawWithCommandline
{
    interface Shape
    {
        void Draw(Graphics g);
        void SetParam(int x, int y, int width, int height, Color color);
    }
}
