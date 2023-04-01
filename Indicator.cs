using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{
	class Indicator
	{
		public Bitmap Arrow = new Bitmap("Arrow.bmp");
		public int x, y;
		public Indicator(int x,int y)
		{
			Arrow.MakeTransparent(Color.White);
			this.x = x;
			this.y = y;
		}
	}
}
