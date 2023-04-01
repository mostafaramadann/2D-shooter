using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Multimedia_Project
{
	public class Character
	{
		public List<Bitmap> WalkingFramesR = new List<Bitmap>();
		public List<Bitmap> WalkingFramesL = new List<Bitmap>();
		public Bitmap CurrentFrame;
		public int FrameIndex;
		public bool left = false;
		public int x;
		public int y;
		public Character(Bitmap CurrentFrame)
		{
			if(CurrentFrame!=null)
			CurrentFrame.MakeTransparent(Color.Black);
			FrameIndex = 0;
			this.CurrentFrame = CurrentFrame;
		}
	}
}
