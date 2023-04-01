using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{
	class Plane
	{
		public Bitmap planeImage = new Bitmap("jet.bmp");
		public int x;
		public int y;
		public Plane(int x,int y)
		{
			this.x = x;
			this.y = y;
			planeImage.MakeTransparent(Color.Black);
		}

	}
}
