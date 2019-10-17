using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Dibujando_Figuras
{
    class Rectangle
    {
        GraphicsPath graphicPath;

        public Point startPoint { get; set; }
        public Point endPoint { get; set; }

        public Rectangle()
        {

        }
        public Rectangle(Point sPoint,Point ePoint)
        {
            graphicPath = new GraphicsPath();
            Rectangle r = new Rectangle(sPoint, new Size(ePoint.X - sPoint.X,ePoint.X - sPoint.X));
            graphicPath.AddRectangle(r);
            graphicPath.CloseFigure();


        }
        public void drawRectangle(Graphics graphics,Point sPoint,Point ePoint)
        {
            Rectangle rectangle = new Rectangle(sPoint, new Size(ePoint.X - sPoint.X, ePoint.X - sPoint.X));
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.DrawRectangle(new Pen (Color.Black,2),rectangle);
            graphics.Dispose();

        }
        public void toDraw(Graphics graphics) {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.DrawPath(new Pen(Color.Black), graphics);
        }
        public bool inside(Point point)
        {
            if (gp.IsOutlineVisible(p, new Pen(Color.DarkViolet)))
            {
                return true;
            }
            else
            {
                return gp.IsVisible(p);
            }
        }

    }
}
