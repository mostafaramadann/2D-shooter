using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{
	class Barrel
	{
		public Bitmap BarrelIm = new Bitmap("Barrel.bmp");
		public int x, y;
		public Barrel(int x, int y)
		{
			BarrelIm.MakeTransparent(Color.Black);
			this.x = x;
			this.y = y;
		}
	}
}
