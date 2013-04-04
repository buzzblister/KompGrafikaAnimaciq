using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //pisalka za polygona
        private static Pen pen;

        //pisalka za skrivane na ostava6tata sleda s fonov cvqt
        private static Pen bPen;

        private static Brush brush;
        private static Brush bBrush;

        //obekt grafika
        private static Graphics gr;

        //d1ljina i viso4ina na polygona
        private static int bodyWidth = 20;
        private static int bodyHeight = 80;

        //to4kite ot elipsata/orbitata
        private static List<Point> O = new List<Point>();

        //to4kite ot poligona sprqmo orbitata
        private static List<Point> A = new List<Point>();
        private static List<Point> B = new List<Point>();
        private static List<Point> C = new List<Point>();
        private static List<Point> D = new List<Point>();
        private static List<Point> E = new List<Point>();
        private static List<Point> F = new List<Point>();
        private static List<Point> G = new List<Point>();

        //to4kite sled rotaciq
        private static List<Point> RA = new List<Point>();
        private static List<Point> RB = new List<Point>();
        private static List<Point> RC = new List<Point>();
        private static List<Point> RD = new List<Point>();
        private static List<Point> RE = new List<Point>();
        private static List<Point> RF = new List<Point>();
        private static List<Point> RG = new List<Point>();

        //to4ki sled ma6tabiraneto
        private static List<Point> SRA = new List<Point>();
        private static List<Point> SRB = new List<Point>();
        private static List<Point> SRC = new List<Point>();
        private static List<Point> SRD = new List<Point>();
        private static List<Point> SRE = new List<Point>();
        private static List<Point> SRF = new List<Point>();
        private static List<Point> SRG = new List<Point>();

        //koordinati za cent1ra na elipsata
        private static int Xc;
        private static int Yc;

        //radiusi na elipsata
        private static int Ra;
        private static int Rb;

        //teku6ta to4ka ot spis1cite
        private static int curElem = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            pen = new Pen(Color.Black, 1);
            bPen = new Pen(this.BackColor, 1);

            brush = pen.Brush;
            bBrush = bPen.Brush;

            gr = Graphics.FromHwnd(this.Handle);

            init();
        }

        public static void init()
        {
            Ra = 600 / 2;
            Rb = 250 / 2;
            Xc = 60 + Ra;
            Yc = 60 + Rb;

            //to4kite ot okr1jnostta prez 2 gradusa
            for (int alpha = 0; alpha <= 360; alpha+=2)
            {
                O.Add(new Point((int)(Xc + Ra * Math.Cos(alpha * 3.14 / 180)), 
                     (int)(Yc + Rb * Math.Sin(alpha * 3.14 / 180))));
            }

            //to4kite za polygona
            foreach (Point point in O)
            {
            /*    A.Add(new Point(point.X - bodyWidth / 2, point.Y - bodyHeight / 2));
                B.Add(new Point(point.X - bodyWidth / 2, point.Y + bodyHeight / 2));
                C.Add(new Point(point.X + bodyWidth / 2, point.Y + bodyHeight / 2));
                D.Add(new Point(point.X + bodyWidth / 2, point.Y - bodyHeight / 2));*/

                A.Add(new Point(point.X, point.Y + 20));
                B.Add(new Point(point.X - 15, point.Y));
                C.Add(new Point(point.X - 25, point.Y - 40));
                D.Add(new Point(point.X - 10, point.Y - 20));
                E.Add(new Point(point.X + 10, point.Y - 20));
                F.Add(new Point(point.X + 25, point.Y - 40));
                G.Add(new Point(point.X + 15, point.Y));
            }

            //to4kite za polygona sled rotaciq
            for (int i = 0; i < O.Count; i++)
            {
                RA.Add(new Point(
                    (int)(O[i].X +
                    (A[i].X - O[i].X) * Math.Cos((double)(i * 2 * 3.14 / 180)) -
                    (A[i].Y - O[i].Y) * Math.Sin((double)(i * 2 * 3.14 / 180))),

                    (int)(O[i].Y +
                    (A[i].Y - O[i].Y) * Math.Cos((double)(i * 2 * 3.14 / 180)) +
                    (A[i].X - O[i].X) * Math.Sin((double)(i * 2 * 3.14 / 180)))
                                )
                      );
                RB.Add(new Point(
                    (int)(O[i].X +
                    (B[i].X - O[i].X) * Math.Cos((double)(i * 2 * 3.14 / 180)) -
                    (B[i].Y - O[i].Y) * Math.Sin((double)(i * 2 * 3.14 / 180))),

                    (int)(O[i].Y +
                    (B[i].Y - O[i].Y) * Math.Cos((double)(i * 2 * 3.14 / 180)) +
                    (B[i].X - O[i].X) * Math.Sin((double)(i * 2 * 3.14 / 180)))
                                )
                      );
                RC.Add(new Point(
                    (int)(O[i].X +
                    (C[i].X - O[i].X) * Math.Cos((double)(i * 2 * 3.14 / 180)) -
                    (C[i].Y - O[i].Y) * Math.Sin((double)(i * 2 * 3.14 / 180))),

                    (int)(O[i].Y +
                    (C[i].Y - O[i].Y) * Math.Cos((double)(i * 2 * 3.14 / 180)) +
                    (C[i].X - O[i].X) * Math.Sin((double)(i * 2 * 3.14 / 180)))
                                )
                      );
                RD.Add(new Point(
                    (int)(O[i].X +
                    (D[i].X - O[i].X) * Math.Cos((double)(i * 2 * 3.14 / 180)) -
                    (D[i].Y - O[i].Y) * Math.Sin((double)(i * 2 * 3.14 / 180))),

                    (int)(O[i].Y +
                    (D[i].Y - O[i].Y) * Math.Cos((double)(i * 2 * 3.14 / 180)) +
                    (D[i].X - O[i].X) * Math.Sin((double)(i * 2 * 3.14 / 180)))
                                )
                      );
                RE.Add(new Point(
                    (int)(O[i].X +
                    (E[i].X - O[i].X) * Math.Cos((double)(i * 2 * 3.14 / 180)) -
                    (E[i].Y - O[i].Y) * Math.Sin((double)(i * 2 * 3.14 / 180))),

                    (int)(O[i].Y +
                    (E[i].Y - O[i].Y) * Math.Cos((double)(i * 2 * 3.14 / 180)) +
                    (E[i].X - O[i].X) * Math.Sin((double)(i * 2 * 3.14 / 180)))
                                )
                      );
                RF.Add(new Point(
                    (int)(O[i].X +
                    (F[i].X - O[i].X) * Math.Cos((double)(i * 2 * 3.14 / 180)) -
                    (F[i].Y - O[i].Y) * Math.Sin((double)(i * 2 * 3.14 / 180))),

                    (int)(O[i].Y +
                    (F[i].Y - O[i].Y) * Math.Cos((double)(i * 2 * 3.14 / 180)) +
                    (F[i].X - O[i].X) * Math.Sin((double)(i * 2 * 3.14 / 180)))
                                )
                      );
                RG.Add(new Point(
                    (int)(O[i].X +
                    (G[i].X - O[i].X) * Math.Cos((double)(i * 2 * 3.14 / 180)) -
                    (G[i].Y - O[i].Y) * Math.Sin((double)(i * 2 * 3.14 / 180))),

                    (int)(O[i].Y +
                    (G[i].Y - O[i].Y) * Math.Cos((double)(i * 2 * 3.14 / 180)) +
                    (G[i].X - O[i].X) * Math.Sin((double)(i * 2 * 3.14 / 180)))
                                )
                      );
            }

            //masivi za ma6tabnite koeficienti
            double[] Sx = new double[O.Count];
            double[] Sy = new double[O.Count];

            double step = 0.019;
            Sx[0] = 1;
            Sy[0] = 1;

            int sector = O.Count / 4;

            for (int i = 1; i < sector; i++)
            {
                Sx[i] = Sx[i - 1] + step;
                Sy[i] = Sy[i - 1] + step;
            }
            for (int i = sector; i < 2 * sector; i++)
            {
                Sx[i] = Sx[i - 1] - step;
                Sy[i] = Sy[i - 1] - step;
            }
            for (int i = 2 * sector; i < 3 * sector; i++)
            {
                Sx[i] = Sx[i - 1] - step;
                Sy[i] = Sy[i - 1] - step;
            }
            for (int i = 3 * sector; i < O.Count; i++)
            {
                Sx[i] = Sx[i - 1] + step;
                Sy[i] = Sy[i - 1] + step;
            }

            //ma6tabirane
            for (int i = 0; i < O.Count; i++)
            {
                SRA.Add(new Point(
                    (int)(O[i].X + (RA[i].X - O[i].X) * Sx[i]),
                    (int)(O[i].Y + (RA[i].Y - O[i].Y) * Sy[i])
                                 )
                       );
                SRB.Add(new Point(
                    (int)(O[i].X + (RB[i].X - O[i].X) * Sx[i]),
                    (int)(O[i].Y + (RB[i].Y - O[i].Y) * Sy[i])
                                 )
                       );
                SRC.Add(new Point(
                    (int)(O[i].X + (RC[i].X - O[i].X) * Sx[i]),
                    (int)(O[i].Y + (RC[i].Y - O[i].Y) * Sy[i])
                                 )
                       );
                SRD.Add(new Point(
                    (int)(O[i].X + (RD[i].X - O[i].X) * Sx[i]),
                    (int)(O[i].Y + (RD[i].Y - O[i].Y) * Sy[i])
                                 )
                       );
                SRE.Add(new Point(
                    (int)(O[i].X + (RE[i].X - O[i].X) * Sx[i]),
                    (int)(O[i].Y + (RE[i].Y - O[i].Y) * Sy[i])
                                 )
                       );
                SRF.Add(new Point(
                    (int)(O[i].X + (RF[i].X - O[i].X) * Sx[i]),
                    (int)(O[i].Y + (RF[i].Y - O[i].Y) * Sy[i])
                                 )
                       );
                SRG.Add(new Point(
                    (int)(O[i].X + (RG[i].X - O[i].X) * Sx[i]),
                    (int)(O[i].Y + (RG[i].Y - O[i].Y) * Sy[i])
                                 )
                       );
            }

            //risuvane na orbitata
        //    gr.DrawEllipse(Pens.Red, new Rectangle(60, 60, 600, 250));
        }

        //risuvane na polygona
        public static void draw(int i)
        {
            int m = SRA.Count - 1;

            //polygon s fonov cvqt
            switch (i)
            {
                case 0:
                    gr.FillPolygon(bBrush, new Point[] { SRA[m], SRB[m], SRC[m], SRD[m], SRE[m], SRF[m], SRG[m] });
                    break;
                default:
                    gr.FillPolygon(bBrush, new Point[] { SRA[i - 1], SRB[i - 1], SRC[i - 1], SRD[i - 1], SRE[i - 1], SRF[i - 1], SRG[i - 1] });
                    break;
            }

            //osnoven polygon
            gr.FillPolygon(brush, new Point[] { SRA[i], SRB[i], SRC[i], SRD[i], SRE[i], SRF[i], SRG[i] });
        }

        //dvijenie
        private void timer1_Tick(object sender, EventArgs e)
        {
            draw(curElem);
            curElem++;

            if (curElem == O.Count)
            {
                curElem = 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
