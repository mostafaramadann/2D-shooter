using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{
	class Car
	{
		public List<Bitmap> WalkingFramesR = new List<Bitmap>();
		public List<Bitmap> WalkingFramesL = new List<Bitmap>();
		public Bitmap CurrentFrame;
		public Rocket crocket;
		public Point Destination;
		public Point p1;
		public Point p2;
		public Point p3;
		public bool reachedp1 = false;
		public bool reachedp2 = false;
		public bool destdirleft = false;
		public bool left = false;
		public int x;
		public int y;
		public Car(int x,int y)
		{
			this.x = x;
			this.y = y;
			for (int i = 0; i < 2; i++)
			{
				WalkingFramesR.Add(new Bitmap("Car (" + (i+1) + ").bmp"));
				WalkingFramesL.Add(new Bitmap("Car (" + (i+1) + ")L.bmp"));
				WalkingFramesL[i].MakeTransparent(Color.Black);
				WalkingFramesR[i].MakeTransparent(Color.Black);
			}
		}

	}
}
