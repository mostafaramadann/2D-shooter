using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{
	class Shop
	{
		public int x, y;
		public Bitmap ShopIm = new Bitmap("Shop.bmp");
		public Shop(int x,int y)
		{
			Color xx = ShopIm.GetPixel(2, 5);
			ShopIm.MakeTransparent(Color.FromArgb(255, 197, 234, 195));
			this.x = x;
			this.x += 80;
			this.y = y;
			this.y += 10;
			this.y -= ShopIm.Height;
		}
	}
}
