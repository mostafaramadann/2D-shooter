using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multimedia_Project
{
	class Bullet:Character
	{
		
		public Bullet(int x,int y,Bitmap currentframe) : base(currentframe)
		{
			this.x = x;
			this.y = y;
			this.FrameIndex = 0;
			for (int i = 0; i < 2; i++)
			{
				Bitmap pnn = new Bitmap("b" + (i+1) + ".bmp");
				pnn.MakeTransparent(Color.Black);
				this.WalkingFramesR.Add(pnn);
				pnn = new Bitmap("b" + (i + 1) + "L.bmp");
				pnn.MakeTransparent(Color.Black);
				this.WalkingFramesL.Add(pnn);
			}
		}

	}
}
