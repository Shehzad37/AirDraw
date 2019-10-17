using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;


namespace Figuras
{
    class MyPen
    {
          private Graphics g;
        private readonly Pen pen = new Pen(Color.Navy, 7);
        private Point oldCoords;
        private readonly GraphicsPath graphicsPaths = new GraphicsPath();
       public MyLine(Panel panel)
        {
            g = panel.CreateGraphics();
        }
        public void drawLine(MouseEventArgs e ){

            if (e.Button == MouseButtons.Left)
            {
                if (oldCoords.IsEmpty)
                    graphicsPaths.StartFigure();
                else
                {
                    graphicsPaths.AddLine(oldCoords, new Point(e.X, e.Y));
                    g.DrawPath(pen, graphicsPaths);
                }
                oldCoords = new Point(e.X, e.Y);
            }
            else
                oldCoords = Point.Empty;
        }
        public void paint()
        {
            g.DrawPath(pen, graphicsPaths);
        }
    }
}
