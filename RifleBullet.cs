using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{
	class RifleBullet
	{
		public int x, y;
		public Bitmap RBR = new Bitmap("riflebulletR.bmp");
		public Bitmap RBL = new Bitmap("riflebulletL.bmp");
		public bool left = false;
		public Bitmap Current;
		public RifleBullet(bool dir,int x, int y)
		{
			left = dir;
			Current = left == true ? RBL : RBR;
			this.x = x;
			this.y = y;
		}
	}
}
