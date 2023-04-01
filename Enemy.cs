using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{
	 class Enemy : Character
	{
		public List<Bitmap> CurrentList = new List<Bitmap>();
		public int Lives = 6;
		public Ground g;
		public bool coin = false;
		public int groundindex;
		public int downvar = 5;
		public bool reachedground = true;
		public Enemy():base(new Bitmap("el0.bmp"))
		{
			for (int i = 0; i < 16; i++)
			{
				WalkingFramesL.Add(new Bitmap("el" + i + ".bmp"));
				WalkingFramesL[i].MakeTransparent(Color.Black);
				WalkingFramesR.Add(new Bitmap("er" + i + ".bmp"));
				WalkingFramesR[i].MakeTransparent(Color.White);
			}
		}
	}
}
