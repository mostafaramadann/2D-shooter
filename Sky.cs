using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multimedia_Project
{
	class Sky
	{

		public List<Bitmap> Skyframes = new List<Bitmap>();
		public Bitmap Currentframe;
		public int x;
		public int y;
		public Sky(int x ,int y,Bitmap skframe)
		{
			Skyframes.Add(skframe);
			
			this.x = x;
			this.y = y;
			
		}
	}
}
