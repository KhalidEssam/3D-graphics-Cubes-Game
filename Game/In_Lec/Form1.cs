using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace In_Lec
{
    class circle : _3D_Model
    {

        public void Design(float startx, float starty)
        {
            int R = 30;
            double x=0, y=0, Z = 0;
            ////Console.WriteLine(starty);
            int iP = 0;
            for (float th = 0; th < 360; th += 1)
            {
                x = R * Math.Cos(th * Math.PI / 180) + startx;
                y = R * Math.Sin(th * Math.PI / 180) + starty;

                AddPoint(new _3D_Point((float)x , (float)y, (float)Z ));
                if (iP > 0 && th != 0)
                {
                    AddEdge(iP, iP - 1, Color.Red);
                }
                iP++;
            }
        }

    }


    public partial class Form1 : Form
    {
        Bitmap off;

        _3D_Model Cube = new _3D_Model();
        _3D_Model hero = new _3D_Model();
        _3D_Model Cube2 = new _3D_Model();

        Camera cam = new Camera();
        Timer ttk = new Timer();

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.Load += new EventHandler(Form1_Load);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            ttk.Tick += Ttk_Tick;
            ttk.Start();

        }
        int fup = 0;
        int ct = 0;
        int flag = 12, rr = 7, ll = 6, lvl = 0, r = 0, l = 0;
        int leftnow = 0, rightnow = 0;
        private void Ttk_Tick(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                cam.cop.Z += 6;
                if (lvl < m.Count / 14 && m[lvl * 14 + (ll + 1)].trap == 0)
                {

                    if (fup == 0)
                    {
                        v1 = m[lvl * 14 + ll].L_3D_Pts[m[lvl * 14 + ll].L_Edges[4].i];
                        v2 = m[lvl * 14 + ll].L_3D_Pts[m[lvl * 14 + ll].L_Edges[4].j];
                        Transformation.RotateArbitrary(hero.L_3D_Pts, v2, v1, -9);
                        ct++;
                        //flag = 0;
                    }

                    if (ct == 10)
                    {
                        lvl += 1;
                        ct = 0;
                        checkmate();
                        if (leftnow != 0)
                        {
                            RotLeft(leftnow);
                        }
                        if (rightnow != 0)
                        {
                            RotRight(rightnow);

                        }
                        rightnow = 0;
                        leftnow = 0;
                       

                    }
                }

            }


            DrawDubble(CreateGraphics());
        }
        int fllag = 0;
        void checkmate()
        {
            if (lvl * 14 + (ll + 1) < m.Count)
            {

                if (m[lvl * 14 + (ll + 1)].trap == 1)
                {
                    fllag = 1;
                    ttk.Stop();
                    die();
                    MessageBox.Show("GAME OVER");
                    restart();
                }
            }
            else
            {

                ttk.Stop();
                win();
                
            }
        }
        void restart()
        {
           
            rightnow = 0;
            leftnow = 0;
            fup = 0;
            ct = 0;
            flag = 12;
            rr = 7;
            ll = 6;
            lvl = 0;
            r = 0;
            l = 0;
            leftnow = 0;
            rightnow = 0;
            Cube = new _3D_Model();
            hero = new _3D_Model();
            Cube2 = new _3D_Model();
            m = new List<_3D_Model>();
            circles = new List<circle>();
            cam = new Camera();

            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);


            int cx = 400;
            int cy = 400;
            cam.ceneterX = (this.ClientSize.Width / 2);
            cam.ceneterY = (this.ClientSize.Height / 2);

            cam.cxScreen = cx;
            cam.cyScreen = cy;
            cam.BuildNewSystem();

            float x = -1300;
            float y = 0;
            float z = -300;
            int count = 0, zz = 35, xx = -67;
            Random rand = new Random();
            int val = rand.Next(0, 2);
            int trap = 0;
            for (int i = 1; i < 30; i++)
            {
                for (int j = 1; j < 15; j++)
                {
                    val = rand.Next(0, 100);
                    Cube = new _3D_Model();
                    Cube.cam = cam;
                    if (val < 90 || i ==1||j==1)
                    { Cube.trap = 0; }
                    else
                    {

                        Cube.trap = 1;
                    }
                  
                    CreateCube(Cube, x, y, z, Color.Black);

                    Transformation.Scale(Cube, 0.3f, 0.3f, 0.3f);
                    x += 200;
                    m.Add(Cube);
                    if (Cube.trap == 1)
                    {
                        trap++;
                        circle circle = new circle();
                        circle.cam = cam;
                        //Console.WriteLine(Cube.retunrstart().Z);
                        circle.Design(Cube.retunrstart().X + xx, Cube.retunrstart().Z+zz );
                        Transformation.RotatX(circle.L_3D_Pts, 90);
                        circles.Add(circle);
                        count += 1;

                    }
                    xx += 5;
                }
                xx = -65;
                if (trap != 0 && i < 10)
                {
                  
                    //xx += trap;
                }

                trap = 0;
                z += 200;
                x = -1300;
            }

            ////Console.WriteLine(count);
            ////Console.WriteLine(circles.Count);
            hero.cam = cam;
            Createhero(hero, 100, 200, -300, Color.Green);
            Transformation.Scale(hero, 0.3f, 0.3f, 0.3f);

            _3D_Point cpyOfCop = new _3D_Point(cam.cop);
            _3D_Model mdl = new _3D_Model();
            mdl.AddPoint(cpyOfCop);
            _3D_Point vv1 = new _3D_Point(cam.lookAt);
            _3D_Point vv2 = new _3D_Point(cam.lookAt);
            vv2.X += 50;
            Transformation.RotateArbitrary(mdl.L_3D_Pts, vv1, vv2, 60);
            cam.cop = new _3D_Point(cpyOfCop);
            cam.BuildNewSystem();
            checkmate();
            SoundPlayer splayer = new SoundPlayer(@"C:\Users\LENOVO\Downloads\T2ADM.wav");
            splayer.Play();
            ttk.Start();
            cam.cop.Y += 100;

        }
        void win()
        {


            SoundPlayer splayer = new SoundPlayer(@"C:\Users\LENOVO\Downloads\win.wav");
            splayer.Play();
            MessageBox.Show("GG");
        }
        void die()
        {
            SoundPlayer splayer = new SoundPlayer(@"C:\Users\LENOVO\Downloads\lose.wav");
            splayer.Play();
            for (int i = 0; i < 20; i++)
            {
                Transformation.Scale(hero, 0.9f , 0.9f , 0.9f );
                DrawDubble(CreateGraphics());
                 
            }
            

        }
         _3D_Point v1;
         _3D_Point v2;
         int laps = 0;
         void Form1_KeyDown(object sender, KeyEventArgs e)
         {
             switch (e.KeyCode)
             {
                 case Keys.X:
                     _3D_Point cpyOfCop = new _3D_Point(cam.cop);
                     _3D_Model mdl = new _3D_Model();
                     mdl.AddPoint(cpyOfCop);
                     _3D_Point vv1 = new _3D_Point(cam.lookAt);
                     _3D_Point vv2 = new _3D_Point(cam.lookAt);
                     vv2.X += 10;
                     c1 += 10;
                     Transformation.RotateArbitrary(mdl.L_3D_Pts, vv1, vv2, 10);
                     cam.cop = new _3D_Point(cpyOfCop);
                     cam.BuildNewSystem();
                     break;
                 case Keys.Y:
                     Transformation.RotatY(Cube.L_3D_Pts, 1);
                     break;
                 case Keys.Z:
                     Transformation.RotatZ(Cube.L_3D_Pts, 1);
                     break;


                 case Keys.F:
                     cam.cop.Y += 10;
                     break;
                 case Keys.G:
                     cam.cop.Y -= 10;
                     break;

                 case Keys.Enter:
                     flag = 1;
                     break;

                 case Keys.Up:
                     cam.cop.Z += 10;
                     break;
                 case Keys.Right:
                     cam.cop.X += 10;
                     //c3 += 10;
                     break;
                 case Keys.Left:
                     cam.cop.X -= 10;
                     //c3 += 10;
                     break;
                 case Keys.Down:
                     cam.cop.Z -= 10;
                     break;

                 case Keys.Space:
                     MessageBox.Show("lap" + laps);
                     break;
             }

             if (e.KeyCode == Keys.A)
             {
                 leftnow ++;
             }
             if (e.KeyCode == Keys.D)
             {
                 rightnow ++ ;
             }


             DrawDubble(this.CreateGraphics());
         }
         int c1 = 0, c2 = 0,c3=0;
         void Createhero(_3D_Model M, float XS, float YS, float ZS, Color vvv)
         {
             float[] vert =
                             {
                                     -100,100,-100,
                                     100,100,-100,
                                     100,-100,-100,
                                     -100,-100,-100,
                                     -100,100,100,
                                     100,100,100,
                                     100,-100,100,
                                     -100,-100,100,

                             };


             _3D_Point pnn;
             int j = 0;
             for (int i = 0; i < 8; i++)
             {
                 pnn = new _3D_Point(vert[j] + XS, vert[j + 1] + YS, vert[j + 2] + ZS);
                 j += 3;
                 M.AddPoint(pnn);
             }


             int[] Edges = {
                                 0,1,
                                 1,2,
                                 2,3,
                                 3,0,
                                 4,5,
                                 5,6,
                                 6,7,
                                 7,4,
                                 0,4,
                                 3,7,
                                 2,6,
                                 1,5
                           };
             j = 0;
             /*            Color[] cl = { Color.Red, Color.Green, Color.Black, Color.Blue };
             */
            for (int i = 0; i < 12; i++)
            {
                
                M.AddEdge(Edges[j], Edges[j + 1], vvv);

                j += 2;
            }
        }
        void CreateCube(_3D_Model M, float XS, float YS, float ZS, Color vvv)
        {
            float[] vert = 
                            {
                                    -100,100,-100,
                                    100,100,-100,
                                    100,-100,-100,
                                    -100,-100,-100,
                                    -100,100,100,
                                    100,100,100,
                                    100,-100,100,
                                    -100,-100,100,

                            };


            _3D_Point pnn;
            int j = 0;
            for (int i = 0; i < 8; i++)
            {
                pnn = new _3D_Point(vert[j]+XS, vert[j + 1]+YS, vert[j + 2]+ZS);
                j += 3;
                M.AddPoint(pnn);
            }


            int[] Edges = {
                                0,1,
                                1,2,
                                2,3,
                                3,0,
                                4,5,
                                5,6,
                                6,7,
                                7,4,
                                0,4,
                                3,7,
                                2,6,
                                1,5
                          };
            j = 0;
/*            Color[] cl = { Color.Red, Color.Green, Color.Black, Color.Blue };
*/            for (int i = 0; i < 12; i++)
            {
                M.AddEdge(Edges[j], Edges[j + 1], vvv);

                j += 2;
            }
        }
        List<_3D_Model> m = new List<_3D_Model>();
        List<circle> circles = new List<circle>();
        void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width , this.ClientSize.Height);


            int cx = 400;
            int cy = 400;
            cam.ceneterX = (this.ClientSize.Width / 2);
            cam.ceneterY = (this.ClientSize.Height / 2);
            
            cam.cxScreen = cx;
            cam.cyScreen = cy;            
            cam.BuildNewSystem();
           
            float x = -1300;
            float y = 0;
            float z = -300;
            int count = 0,zz=35,xx=-67;
            Random rand = new Random();
            int val = rand.Next(0, 2);
            int trap = 0;
            for (int i = 1; i < 30; i++)
            {
                for (int j = 1; j < 15; j++)
                {
                    val = rand.Next(0, 100);
                    Cube = new _3D_Model();
                    Cube.cam = cam;
                    if (val < 90 || i==1||j==1)
                    { Cube.trap = 0; }
                    else
                    {
                        
                        Cube.trap = 1;
                    }
                 
                    CreateCube(Cube, x, y, z, Color.Black);
                    
                    Transformation.Scale(Cube, 0.3f, 0.3f, 0.3f);
                    x += 200;
                    m.Add(Cube);
                    if (Cube.trap == 1)
                    {
                        trap ++;
                        circle circle = new circle();
                        circle.cam = cam;
                        ////Console.WriteLine(Cube.retunrstart().Z);
                        circle.Design(Cube.retunrstart().X+xx, Cube.retunrstart().Z+zz);
                        Transformation.RotatX(circle.L_3D_Pts, 90);
                        circles.Add(circle);
                        count += 1;
                        
                    }
                    xx +=5;
                }
                xx = -65;
      
                

                trap = 0;
                z += 200;
                x = -1300;
            }

            ////Console.WriteLine(count);
            ////Console.WriteLine(circles.Count);
            hero.cam = cam;
            Createhero(hero,100,200,-300 ,Color.Green);
            Transformation.Scale(hero, 0.3f, 0.3f, 0.3f);

            _3D_Point cpyOfCop = new _3D_Point(cam.cop);
            _3D_Model mdl = new _3D_Model();
            mdl.AddPoint(cpyOfCop);
            _3D_Point vv1 = new _3D_Point(cam.lookAt);
            _3D_Point vv2 = new _3D_Point(cam.lookAt);
            vv2.X += 50;
            //cam.cop.Y += 1500;
            Transformation.RotateArbitrary(mdl.L_3D_Pts, vv1, vv2, 60);
            cam.cop = new _3D_Point(cpyOfCop);
            cam.BuildNewSystem();
            checkmate();
            SoundPlayer splayer = new SoundPlayer(@"C:\Users\LENOVO\Downloads\T2ADM.wav");
            splayer.Play();
            cam.cop.Y += 100;
            cam.cop.X += 50;
        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubble(e.Graphics);
        }

        void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);
            
            hero.DrawYourSelf(g,0,hero.trap);
           for (int i=0;i<m.Count;i++)
           {
                if (i % 14 != 0)
                {
                    m[i].DrawYourSelf(g, 1, m[i].trap);
                }
           }
            for (int i = 0; i < circles.Count; i++)
            {

                circles[i].DrawYourSelf(g, 0, 0);

            }
        }

        void DrawDubble(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
        void RotRight(int num)
        {
            for (int j = 0; j < num; j++)
            {
                if (rr < 13)
                {

                    
                    for (int i = 0; i < 15; i++)
                    {
                        /*   cam.cop.X -= 10;*/
                        v1 = m[lvl * 14 + rr].L_3D_Pts[m[lvl * 14 + rr].L_Edges[11].i];
                        v2 = m[lvl * 14 + rr].L_3D_Pts[m[lvl * 14 + rr].L_Edges[11].j];
                        Transformation.RotateArbitrary(hero.L_3D_Pts, v1, v2, -6);
                        DrawDubble(CreateGraphics());
                    }

                    ll++;
                    rr++;
          
                    checkmate();
                    
                    if (fllag == 1)
                    {
                        fllag = 0;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            
        }
        void RotLeft(int num)
        {
            for (int j = 0; j < num; j++)
            {
                if (ll > 0)
                {


                    for (int i = 0; i < 15; i++)
                    {
                        v1 = m[lvl * 14 + ll].L_3D_Pts[m[lvl * 14 + ll].L_Edges[11].i];
                        v2 = m[lvl * 14 + ll].L_3D_Pts[m[lvl * 14 + ll].L_Edges[11].j];
                        Transformation.RotateArbitrary(hero.L_3D_Pts, v1, v2, 6);
                        DrawDubble(CreateGraphics());
                    }
                    ll--;
                    rr--;
                    
                    checkmate();
                   
                    if (fllag == 1)
                    {
                        fllag = 0;
                        break;
                    }


                }
                else
                {
                    break;
                }
            }
           
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
