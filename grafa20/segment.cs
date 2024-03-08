using System.Numerics;

namespace grafa20
{
    public class Segment
    {
        public Vector3 ps;

        public Vector3 pe;

        //public (int, int) maxY()
        //{
        //    return ps.Y1 >= pe.Y1 ? (ps.Y1, ps.X1) : (pe.Y1, pe.X1);
        //}
        //public (int, int) minY()
        //{
        //    return ps.Y1 <= pe.Y1 ? (ps.Y1, ps.X1) : (pe.Y1, pe.X1);
        //}
        //public (int, int) minX()
        //{
        //    return ps.X1 <= pe.X1 ? (ps.X1, ps.Y1) : (pe.X1, pe.Y1);
        //}

        public (float, float) maxY()
        {
            return ps.Y >= pe.Y ? (ps.Y, ps.X) : (pe.Y, pe.X);
        }

        public (float, float) minY()
        {
            return ps.Y <= pe.Y ? (ps.Y, ps.X) : (pe.Y, pe.X);
        }

        public Segment(Vector3 pps, Vector3 ppe)
        {
            ps = pps;
            pe = ppe;
        }
    }
}