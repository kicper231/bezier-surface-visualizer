using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static grafa20.Geometry;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace grafa20
{
    public class Przestrzen
    {
        public Vector3[,] PunktyBaz { get; set; }
        public bool sfera = false;
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

        //sfera
        float srodekx = 1.0f / 2.0f;
        float srodey = 1.0f / 2.0f;
        float R = 1.0f / 2.0f;
        Vector3 sferasrodek;

        public float height = 500;
        public float width = 500;

        public float alfa = 0.1f;
        public float beta = 0.1f;
        public bool transform = false;
        public Matrix4x4 M;
        public Przestrzen()
        {
            sferasrodek = new Vector3(1.0f / 2.0f, 1.0f / 2.0f, 0);
            PunktyTroj = new Dictionary<(int, int), Vector3>();
            WektoryTroj= new Dictionary<(int, int), Vector3>();

            M = Matrix4x4.CreateTranslation(-height / 2, -width / 2, 0) * Matrix4x4.CreateFromYawPitchRoll(0, alfa, beta) * Matrix4x4.CreateTranslation(height / 2, width / 2, 0);

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
                    if(sfera==false)
                    { 
                    PunktyTroj[(i, j)] = new Vector3(i / (3.0f * podzial), j / (3.0f * podzial), OblicZ(i / (3.0f * podzial), j / (3.0f * podzial)));
                    WektoryTroj[(i, j)] = ObliczWektorNormalny(PunktyBaz, 3, 3, i / (3.0f * podzial), j / (3.0f * podzial));
                    }
                    else
                    {
                        if(i==0&&j==0)
                        {
                            i = i;
                        }
                        PunktyTroj[(i, j)] = new Vector3(i / (3.0f * podzial), j / (3.0f * podzial), OblicZsfera(i / (3.0f * podzial), j / (3.0f * podzial)));
                       WektoryTroj[(i, j)] = ObliczWektorsfera(PunktyTroj[( i,j)]);
                    }
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


        public Vector3 ObliczWektorsfera(Vector3 wektortrojkata)
        {
            return Vector3.Normalize((wektortrojkata - sferasrodek));
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





        public float OblicZsfera(float x, float y)
        {
            float z = 0;
            float x1 = x - 1.0f / 2.0f;
            float y1 = y - 1.0f / 2.0f;

            float srodekpierwiastka = R * R - y1 * y1 - x1 * x1;
            if (srodekpierwiastka > 0)
            {
                z = (float)Math.Sqrt(srodekpierwiastka);
                
            }

            return z;
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
                //int mm = (int)Math.Round(m);
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
                        if (j == -1) j = 0;
                      
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
