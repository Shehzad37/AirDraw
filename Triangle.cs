using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Dibujando_Figuras;

public class Triangle
{
    GraphicsPath gp = new GraphicsPath();
    Color clr = new Color();
    float width = 1;
    Brush tbrush = new SolidBrush(Color.White);

    public Point PI { get; set; }

    Pen p;
    public Point PF { get; set; }
    public Triangle() { }

    public Triangle(Point ptI, Point ptM, Point ptF)
    {

        Point[] vertices = { ptI, ptM, ptF };
        gp.AddPolygon(vertices);
        gp.CloseFigure();
        clr = Main.uPen.Color;
        width = Main.uPen.Width;
        Brush b = new SolidBrush(clr);
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

        Pen p = new Pen(clr,width);
        e.SmoothingMode = SmoothingMode.AntiAlias;

        if (Main.fill)
        {
            e.FillPath(tbrush, gp);
        }
        e.DrawPath(p, gp);



    }
    public void changeColor(Color color)
    {
        clr = new Color();
        clr = color;
    }
    public void changeBrush(Brush b)
    {
        tbrush = b;
    }
    public void rotate(int dx,int dy)
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

    public void fill(Graphics g)
    {

        if (Main.fill)
        {
            g.FillPath(tbrush, gp);
        }
    }
}
