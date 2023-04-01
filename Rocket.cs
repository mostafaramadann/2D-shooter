using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{
	class Rocket
	{
		public Bitmap RocketImage = new Bitmap("missle.bmp");
		public int x, y;
		public Rocket(int x,int y)
		{
			this.x = x;
			this.y = y;
			RocketImage.MakeTransparent(Color.White);
		}
	}
}
