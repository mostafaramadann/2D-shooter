using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{
	class Mbullet
	{
		public int x, y;
		public Bitmap currentframe;
		public Bitmap Left=new Bitmap("MegabulletL.bmp");
		public Bitmap Right=new Bitmap("Megabullet.bmp");
		public bool left = false;
		public Mbullet(bool left,int x,int y)
		{
			this.x = x;
			Left.MakeTransparent(Color.White);
			Left.MakeTransparent(Color.Black);
			Right.MakeTransparent(Color.White);
			Right.MakeTransparent(Color.Black);
			this.y = y;
			this.left = left;
			currentframe = this.left ?Left : Right;
		}
	}
}
