using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{
	class Dragon
	{
		public int x, y;
		public int lifes = 500;
		public List<Bitmap> dragonframes = new List<Bitmap>();
		public int DragonIndex = 0;
		public List<screensource> sources = new List<screensource>();
		public bool fire = true;
		public int xdragbull = 0;
		public int ydragbull = 0;
		public int firecooldown = 0;
		public Dragon(int x, int y)
		{
			this.x = x;
			this.y = y;
			for (int i = 1; i < 11; i++)
			{
				dragonframes.Add(new Bitmap("Drag (" + (i)+").bmp"));
				dragonframes[i - 1].MakeTransparent(Color.White);
			}
			xdragbull = this.x;
			ydragbull = this.y + 20;
		}
	}
}
