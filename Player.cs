using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Multimedia_Project
{
	class Player:Character
	{
		public List<Bitmap> AirStrike = new List<Bitmap>();
		public List<Bitmap> AirStrikeL = new List<Bitmap>();
		public List<Bitmap> Shooting = new List<Bitmap>();
		public List<Bitmap> ShootingL = new List<Bitmap>();
		public List<Bitmap> jumplist = new List<Bitmap>();
		public List<Bitmap> jumplistl = new List<Bitmap>();
		public List<Enemy> Enemies;
		public bool[] shooting = new bool[1];
		public bool[] airstrike = new bool[1];
		public Bitmap LifesSprite = new Bitmap("life.bmp");
		public bool jump = false;
		public int lifes = 3;
		public int builddirection = 0;
		public int coincount = 0;
		public bool tjumpp = true;
		public int jumpvar = 4;
		public int tjump = 1;
		public int downvar = 2;
		public int previousyscroll=0;
		public bool reachedground = false;
		public bool shield = false;
		public int iy = 0;
		public Bitmap previousframe = null;
		public int jindex = 0;
		public List<Coin> coins;
		/*List<int[,]> integrs = new List<int[,]>();
		int[,] xxxx = new int[5,5];///2d array
		int[][] xxx = new int[4][];/// array of pointers that point to array of unallocated size*/
		public Player(Bitmap Current) : base(Current)
		{
			for (int i = 0; i < 30; i++)
			{
				this.AirStrike.Add(new Bitmap(i + "_BulkPicMe.bmp"));
				AirStrike[i].MakeTransparent(Color.Black);
			}
			for (int i = 0; i < 30; i++)
			{
				AirStrikeL.Add(new Bitmap(i + "L.bmp"));
				AirStrikeL[i].MakeTransparent(Color.Black);
			}
			for (int i = 0; i < 10; i++)
			{
				this.Shooting.Add(new Bitmap("s" + (i + 1) + ".bmp"));
				Shooting[i].MakeTransparent(Color.Black);
			}
			for (int i = 0; i < 10; i++)
			{
				this.ShootingL.Add(new Bitmap("s" + (i + 1) + "L.bmp"));
				ShootingL[i].MakeTransparent(Color.Black);
			}
			for (int i = 1; i < 7; i++)
			{
				this.jumplist.Add(new Bitmap("j" + (i) + ".bmp"));
				jumplistl.Add(new Bitmap("j" + (i) + "L.bmp"));
				jumplistl[i - 1].MakeTransparent(Color.Black);
				jumplist[i - 1].MakeTransparent(Color.Black);
			}
			LifesSprite.MakeTransparent(Color.White);
		}
		//////Showing frames of bullets and airstrike//////////
		public void showframes(List<Bitmap> plist, bool[] ff,SoundPlayer splayer,Level level)
		{
			this.CurrentFrame = plist[this.FrameIndex];
			if (this.FrameIndex + 1 < plist.Count)
				this.FrameIndex++;
			else
			{
				ff[0] = false;
				this.FrameIndex = 0;
				this.CurrentFrame = this.left ? this.WalkingFramesL[0] : this.WalkingFramesR[0];
				if (plist.Count == 10)
				{
					if (!Form1.purchasedRifle)
					{
						Bullet pnn = new Bullet(!this.left ? this.x + this.CurrentFrame.Width : this.x - this.CurrentFrame.Width, this.y + this.CurrentFrame.Height / 4, this.left ? new Bitmap("b1L.bmp") : new Bitmap("b1.bmp"));
						pnn.left = this.left ? true : false;
						level.pbullets.Add(pnn);
					}
					else
					{
						int xx = 0;
						for (int i = 0; i < 3; i++)
						{
							RifleBullet pnn = new RifleBullet(this.left, (!this.left ? this.x + this.CurrentFrame.Width : this.x - this.CurrentFrame.Width) +xx, this.y + this.CurrentFrame.Height / 4);
							if (this.left)
								xx -= 100;
							else
								xx += 100;
							level.rifleBullets.Add(pnn);
						}
					}
					
				}
				else
				{
					splayer.SoundLocation = "Jet.wav";
					splayer.Play();
				}
			}

		}
		public void HandleFrames(SoundPlayer splayer,Level level)
		{
			if (shooting[0] && !airstrike[0] && !this.left)
				showframes(Shooting, shooting,splayer,level);
			else if (shooting[0] && !airstrike[0] && this.left)
				showframes(this.ShootingL, shooting,splayer,level);
			else if (airstrike[0] && !shooting[0] && !this.left)
				showframes(this.AirStrike, airstrike,splayer,level);
			else if (airstrike[0] && !shooting[0] && this.left)
				showframes(this.AirStrikeL, airstrike,splayer,level);
			else if (shooting[0] && airstrike[0])
			{
				shooting[0] = false;
				airstrike[0] = false;
				this.CurrentFrame = WalkingFramesR[0];
				this.FrameIndex = 0;
			}
		}
		public void CheckifPlayerCollidedWithEnemy(int []xscroll)
		{
			for (int i = 0; i < Enemies.Count; i++)
			{
				if ((Enemies[i].left && this.x + this.CurrentFrame.Width - 70 >= Enemies[i].x && this.x <= Enemies[i].x + Enemies[i].CurrentList[Enemies[i].FrameIndex].Width
					&& Enemies[i].y <= this.y && Enemies[i].y + Enemies[i].CurrentList[Enemies[i].FrameIndex].Height >= this.y + this.CurrentFrame.Height)
					|| (!Enemies[i].left && Enemies[i].x + Enemies[i].CurrentList[Enemies[i].FrameIndex].Width >= this.x && Enemies[i].x + Enemies[i].CurrentList[Enemies[i].FrameIndex].Width <= this.x + this.CurrentFrame.Width - 70
					&& Enemies[i].y <= this.y && Enemies[i].y + Enemies[i].CurrentList[Enemies[i].FrameIndex].Height >= this.y + this.CurrentFrame.Height))
				{
					if (this.lifes - 1 == 0)
					{
						this.lifes--;
						//this = null;
						//MessageBox.Show("GameOver");
						break;
					}
					else
					{
						if (this != null)
						{
							this.lifes--;
							if (Enemies[i].left)
							{
								if (this.x >= 500)
								{

									this.x -= 150;
									if(xscroll[0]-150>0)
									xscroll[0] -= 150;
								}
							}
							else
							{
								this.x += 150;
								if(xscroll[0]+150<4350)
								xscroll[0] += 150;
							}
						}

					}
				}
			}
		}
		public void Jump(int[]yscroll,Level level,int scrollindex)
		{
			if (tjump % 8 == 0)
			{
				//player.jump = false;
				tjump = 1;
				tjumpp = false;
				jumpvar = 4;
			}
			else
			{
				if (tjumpp)
				{
					this.y -= 5 + jumpvar;
					if(yscroll[scrollindex]-jumpvar>0)
					yscroll[scrollindex] -=jumpvar;
					jumpvar += 3;
					tjump++;
					if (level != null)
					{
						if (CurrentFrame == WalkingFramesL[0] || CurrentFrame == WalkingFramesR[0])
							previousframe = CurrentFrame;
						if (previousframe == WalkingFramesR[0])
							this.CurrentFrame = new Bitmap("jj.bmp");
						else
							this.CurrentFrame = new Bitmap("jjL.bmp");
						CurrentFrame.MakeTransparent(Color.Black);
						if (jindex + 1 < 6)
							jindex++;
					}
				}
			}
		}
		public void CoinPick()
		{
			for (int i = coins.Count - 1; i >= 0; i--)
			{
				if (this.x + this.CurrentFrame.Width >= coins[i].x && this.x + this.CurrentFrame.Width <= coins[i].x + coins[i].coinframes.Width
					&& this.y < coins[i].y && this.y + this.CurrentFrame.Height + 10 >= coins[i].y + coins[i].coinframes.Height)
				{
					//MessageBox.Show("coins are"+this.coins+";");
					coins.RemoveAt(i);
					this.coincount++;
				}

			}
		}
		public void Fall(List<Ground>grounds,int []yscroll,int scrollindex)
		{
			for (int i = 0; i < grounds.Count; i++)
			{
				if ((!this.left && this.x + 5 >= grounds[i].x && this.x <= grounds[i].x + grounds[i].Groundnim.Width
						&& this.y + this.WalkingFramesL[0].Height-2>= grounds[i].y && this.y + this.WalkingFramesL[0].Height + 16 < grounds[i].y + grounds[i].Groundnim.Height) ||
						(this.left && this.x + this.WalkingFramesL[0].Width / 2 >= grounds[i].x && this.x + this.WalkingFramesL[0].Width / 2 <= grounds[i].x + grounds[i].Groundnim.Width
						&& this.y + this.WalkingFramesL[0].Height-2 >= grounds[i].y && this.y + this.WalkingFramesL[0].Height + 16 < grounds[i].y + grounds[i].Groundnim.Height))
				{
					if (this.jump && !tjumpp)
					{
						tjumpp = true;
						this.jump = false;
					if (previousframe != null&&(CurrentFrame!=WalkingFramesL[0]||CurrentFrame!=WalkingFramesR[0]))
						CurrentFrame = previousframe;
					}
					if (yscroll[scrollindex] > 0)
					{
						if (!Form1.level2)
						{ 
							yscroll[scrollindex] = y - 470;
						}
						else
						{
							if(scrollindex==1)
							yscroll[scrollindex] = y - 620;
							else
							yscroll[scrollindex] = y - (470 / 2);
						}
					}
					reachedground = true;
					jindex = 0;
					downvar = 5;
				}
			}
			if (tjumpp && !this.jump)
			{
				if (!reachedground)
				{
					this.y += downvar;
					downvar += 2;
					yscroll[scrollindex] += downvar - 6;
				}
				else
				{
					reachedground = false;
				}
			}
			else if (!tjumpp && this.jump)
			{
				this.y += downvar;
				downvar += 2;
				yscroll[scrollindex] += downvar-6;
			}
		}
	}
}
