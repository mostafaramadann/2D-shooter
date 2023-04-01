using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{
	class SEnemy:Character
	{
		public int stimer;
		public bool shoot = false;
		public Ground g;
		public bool coin = false;
		public SEnemy():base(null)
		{
			stimer = 1;
			for (int i = 1; i < 11; i++)
			{
				this.WalkingFramesL.Add(new Bitmap("sel (" + i + ").bmp"));
				WalkingFramesL[i - 1].MakeTransparent(Color.Black);
				//WalkingFramesL[i - 1].MakeTransparent(Color.Black);
			}
			CurrentFrame = WalkingFramesL[1];
		}
	}
}
