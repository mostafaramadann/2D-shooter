using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{

	class pausebutton
	{
		public List<Bitmap> pauseframes = new List<Bitmap>();
		public Bitmap currentframe = null;
		public pausebutton()
		{
			pauseframes.Add(new Bitmap("p1.bmp"));
			pauseframes.Add(new Bitmap("p2.bmp"));
			currentframe = pauseframes[0];
			
		}
	}
}
