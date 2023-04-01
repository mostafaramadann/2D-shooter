using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Multimedia_Project
{
	class SBullet:Character
	{
		public Point Destination;
		public int Factorx;
		public int Factory;
		public SBullet(int x,int y,Point Destination):base(null)
		{
			this.CurrentFrame = new Bitmap("b3.bmp");
			CurrentFrame.MakeTransparent(Color.White);
			this.Destination = Destination;
			this.x = x;
			this.y = y;
			if (x >= Destination.X)
				Factorx = x / Destination.X;
			else Factorx = Destination.X / x;
			if (y >= Destination.Y)
				Factory = (y / Destination.Y);
			else
				Factory = (Destination.Y /y);

		}
	}
}
