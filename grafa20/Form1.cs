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
        public Przestrzen przestrzen;
        Stopwatch stopwatch = new Stopwatch();
        int siatkatroj = 0;
        int siatka = 0;

        private float t = 0.0f;
        private float cx = 250;
        private float cy = 250;
        private float a = 200;
        private float b = 100;
        public Form1()
        {
            InitializeComponent();
            przestrzen = new Przestrzen();
            int width = 500;
            int height = 500;
            przestrzen.pixels = new Byte[width * height * 3];
            przestrzen.BitsHanle = GCHandle.Alloc(przestrzen.pixels, GCHandleType.Pinned);
            przestrzen.wyswietl = new Bitmap(width, height, width * 3, PixelFormat.Format24bppRgb, przestrzen.BitsHanle.AddrOfPinnedObject());
            przestrzen.ObliczZiWektory();
            draw();

        }


        public void DrawPixel(int r, int g, int b, int x, int y)
        {


            int currentLine = 3 * (y * 500 + x);
            przestrzen.pixels[currentLine] = (byte)b;
            przestrzen.pixels[currentLine + 1] = (byte)g;
            przestrzen.pixels[currentLine + 2] = (byte)r;



        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {


            using (var g = Graphics.FromImage(przestrzen.wyswietl))
            {
                // g.DrawRectangle(Pens.White, new Rectangle(50, 50, 100, 100));
                if (siatkatroj == 1)
                {
                    foreach (var trojkat in przestrzen.trojkaty)
                    {

                        RysujLinie(g, trojkat.P1, trojkat.P2);
                        RysujLinie(g, trojkat.P2, trojkat.P3);
                        RysujLinie(g, trojkat.P3, trojkat.P1);
                    }
                }

                if (siatka == 1)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        for (int y = 0; y < 4; y++)
                        {
                            if (x < 4 - 1) // Linia w poziomie
                                g.DrawLine(Pens.Black, doint(przestrzen.PunktyBaz[x, y].X), doint(przestrzen.PunktyBaz[x, y].Y), doint(przestrzen.PunktyBaz[x + 1, y].X), doint(przestrzen.PunktyBaz[x + 1, y].Y));

                            if (y < 4 - 1) // Linia w pionie
                                g.DrawLine(Pens.Black, doint(przestrzen.PunktyBaz[x, y].X), doint(przestrzen.PunktyBaz[x, y].Y), doint(przestrzen.PunktyBaz[x, y + 1].X), doint(przestrzen.PunktyBaz[x, y + 1].Y));

                            // Rysowanie k�ka
                            g.DrawEllipse(Pens.Red, doint(przestrzen.PunktyBaz[x, y].X) - 5, doint(przestrzen.PunktyBaz[x, y].Y) - 5, 10, 10);
                        }
                    }
                }
            }


            e.Graphics.DrawImage(przestrzen.wyswietl, 0, 0);

        }

        private void RysujLinie(Graphics g, Vector3 p1, Vector3 p2)
        {

            PointF point1 = new PointF(doint(p1.X), doint(p1.Y));
            PointF point2 = new PointF(doint(p2.X), doint(p2.Y));


            g.DrawLine(Pens.Black, point1, point2);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            przestrzen.podzial = trackBar1.Value;
            przestrzen.InicjujPunkty();
            //funkcja rysuj�ca 
            przestrzen.ObliczZiWektory();
            draw();
            pictureBox1.Invalidate();

        }



        public void draw()
        {
            //for (int z = 0; z < 500; z++)
            //{
            //    for (int j = 0; j < 500; j++)
            //    {
            //        DrawPixel(0, 0, 0, z, j);
            //    }
            //}


            stopwatch.Start();
            // Parrarel.Foreach(przestrzen.trojkaty, FillPolygon);
            //Parallel.ForEach(przestrzen.trojkaty, FillPolygon);
            //int i = 0;
            //foreach (var trojkat in przestrzen.trojkaty)
            //{
            //    if(i%2==0)
            //    FillPolygon(trojkat);
            //    i++;
            //}
            Parallel.ForEach(przestrzen.trojkaty, trojkat =>
            {
                List<Segment> lista = new List<Segment>
    {
        new Segment(trojkat.P1, trojkat.P2),
        new Segment(trojkat.P2, trojkat.P3),
        new Segment(trojkat.P3, trojkat.P1)
    };

                FillPolygon2(lista);
            });

            //int i = 0;
            //foreach (var trojkat in przestrzen.trojkaty)
            //{
            //    if(trojkat.P1.X==1)
            //    { i = i; }
            //    if (i % 2 == 1)
            //    {
            //        List<Segment> lista = new List<Segment>
            //{
            //    new Segment(trojkat.P1, trojkat.P2),
            //    new Segment(trojkat.P2, trojkat.P3),
            //    new Segment(trojkat.P3, trojkat.P1)
            //};

            //        FillPolygon2(lista);
            //    }
            //    i++;
            //}

            //            FillPolygon2(lista);
            //        });

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


                        float u = wroc(j);
                        float v = wroc(y);
                        if (j == 233 && y == 120)
                        {
                            j = j;
                        }
                        if (j == -1) j = 0;

                        Vector3 gdzie = new Vector3(u, v, przestrzen.Zety[j, y]);


                        Vector3 aa = przestrzen.WektoryNormalne[j, y];

                        Vector3 wersor = obliczWersorSwiatla(przestrzen.swiatlo, gdzie);
                        Color obiektu = przestrzen.IO;
                        if (przestrzen.normalcolor != null)
                            obiektu = przestrzen.ImageColors[j, y];



                        (int r, int g, int bb) = ObliczKolor(aa, wersor, new Vector3(0, 0, 1), przestrzen.kd, przestrzen.ks, przestrzen.m, obiektu, przestrzen.IL);
                        Vector3 p = new Vector3(j, y, 0);
                        if (przestrzen.transform == true)
                        {
                            p = new Vector3(j, y, przestrzen.Zety[j, y] * 400);
                            p = Vector3.Transform(p, przestrzen.M);
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
            przestrzen.swiatlo.X = trackBar2.Value / 10f;
            //  Vector3.Normalize(przestrzen.swiatlo);
            draw();

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            przestrzen.swiatlo.Y = trackBar3.Value / 10f;
            //   Vector3.Normalize(przestrzen.swiatlo);
            draw();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {

                przestrzen.IO = colorDialog1.Color;
                przestrzen.normalcolor = null;
                draw();
            }

        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {

                przestrzen.IL = colorDialog1.Color;
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
                przestrzen.normalcolor = resizedImage;
                for (int x = 0; x < resizedImage.Width; x++)
                {
                    for (int y = 0; y < resizedImage.Height; y++)
                    {
                        przestrzen.ImageColors[x, y] = resizedImage.GetPixel(x, y);

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
                // Teraz mo�esz wczyta� plik graficzny
                Bitmap originalImage = new Bitmap(filePath);
                Bitmap resizedImage = new Bitmap(originalImage, new Size(500, 500));
                przestrzen.normalmap = resizedImage;

                for (int x = 0; x < resizedImage.Width; x++)
                {
                    for (int y = 0; y < resizedImage.Height; y++)
                    {
                        przestrzen.BitmapVector[x, y] = przestrzen.ColorToNormalVector(resizedImage.GetPixel(x, y));
                    
                    }
                }
                przestrzen.ObliczZiWektory();
                draw();

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {


                Bitmap originalImage = new Bitmap(Properties.Resources.normal_map_example);

                Bitmap resizedImage = new Bitmap(originalImage, new Size(500, 500));
                przestrzen.normalmap = resizedImage;

                for (int x = 0; x < resizedImage.Width; x++)
                {
                    for (int y = 0; y < resizedImage.Height; y++)
                    {
                        przestrzen.BitmapVector[x, y] = przestrzen.ColorToNormalVector(resizedImage.GetPixel(x, y));

                    }
                }
                przestrzen.ObliczZiWektory();
                draw();
            }
            else
            {
                przestrzen.normalmap = null;
                przestrzen.ObliczZiWektory();
                draw();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {


                Bitmap originalImage = new Bitmap(Properties.Resources.original);

                Bitmap resizedImage = new Bitmap(originalImage, new Size(500, 500));
                przestrzen.normalcolor = resizedImage;
                for (int x = 0; x < resizedImage.Width; x++)
                {
                    for (int y = 0; y < resizedImage.Height; y++)
                    {
                        przestrzen.ImageColors[x, y] = resizedImage.GetPixel(x, y);

                    }
                }


                draw();
            }
            else
            {
                przestrzen.normalcolor = null;

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
            przestrzen.swiatlo.Z = trackBar6.Value / 5.0f;
            label12.Text = $"z: {trackBar6.Value / 5.0f}";
            //   Vector3.Normalize(przestrzen.swiatlo);
            draw();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                siatkatroj = 1;
                draw();
            }
            else
            {
                siatkatroj = 0;
                draw();
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                siatka = 1;
                draw();
            }
            else
            {
                siatka = 0;
                draw();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t += 0.08f; 

            przestrzen.swiatlo.X = (cx + a * (float)Math.Cos(t)) / 499.0f;
            przestrzen.swiatlo.Y = (cy + b * (float)Math.Sin(t)) / 499.0f;

            draw();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true) timer1.Start();
            else { timer1.Stop(); }
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            przestrzen.kd = trackBar5.Value / 10f;

            draw();
        }

        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            przestrzen.m = trackBar7.Value;

            draw();
        }
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            przestrzen.ks = trackBar4.Value / 10f;

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

         
            float scaleX = 500;
            float scaleY = 500;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                  
                    float scaledX = przestrzen.PunktyBaz[i, j].X * scaleX;
                    float scaledY = przestrzen.PunktyBaz[i, j].Y * scaleY;

                  
                    if (Math.Abs(scaledX - coordinates.X) < 10 && Math.Abs(scaledY - coordinates.Y) < 10)
                    {
                      
                        przestrzen.PunktyBaz[i, j].Z = trackBar8.Value / 10.0f;
                    }
                }
            }
            przestrzen.InicjujPunkty();
            przestrzen.ObliczZiWektory();
            draw();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog
            //{
            //    Title = "Wybierz plik TXT",
            //    Filter = "Plik tekstowy (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*"
            //};

            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    string filePath = openFileDialog.FileName;

            //    try
            //    {
            //        string zawartoscPliku = File.ReadAllText(filePath);
            //        float[] liczby = zawartoscPliku.Split(';')
            //                                       .Select(float.Parse)
            //                                       .ToArray();

            //        if (liczby.Length != 16)
            //        {
            //            throw new InvalidOperationException("Plik powinien zawiera� dok�adnie 16 liczb.");
            //        }

            //        int licznik = 0;
            //        for (int i = 0; i < 4; i++)
            //        {
            //            for (int j = 0; j < 4; j++)
            //            {
            //                punkty[i, j].Z = liczby[licznik++];
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Wyst�pi� b��d: " + ex.Message);
            //    }
            //}
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
                przestrzen.swiatlo.Z = 1.0f;
                przestrzen.sfera = true;
                przestrzen.InicjujPunkty();
                przestrzen.ObliczZiWektory();
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
                przestrzen.sfera = false;
                przestrzen.InicjujPunkty();
                przestrzen.ObliczZiWektory();
                draw();
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                przestrzen.transform = true;
            }
            else
            {
                przestrzen.transform = false;
            }
        }

        private void trackBar10_Scroll(object sender, EventArgs e)
        {
            przestrzen.alfa = trackBar10.Value / 5.0f;
            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    DrawPixel(0, 0, 0, i, j);
                }
            }
            przestrzen.M = Matrix4x4.CreateTranslation(-przestrzen.height / 2, -przestrzen.width / 2, 0) * Matrix4x4.CreateFromYawPitchRoll(0, przestrzen.alfa, przestrzen.beta) * Matrix4x4.CreateTranslation(przestrzen.height / 2, przestrzen.width / 2, 0);
            draw();
        }

        private void trackBar9_Scroll(object sender, EventArgs e)
        {
            przestrzen.beta = trackBar9.Value / 5.0f;

            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    DrawPixel(0, 0, 0, i, j);
                }
            }
            przestrzen.M = Matrix4x4.CreateTranslation(-przestrzen.height / 2, -przestrzen.width / 2, 0) * Matrix4x4.CreateFromYawPitchRoll(0, przestrzen.alfa, przestrzen.beta) * Matrix4x4.CreateTranslation(przestrzen.height / 2, przestrzen.width / 2, 0);
            draw();
        }


    }
}
