using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Multimedia_Project
{
	class Level1
	{
		public int Endoflevel = 1500;
		public int Endoflevelh = 1125;
		public screensource levelsource;
		public screensource levelsource2;
		public List<Ground> grounds = new List<Ground>();
		public List<Sky> skies = new List<Sky>();
		public Bitmap level;
		public Bitmap sky = new Bitmap("skn.bmp");
		public int[] xscroll;
		public int[] yscroll;
		public int amount = 0;
		int amount2 = 1;
		public Plane plane;
		public List<Rocket> rockets = new List<Rocket>();
		public List<Explosion> explosions = new List<Explosion>();
		public List<Mbullet> mbullets = new List<Mbullet>();
		public Dragon dragon;
		public Shield Shield = new Shield();
		public Level1(Size ClientSize)
		{
			level = new Bitmap(Endoflevel, Endoflevelh);
			yscroll = new int[2];
			xscroll = new int[2];
			dragon = new Dragon(Endoflevel - 250, 150);
			levelsource = new screensource(new Rectangle(0, 0, ClientSize.Width, ClientSize.Height), new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
			levelsource2 = new screensource(new Rectangle(0, 0, ClientSize.Width, ClientSize.Height), new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
		}
		public void CheckFire(Player player,Player player2)
		{
			for (int k = dragon.sources.Count - 1; k >= 0; k--)
			{
				if (player != null)
				{
					if (dragon.sources[k].rD.X < player.x && dragon.sources[k].rD.X + dragon.sources[k].rD.Width > player.x
						&& dragon.sources[k].rD.Y + dragon.sources[k].rD.Height > player.y && dragon.sources[k].rD.Y < player.y && !player.shield)
					{
						player = null;
					}
				}
				if (player2 != null)
				{
					if (dragon.sources[k].rD.X < player2.x && dragon.sources[k].rD.X + dragon.sources[k].rD.Width > player2.x
					&& dragon.sources[k].rD.Y + dragon.sources[k].rD.Height > player2.y && dragon.sources[k].rD.Y < player2.y)
					{
						if (player2.x > Shield.x && player2.x + player2.CurrentFrame.Width < Shield.Shieldim.Width
							&& player2.y < Shield.y && player2.y + player2.CurrentFrame.Height > Shield.y + Shield.Shieldim.Height)
						{
						}
						else player2 = null;
					}
				}
			}
			for (int i = grounds.Count-1;i>=0; i--)
			{
				for (int k = dragon.sources.Count - 1; k >= 0; k--)
				{
					if (dragon.sources[k].rD.X > grounds[i].x && dragon.sources[k].rD.X > grounds[i].x + grounds[i].Groundnim.Width
						&& dragon.sources[k].rD.Y + dragon.sources[k].rD.Height > grounds[i].y&&grounds[i].builtground)
					{
						
						grounds.RemoveAt(i);
						break;
					}
				}
			}

		}
		public void MoveMbullets()
		{
			for (int i = mbullets.Count - 1; i >= 0; i--)
			{
				if (dragon != null)
				{
					if (!mbullets[i].left && mbullets[i].x > dragon.x && mbullets[i].x + mbullets[i].currentframe.Width < dragon.x + dragon.dragonframes[dragon.DragonIndex].Width
						&& mbullets[i].y > dragon.y && mbullets[i].y + mbullets[i].currentframe.Height < dragon.y + dragon.dragonframes[dragon.DragonIndex].Height)
					{
						dragon.lifes -= 5;
						mbullets.RemoveAt(i);
					}
					else if (mbullets[i].left)
					{
						mbullets[i].x -= 20;
					}
					else if(!mbullets[i].left)
					{
						if (mbullets[i].x > Endoflevel)
							mbullets.RemoveAt(i);
						else
						mbullets[i].x += 20;
					}
				}

			   
			}
		}
		public void CreateEnvironment(Size ClientSize)
		{
			Bitmap g = new Bitmap("Earth.bmp");

			for (int x = 0; x <= Endoflevel; x += g.Width)
			{
				Ground gg = new Ground(x, ClientSize.Height - g.Height, new Bitmap("Earth.bmp"));
				for (int y = 0; y < (4 * 250); y += 250)
				{
					Sky pnn = new Sky(x, y, new Bitmap("skn.bmp"));
					skies.Add(pnn);
				}
				grounds.Add(gg);
			}
		}
		public void DragonFire()
		{
			if (dragon.fire)
			{
				bool inn = false;
				for (int index = 1; index < grounds.Count; index++)
				{
					if (dragon.sources.Count > 0)
					{
						if (dragon.sources[dragon.sources.Count - 1].rD.Y + dragon.sources[dragon.sources.Count - 1].rD.Height > grounds[index].y)
						{
							inn = true;
						}

					}
				}
				if (amount2 != -1)
				{
					if (inn==false)
					{
						dragon.sources.Add(new screensource(new Rectangle(0, 0, 100, 78), new Rectangle(dragon.xdragbull, dragon.ydragbull, 100, 78)));
						dragon.xdragbull -= 40;
						dragon.ydragbull += 10;
					
					}
					else
					{
						for (int i = dragon.sources.Count - 1; i >= 0; i--)
						{
							dragon.sources.RemoveAt(i);
						}
					}
				}
				//dragon.firecooldown++;
			}
		}
		public void MoveDragon()
		{
			if ((dragon.y < 0) || dragon.y + dragon.dragonframes[dragon.DragonIndex].Height+50 > Endoflevelh - grounds[0].Groundnim.Height * 7 - 20)
			{
		
				amount2 *= -1;
				for (int i = dragon.sources.Count - 1; i >= 0; i--)
				{
					dragon.sources.RemoveAt(i);
				}
				dragon.fire = false;
			}
			else
			{
				dragon.fire = true;
				if (dragon.firecooldown % 30 == 0)
				{
					for (int i = dragon.sources.Count - 1; i >= 0; i--)
					{
						dragon.sources.RemoveAt(i);
					}
					dragon.xdragbull = dragon.x;
				}

			}
			if (dragon.DragonIndex + 1 < dragon.dragonframes.Count)
				dragon.DragonIndex++;
			else
			dragon.DragonIndex = 0;

			if (dragon.firecooldown % 30 == 0)
			{
				if (amount2 == 1)
					dragon.ydragbull = dragon.y + 90;
				else
					dragon.ydragbull = dragon.y - 10;
			}
			dragon.y += 5 * amount2;
			dragon.firecooldown++;

		}
		public void CreateRockets(int time, int[] rocketsSpawnFrequency)
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
		public void DrawScene(Graphics G, Size ClientSize, Player player,Player player2)
		{
			G.Clear(Color.White);
			for(int i=0;i<skies.Count;i++)
			{
				G.DrawImage(skies[i].Skyframes[0], skies[i].x, skies[i].y);
			}
			for (int i = 0; i < grounds.Count; i++)
			{
				G.DrawImage(grounds[i].Groundnim, grounds[i].x, grounds[i].y);
			}
			for (int i = 0; i < mbullets.Count; i++)
			{
				G.DrawImage(mbullets[i].currentframe, mbullets[i].x, mbullets[i].y);
			}
			for (int i = 0; i < dragon.sources.Count; i++)
			{
				Bitmap D = new Bitmap("Fire.bmp");
				D.MakeTransparent(Color.Black);
				G.DrawImage(D, dragon.sources[i].rD, dragon.sources[i].rs, GraphicsUnit.Pixel);
			}
			int pcoins = 0;
			if (player != null)
				pcoins = player.coincount;
			//G.DrawString("" + pcoins, new Font(FontFamily.Families[0], 22), Brushes.Black, ClientSize.Width - 50 + xscroll[0], 12 + this.amount + yscroll[0]);
			if (this.plane != null)
				G.DrawImage(this.plane.planeImage, this.plane.x, this.plane.y);
			if (player != null)
			{
				if (player.lifes == 1)
					G.DrawImage(player.LifesSprite, 0 + xscroll[0], 0 + this.amount + yscroll[0]);
				else if (player.lifes == 2)
				{
					G.DrawImage(player.LifesSprite, 0 + xscroll[0], 0 + this.amount + yscroll[0]);
					G.DrawImage(player.LifesSprite, 70 + xscroll[0], 0 + this.amount + yscroll[0]);
				}
				else if (player.lifes == 3)
				{
					G.DrawImage(player.LifesSprite, 0 + xscroll[0], 0 + this.amount + yscroll[0]);
					G.DrawImage(player.LifesSprite, 70 + xscroll[0], 0 + this.amount + yscroll[0]);
					G.DrawImage(player.LifesSprite, 140 + xscroll[0], 0 + this.amount + yscroll[0]);
				}
				G.DrawImage(player.CurrentFrame, player.x, player.y);
					if (player.shield)
					{
						if (!player.left)
						{
							G.DrawImage(this.Shield.Shieldim, player.x + 45, player.y + 20);
							Shield.x = player.x + 45;
							Shield.y = 20;
						}
						else
						{
							G.DrawImage(this.Shield.Shieldim, player.x + 50, player.y + 20);
							Shield.x = player.x + 50;
							Shield.y = 20;
						}
					}
			}
			if (player2 != null)
				G.DrawImage(player2.CurrentFrame, player2.x, player2.y);
			if (dragon != null)
				G.DrawImage(dragon.dragonframes[dragon.DragonIndex], dragon.x, dragon.y);
			//G.DrawImage(new Bitmap("coin (1).bmp"), ClientSize.Width - 50 - new Bitmap("coin (1).bmp").Width + xscroll[0], 0 + this.amount + yscroll[0]);
			levelsource = new screensource(new Rectangle(0 + xscroll[0], 0 + yscroll[0], ClientSize.Width, ClientSize.Height), new Rectangle(0, ClientSize.Height / 2, ClientSize.Width, ClientSize.Height));
			levelsource2 = new screensource(new Rectangle(0 + xscroll[1], 0 + yscroll[1], ClientSize.Width, ClientSize.Height), new Rectangle(0, -200, ClientSize.Width, ClientSize.Height / 2 + 200));
		}
	}

}
