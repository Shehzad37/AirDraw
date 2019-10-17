using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using Dibujando_Figuras;
public class Daimond
{
    GraphicsPath gp;
    public Point startPoint { get; set; }
    public Point endPoint { get; set; }
    public Color clr;
    float width = 1;
    Brush dBrush = new SolidBrush(Color.White);

    Point point3;
    Point point4;

    public Daimond() { }
    public Daimond(Point pt1, Point pt2, Point pt3, Point pt4)
    {
        gp = new GraphicsPath();
        Point[] vertices = { pt1, pt2, pt3, pt4 };
        gp.AddPolygon(vertices);
        gp.CloseFigure();
        clr = Main.uPen.Color;
        width = Main.uPen.Width;
    }

    public bool inside(Point p)
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
    public void Mover(int dx, int dy)
    {
        gp.Transform(new Matrix(1, 0, 0, 1, dx, dy));
    }
    public void toDraw(Graphics e)
    {
        Pen pen = new Pen(clr,width);
        e.SmoothingMode = SmoothingMode.AntiAlias;
        if (Main.fill)
        {
            e.FillPath(dBrush, gp);
        }
        
        e.DrawPath(pen, gp);
    }
    public void changeColor(Color color)
    {
        clr = new Color();
        clr = color;
    }
    public void changeBrush(Brush b)
    {
        dBrush = b;
    }
    public void rotate(int dx, int dy)
    {

        Matrix m = new Matrix();
        Point p = new Point();
        p.X = dx;
        p.Y = dy;
        Console.WriteLine("dx" + dx + "");
        Console.WriteLine("dy" + dy + "");
        // m.Scale(p.X,p.Y);
        //  m.Translate(200, 200);
        m.RotateAt(10, p);
        // m.Translate(0, 0);
        // m.Reset();
        gp.Transform(m);
        m.Reset();

    }
}