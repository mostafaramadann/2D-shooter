using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{
	class Shield
	{
		public Bitmap Shieldim = new Bitmap("Shield.bmp");
		public int x,y;
		public Shield()
		{
			Shieldim.MakeTransparent(Color.Black);
			
		}
	}
}
