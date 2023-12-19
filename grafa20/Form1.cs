using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Imaging;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static grafa20.Geometry;

namespace grafa20
{
    public partial class Form1 : Form
    {
        public Space space;
        Stopwatch stopwatch = new Stopwatch();
        int trianglevave = 0;
        int nets = 0;
    
        private float t = 0.0f;
        private float cx = 250;
        private float cy = 250;
        private float a = 200;
        private float b = 100;
        public Form1()
        {
            InitializeComponent();
            space = new Space();
            int width = 500;
            int height = 500;
            space.pixels = new Byte[width * height * 3];
            space.BitsHanle = GCHandle.Alloc(space.pixels, GCHandleType.Pinned);
            space.vieew = new Bitmap(width, height, width * 3, PixelFormat.Format24bppRgb, space.BitsHanle.AddrOfPinnedObject());
            space.ObliczZiWektory();
            draw();

        }


        public void DrawPixel(int r, int g, int b, int x, int y)
        {


            int currentLine = 3 * (y * 500 + x);
            space.pixels[currentLine] = (byte)b;
            space.pixels[currentLine + 1] = (byte)g;
            space.pixels[currentLine + 2] = (byte)r;



        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {


            using (var g = Graphics.FromImage(space.vieew))
            {
                
                if (trianglevave == 1)
                {
                    foreach (var triangle in space.triangles)
                    {

                        DrawLine(g, triangle.P1, triangle.P2);
                        DrawLine(g, triangle.P2, triangle.P3);
                        DrawLine(g, triangle.P3, triangle.P1);
                    }
                }

                if (nets == 1)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        for (int y = 0; y < 4; y++)
                        {
                            if (x < 4 - 1) // w pionie
                                g.DrawLine(Pens.Black, doint(space.BasePoints[x, y].X), doint(space.BasePoints[x, y].Y), doint(space.BasePoints[x + 1, y].X), doint(space.BasePoints[x + 1, y].Y));

                            if (y < 4 - 1)  // poziomie
                                g.DrawLine(Pens.Black, doint(space.BasePoints[x, y].X), doint(space.BasePoints[x, y].Y), doint(space.BasePoints[x, y + 1].X), doint(space.BasePoints[x, y + 1].Y));

                            //kulka siatki
                            g.DrawEllipse(Pens.Red, doint(space.BasePoints[x, y].X) - 5, doint(space.BasePoints[x, y].Y) - 5, 10, 10);
                        }
                    }
                }
            }


            e.Graphics.DrawImage(space.vieew, 0, 0);

        }

        private void DrawLine(Graphics g, Vector3 p1, Vector3 p2)
        {

            PointF point1 = new PointF(doint(p1.X), doint(p1.Y));
            PointF point2 = new PointF(doint(p2.X), doint(p2.Y));


            g.DrawLine(Pens.Black, point1, point2);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            space.numofdivision = trackBar1.Value;
            space.InitPoints();
            
            space.ObliczZiWektory();
            draw();
            pictureBox1.Invalidate();

        }



        public void draw()
        {
           

            stopwatch.Start();
            
            Parallel.ForEach(space.triangles, triangle =>
            {
                List<Segment> lista = new List<Segment>
    {
        new Segment(triangle.P1, triangle.P2),
        new Segment(triangle.P2, triangle.P3),
        new Segment(triangle.P3, triangle.P1)
    };

                FillPolygon2(lista);
            });

           ;

            stopwatch.Stop();
            label2.Text = $"{stopwatch.ElapsedMilliseconds}";
            stopwatch.Reset();
            pictureBox1.Invalidate();
        }





        public void FillPolygon2(List<Segment> krawedzie)
        {
            float ymin1 = float.MaxValue;
            float ymax1 = float.MinValue;


            foreach (var segment in krawedzie)
            {
                if (segment.ps.Y > ymax1) ymax1 = segment.ps.Y;
                if (segment.pe.Y > ymax1) ymax1 = segment.pe.Y;
                if (segment.ps.Y < ymin1) ymin1 = segment.ps.Y;
                if (segment.pe.Y < ymin1) ymin1 = segment.pe.Y;
            }

            int ymin = doint(ymin1);
            int ymax = doint(ymax1);

            List<Kubel> aktualna = new List<Kubel>();
            int y = ymin;
            List<Kubel>[] kubelki = new List<Kubel>[ymax - ymin + 1];
            int licznik = 0;

            foreach (var segment in krawedzie)
            {
                int i = doint(segment.minY().Item1) - ymin;

                float m1 = (segment.ps.X - segment.pe.X);
                float m2 = (segment.ps.Y - segment.pe.Y);
                float m = 0;
                if (m2 == 0) continue;
                m = m1 / m2;
                // int mm = (int)Math.Round(m);
                if (kubelki[i] == null) kubelki[i] = new List<Kubel>();
                kubelki[i].Add(new Kubel(doint(segment.maxY().Item1), doint(segment.minY().Item2), m));
                licznik++;
            }

            while (licznik != 0 || aktualna.Count != 0)
            {
                if (kubelki[y - ymin] != null)
                {
                    foreach (var kubel in kubelki[y - ymin])
                    {
                        aktualna.Add(kubel);
                        licznik--;
                    }
                }

                aktualna.Sort((a, b) => a.x.CompareTo(b.x));

                for (int i = 0; i < aktualna.Count - 1; i += 2)
                {
                    var przeciecie1 = aktualna[i];
                    var przeciecie2 = aktualna[i + 1];

                    for (int j = (int)przeciecie1.x; j <= (int)przeciecie2.x; j++)
                    {


                        float u = tofloat(j);
                        float v = tofloat(y);
                        if (j == -1) j = 0;


                        Vector3 gdzie = new Vector3(u, v, space.Zety[j, y]);


                        Vector3 aa = space.NormalVectors[j, y];

                        Vector3 wersor = obliczWersorSwiatla(space.swiatlo, gdzie);
                        Color obiektu = space.IO;
                        if (space.normalcolor != null)
                            obiektu = space.ImageColors[j, y];



                        (int r, int g, int bb) = CalculateColor(aa, wersor, new Vector3(0, 0, 1), space.kd, space.ks, space.m, obiektu, space.IL);
                        Vector3 p = new Vector3(j, y, 0);
                        if (space.transform == true)
                        {
                            p = new Vector3(j, y, space.Zety[j, y] * 400);
                            p = Vector3.Transform(p, space.M);
                        }
                        if (p.X >= 0 && p.Y >= 0 && p.X <= 499 && p.Y <= 499)
                            DrawPixel(r, g, bb, (int)p.X, (int)p.Y);


                    }
                }

                aktualna.RemoveAll(element => element.ymax == y);
                y++;

                aktualna.ForEach(element => element.x += element.m);

            }
        }



        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            space.swiatlo.X = trackBar2.Value / 10f;
            //  Vector3.Normalize(space.swiatlo);
            draw();

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            space.swiatlo.Y = trackBar3.Value / 10f;
            //   Vector3.Normalize(space.swiatlo);
            draw();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {

                space.IO = colorDialog1.Color;
                space.normalcolor = null;
                draw();
            }

        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {

                space.IL = colorDialog1.Color;
                draw();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Wybierz plik mapy normalnej",
                Filter = "Pliki graficzne (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp|Wszystkie pliki (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                // Teraz mo�esz wczyta� plik graficzny
                Bitmap originalImage = new Bitmap(filePath);
                Bitmap resizedImage = new Bitmap(originalImage, new Size(500, 500));
                space.normalcolor = resizedImage;
                for (int x = 0; x < resizedImage.Width; x++)
                {
                    for (int y = 0; y < resizedImage.Height; y++)
                    {
                        space.ImageColors[x, y] = resizedImage.GetPixel(x, y);

                    }
                }
                draw();

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Wybierz plik mapy normalnej",
                Filter = "Pliki graficzne (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp|Wszystkie pliki (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
               
                Bitmap originalImage = new Bitmap(filePath);
                Bitmap resizedImage = new Bitmap(originalImage, new Size(500, 500));
                space.normalmap = resizedImage;

                for (int x = 0; x < resizedImage.Width; x++)
                {
                    for (int y = 0; y < resizedImage.Height; y++)
                    {
                        space.BitmapVector[x, y] = space.ColorToNormalVector(resizedImage.GetPixel(x, y));
                    
                    }
                }
                space.ObliczZiWektory();
                draw();

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {


                Bitmap originalImage = new Bitmap(Properties.Resources.normal_map_example);

                Bitmap resizedImage = new Bitmap(originalImage, new Size(500, 500));
                space.normalmap = resizedImage;

                for (int x = 0; x < resizedImage.Width; x++)
                {
                    for (int y = 0; y < resizedImage.Height; y++)
                    {
                        space.BitmapVector[x, y] = space.ColorToNormalVector(resizedImage.GetPixel(x, y));

                    }
                }
                space.ObliczZiWektory();
                draw();
            }
            else
            {
                space.normalmap = null;
                space.ObliczZiWektory();
                draw();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {


                Bitmap originalImage = new Bitmap(Properties.Resources.original);

                Bitmap resizedImage = new Bitmap(originalImage, new Size(500, 500));
                space.normalcolor = resizedImage;
                for (int x = 0; x < resizedImage.Width; x++)
                {
                    for (int y = 0; y < resizedImage.Height; y++)
                    {
                        space.ImageColors[x, y] = resizedImage.GetPixel(x, y);

                    }
                }


                draw();
            }
            else
            {
                space.normalcolor = null;

                draw();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }



        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            space.swiatlo.Z = trackBar6.Value / 5.0f;
            label12.Text = $"z: {trackBar6.Value / 5.0f}";
            //   Vector3.Normalize(space.swiatlo);
            draw();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                trianglevave = 1;
                draw();
            }
            else
            {
                trianglevave = 0;
                draw();
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                nets = 1;
                draw();
            }
            else
            {
                nets = 0;
                draw();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t += 0.08f; 

            space.swiatlo.X = (cx + a * (float)Math.Cos(t)) / 499.0f;
            space.swiatlo.Y = (cy + b * (float)Math.Sin(t)) / 499.0f;

            draw();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true) timer1.Start();
            else { timer1.Stop(); }
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            space.kd = trackBar5.Value / 10f;

            draw();
        }

        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            space.m = trackBar7.Value;

            draw();
        }
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            space.ks = trackBar4.Value / 10f;

            draw();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void trackBar8_Scroll(object sender, EventArgs e)
        {
            zlabel.Text = $"Z: {trackBar8.Value / 10.0f}";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;

         // rozmiar 500x500
            float scaleX = 500;
            float scaleY = 500;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                  
                    float scaledX = space.BasePoints[i, j].X * scaleX;
                    float scaledY = space.BasePoints[i, j].Y * scaleY;

                  
                    if (Math.Abs(scaledX - coordinates.X) < 10 && Math.Abs(scaledY - coordinates.Y) < 10)
                    {
                      //klikniecie zmiana z punktu 
                        space.BasePoints[i, j].Z = trackBar8.Value / 10.0f;
                    }
                }
            }
            space.InitPoints();
            space.ObliczZiWektory();
            draw();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {
           
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                for (int i = 0; i < 500; i++)
                {
                    for (int j = 0; j < 500; j++)
                    {
                        DrawPixel(0, 0, 0, i, j);
                    }
                }
                space.swiatlo.Z = 1.0f;
                space.sfera = true;
                space.InitPoints();
                space.ObliczZiWektory();
                draw();
            }
            else
            {
                for (int i = 0; i < 500; i++)
                {
                    for (int j = 0; j < 500; j++)
                    {
                        DrawPixel(0, 0, 0, i, j);
                    }
                }
                space.sfera = false;
                space.InitPoints();
                space.ObliczZiWektory();
                draw();
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                space.transform = true;
            }
            else
            {
                space.transform = false;
            }
        }

        private void trackBar10_Scroll(object sender, EventArgs e)
        {
            space.alfa = trackBar10.Value / 5.0f;
            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    DrawPixel(0, 0, 0, i, j);
                }
            }
            space.M = Matrix4x4.CreateTranslation(-space.height / 2, -space.width / 2, 0) * Matrix4x4.CreateFromYawPitchRoll(0, space.alfa, space.beta) * Matrix4x4.CreateTranslation(space.height / 2, space.width / 2, 0);
            draw();
        }

        private void trackBar9_Scroll(object sender, EventArgs e)
        {
            space.beta = trackBar9.Value / 5.0f;

            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    DrawPixel(0, 0, 0, i, j);
                }
            }
            space.M = Matrix4x4.CreateTranslation(-space.height / 2, -space.width / 2, 0) * Matrix4x4.CreateFromYawPitchRoll(0, space.alfa, space.beta) * Matrix4x4.CreateTranslation(space.height / 2, space.width / 2, 0);
            draw();
        }


    }
}
