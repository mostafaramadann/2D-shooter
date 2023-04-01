using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{
	class Level
	{
		//////////////Level Objects//////////////////////////////
		public List<Ground> grounds = new List<Ground>();
		public List<Sky> skies = new List<Sky>();
		public List<Bullet> pbullets = new List<Bullet>();
		public List<SBullet> sBullets = new List<SBullet>();
		public List<Enemy> Enemies = new List<Enemy>();
		public List<SEnemy> SEnemies = new List<SEnemy>();
		public List<Rocket> rockets = new List<Rocket>();
		public List<Explosion> explosions = new List<Explosion>();
		public Car truck;
		public List<Coin> coins = new List<Coin>();
		public List<Barrel> Barrels = new List<Barrel>();
		public List<RifleBullet> rifleBullets = new List<RifleBullet>();
		public Plane plane;
		public Shield shield;
		public Indicator indicator;
		public Shop shop;
		/////////////////////////////////////////////////////
		public screensource legend;
		public Bitmap Legend = new Bitmap(50, 50);
		public Graphics G5;
		public pausebutton pbutton;
		public screensource sc;
		public Bitmap Screensource;
		public Bitmap coindisp = new Bitmap("coin (1).bmp");
		public Bitmap Checkpoint = new Bitmap("Checkpoint.bmp");
		public Bitmap StartGame = new Bitmap("StartGame.bmp");
		public bool drawinlegend = false;
		public bool explosion = false;
		public int amount = 0;
		public int Endoflevel = 2800;
		public int Endoflevelh = 1125;
		public bool exppp = false;
		public bool ShowLegend = true;
		public bool intitle = true;
		public int texplosion = 1;
		public int[] xscroll = new int[1];
		public int[] yscroll = new int[1];
		public int checkpoint = 1500;
		public int checkpointy;
		public bool death = false;
		public Level()
		{
			shield = new Shield();
		}
		public void MoveSBullets(Player player)
		{
			if (player != null)
			{
				for (int i = this.sBullets.Count - 1; i >= 0; i--)
				{
					////Check Enemy Direction
					if ((this.sBullets[i].x <= player.x + player.CurrentFrame.Width && this.sBullets[i].x >= player.x
						&& this.sBullets[i].y >= player.y && this.sBullets[i].y + this.sBullets[i].CurrentFrame.Height <= player.y + player.CurrentFrame.Height)
						|| (this.sBullets[i].x <= player.x + player.CurrentFrame.Width && this.sBullets[i].x >= player.x
						&& this.sBullets[i].y + this.sBullets[i].CurrentFrame.Height >= player.y && this.sBullets[i].y + this.sBullets[i].CurrentFrame.Height <= player.y + player.CurrentFrame.Height))
					{
						this.sBullets.RemoveAt(i);
						player.lifes--;
					}
					else if ((this.sBullets[i].x + this.sBullets[i].CurrentFrame.Width < player.x) || (this.sBullets[i].x <= this.sBullets[i].Destination.X))
						this.sBullets.RemoveAt(i);
					else
					{
						if (this.sBullets[i].x > this.sBullets[i].Destination.X)
							this.sBullets[i].x -= (this.sBullets[i].Factorx * 30);
						/*else
							this.sBullets[i].x += (this.sBullets[i].Factorx * 20);*/
						//	if(this.sBullets[i].y<this.sBullets[i].Destination.Y&&this.sBullets[i].Factory!=1)
						if (this.sBullets[i].y > this.sBullets[i].Destination.Y)
							this.sBullets[i].y -= (this.sBullets[i].Factory * 10);
						else if (this.sBullets[i].y < this.sBullets[i].Destination.Y)
							this.sBullets[i].y += (this.sBullets[i].Factory * 10);
						else
						{
							this.sBullets[i].y += 0;
						}
					}
				}
			}
		}
		public void IncrementBulletFrames()
		{
			//////////////Handling Bullets Frames/////////////////////
			for (int i = this.pbullets.Count - 1; i >= 0; i--)
			{
				if (this.pbullets[i].FrameIndex + 1 < 2)
					this.pbullets[i].FrameIndex++;
				else
					this.pbullets[i].FrameIndex = 0;
				if (this.pbullets[i].left)
				{
					this.pbullets[i].CurrentFrame = this.pbullets[i].WalkingFramesL[this.pbullets[i].FrameIndex];
					this.pbullets[i].x -= 50;

				}
				else
				{
					this.pbullets[i].CurrentFrame = this.pbullets[i].WalkingFramesR[this.pbullets[i].FrameIndex];
					this.pbullets[i].x += 50;
				}
				if (this.pbullets[i].left && this.pbullets[i].x < 0)
					this.pbullets.RemoveAt(i);
				else if (!this.pbullets[i].left && this.pbullets[i].x > Endoflevel)
					this.pbullets.RemoveAt(i);
			}
			///////////////////////////////////////////////////////////////////////////////////////

		}
		public void CreateRockets(int time,int []rocketsSpawnFrequency)
		{
			if (this.plane != null)
			{
				if (this.plane.x < 50)
					this.plane = null;
				else
				{
					if (time % rocketsSpawnFrequency[0] == 0)
					{
						Rocket x = new Rocket(this.plane.x, this.plane.y + this.plane.planeImage.Height / 2);
						this.rockets.Add(x);
					}
					this.plane.x -= 50;
				}
			}
		}
		public void IncrementExplosionFrames()
		{
			for (int i = this.explosions.Count - 1; i >= 0; i--)
			{
				if (this.explosions[i].frameIndex + 1 < this.explosions[i].explosionframes.Count)
					this.explosions[i].frameIndex++;
				else
					this.explosions.RemoveAt(i);
			}
		}
		public void MoveTruckRocket(Player player)
		{
			/*if (truck.crocket != null)
			{
				///////////e power x ama ttl3 we el asymptotes ama tnzl
				/////////////Mehtaga Graph el x ama tzeed ely y tzeed le7ad ama el x twsl le level ely y feh el maximum;
				if ((truck.destdirleft && truck.crocket.x <= truck.Destination.X)||(!truck.destdirleft && truck.crocket.x >= truck.Destination.X))
				truck.crocket = null;
				if (truck.crocket != null)
				{
					if (truck.crocket.y <= truck.Destination.Y)
						truck.crocket.y += 10;
					else if (truck.crocket.y > truck.Destination.Y)
						truck.crocket.y -= 10;
					if (truck.destdirleft && truck.crocket.x > truck.Destination.X)
						truck.crocket.x -= 10;
					else if (!truck.destdirleft && truck.crocket.x < truck.Destination.X)
						truck.crocket.x += 10;
					if ((truck.destdirleft && truck.crocket.x <= truck.p1.X && truck.crocket.y <= truck.p1.Y) || (!truck.destdirleft && truck.crocket.x >= truck.p1.X && truck.crocket.y <= truck.p1.Y))
						truck.Destination = truck.p2;
				}
			}
			else
			{
				if (truck.left)
				{
					truck.p1 = new Point(truck.x - 80, truck.y - 80);
					truck.p2 = new Point(truck.x - 20, truck.y - 20);
					truck.crocket = new Rocket(truck.x - 20, truck.y);
					truck.crocket.RocketImage = new Bitmap("CRocketL.bmp");
					truck.crocket.RocketImage.MakeTransparent(Color.Black);
				}
				else
				{
					truck.p1 = new Point(truck.x + 110, truck.y - 80);
					truck.p2 = new Point(truck.x + 50, truck.y - 20);
					truck.crocket = new Rocket(truck.x + 50, truck.y);
					truck.crocket.RocketImage = new Bitmap("CRocket.bmp");
					truck.crocket.RocketImage.MakeTransparent(Color.Black);
				}
				truck.p3 = new Point(player.x, player.y + 50);
				truck.Destination = truck.p1;
				if (player.x < truck.x)
					truck.destdirleft = true;
				else
					truck.destdirleft = false;
			}*/
		}
		public void MoveRifleBullets()
		{
			for(int i= Enemies.Count-1; i>=0;i--)
			{
				for (int k = rifleBullets.Count-1; k >=0; k--)
				{
					if (rifleBullets[k].x >= Enemies[i].x &&
						rifleBullets[k].x + rifleBullets[k].Current.Width <= Enemies[i].x + Enemies[i].CurrentList[Enemies[i].FrameIndex].Width
						&& rifleBullets[k].y>=Enemies[i].y&&rifleBullets[k].y+rifleBullets[k].Current.Height<=Enemies[i].y + Enemies[i].CurrentList[Enemies[i].FrameIndex].Height)
					{
						Enemies.RemoveAt(i);
						rifleBullets.RemoveAt(k);
						break;
					}

				}
			}
			for (int i = SEnemies.Count - 1; i >= 0; i--)
			{
				for (int k = rifleBullets.Count - 1; k >= 0; k--)
				{
					if (rifleBullets[k].x >= SEnemies[i].x &&
						rifleBullets[k].x + rifleBullets[k].Current.Width <= SEnemies[i].x + SEnemies[i].CurrentFrame.Width
						&& rifleBullets[k].y >= SEnemies[i].y && rifleBullets[k].y + rifleBullets[k].Current.Height <= SEnemies[i].y + SEnemies[i].CurrentFrame.Height)
					{
						SEnemies.RemoveAt(i);
						rifleBullets.RemoveAt(k);
						break;
					}
				}
			}
		
				for (int k = grounds.Count - 1; k >= 0; k--)
				{
				   for (int i = rifleBullets.Count - 1; i >= 0; i--)
				   {
						if (rifleBullets[i].x >= grounds[k].x &&
							rifleBullets[i].x + rifleBullets[i].Current.Width <= grounds[k].x +grounds[k].Groundnim.Width
							&& rifleBullets[i].y >= grounds[k].y && rifleBullets[i].y + rifleBullets[i].Current.Height <= grounds[k].y + grounds[k].Groundnim.Height)
						{
							rifleBullets.RemoveAt(i);
							break;
						}
				   }
			    }
			for (int i = rifleBullets.Count-1; i >=0; i--)
			{
				if (rifleBullets[i].left)
				{
					if (rifleBullets[i].x < 0)
						rifleBullets.RemoveAt(i);
					else
						rifleBullets[i].x -= 30;
				}
				else
				{
					if (rifleBullets[i].x > Endoflevel)
						rifleBullets.RemoveAt(i);
					else
					rifleBullets[i].x += 30;
				}
			}
		}
		public void DrawScene(Graphics G,Size ClientSize,Player player)
		{
			G.Clear(Color.White);
			if (Form1.pause == false)
			{
				for (int i = 0; i < this.skies.Count; i++)
				{
					if (this.skies[i].x >= ClientSize.Width + xscroll[0])
						break;
					G.DrawImage(this.skies[i].Currentframe, this.skies[i].x, this.skies[i].y);
				}
				for (int i = 0; i < this.grounds.Count; i++)
				{
					G.DrawImage(this.grounds[i].Groundnim, this.grounds[i].x, this.grounds[i].y);
				}
				for (int i = 0; i < this.rockets.Count; i++)
				{
					G.DrawImage(this.rockets[i].RocketImage, this.rockets[i].x, this.rockets[i].y);
				}
				for (int i = 0; i < this.explosions.Count; i++)
				{
					G.DrawImage(this.explosions[i].explosionframes[this.explosions[i].frameIndex], this.explosions[i].x, this.explosions[i].y);
				}
				for (int i = 0; i < this.coins.Count; i++)
				{
					G.DrawImage(this.coins[i].coinframes, this.coins[i].x, this.coins[i].y);
				}
				for (int i = 0; i < this.Barrels.Count; i++)
				{
					G.DrawImage(this.Barrels[i].BarrelIm, this.Barrels[i].x, this.Barrels[i].y);
				}
				for (int i = 0; i < this.pbullets.Count; i++)
				{
					G.DrawImage(this.pbullets[i].CurrentFrame, this.pbullets[i].x, this.pbullets[i].y);
				}
				for (int i = 0; i < this.rifleBullets.Count; i++)
				{
					G.DrawImage(rifleBullets[i].Current, rifleBullets[i].x, rifleBullets[i].y);
				}
				for (int i = 0; i < this.sBullets.Count; i++)
				{
					G.DrawImage(this.sBullets[i].CurrentFrame, this.sBullets[i].x, this.sBullets[i].y);
				}
				for (int i = 0; i < this.Enemies.Count; i++)
				{
					G.DrawImage(this.Enemies[i].CurrentList[this.Enemies[i].FrameIndex], this.Enemies[i].x, this.Enemies[i].y);
				}
				for (int i = 0; i < this.SEnemies.Count; i++)
				{
					G.DrawImage(this.SEnemies[i].CurrentFrame, this.SEnemies[i].x, this.SEnemies[i].y);
				}
				/*G.DrawImage(truck.CurrentFrame, truck.x, truck.y);
				if(truck.crocket!=null)
				G.DrawImage(truck.crocket.RocketImage, truck.crocket.x, truck.crocket.y);*/
				G.DrawImage(shop.ShopIm, shop.x, shop.y);
				Checkpoint.MakeTransparent(Color.White);
				checkpointy = grounds[0].y - 50;
				checkpoint = grounds[8].x+100;
				G.DrawImage(Checkpoint, checkpoint, checkpointy);
				G.DrawImage(coindisp, ClientSize.Width - 50 - coindisp.Width + xscroll[0], 0 + this.amount + yscroll[0]);
				int pcoins = 0;
				if (player != null)
					pcoins = player.coincount;
				G.DrawString("" + pcoins, new Font(FontFamily.Families[1], 22), Brushes.Black, ClientSize.Width - 50 + xscroll[0], 5+ this.amount + yscroll[0]);
				if (this.plane != null)
					G.DrawImage(this.plane.planeImage, this.plane.x, this.plane.y);
				if (player != null)
				{
					if (player.lifes == 1)
						G.DrawImage(player.LifesSprite, 0 + xscroll[0], 0 + this.amount + yscroll[0]);
					else if (player.lifes == 2)
					{
						G.DrawImage(player.LifesSprite, 0 + xscroll[0], 0 + this.amount + yscroll[0]);
						G.DrawImage(player.LifesSprite, 42 + xscroll[0], 0 + this.amount + yscroll[0]);
					}
					else if (player.lifes == 3)
					{
						G.DrawImage(player.LifesSprite, 0 + xscroll[0], 0 + this.amount + yscroll[0]);
						G.DrawImage(player.LifesSprite, 42 + xscroll[0], 0 + this.amount + yscroll[0]);
						G.DrawImage(player.LifesSprite, 84 + xscroll[0], 0 + this.amount + yscroll[0]);
					}
					G.DrawImage(player.CurrentFrame, player.x, player.y);
					if (player.jump)
					{
						if (!player.left)
							G.DrawImage(player.jumplist[player.jindex], player.x + 5, player.y + player.WalkingFramesL[0].Height - 25);
						else
							G.DrawImage(player.jumplistl[player.jindex], player.x + 40, player.y + player.WalkingFramesL[0].Height - 25);
					}

				}
				if (!intitle && ShowLegend)
				{
					this.legend = new screensource(new Rectangle(0, 0, Endoflevel, Endoflevelh), new Rectangle(350 + xscroll[0], 0 + 10 + yscroll[0], ClientSize.Width / 2, ClientSize.Height / 4));
					G.DrawRectangle(new Pen(Brushes.Black, 5), 350 + xscroll[0], 0 + 10 + yscroll[0], ClientSize.Width / 2, ClientSize.Height / 4);
					G.DrawImage(this.Legend, this.legend.rD, this.legend.rs, GraphicsUnit.Pixel);

				}
				sc = new screensource(new Rectangle(0 + xscroll[0], 0 + this.amount + yscroll[0], ClientSize.Width, ClientSize.Height), new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
			}
			else
			{
				G.Clear(Color.White);
				G.DrawImage(pbutton.currentframe, ClientSize.Width/2+xscroll[0], ClientSize.Height/2+yscroll[0]);
			}
		}
		public void CreateEnvironmentlevel1(Size ClientSize)
		{
			int x = -5;
			int yground = ClientSize.Height /2+85;
			int ctground = 1;

			for (int i = 0; i < 15; i++)
			{
				Ground pnn = new Ground(x, yground, new Bitmap("Earth.bmp"));
				if (i == 14)
				{
					yground += 180;
					pnn = new Ground(x, yground, new Bitmap("Earth.bmp"));
				}
				else if (ctground % 4 == 0)
					pnn = new Ground(x, yground, new Bitmap("Earth.bmp"));
				else
					pnn = new Ground(x, ClientSize.Height / 2 + 150, new Bitmap("Earth.bmp"));
				grounds.Add(pnn);
				x += pnn.Groundnim.Width;
				ctground++;
			}
			for (int i = 0; i < 15; i++)
			{
				Ground pnn;
				if (i == 2)
					yground = 900;
				else if (i == 1)
					yground = 800;
				else if (i == 0)
				{
					yground = 750;
					shop = new Shop(x-100, yground);
				}
				else
					yground = 1000;
				pnn = new Ground(x, yground, new Bitmap("Earth.bmp"));
				x -= pnn.Groundnim.Width;
				grounds.Add(pnn);

			}
			/*x = grounds[7].x-grounds[0].Groundnim.Width;
			int y = grounds[7].y - grounds[0].Groundnim.Height*4;
			for (int i = 0; i < 6; i++)
			{
				Ground pnn;
				pnn = new Ground(x, y, new Bitmap("Earth.bmp"));
				x -= pnn.Groundnim.Width;
				grounds.Add(pnn);
			}*/
			int x2 = 0;
			int y = 0;
			for (int i = 0; i < 15; i++)
			{
				Sky pnn = new Sky(x2, y,new Bitmap("sk1.bmp"));
				pnn.Currentframe = pnn.Skyframes[0];
				skies.Add(pnn);
				y += pnn.Currentframe.Height;
				pnn = new Sky(x2, y, new Bitmap("sk1.bmp"));
				pnn.Currentframe = pnn.Skyframes[0];
				skies.Add(pnn);
				y += pnn.Currentframe.Height;
				pnn = new Sky(x2, y, new Bitmap("sk1.bmp"));
				pnn.Currentframe = pnn.Skyframes[0];
				skies.Add(pnn);
				y += pnn.Currentframe.Height;
				pnn = new Sky(x2, y, new Bitmap("sk1.bmp"));
				pnn.Currentframe = pnn.Skyframes[0];
				skies.Add(pnn);
				y = 0;
				x2 += pnn.Currentframe.Width;
			}
			Random RR = new Random();
			int random = 0;
			//int random2 = 0;
			for (int i = 0; i < 10; i++)
			{
				Barrel pnn;
				random = RR.Next(0, grounds.Count);
				pnn = new Barrel(grounds[random].x, grounds[random].y - 38);
				Barrels.Add(pnn);
			}
			random = 0;
			while (grounds[random].y < grounds[15].y)
			{
				random = RR.Next(0, grounds.Count);
			}
			//truck = new Car(grounds[random].x, grounds[random].y-110);
			//truck.CurrentFrame = truck.WalkingFramesR[0];
			legend = new screensource(new Rectangle(0, 0, Endoflevel, Endoflevelh), new Rectangle(350, 0, ClientSize.Width / 2, ClientSize.Height / 4));
			G5 = Graphics.FromImage(Legend);
			DrawInLegend(G5);
		}
		public void EnemyFall()
		{
			for (int i = this.Enemies.Count - 1; i >= 0; i--)
			{
				if ((!this.Enemies[i].left && this.Enemies[i].x + this.Enemies[i].CurrentList[this.Enemies[i].FrameIndex].Width / 2 - 30 >= this.Enemies[i].g.x && this.Enemies[i].x + this.Enemies[i].CurrentList[this.Enemies[i].FrameIndex].Width / 2 - 30 <= this.Enemies[i].g.x + this.Enemies[i].g.Groundnim.Width && this.Enemies[i].groundindex != -1)
					|| (this.Enemies[i].left && this.Enemies[i].x + this.Enemies[i].CurrentList[this.Enemies[i].FrameIndex].Width / 2 + 30 >= this.Enemies[i].g.x && this.Enemies[i].x - this.Enemies[i].CurrentList[this.Enemies[i].FrameIndex].Width / 2 + 30 <= this.Enemies[i].g.x + this.Enemies[i].g.Groundnim.Width && this.Enemies[i].groundindex != -1))
				{
					this.Enemies[i].reachedground = true;
				}
				else
				{
					if (this.Enemies[i].y > Endoflevelh)
						this.Enemies.RemoveAt(i);
					else
					{
					this.Enemies[i].y += 10;
					// Enemies[i].reachedground = false;
					}
				}
			}
		}
		public void ChangeEnemyGround()
		{
			for (int i = 0; i < this.Enemies.Count; i++)
			{

				for (int k = 0; k < this.grounds.Count; k++)
				{
					if ((!this.Enemies[i].left && this.Enemies[i].x + this.Enemies[i].CurrentList[this.Enemies[i].FrameIndex].Width / 2 - 30 >= this.grounds[k].x && this.Enemies[i].x + this.Enemies[i].CurrentList[this.Enemies[i].FrameIndex].Width / 2 - 30 <= this.grounds[k].x + this.grounds[k].Groundnim.Width
						&& this.Enemies[i].y < this.grounds[k].y && this.Enemies[i].y + this.Enemies[i].CurrentList[this.Enemies[i].FrameIndex].Height - 17 >= this.grounds[k].y
						&& this.Enemies[i].y + this.Enemies[i].CurrentList[this.Enemies[i].FrameIndex].Height + 23 < this.grounds[k].y + this.grounds[k].Groundnim.Height && this.Enemies[i].reachedground)
						|| (this.Enemies[i].left && this.Enemies[i].x + this.Enemies[i].CurrentList[this.Enemies[i].FrameIndex].Width / 2 - 30 >= this.grounds[k].x && this.Enemies[i].x - this.Enemies[i].CurrentList[this.Enemies[i].FrameIndex].Width / 2 + 30 <= this.grounds[k].x + this.grounds[k].Groundnim.Width
						&& this.Enemies[i].y < this.grounds[k].y && this.Enemies[i].y + this.Enemies[i].CurrentList[this.Enemies[i].FrameIndex].Height - 17 >= this.grounds[k].y
						&& this.Enemies[i].y + this.Enemies[i].CurrentList[this.Enemies[i].FrameIndex].Height + 23 < this.grounds[k].y + this.grounds[k].Groundnim.Height && this.Enemies[i].reachedground))
					{
						this.Enemies[i].g = this.grounds[k];
						this.Enemies[i].groundindex = k;
					}
					else if (this.Enemies[i].y > this.Enemies[i].g.y)
					{
						this.Enemies[i].groundindex = -1;
						this.Enemies[i].reachedground = true;
					}
				}
			}
		}
		public void CreateSEnemy(Player player)
		{
			Random RR = new Random();
			int randomGround = RR.Next(0, this.grounds.Count);
			while (this.grounds[randomGround].y == this.grounds[0].y || player.x > this.grounds[randomGround].x)
			{
				randomGround = RR.Next(0, this.grounds.Count);
			}
			SEnemy sen = new SEnemy();
			sen.x = this.grounds[randomGround].x;
			sen.y = this.grounds[randomGround].y - sen.WalkingFramesL[0].Height + 12;
			sen.g = this.grounds[randomGround];
			this.SEnemies.Add(sen);
		}
		public void CollideBulletsWithGround()
		{
			for (int i = this.pbullets.Count - 1; i >= 0; i--)
			{
				for (int k = this.grounds.Count - 1; k >= 0; k--)
				{
					if (!this.pbullets[i].left && this.pbullets[i].x + this.pbullets[i].CurrentFrame.Width >= this.grounds[k].x && this.pbullets[i].x + this.pbullets[i].CurrentFrame.Width < this.grounds[k].x + this.grounds[k].Groundnim.Width
						&& this.pbullets[i].y >= this.grounds[k].y && this.pbullets[i].y + this.pbullets[i].CurrentFrame.Height <= this.grounds[k].y + this.grounds[k].Groundnim.Height)
					{
						this.pbullets.RemoveAt(i);
						break;
					}
				}
			}
		}
		public void CollideRocketsWithBarrels()
		{
			for (int i = 0; i < this.rockets.Count; i++)
			{
				for (int k = this.Barrels.Count - 1; k >= 0; k--)
				{
					if (((this.rockets[i].x + this.rockets[i].RocketImage.Width >= this.Barrels[k].x && this.rockets[i].x + this.rockets[i].RocketImage.Width <= this.Barrels[k].x + this.Barrels[k].BarrelIm.Width) || (this.rockets[i].x >= this.Barrels[k].x && this.rockets[i].x <= this.Barrels[k].x + this.Barrels[k].BarrelIm.Width) || (this.rockets[i].x >= this.Barrels[k].x && this.rockets[i].x + this.rockets[i].RocketImage.Width <= this.Barrels[k].x + this.Barrels[k].BarrelIm.Width)
						|| (this.rockets[i].x - this.rockets[i].RocketImage.Width <= this.Barrels[k].x + this.Barrels[k].BarrelIm.Width && this.rockets[i].x - this.rockets[i].RocketImage.Width >= this.Barrels[k].x))
						  && this.rockets[i].y + this.rockets[i].RocketImage.Height >= this.Barrels[k].y - 10 && this.rockets[i].y + this.rockets[i].RocketImage.Height <= this.Barrels[k].y + this.Barrels[k].BarrelIm.Height - 10)
					{
						this.Barrels.RemoveAt(k);
						break;
					}
				}
			}
		}
		public void MoveRockets()
		{
			for (int i = 0; i < this.rockets.Count; i++)
			{
			 this.rockets[i].y += 15;
			}
		}
		public void CollideRocketsWithEnemies()
		{
			///////////////////////Checking if level.rockets collided with an enemy and creating level.level.explosions///////////////////
			for (int i = this.rockets.Count - 1; i >= 0; i--)
			{
				for (int k = this.Enemies.Count - 1; k >= 0; k--)
				{
					if ((this.rockets[i].x + this.rockets[i].RocketImage.Width >= this.Enemies[k].x || this.rockets[i].x + this.rockets[i].RocketImage.Width >= this.Enemies[k].x + 5 || this.rockets[i].x + this.rockets[i].RocketImage.Width >= this.Enemies[k].x - 5) && this.rockets[i].x + this.rockets[i].RocketImage.Width <= this.Enemies[k].x + this.Enemies[k].CurrentList[this.Enemies[k].FrameIndex].Width
						&& this.rockets[i].y + this.rockets[i].RocketImage.Height >= this.Enemies[k].y && this.rockets[i].y + this.rockets[i].RocketImage.Height <= this.Enemies[k].y + this.Enemies[k].CurrentList[this.Enemies[k].FrameIndex].Height)
					{

						Explosion exp = new Explosion(this.Enemies[k].x, this.Enemies[k].g.y - 134);
						this.rockets.RemoveAt(i);
						if (this.Enemies[k].coin)
						{
							Coin cc = new Coin(this.Enemies[k].x, this.Enemies[k].y + this.Enemies[k].CurrentList[this.Enemies[k].FrameIndex].Height / 2 + 2);
							this.coins.Add(cc);
						}
						this.Enemies.RemoveAt(k);
						this.explosions.Add(exp);
						break;

					}
				}
			}
			//////////////////////////////////////////////////////////////////////////////////////////////////////////
			/////////////////////////////////Check if this.rockets collided with SEnenmy//////////////////////////////////
			for (int i = this.rockets.Count - 1; i >= 0; i--)
			{
				for (int k = this.SEnemies.Count - 1; k >= 0; k--)
				{
					if ((this.rockets[i].x + this.rockets[i].RocketImage.Width >= this.SEnemies[k].x || this.rockets[i].x + this.rockets[i].RocketImage.Width >= this.SEnemies[k].x + 5 || this.rockets[i].x + this.rockets[i].RocketImage.Width >= this.SEnemies[k].x - 5 || this.rockets[i].x >= this.SEnemies[k].x || this.rockets[i].x + 10 >= this.SEnemies[k].x) && this.rockets[i].x + this.rockets[i].RocketImage.Width <= this.SEnemies[k].x + this.SEnemies[k].CurrentFrame.Width
						&& this.rockets[i].y + this.rockets[i].RocketImage.Height >= this.SEnemies[k].y && this.rockets[i].y + this.rockets[i].RocketImage.Height <= this.SEnemies[k].y + this.SEnemies[k].CurrentFrame.Height)
					{

						Explosion exp = new Explosion(this.SEnemies[k].x, this.SEnemies[k].g.y -134);
						this.rockets.RemoveAt(i);
						if (this.SEnemies[k].coin)
						{
							Coin cc = new Coin(this.SEnemies[k].x, this.SEnemies[k].y + this.SEnemies[k].CurrentFrame.Height / 2 + 2);
							this.coins.Add(cc);
						}
						this.SEnemies.RemoveAt(k);
						this.explosions.Add(exp);
						break;
					}
				}
			}
			///////////////////////////////////////////////////////////////////////////////////////////////////////
		}
		public void CollideRocketsWithGround()
		{
			for (int i = this.rockets.Count - 1; i >= 0; i--)
			{
				for (int k = 0; k < this.grounds.Count; k++)
				{
					if (((this.rockets[i].x > this.grounds[k].x) || (this.rockets[i].x + this.rockets[i].RocketImage.Width > this.grounds[k].x)) && this.rockets[i].x + this.rockets[i].RocketImage.Width <= this.grounds[k].x + this.grounds[k].Groundnim.Width
						&& this.rockets[i].y + this.rockets[i].RocketImage.Height >= this.grounds[k].y && this.rockets[i].y + this.rockets[i].RocketImage.Height < this.grounds[k].y + this.grounds[k].Groundnim.Height)
					{
						Explosion exp = new Explosion(this.rockets[i].x - this.rockets[i].RocketImage.Width / 2, this.grounds[k].y - 134);
						this.rockets.RemoveAt(i);
						this.explosions.Add(exp);
						explosion = true;
						amount = 5;
						exppp = true;
						//DrawScene(G3);
						break;
					}

				}
			}
		}
		public void ShootBarrels()
		{
			for (int i = this.pbullets.Count - 1; i >= 0; i--)
			{
				for (int k = this.Barrels.Count - 1; k >= 0; k--)
				{
					////////////leftside mosh sha8allaa//////
					if ((!this.pbullets[i].left && this.pbullets[i].x + this.pbullets[i].CurrentFrame.Width >= this.Barrels[k].x && this.pbullets[i].x + this.pbullets[i].CurrentFrame.Width < this.Barrels[k].x + this.Barrels[k].BarrelIm.Width
						&& this.pbullets[i].y + 5 >= this.Barrels[k].y && this.pbullets[i].y + 5 + this.pbullets[i].CurrentFrame.Height <= this.Barrels[k].y + this.Barrels[k].BarrelIm.Height
						) || (this.pbullets[i].left && this.pbullets[i].x > this.Barrels[k].x + this.Barrels[k].BarrelIm.Width && this.pbullets[i].x < this.Barrels[k].x + Barrels[k].BarrelIm.Width
						&& this.pbullets[i].y +5 >= this.Barrels[k].y && this.pbullets[i].y + 5 + this.pbullets[i].CurrentFrame.Height <= this.Barrels[k].y + this.Barrels[k].BarrelIm.Height))
					{
						this.Barrels.RemoveAt(k);
						pbullets.RemoveAt(i);
						break;
					}
				}
			}
		}
		public void ShootEnemeies()
		{
			for (int i = this.pbullets.Count - 1; i >= 0; i--)
			{
				for (int k = this.Enemies.Count - 1; k >= 0; k--)
				{

					////////////////////////////Checking if Bullet hitted the enemy in both directions left And Right////////////////////////////////////
					if ((this.pbullets[i].x + this.pbullets[i].CurrentFrame.Width >= this.Enemies[k].x && this.pbullets[i].x + this.pbullets[i].CurrentFrame.Width <= this.Enemies[k].x + this.Enemies[k].CurrentList[this.Enemies[k].FrameIndex].Width
						&& this.pbullets[i].y >= this.Enemies[k].y && this.pbullets[i].y <= this.Enemies[k].y + this.Enemies[k].CurrentList[this.Enemies[k].FrameIndex].Height && !this.pbullets[i].left
						) || (this.Enemies[k].x <= this.pbullets[i].x && this.Enemies[k].x + this.Enemies[k].CurrentList[this.Enemies[k].FrameIndex].Width >= this.pbullets[i].x
						&& this.pbullets[i].y >= this.Enemies[k].y && this.pbullets[i].y <= this.Enemies[k].y + this.Enemies[k].CurrentList[this.Enemies[k].FrameIndex].Height && this.pbullets[i].left))
					{
						if (this.Enemies[k].Lives - 1 == 0)
						{
							if (this.Enemies[k].coin)
							{
								Coin cc = new Coin(this.Enemies[k].x, this.Enemies[k].y + this.Enemies[k].CurrentList[this.Enemies[k].FrameIndex].Height / 2 + 5);
								this.coins.Add(cc);
							}
							this.Enemies.RemoveAt(k);

						}
						else
						{
							if (!this.pbullets[i].left)
								this.Enemies[k].x += 40;
							else
								this.Enemies[k].x -= 40;
							this.Enemies[k].Lives--;
						}
						this.pbullets.RemoveAt(i);
						break;
					}
				}
			}
			////////////////////////////////////////////////////////////////////////////////
			///////////////Shooting Enemy2///////////////////////////////////////////////////
			for (int i = this.pbullets.Count - 1; i >= 0; i--)
			{
				for (int k = this.SEnemies.Count - 1; k >= 0; k--)
				{
					if ((!this.pbullets[i].left && this.pbullets[i].x >= this.SEnemies[k].x && this.pbullets[i].x + this.pbullets[i].CurrentFrame.Width <= this.SEnemies[k].x + this.SEnemies[k].CurrentFrame.Width
						&& this.pbullets[i].y >= this.SEnemies[k].y && this.pbullets[i].y + this.pbullets[i].CurrentFrame.Height <= this.SEnemies[k].y + this.SEnemies[k].CurrentFrame.Height)
						||(this.pbullets[i].left&&pbullets[i].x<=SEnemies[k].x+SEnemies[k].CurrentFrame.Width&&this.pbullets[i].x+this.pbullets[i].CurrentFrame.Width>=SEnemies[k].x
						&& this.pbullets[i].y >= this.SEnemies[k].y && this.pbullets[i].y + this.pbullets[i].CurrentFrame.Height <= this.SEnemies[k].y + this.SEnemies[k].CurrentFrame.Height))
					{
						this.pbullets.RemoveAt(i);
						this.SEnemies.RemoveAt(k);
						break;
					}
				}
			}
			/////////////////////////////////////////////////////////////////////////////////
		}
		public void MoveEnemies(Player player)
		{

			//////////////////Moving Enemy&& Frame Handling///////////
			for (int i = 0; i < this.Enemies.Count; i++)
			{
				if (this.Enemies[i].left)
					this.Enemies[i].x -= 5;
				else
					this.Enemies[i].x += 5;
				if (this.Enemies[i].FrameIndex + 1 < this.Enemies[i].CurrentList.Count)
					this.Enemies[i].FrameIndex++;
				else
					this.Enemies[i].FrameIndex = 0;
			}
			for (int i = 0; i < this.SEnemies.Count; i++)
			{
				if(SEnemies[i].x<player.x)
				{
					this.SEnemies[i].FrameIndex = 0;
					this.SEnemies[i].CurrentFrame = this.SEnemies[i].WalkingFramesL[1];
					this.SEnemies[i].shoot = false;
				}
				else if (this.SEnemies[i].shoot)
				{
					if (this.SEnemies[i].FrameIndex + 1 < this.SEnemies[i].WalkingFramesL.Count)
					{
						this.SEnemies[i].FrameIndex++;
						//////////////Check Shooting frame and make destination point within the bullet object and determine a factor to get it moving
						if (this.SEnemies[i].FrameIndex == 3)
						{
							if (player != null)
							{
								SBullet pnn = new SBullet(this.SEnemies[i].x, this.SEnemies[i].y + this.SEnemies[i].WalkingFramesL[this.SEnemies[i].FrameIndex].Height / 2, new Point(player.x, player.y));
								this.sBullets.Add(pnn);
							}
						}
						this.SEnemies[i].CurrentFrame = this.SEnemies[i].WalkingFramesL[this.SEnemies[i].FrameIndex];
					}
					else
					{
						this.SEnemies[i].FrameIndex = 0;
						this.SEnemies[i].CurrentFrame = this.SEnemies[i].WalkingFramesL[1];
						this.SEnemies[i].shoot = false;
						this.SEnemies[i].stimer = 1;
					}
				}
				else
				{
					if (this.SEnemies[i].stimer % 15 == 0)
						this.SEnemies[i].shoot = true;
					else if (this.SEnemies[i].stimer % 15 != 0 && !this.SEnemies[i].shoot)
						this.SEnemies[i].stimer++;
				}
			}
			////////////////////////////////////////
		}
		public void ChangeEnemyWithRespecttoplayer(Player player)
		{
			if (player != null)
			{
				for (int i = 0; i < this.Enemies.Count; i++)
				{

					if (this.Enemies[i].left && player.x - 50 > this.Enemies[i].x)
					{
						this.Enemies[i].CurrentList = this.Enemies[i].WalkingFramesR;
						this.Enemies[i].left = false;
					}
					else if (!this.Enemies[i].left && player.x + 50 < this.Enemies[i].x)
					{
						this.Enemies[i].CurrentList = this.Enemies[i].WalkingFramesL;
						this.Enemies[i].left = true;

					}
				}
				/*if (this.truck.left && player.x - 50 > this.truck.x)
				{
					truck.CurrentFrame = this.truck.WalkingFramesR[0];
					this.truck.left = false;
					if (truck.crocket != null)
					{
						truck.crocket.RocketImage = new Bitmap("CRocket.bmp");
						truck.crocket.RocketImage.MakeTransparent(Color.Black);
					}
				}
				else if (!this.truck.left && player.x + 50 < this.truck.x)
				{
					this.truck.CurrentFrame = this.truck.WalkingFramesL[0];
					this.truck.left = true;
					if (truck.crocket != null)
					{
						truck.crocket.RocketImage = new Bitmap("CRocketL.bmp");
						truck.crocket.RocketImage.MakeTransparent(Color.Black);
					}
				}*/
			}
		}
		public void CreateEnemy(Player player)
		{
			Random RR = new Random();
			int randomGround = RR.Next(0, this.grounds.Count);
			while (player.x >= this.grounds[randomGround].x && player.x + player.CurrentFrame.Width >= this.grounds[randomGround].x + this.grounds[randomGround].Groundnim.Width
				&& this.grounds[randomGround].y != this.grounds[3].y)
			{
				randomGround = RR.Next(0, this.grounds.Count);
			}
			Enemy en = new Enemy();
			en.x = this.grounds[randomGround].x;
			en.y = this.grounds[randomGround].y - en.CurrentFrame.Height + 21;
			en.g = this.grounds[randomGround];
			en.groundindex = randomGround;
			int c = RR.Next(0, 2);
			en.coin = c == 1 ? true : false;
			if (player.x > en.x)
			{
				en.left = false;
				en.CurrentList = en.WalkingFramesR;
			}
			else
			{
				en.left = true;
				en.CurrentList = en.WalkingFramesL;
			}
			this.Enemies.Add(en);
		}
		public void DrawScene(Graphics G, Size ClientSize)
		{
			if (!this.intitle)
			{
				Bitmap Death = new Bitmap("Death.bmp");
				Death.MakeTransparent(Color.White);
				if (death)
					G.DrawImage(Death, 0, 0);
				else
					G.DrawImage(Screensource, sc.rD, sc.rs, GraphicsUnit.Pixel);
			}
			else if(Form1.pause==false&&this.intitle)
				G.DrawImage(new Bitmap("Title.bmp"), 0, 0, ClientSize.Width, ClientSize.Height);
		}
		public void DrawInLegend(Graphics G)
		{
			G.Clear(Color.White);
			for (int i = 0; i < grounds.Count; i++)
			{
				G.DrawImage(grounds[i].Groundnim, grounds[i].x, grounds[i].y);
			}
			G.DrawImage(indicator.Arrow, indicator.x, indicator.y);
		}
	}

}
