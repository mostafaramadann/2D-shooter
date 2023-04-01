using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{

	class Coin
	{
		public Bitmap coinframes = new Bitmap("coin (1).bmp");
		public int x, y;
		public Coin(int x,int y)
		{
			coinframes.MakeTransparent(Color.Black);
			this.x = x;
			this.y = y;
		}
	}
}
