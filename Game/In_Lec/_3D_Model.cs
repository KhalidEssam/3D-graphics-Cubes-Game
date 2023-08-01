using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace In_Lec
{
   
    class _3D_Model
    {
        public List<_3D_Point> L_3D_Pts = new List<_3D_Point>();
        public List<Edge> L_Edges = new List<Edge>();
        public Camera cam;
        public int trap=0;

        public void AddPoint(_3D_Point pnn)
        {
            L_3D_Pts.Add(pnn);
        }
        public _3D_Point retunrstart()
        {
           
            _3D_Point pi = L_3D_Pts[1];

            return pi;
        }
        
        public void AddEdge(int i , int j , Color cl)
        {
            Edge pnn = new Edge(i, j);
            pnn.cl = cl;
            L_Edges.Add(pnn);
        }

        public void DrawYourSelf(Graphics g, int L, int flag)
        {
            Font FF = new Font("System", 10);
            if (L == 0)
            { 
                for (int k = 0; k < L_Edges.Count; k++)
                {
                    int i = L_Edges[k].i;
                    int j = L_Edges[k].j;

                    _3D_Point pi = L_3D_Pts[i];
                    _3D_Point pj = L_3D_Pts[j];

                    PointF pi_2D = cam.TransformToOrigin_And_Rotate_And_Project(pi);
                    PointF pj_2D = cam.TransformToOrigin_And_Rotate_And_Project(pj);



                    Pen Pn = new Pen(L_Edges[k].cl, 5);

                    if (pi_2D.Y < 2000.0f && pj_2D.Y < 2000.0f && pi_2D.Y > 0.0f && pj_2D.Y > 0.0f)
                    {
                        g.DrawLine(Pn, pi_2D.X, pi_2D.Y, pj_2D.X, pj_2D.Y);
                    }
                }
            }
            else
            {
                Pen Pn;
                int k = 0;
               
              
                for (int ii = 0; ii < 4; ii++)
                {
                    if (trap == 0)
                    {
                        Pn = new Pen(Color.White, 2);
                    }
                    else
                    {
                        Pn = new Pen(Color.Red, 2);
                    }
                    //if(k==8)
                    //{
                    //    Pn = new Pen(Color.Red, 2);
                    //}
                    //else if(k == 11)
                    //{
                    //    Pn = new Pen(Color.Green, 2);
                    //}

                    int i = L_Edges[k].i;
                    int j = L_Edges[k].j;

                    _3D_Point pi = L_3D_Pts[i];
                    _3D_Point pj = L_3D_Pts[j];

                    PointF pi_2D = cam.TransformToOrigin_And_Rotate_And_Project(pi);
                    PointF pj_2D = cam.TransformToOrigin_And_Rotate_And_Project(pj);

                    if (pi_2D.Y < 1800.0f && pj_2D.Y < 1800.0f && pi_2D.Y > 0.0f && pj_2D.Y > 0.0f)
                    {
                        g.DrawLine(Pn, pi_2D.X, pi_2D.Y, pj_2D.X, pj_2D.Y);
                        if (ii==1&&flag==1)
                        {
                            
                            
                           // g.DrawEllipse(Pn, new Rectangle((int)pi_2D.X, (int)pi_2D.Y, (int)pi_2D.X - 5, (int)pi_2D.Y - 5));
                            flag = 2;
                        }
                    }
                    if (k == 0)
                    {
                        k = 11;
                       Pn = new Pen(Color.Red, 2);
                    }
                    else if (k == 11)
                    {
                        Pn = new Pen(Color.Green, 2);
                        k = 8;
                    }
                    else if (k == 8)
                    {
                        //  Pn = new Pen(Color.Black, 2);
                        k = 4;
                    }

                }

            }
        }
    }
}
