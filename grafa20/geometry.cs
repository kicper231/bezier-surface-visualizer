using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Policy;

namespace grafa20
{
    public static class Geometry
    {
       
        
        

      

     public static Vector3 ObliczWektorNormalny(Vector3[,] controlPoints, int n, int m, float u, float v)
    {
        Vector3 Pu = ObliczPu(controlPoints, n, m, u, v);
        Vector3 Pv = ObliczPv(controlPoints, n, m, u, v);

        Vector3 normalny = Vector3.Cross(Pu, Pv);
        normalny = Vector3.Normalize(normalny);

        return normalny;
    }

        public static Vector3 ObliczPu(Vector3[,] controlPoints, int n, int m, float u, float v)
        {
            Vector3 Pu = new Vector3(0, 0, 0);
            for (int i = 0; i <= n - 1; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    Vector3 difference = controlPoints[i + 1, j] - controlPoints[i, j];

                    float bi = CalculateB2(i, u); 
                    float bj = CalculateB(j, v); 

                    Pu += difference * bi * bj;
                }
            }
            Pu *= 3.0f;
            return Pu;
        }

        public static Vector3 ObliczPv(Vector3[,] controlPoints, int n, int m, float u, float v)
        {
            Vector3 Pv = new Vector3(0, 0, 0);
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= m - 1; j++)
                {
                    Vector3 difference = controlPoints[i, j + 1] - controlPoints[i, j];

                    float bi = CalculateB(i, u); 
                    float bj = CalculateB2(j, v); 

                    Pv += difference * bi * bj;
                }
            }
            Pv *= 3.0f;
            return Pv;
        }
        //git
        public static float CalculateB(int i, float x)
        {
            switch (i)
            {
                case 0:
                    return -1.0f * x*x*x + 3.0f *x*x - 3.0f * x + 1;
                case 1:
                    return 3.0f * x * x * x - 6.0f *x * x + 3.0f * x;
                case 2:
                    return -3.0f * x * x * x + 3.0f *x * x;
                case 3:
                    return x * x * x;
                default:
                    throw new ArgumentException("Invalid value for i");
            }
        }
        //git
        public static float CalculateB2(int i, float x)
        {
            switch (i)
            {
                case 0:
                    return x*x - 2.0f * x + 1.0f;
                case 1:
                    return -2.0f * x*x + 2.0f * x;

                case 2:
                    return x * x;
                default:
                    throw new ArgumentException("Invalid value for i");
            }
        }




        public static float Barycentric2D(float x, float y, Vector3 a, Vector3 b, Vector3 c)
        {
            Vector3 p = new Vector3(x, y, 0);

            Vector3 ap = p - a;
            Vector3 bp = p - b;
            Vector3 ab = b - a;
            Vector3 ac = c - a;
            Vector3 bc = c - b;

            float ail = Math.Abs(Vector3.Cross(bc, bp).Z);
            float bil = Math.Abs(Vector3.Cross(ac, ap).Z);
            float cil = Math.Abs(Vector3.Cross(ab, ap).Z);

            float pole = Math.Abs(Vector3.Cross(ab, ac).Z);

            float z = (a.Z * ail + b.Z * bil + c.Z * cil) / pole;

            return z;
        }





        public static Vector3 Barycentric3D(Vector3 p,Vector3 a, Vector3 b, Vector3 c, Vector3 aw, Vector3 bw, Vector3 cw)
        {
            Vector3 ap = p - a;
            Vector3 bp = p - b;
            Vector3 ab = b - a;
            Vector3 ac = c - a;
            Vector3 bc = c - b;

            float ail = Vector3.Cross(bc, bp).Length();
            Vector3 z = Vector3.Cross(ac, ap);
            float bil = z.Length();

            float cil = Vector3.Cross(ab, ap).Length();

            float pole = Vector3.Cross(ab, ac).Length();

            Vector3 barycentricwektor = (aw * ail + bw * bil + cw * cil) / pole;
            barycentricwektor = Vector3.Normalize(barycentricwektor);

            return barycentricwektor;
        }


        public static Vector3 obliczWersorSwiatla(Vector3 swiatlo, Vector3 punkt)
        {
            Vector3 zwroc = Vector3.Normalize(swiatlo-punkt);
            return zwroc;
        }


        public static int doint(float a)
        {


            return (int)(a * 499);
        }
        public static float tofloat(int a)
        {
            return a / 499f;
        }


        public static (int, int, int) CalculateColor(Vector3 N, Vector3 L, Vector3 V, float kd, float ks, float m, Color IO, Color IL)
        {

            float IO_R = IO.R / 255f;
            float IO_G = IO.G / 255f;
            float IO_B = IO.B / 255f;
            float IL_R = IL.R / 255f;
            float IL_G = IL.G / 255f;
            float IL_B = IL.B / 255f;
          
            float cosTheta = Math.Max(0, Vector3.Dot(N, L));
            float diffuseR = kd * IL_R * IO_R * cosTheta;
            float diffuseG = kd * IL_G * IO_G * cosTheta;
            float diffuseB = kd * IL_B * IO_B * cosTheta;
       
            Vector3 R = 2 * Vector3.Dot(N, L) * N - L;
            Vector3.Normalize(R);
            float cosAlpha = Math.Max(0, Vector3.Dot(V, R));
            float cosAlpham = cosAlpha;
            for (int i = 0; i < m - 1; i++)
            {
                cosAlpham *= cosAlpha;
            }

            float specularR = ks * IL_R * IO_R * cosAlpham;
            float specularG = ks * IL_G * IO_G * cosAlpham;
            float specularB = ks * IL_B * IO_B * cosAlpham;

            
            int r = (int)Math.Min(255, (diffuseR +specularR ) * 255);
            int g = (int)Math.Min(255, (diffuseG +specularG  ) * 255);
            int b = (int)Math.Min(255, (diffuseB+ specularB) * 255);

            return (r, g, b);
        }



    }
}
