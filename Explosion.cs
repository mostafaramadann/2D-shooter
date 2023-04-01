using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{
	class Explosion
	{
		public List<Bitmap> explosionframes = new List<Bitmap>();
		public int x;
		public int y;
		public int frameIndex;
		public Explosion(int x,int y)
		{
			for (int i = 0; i < 15; i++)
			{
				explosionframes.Add(new Bitmap("exp "+"("+(i+1)+")"+".bmp"));
				explosionframes[i].MakeTransparent(Color.White);
				explosionframes[i].MakeTransparent(Color.Black);
				explosionframes[i].MakeTransparent(Color.FromArgb(148, 41, 0, 1));
				explosionframes[i].MakeTransparent(Color.FromArgb(0, 189, 74, 8));
				explosionframes[i].MakeTransparent(Color.FromArgb(255, 41, 24, 8));
			}
			this.x = x;
			this.y = y;
		}
	}
}
