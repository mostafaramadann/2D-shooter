using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multimedia_Project
{
	class Ground
	{
		public int x, y;
		public Bitmap Groundnim;
		public bool builtground = false;
		public Ground(int x, int y, Bitmap Groundim)
		{
			this.x = x;
			this.y = y;
			this.Groundnim = Groundim;
			this.Groundnim.MakeTransparent(Color.White);
		}
	}
}
