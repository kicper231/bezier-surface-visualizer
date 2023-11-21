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

                            // Rysowanie kó³ka
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
            //funkcja rysuj¹ca 
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


        public void FillPolygon(Trojkat trojkat)
        {


            //if (g == 13)
            //{
            //    g = g;
            //}


            float ymax = trojkat.P1.Y;
            float ymin = trojkat.P1.Y;

            Vector3 a = trojkat.P1;
            Vector3 b = trojkat.P2;
            Vector3 c = trojkat.P3;

            if (a.X == 0.5 && a.Y == 0.5)
                a.X = a.X;

            List<Kubel> aktualna = new List<Kubel>();

            if (trojkat.P2.Y > ymax)
                ymax = trojkat.P2.Y;
            if (trojkat.P3.Y > ymax)
                ymax = trojkat.P3.Y;

            if (trojkat.P2.Y < ymin)
                ymin = trojkat.P2.Y;
            if (trojkat.P3.Y < ymin)
                ymin = trojkat.P3.Y;



            int y = doint(ymin);
            int ymaxi = doint(ymax);
            int ymini = doint(ymin);
            // robimy to tylko dla trójkatów wiêc kubelek bêdzie jeden
            // celowe uproszeczenie algorytmu dla poprawy zlo¿onoœci 

            // List<Kubel>[] kubelki = new List<Kubel>[ymaxi - ymini + 1];
            List<Kubel> kubel = new List<Kubel>();
            int licznik = 0;


            void ObsluzSegment(Vector3 pStart, Vector3 pEnd)
            {
                float m1 = (pStart.X - pEnd.X);
                float m2 = (pStart.Y - pEnd.Y);
                if (m2 == 0) return; // Unikniêcie dzielenia przez zero // pionowa nie dodajemy 

                float m22 = m1 / m2;
                int m = (int)Math.Round(m22);
                (int, int) sybko(Vector3 pStart, Vector3 pEnd)
                {
                    if (pStart.Y < pEnd.Y)
                        return (doint(pStart.Y), doint(pStart.X));
                    else
                        return (doint(pEnd.Y), doint(pEnd.X));
                }
                int minY = 0;
                int endX = 0;
                (minY, endX) = sybko(pStart, pEnd);

                int maxY = doint(Math.Max(pStart.Y, pEnd.Y));
                int i = minY - ymini;
                // jakby wiele to stwórz nowa liste
                //     if (kubelki[i] == null) kubelki[i] = new List<Kubel>();


                //kubelki[i].Add(new Kubel(maxY, endx, m));
                kubel.Add(new Kubel(maxY, endX, m));
                licznik++;
            }

            ObsluzSegment(trojkat.P1, trojkat.P2);
            ObsluzSegment(trojkat.P2, trojkat.P3);
            ObsluzSegment(trojkat.P3, trojkat.P1);
            int podzial = przestrzen.podzial;
            int ax, ay, bx, by, cx, cy;


            ax = (int)(a.X * 3 * podzial);



            ay = (int)(a.Y * 3 * podzial);



            bx = (int)(b.X * 3 * podzial);



            by = (int)(b.Y * 3 * podzial);



            cx = (int)(c.X * 3 * podzial);




            cy = (int)(c.Y * 3 * podzial);





            while (licznik != 0 || aktualna.Count != 0)
            {


                //if (y - ymin >= kubelki.Length) return;
                //if (kubelki[y - ymin] != null)
                //{
                //    foreach (var kubel in kubelki[y - ymin])
                //    {
                //        aktualna.Add(kubel);
                //        licznik--;
                //    }

                //}
                if (y == doint(ymin))
                {
                    aktualna.Add(kubel[0]);
                    aktualna.Add(kubel[1]);
                    aktualna.Sort((a, b) => a.x.CompareTo(b.x));
                    licznik = licznik - 2;
                }

                //aktualna.Sort((a, b) => a.x.CompareTo(b.x));
                // rysowanie pixeli
                for (int i = 0; i < aktualna.Count - 1; i += 2)
                {

                    var przeciecie1 = aktualna[i];
                    var przeciecie2 = aktualna[i + 1];





                    for (int j = (int)przeciecie1.x; j <= (int)przeciecie2.x; j++)
                    {

                        float u = wroc(j);
                        float v = wroc(y);

                        if (j == -1) j = 0;
                        //float z = Barycentric2D(u, v, a, b, c);
                        // PointR gdzie = new PointR(u, v, z);
                        Vector3 gdzie = new Vector3(u, v, przestrzen.Zety[j, y]);


                        Vector3 aa = przestrzen.WektoryNormalne[j, y];
                        //if (normalmap != null)
                        //{
                        //    aa = wektorprzeksztalcony(aa, gdzie);
                        //}
                        //if (j == 24)
                        //{
                        //    j = j;
                        //}

                        Vector3 wersor = obliczWersorSwiatla(przestrzen.swiatlo, gdzie);
                        Color obiektu = przestrzen.IO;
                        if (przestrzen.normalcolor != null)
                            obiektu = przestrzen.ImageColors[j, y];

                        if (y == 0 && j == 200)
                            y = 0;
                        (int r, int g, int bb) = ObliczKolor(aa, wersor, new Vector3(0, 0, 1), przestrzen.kd, przestrzen.ks, przestrzen.m, obiektu, przestrzen.IL);
                        DrawPixel(r, g, bb, j, y);
                    }

                }


                aktualna.RemoveAll(element => element.ymax == y);
                y++;


                aktualna.ForEach(element => element.x += element.m);


            }


            //if (a == a)
            //{ a = a; }


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
                int mm = (int)Math.Round(m);
                if (kubelki[i] == null) kubelki[i] = new List<Kubel>();
                kubelki[i].Add(new Kubel(doint(segment.maxY().Item1), doint(segment.minY().Item2), mm));
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
                        DrawPixel(r, g, bb, j, y);


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
                // Teraz mo¿esz wczytaæ plik graficzny
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
                // Teraz mo¿esz wczytaæ plik graficzny
                Bitmap originalImage = new Bitmap(filePath);
                Bitmap resizedImage = new Bitmap(originalImage, new Size(500, 500));
                przestrzen.normalmap = resizedImage;

                for (int x = 0; x < resizedImage.Width; x++)
                {
                    for (int y = 0; y < resizedImage.Height; y++)
                    {
                        przestrzen.BitmapVector[x, y] = przestrzen.ColorToNormalVector(resizedImage.GetPixel(x, y));
                        // Tutaj mo¿esz u¿yæ koloru imageColors[x, y] w funkcji ObliczKolor
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
            t += 0.08f; // Zwiêkszanie parametru czasu

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

            // Zak³adaj¹c, ¿e PictureBox ma rozmiar 500x500
            float scaleX = 500;
            float scaleY = 500;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    // Skalowanie punktów
                    float scaledX = przestrzen.PunktyBaz[i, j].X * scaleX;
                    float scaledY = przestrzen.PunktyBaz[i, j].Y * scaleY;

                    // Sprawdzenie, czy klikniêcie jest blisko punktu
                    if (Math.Abs(scaledX - coordinates.X) < 10 && Math.Abs(scaledY - coordinates.Y) < 10)
                    {
                        // Aktualizacja wartoœci z
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

        }
    }
}