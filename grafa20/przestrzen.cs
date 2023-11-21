using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static grafa20.Geometry;
using System.Runtime.InteropServices;


namespace grafa20
{
    public class Przestrzen
    {
        public Vector3[,] PunktyBaz { get; set; }
        public Dictionary<(int, int), Vector3> PunktyTroj { get; set; }
        public Dictionary<(int, int), Vector3> WektoryTroj { get; set; }
        public Bitmap normalcolor;
        public List<Trojkat> trojkaty = new List<Trojkat>();
        public Vector3[,] WektoryNormalne = new Vector3[500, 500];
        public float[,] Zety = new float[500, 500]; 
        public GCHandle BitsHanle { get; set; }
        public Byte[] pixels;
        public Bitmap wyswietl;
        public Bitmap normalmap;
       // public Bitmap normalmap;
        public int podzial = 5;
        public Color IO = Color.Red;
        public Color IL = Color.White;
        public Color[,] ImageColors = new Color[500, 500];
        public Vector3[,] BitmapVector = new Vector3[500, 500];
        public float kd = 0.5f;
        public float ks = 0.5f;
        public float m;

        public Przestrzen()
        {
            PunktyTroj = new Dictionary<(int, int), Vector3>();
            WektoryTroj= new Dictionary<(int, int), Vector3>();

            PunktyBaz = new Vector3[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                   

                    float z = 0;
                    //if ((i == 1 || i == 2) && (j == 1 || j == 2))
                    //{
                    //    z = 0.3f;
                    //}

                    PunktyBaz[i, j] = new Vector3(i / 3.0f, j / 3.0f, z);
                }
            }
            InicjujPunkty();
        }
        public Vector3 swiatlo = new Vector3(0.5f,0.5f,0.25f);
        //funkcja uzywana przy stacie programu i zmianie trangulacjji 
        public void InicjujPunkty()
        {
            PunktyTroj = new Dictionary<(int, int), Vector3>();
            WektoryTroj = new Dictionary<(int, int), Vector3>();
            trojkaty = new List<Trojkat>();
            for (int i = 0; i <= 3 * podzial; i++)
            {
                for (int j = 0; j <= 3 * podzial; j++)
                {
                    PunktyTroj[(i, j)] = new Vector3(i / (3.0f * podzial), j / (3.0f * podzial), OblicZ(i / (3.0f * podzial), j / (3.0f * podzial)));
                    WektoryTroj[(i, j)] = ObliczWektorNormalny(PunktyBaz, 3, 3, i / (3.0f * podzial), j / (3.0f * podzial));
                }
            }

           
                

               
             
            

            for (int i = 0; i < 3 * podzial; i++)
            {
                for (int j = 0; j < 3 * podzial; j++)
                {
                    trojkaty.Add(new Trojkat(
                        PunktyTroj[(i, j)],
                        PunktyTroj[(i, j + 1)],
                        PunktyTroj[(i + 1, j)]));

                    trojkaty.Add(new Trojkat(
                        PunktyTroj[(i + 1, j)],
                        PunktyTroj[(i, j + 1)],
                        PunktyTroj[(i + 1, j + 1)]));
                }
            }
        }

        public float OblicZ(float x, float y)
        {
            float z = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    z += PunktyBaz[i, j].Z * CalculateB(i, x) * CalculateB(j, y);
                }
            }
            return z;
        }

       




        public void obliczanie(Trojkat trojkat)
        {


            //if (g == 13)
            //{
            //    g = g;
            //}

            Vector3 a = trojkat.P1;
            Vector3 b = trojkat.P2;
            Vector3 c = trojkat.P3;
            float ymax = trojkat.P1.Y;
            float ymin = trojkat.P1.Y;

            


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
            // robimy to tylko dla trójkatów więc kubelek będzie jeden
            // celowe uproszeczenie algorytmu dla poprawy zlożoności 

            // List<Kubel>[] kubelki = new List<Kubel>[ymaxi - ymini + 1];
            List<Kubel> kubel = new List<Kubel>();
            int licznik = 0;


            void ObsluzSegment(Vector3 pStart, Vector3 pEnd)
            {
                float m1 = (pStart.X - pEnd.X);
                float m2 = (pStart.Y - pEnd.Y);
                if (m2 == 0) return; // Uniknięcie dzielenia przez zero // pionowa nie dodajemy 

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

            int ax, ay, bx, by, cx, cy;
            {
               
                    ax = (int)(a.X*3*podzial);
              

                
                    ay = (int)(a.Y * 3 * podzial);
                

               
                    bx = (int)(b.X * 3 * podzial);
                

               
                    by = (int)(b.Y * 3 * podzial);
                

                
                    cx = (int)(c.X * 3 * podzial);
                
                 

             
                    cy = (int)(c.Y * 3 * podzial);
               
            }



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
                        if (y == 0 && j == 200)
                            y = y;
                        if (y == 120 && j == 230)
                            y = y;
                        float z = Barycentric2D(u, v, a, b, c);
                        // PointR gdzie = new PointR(u, v, z);
                        Vector3 gdzie = new Vector3(u, v, z);


                        Vector3 aa = Barycentric3D(gdzie, a,b,c,WektoryTroj[(ax, ay)], WektoryTroj[(bx, by)], WektoryTroj[(cx, cy)]);
                        if (normalmap != null)
                        {
                            aa = WektorPrzeksztalcony(aa, j,y);
                        }

                        WektoryNormalne[j, y] = aa;
                        
                        Zety[j, y] = z;
                       
                        
                      
                        
                    }

                }


                aktualna.RemoveAll(element => element.ymax == y);
                y++;


                aktualna.ForEach(element => element.x += element.m);


            }


            //if (a == a)
            //{ a = a; }


        }
     


        public void ObliczZiWektory()
        {
            //Parallel.ForEach(trojkaty, obliczanie);

            Parallel.ForEach(trojkaty, trojkat =>
            {
                List<Segment> lista = new List<Segment>
    {
                        new Segment(trojkat.P1, trojkat.P2),
                        new Segment(trojkat.P2, trojkat.P3),
                        new Segment(trojkat.P3, trojkat.P1)
    };

                obliczanie2(lista);
            });

        }



        public void obliczanie2(List<Segment> krawedzie)
        {

            Vector3 a = krawedzie[0].ps;
            Vector3 b= krawedzie[1].ps;
            Vector3 c= krawedzie[2].ps;
            float ymin1 = float.MaxValue;
            float ymax1 = float.MinValue;

            int ax = (int)(a.X * 3 * podzial);
            int ay = (int)(a.Y * 3 * podzial);
            int bx = (int)(b.X * 3 * podzial);
            int by = (int)(b.Y * 3 * podzial);
            int cx = (int)(c.X * 3 * podzial);
            int cy = (int)(c.Y * 3 * podzial);

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
                        if (j == -1) j = 0;
                        if (y == 120 && j == 230)
                            y = y;
                        float z = Barycentric2D(u, v, a, b, c);
                        // PointR gdzie = new PointR(u, v, z);
                        Vector3 gdzie = new Vector3(u, v, z);


                        Vector3 aa = Barycentric3D(gdzie, a, b, c, WektoryTroj[(ax, ay)], WektoryTroj[(bx, by)], WektoryTroj[(cx, cy)]);
                        if (normalmap != null)
                        {
                            aa = WektorPrzeksztalcony(aa, j, y);
                        }

                        WektoryNormalne[j, y] = aa;

                        Zety[j, y] = z;


                    }
                }

                aktualna.RemoveAll(element => element.ymax == y);
                y++;

                aktualna.ForEach(element => element.x += element.m);

            }
        }

        public Vector3 WektorPrzeksztalcony(Vector3 obliczony, int xx, int yy)
        {
            // Color normalMapColor = normalmap.GetPixel(xx, yy);
            Vector3 normalMapVector = BitmapVector[xx, yy]; // Zakładam, że ta metoda zwraca już Vector3

        Vector3 B = Vector3.Cross(obliczony, new Vector3(0, 0, 1));
        if (obliczony.Z == 1)
        {
            B = new Vector3(0, 1, 0);
        }
        Vector3 T = Vector3.Cross(B, obliczony);

            Vector3 k1 = new Vector3(T.X, B.X, obliczony.X);
            Vector3 k2 = new Vector3(T.Y, B.Y, obliczony.Y);
            Vector3 k3 = new Vector3(T.Z, B.Z, obliczony.Z);

        float x = Vector3.Dot(k1, normalMapVector);
        float y = Vector3.Dot(k2, normalMapVector);
        float z = Vector3.Dot(k3, normalMapVector);

        return new Vector3(x, y, z);
    }
        public Vector3 ColorToNormalVector(Color color)
        {
            float x = (color.R / 255f) * 2 - 1;
            float y = (color.G / 255f) * 2 - 1;
            float z = (color.B / 255f)*2-1;
            return new Vector3(x, y, z);
        }


    }






   
}
