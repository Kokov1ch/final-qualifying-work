using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ships
{
    public partial class DrawFowm : Form
    {
        Vessel ship;
        public DrawFowm(Vessel s)
        {
            this.ship = s;
            InitializeComponent();
        }

        int numFrame;

        //Кнопка "Рисовать"
        private void button2_Click(object sender, EventArgs e)
        {
            Graphics shipGraph = pictureBox2.CreateGraphics();
            for (int k = 0; k<ship.frames.Count; k++)
            {
                //numFrame = 10;
                Pen pen = new Pen(Color.Black, 2f);

                //var numFrame = int.Parse(textBox1.Text);
                //shipGraph.Clear(BackColor);

                //Рисуем правую часть
                PointF[] points = new PointF[ship.frames[k].cordinates.Count];
                for (int i = 0; i < ship.frames[k].cordinates.Count; i++)
                {
                    points[i] = new PointF(65 * (float)ship.frames[k].cordinates[i].x + 500, -65 * (float)ship.frames[k].cordinates[i].y + 700);
                }
                for (int i = 0; i < points.Length - 1; i++)
                {
                    shipGraph.DrawLine(pen, points[i], points[i + 1]);

                }
                shipGraph.DrawLine(pen, points[0], points[points.Length - 1]);
                label1.Text = ship.frames[k].position.ToString();

                //Левую часть
                PointF[] points1 = new PointF[ship.frames[k].cordinates.Count];
                for (int i = 0; i < ship.frames[k].cordinates.Count; i++)
                {
                    points1[i] = new PointF(-65 * (float)ship.frames[k].cordinates[i].x + 500, -65 * (float)ship.frames[k].cordinates[i].y + 700);
                }
                for (int i = 0; i < points.Length - 1; i++)
                {
                    shipGraph.DrawLine(pen, points1[i], points1[i + 1]);

                }
                shipGraph.DrawLine(pen, points1[0], points1[points.Length - 1]);
                label1.Text = ship.frames[k].position.ToString();
            }

        }

        //Кнопки плюс и минус. Удалить надо наверное. Вряд ли нужно оставить
        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if (numFrame > 0)
            {
                numFrame = numFrame - 1;
                Graphics shipGraph = pictureBox2.CreateGraphics();
                Pen pen = new Pen(Color.Black, 2f);

                //var numFrame = int.Parse(textBox1.Text);
                shipGraph.Clear(BackColor);

                PointF[] points = new PointF[ship.frames[numFrame].cordinates.Count];
                for (int i = 0; i < ship.frames[numFrame].cordinates.Count; i++)
                {
                    points[i] = new PointF(20 * (float)ship.frames[numFrame].cordinates[i].x + 500, 20 * (float)ship.frames[numFrame].cordinates[i].y + 280);
                }
                for (int i = 0; i < points.Length - 1; i++)
                {
                    shipGraph.DrawLine(pen, points[i], points[i + 1]);

                }

                shipGraph.DrawLine(pen, points[0], points[points.Length - 1]);
                label1.Text = ship.frames[numFrame].position.ToString();
                PointF[] points1 = new PointF[ship.frames[numFrame].cordinates.Count];
                for (int i = 0; i < ship.frames[numFrame].cordinates.Count; i++)
                {
                    points1[i] = new PointF(-20 * (float)ship.frames[numFrame].cordinates[i].x + 500, 20 * (float)ship.frames[numFrame].cordinates[i].y + 280);
                }
                for (int i = 0; i < points.Length - 1; i++)
                {
                    shipGraph.DrawLine(pen, points1[i], points1[i + 1]);

                }
                shipGraph.DrawLine(pen, points1[0], points1[points.Length - 1]);
                label1.Text = ship.frames[numFrame].position.ToString();
            }
        }

        private void buttonPlud_Click(object sender, EventArgs e)
        {
            
            if (numFrame < ship.frames.Count-1)
            {
                numFrame = numFrame + 1;
                Graphics shipGraph = pictureBox2.CreateGraphics();
                Pen pen = new Pen(Color.Black, 2f);

                //var numFrame = int.Parse(textBox1.Text);
                shipGraph.Clear(BackColor);

                PointF[] points = new PointF[ship.frames[numFrame].cordinates.Count];
                for (int i = 0; i < ship.frames[numFrame].cordinates.Count; i++)
                {
                    points[i] = new PointF(20 * (float)ship.frames[numFrame].cordinates[i].x + 500, 20 * (float)ship.frames[numFrame].cordinates[i].y + 280);
                }
                for (int i = 0; i < points.Length - 1; i++)
                {
                    shipGraph.DrawLine(pen, points[i], points[i + 1]);

                }
                label1.Text = ship.frames[numFrame].position.ToString();
                shipGraph.DrawLine(pen, points[0], points[points.Length - 1]);
                PointF[] points1 = new PointF[ship.frames[numFrame].cordinates.Count];
                for (int i = 0; i < ship.frames[numFrame].cordinates.Count; i++)
                {
                    points1[i] = new PointF(-20 * (float)ship.frames[numFrame].cordinates[i].x + 500, 20 * (float)ship.frames[numFrame].cordinates[i].y + 280);
                }
                for (int i = 0; i < points.Length - 1; i++)
                {
                    shipGraph.DrawLine(pen, points1[i], points1[i + 1]);

                }
                shipGraph.DrawLine(pen, points1[0], points1[points.Length - 1]);
                label1.Text = ship.frames[numFrame].position.ToString();

            }
        }

        private void DrawFowm_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
        }
    }
}
