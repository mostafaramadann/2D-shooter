using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace Multimedia_Project
{
	class screensource
	{
		public Rectangle rs;
		public Rectangle rD;
		public screensource(Rectangle rs, Rectangle rd)
		{
			this.rs = rs;
			this.rD = rd;
		}
	}
	public partial class Form1 : Form
	{
		/// <Game Objects>//////////
		Player player;
		Player player2;
		Level level = new Level();
		Level1 level1;
		//Level1 level1;
		/// </GameObjects>
		///////////////////////////////////
		/// <variables>
		Bitmap Shopmenu;
		Random skyframeindex = new Random();
		int time = 0;
		public  static bool pause = false;
		bool saved = false;
		SoundPlayer Splayer = new SoundPlayer();
		int xstartgame = 0;
		int ystartgame = 0;
		Bitmap LoadGame = new Bitmap("LoadGame.bmp");
		int xloadgame = 0;
		int yloadgame = 0;
		bool scroll = true;
		int savex = 0;
		Bitmap menuscreen;
		bool menu = false;
		public static bool purchasedRifle = false;
		bool inshop = false;
		bool withinshop = false;
		public static bool level2 = false;
		int enemySpawnTime = 10;
		MyFile myFile = new MyFile();
		int []rocketsSpawnFrequency = new int[1];
		/// </variables>
		///////////////////////////////////
		/// <RoutineVars>
		Bitmap Screen;
		Timer t = new Timer();
		Graphics G3;
		Graphics G2;
		/// </RoutineVars>
		public Form1()
		{
			rocketsSpawnFrequency[0] = 5;
			this.Load += new EventHandler(loaded);
			this.WindowState = FormWindowState.Maximized;
			this.KeyDown += new KeyEventHandler(kdown);
			this.MouseDown += new MouseEventHandler(mdown);
			this.MouseMove += new MouseEventHandler(mmove);
			t.Tick += new EventHandler(tick);
			//t.Interval=1;
			t.Start();
		}
		private void mmove(object sender, MouseEventArgs e)
		{
			if (e.X >= ClientSize.Width / 2 && e.X <= ClientSize.Width / 2 + 25 && e.Y >= this.ClientSize.Height / 2 && e.Y <= ClientSize.Height / 2 + 23 && pause)
				level.pbutton.currentframe = level.pbutton.pauseframes[0];
			else if(pause)
				level.pbutton.currentframe = level.pbutton.pauseframes[1];
		}

		private void mdown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (level != null)
				{
					if (e.X >= xstartgame && e.X <= xstartgame + level.StartGame.Width
						&& e.Y >= ystartgame && e.Y <= ystartgame + level.StartGame.Height)
						menu = false;
				}
				else if (e.X >= xloadgame && e.X <= xloadgame + LoadGame.Width
					&& e.Y >= yloadgame && e.Y <= yloadgame + LoadGame.Height && pause == false)
				{
					myFile.Load(player, level.xscroll, level.yscroll);
					saved = true;
					menu = false;
				}
				if (e.X >= ClientSize.Width / 2 && e.X <= ClientSize.Width / 2 + 25 && e.Y >= this.ClientSize.Height / 2 && e.Y <= ClientSize.Height / 2 + 23 && pause)
					pause = false;
				if (level != null)
				{
					if (e.X >= ClientSize.Width / 4 && e.X <= ClientSize.Width / 4 * 3 && e.Y >= ClientSize.Height / 2 && e.Y <= ClientSize.Height / 2 + 200 && level.death && player == null)
					{
						level.death = false;
						level.xscroll[0] = 0;
						level.yscroll[0] = 100;
						CreatePlayer();
					}
					if (e.X >= 0 && e.X <= 320
						&& e.Y > 0 && e.Y < 180 && inshop && !pause && !menu)
					{
						if (player.coincount >= 5)
						{
							if (!purchasedRifle)
							{
								player.coincount -= 5;
								purchasedRifle = true;
								MessageBox.Show("Purchased Rifle");
							}
						}
						else
							MessageBox.Show("InSuffecient Funds");
					}
					if (e.X >= 350 && e.X <= 670 && e.Y > 0 && e.Y < 180 && inshop && !pause && !menu)
					{
						if (player.coincount >= 5)
						{
							if (!level2)
							{
								player.coincount -= 5;
								level2 = true;
								MessageBox.Show("Purchased Level 2");
							}
						}
						else
							MessageBox.Show("InSuffecient Funds");
					}
				}
			
			}
		}
		private void loaded(object sender, EventArgs e)
		{
			///////////////Menu and title////////////////////////////
			//this.FormBorderStyle = FormBorderStyle.None;
			//level1 = new Level1(ClientSize);
			Screen = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
			menuscreen = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
			level.Screensource = new Bitmap(level.Endoflevel, level.Endoflevelh);
			level1 = new Level1(ClientSize);
			xstartgame = this.ClientSize.Width/2 - 200;
			xloadgame = this.ClientSize.Width / 2 - 200;
			ystartgame = 200;
			yloadgame = 325;
			level.yscroll[0] = 0;
			G3 = Graphics.FromImage(level.Screensource);
			G2 = Graphics.FromImage(Screen);
			Shopmenu = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
			level.G5 = Graphics.FromImage(level.Legend);
			level.Legend = new Bitmap(level.Endoflevel,level.Endoflevelh);
			level.indicator = new Indicator(500, this.ClientSize.Height);
			level.pbutton = new pausebutton();
			level.coindisp.MakeTransparent(Color.Black);
			///////////////////////////////////////////////////////
			CreatePlayer();
			level.CreateEnvironmentlevel1(ClientSize);
			level1.CreateEnvironment(ClientSize);
			level.DrawScene(G3,ClientSize,player);
			for(int i=0;i<2;i++)
			level.CreateSEnemy(player);
			if (level.intitle)
				Splayer.SoundLocation = "Title.wav";
			Splayer.PlayLooping();
		}
		public void CreatePlayer2()
		{
			player2 = new Player(new Bitmap("M1.bmp"));
			player2.CurrentFrame.MakeTransparent(Color.White);
			for (int i = player2.WalkingFramesL.Count-1; i >= 0; i--)
			{
				player2.WalkingFramesL.RemoveAt(i);
				player2.WalkingFramesR.RemoveAt(i);
			}
			for (int i = 1; i < 7; i++)
			{
				player2.WalkingFramesR.Add(new Bitmap("M" + (i) + ".bmp"));
				player2.WalkingFramesL.Add(new Bitmap("M" + (i) + "L.bmp"));
				player2.WalkingFramesL[i - 1].MakeTransparent(Color.White);
				player2.WalkingFramesR[i - 1].MakeTransparent(Color.White);
			}
			player2.x = 500;
			player2.y= this.ClientSize.Height / 2 + 190;
			player.shooting[0] = false;
			player.airstrike[0] = false;
		}
		public void CreatePlayer()
		{
			player = new Player(new Bitmap("0_BulkPicMe.bmp"));
			player.WalkingFramesL.Add(new Bitmap("0L.bmp"));
			player.WalkingFramesL[0].MakeTransparent(Color.Black);
			player.WalkingFramesR.Add(new Bitmap("0_BulkPicMe.bmp"));
			player.WalkingFramesR[0].MakeTransparent(Color.Black);
			player.x = 500;
			player.y = this.ClientSize.Height / 2 + 190;
			player.shooting[0] = false;
			player.airstrike[0] = false;
		}
		private void tick(object sender, EventArgs e)
		{
			if (pause == false && level != null)
			{
				if (!level.intitle && !menu)
				{
					if (player != null)
					{
						if ((player.y + player.CurrentFrame.Height >= level.Endoflevelh) || player.lifes <= 0)
						{
							level.death = true;
							player = null;
						}


						//////Check if within Shop///////////////////
						if (player.x >= level.shop.x && player.x + player.CurrentFrame.Width <= level.shop.x + level.shop.ShopIm.Width
							&& player.y >= level.shop.y && player.y + player.CurrentFrame.Height + 10 >= level.shop.y)
						{
							withinshop = true;
						}
						else
							withinshop = false;

					}
					level.MoveRifleBullets();
					///////////////////////Explosion effect  (Up&down)/////////////////
					if (level.texplosion % 6 == 0 && level.explosion)
					{
						level.texplosion = 1;
						level.amount = 0;
						level.explosion = false;
					}
					else if (level.explosion && level.texplosion % 6 != 0)
					{
						level.DrawScene(G2, ClientSize);
						level.amount *= -1;
						level.DrawScene(G3, ClientSize, player);
						level.texplosion++;
					}
					////////////////////////////////////////////////////////////////
					////////Creating Enemy/////////////////////////////////////////////
					if (player != null)
					{
						if (time % enemySpawnTime == 0)
						{
							level.CreateEnemy(player);
						}
					}
					////////////////////////////////////////////
					if (player != null)
					{
						//////////////////Moving Enemy&& Frame Handling///////////
						level.MoveEnemies(player);
						////////////////////////////////////////
						///////////Changing enemy frame with respect to player////////
						level.ChangeEnemyWithRespecttoplayer(player);
						////////////////////////////////////////////////////////////
						///	///////////////////////////////
							level.MoveTruckRocket(player);
						//////////////////////////////
						///////////////////////Shooting Enemy//////////////////////////
						level.ShootEnemeies();
						///////////////////////////shooting level.Barrels///////////////////////////////////////
						level.ShootBarrels();
					}
					/////////////////////////////////////////////////////////////////////////////////
					/////////////bullets colliding with ground/////////////////////////////////////////////
					level.CollideBulletsWithGround();
					///////////////////////////////////////////////////////////////////////////////////////
					//////////////////Check if enemy collided with player//////////////////////////////////
					if (player != null)
					{
						player.Enemies = this.level.Enemies;
						player.CheckifPlayerCollidedWithEnemy(level.xscroll);
					}
					//////////////////////////////////////////////////////////////////////////////////////////////////
					/////////////handling player Jump/////////////////
					if (player != null)
					{
						if (player.jump)
							player.Jump(level.yscroll, level,0);
					}
					/////////////////////////////////////////////////////////////////////////////////////////////////
					///////////////player fall////////////////////////////////////////////////////////////////////////
					if (player != null)
					{
						player.Fall(level.grounds, level.yscroll,0);
					}
					/////////////////////////////////////////////////////////////////////////////////
					/////////////Changing EnemyGround//////////
					level.ChangeEnemyGround();
					///////////////////////////////////////////////////////
					//////////////////////////////////////////EnemyFall/////////////////////////////
					level.EnemyFall();
					////////////////////////////////////////////////////////////////////////////////
					///////////////////////////Check if player reached checkpoint we bysave//////////
					if (player != null)
					{
						if (player.x >= level.checkpoint && player.x <= level.checkpoint + level.Checkpoint.Width
							&& player.y <= level.checkpointy && player.y + player.CurrentFrame.Height >= level.checkpointy
							&& !saved)
						{
							myFile.Save(player, level.xscroll, level.yscroll);
							Splayer.SoundLocation = "Checkpoint.wav";
							Splayer.Play();
							saved = true;
						}
					}
					///////////////////////////////////////////////////////////////////////////////////////////////
					/*if (player.x-level.xscroll[0] <50 )
					{
						if(player.x-200>0)
						level.xscroll[0] -= 15;
						if (player.x - 200 < 0)
							level.xscroll[0] += 15;
					}*/
					if (player != null)
					{
						if (!scroll && player.x > savex)
							scroll = true;
						else if (level.xscroll[0] < 15 && scroll)
						{
							savex = player.x;
							scroll = false;
						}
					}
						///////////////////////////picking up coins////////////////////////
						if (player != null)
					{
						player.coins = this.level.coins;
						player.CoinPick();
					}
					//////////////////////////////////////////////////////////////////
					level.IncrementBulletFrames();
					////////////Moving level.sBullets///////////////////////////////////////////////////////////
					level.MoveSBullets(player);
					/////////////////////////////////////////////////////////////////////////////////////
					///////////////////Frame handling for hero shooting and air strike//////////////////////
					if (player != null)
					{
						player.HandleFrames(Splayer, level);
					}
					/////////////////////Moving AirStrike level.plane and Creating Rockets///////////
					level.CreateRockets(time, rocketsSpawnFrequency);
					//////////////Moving level.rockets//////////////////////////////////////////
					level.MoveRockets();
					/////////////////////////////////check//////////////////////////////////////////////////////////////////////////
					level.CollideRocketsWithEnemies();
					//////////////////Check if level.rockets collided with barrel////////////////////////////////////////////////
					level.CollideRocketsWithBarrels();
					//////////////////////////////////////////////////////////////////////////////////////////////////////////
					///////////////////////////Checking if level.rockets Collided with a Ground////////////////////////////////////
					level.CollideRocketsWithGround();
					//////////////////////////////////////////////////////////////////////////////////////////////////////////////
					/////////////////////////Frame Handling For level.explosions////////////////////
					level.IncrementExplosionFrames();
					//////////////////////////////////////////////////////////////////////////
					time++;
				}
			}
			else if (level == null)
			{
				if (player != null)
				{
					if (level1.dragon.lifes <= 0)
						level1.dragon = null;
					if (player.jump)
						player.Jump(level1.yscroll,level,0);
					player.Fall(level1.grounds, level1.yscroll,0);
					if (!scroll && player.x > savex)
						scroll = true;
					else if (level1.xscroll[0] < 15 && scroll)
					{
						savex = player.x;
						scroll = false;
					}
					if(level1.dragon!=null)
					
					level1.MoveDragon();
					level1.DragonFire();
					level1.CheckFire(player,player2);
					level1.MoveMbullets();
					if (player2.jump)
						player2.Jump(level1.yscroll, level,1);
					player2.Fall(level1.grounds, level1.yscroll,1);
				}

			}
			if (level != null)
			{
				if (!level.exppp)
					DoubleBuffer(this.CreateGraphics());
				else
					level.exppp = false;
			}
			else
				DoubleBuffer(this.CreateGraphics());
		}
		private void kdown(object sender, KeyEventArgs e)
		{
			if (player != null)
			{
				switch (e.KeyCode)
				{
					case Keys.B:
						Ground pnn;
						switch (player.builddirection)
						{
							case 1:
								pnn = new Ground(player.x, player.y - player.CurrentFrame.Height, new Bitmap("Earth.bmp"));
								pnn.builtground = true;
								if (level != null)
									level.grounds.Add(pnn);
								else
									level1.grounds.Add(pnn);
								break;
							case 0:
								int x = 0;
								if (player.left)
									x = player.x - player.CurrentFrame.Width * 3 / 2 - 100;
								else
									x = player.x + player.CurrentFrame.Width + 20;
								pnn = new Ground(x, player.y + player.CurrentFrame.Height / 2 - 20, new Bitmap("Earth.bmp"));
								pnn.builtground = true;
								if (level != null)
									level.grounds.Add(pnn);
								else
									level1.grounds.Add(pnn);
								break;
							case -1:
								pnn = new Ground(player.x, player.y + player.CurrentFrame.Height + 10, new Bitmap("Earth.bmp"));
								pnn.builtground = true;
								if (level != null)
									level.grounds.Add(pnn);
								else
									level1.grounds.Add(pnn);
								break;
						}
						break;
					case Keys.Up:
						player.builddirection = 1;
						break;
					case Keys.Down:
						player.builddirection = -1;
						break;
					case Keys.Left:
						player.builddirection = 0;
						break;
					case Keys.Right:
						player.builddirection = 0;
						break;
					case Keys.Tab:
						if (level2)
						{
							level = null;
							player = null;
							CreatePlayer();
							for (int i = player.WalkingFramesR.Count - 1; i >= 0; i--)
							{
								player.WalkingFramesR.RemoveAt(i);
								player.WalkingFramesL.RemoveAt(i);
							}
							for (int i = 1; i < 26; i++)
							{
								player.WalkingFramesR.Add(new Bitmap("HZR (" + (i) + ").bmp"));
								player.WalkingFramesL.Add(new Bitmap("HZL (" + (i) + ").bmp"));
								player.WalkingFramesR[i - 1].MakeTransparent(Color.Black);
								player.WalkingFramesL[i - 1].MakeTransparent(Color.Black);
							}
							CreatePlayer2();
						}
						break;
					case Keys.Escape:
						pause = true;
						break;
					case Keys.Enter:
						if (level != null)
						{
							if (!level.intitle)
							{

								player.shooting[0] = true;
								if (!purchasedRifle)
								{
									Splayer.SoundLocation = "Shotgun.wav";
									Splayer.Play();
								}
							}
							else
							{
								Splayer.Stop();
								level.intitle = false;
								menu = true;
							}
						}
						break;
					case Keys.Q:
						player.airstrike[0] = true;
						if (level != null)
							level.plane = new Plane(level.Endoflevel, player.y - 200);
						else
							level1.plane = new Plane(level1.Endoflevel, 0);
						break;
					case Keys.D:
						if (level != null)
							player.CurrentFrame = player.WalkingFramesR[0];
						else
							player.CurrentFrame = player.WalkingFramesR[5];
						player.left = false;
						player.x += 15;
						if (level == null)
						{
							if (player.FrameIndex < 22)
								player.FrameIndex++;
							else
								player.FrameIndex = 5;
							player.CurrentFrame = player.WalkingFramesR[player.FrameIndex];
							if (level1.xscroll[0] + this.ClientSize.Width < level1.Endoflevel)
								level1.xscroll[0] += 15;
						}
						if (level != null)
						{
							if (level.xscroll[0] + 15 < level.Endoflevel)
							{
								if (scroll)
									level.xscroll[0] += 15;
							}
							level.drawinlegend = true;
							level.indicator.x = player.x - player.CurrentFrame.Width;
							level.indicator.y = player.y - 200;
						}
						break;
					case Keys.A:
						if (level != null)
							player.CurrentFrame = player.WalkingFramesL[0];
						else
							player.CurrentFrame = player.WalkingFramesL[5];
						player.left = true;
						player.x -= 15;
						if (level == null)
						{
							if (player.FrameIndex < 22)
								player.FrameIndex++;
							else
								player.FrameIndex = 5;
							player.CurrentFrame = player.WalkingFramesL[player.FrameIndex];
							if (level1.xscroll[0] > 0)
								level1.xscroll[0] -= 15;
						}
						else if (level != null)
						{
							if (scroll)
								level.xscroll[0] -= 15;
							level.indicator.x = player.x - player.CurrentFrame.Width;
							level.indicator.y = player.y - 200;
							level.drawinlegend = true;
						}
						break;
					case Keys.Space:
						if (!player.jump && !player.reachedground)
						{
							player.jump = true;
							player.tjumpp = true;
							if (level != null)
								level.drawinlegend = true;
						}
						break;
					case Keys.Z:
						if (level != null)
							level.ShowLegend = !level.ShowLegend;
						break;
					case Keys.R:
						if (withinshop)
						{
							if (!inshop)
								inshop = true;
							else
								inshop = false;
						}
						break;
					case Keys.NumPad4:
						if (level == null)
						{
							if (player2 != null)
							{
								player2.x -= 15;
								if (player2.FrameIndex + 1 < player2.WalkingFramesL.Count)
									player2.FrameIndex++;
								else
									player2.FrameIndex = 0;
								player2.CurrentFrame = player2.WalkingFramesL[player2.FrameIndex];
								if (level1.xscroll[1]>0)
								level1.xscroll[1]-=15;
								player.CurrentFrame.MakeTransparent(Color.White);
								player2.left = true;
							}
						}
						break;
					case Keys.NumPad6:
						if (level == null)
						{
							if (player2 != null)
							{
								player2.x += 15;
								if (level1.xscroll[1] + this.ClientSize.Width < level1.Endoflevel)
									level1.xscroll[1] += 15;
								if (player2.FrameIndex + 1 < player2.WalkingFramesR.Count)
									player2.FrameIndex++;
								else
									player2.FrameIndex = 0;
								player2.CurrentFrame = player2.WalkingFramesR[player2.FrameIndex];
								player.CurrentFrame.MakeTransparent(Color.White);
								player2.left = false;
							}
						}
						break;
					case Keys.NumPad8:
						if (level == null)
						{
							if (!player2.jump && !player2.reachedground)
							{
								player2.jump = true;
								player2.tjumpp = true;
							}
						}
						break;
					case Keys.NumPad5:
						if (level == null)
						{
							Mbullet pnn22 = new Mbullet(player2.left, player2.left ? player2.x - 50 : player2.x + 50, player2.y + player2.CurrentFrame.Height / 2 - 20);
							level1.mbullets.Add(pnn22);
						}
						break;
					case Keys.C:
						if (level == null)
							player.shield = !player.shield;
						
						break;
				}

			}
		}
	    void DoubleBuffer(Graphics G)
		{
			Graphics G4 = Graphics.FromImage(menuscreen);
			
			if (menu && !level.intitle)
			{
				DrawMenu(G4);
				G.DrawImage(menuscreen, 0, 0);
			}
			else if (!menu)
			{
				if (level != null)
				{
					
					level.Screensource = new Bitmap(this.ClientSize.Width + level.xscroll[0], this.ClientSize.Height + level.yscroll[0]);
					Graphics G3 = Graphics.FromImage(level.Screensource);
					level.DrawScene(G3, ClientSize, player);
					//Graphics G2 = Graphics.FromImage(Screen);
					//G5 = Graphics.FromImage(Legend);
					if (level.drawinlegend)
					{
						level.DrawInLegend(level.G5);
						level.drawinlegend = false;
					}
					if (!inshop)
						level.DrawScene(G2, ClientSize);
					else
						DrawShopMenu(G2);

				}
				else
				{
					Graphics G6 = Graphics.FromImage(level1.level);
					level1.DrawScene(G6,ClientSize,player,player2);
					G2.DrawImage(level1.level, level1.levelsource.rD, level1.levelsource.rs, GraphicsUnit.Pixel);
					G2.DrawImage(level1.level, level1.levelsource2.rD, level1.levelsource2.rs, GraphicsUnit.Pixel);
				}
				G.DrawImage(Screen, 0, 0);
			}
		}
		private void DrawShopMenu(Graphics G)
		{
			G.Clear(Color.White);
			G.DrawImage(new Bitmap("MultiBullet.bmp"), 0, 0);
			G.DrawImage(new Bitmap("skn.bmp"), new Rectangle(350,0,320,180),new Rectangle(0,0,445,250),GraphicsUnit.Pixel);
			G.DrawString("Multi Bullet (5) Coins", new Font("Times New Roman", 22, FontStyle.Regular, GraphicsUnit.Pixel),new SolidBrush(Color.Black),30,180);
			G.DrawString("Level 2 (5) Coins", new Font("Times New Roman", 22, FontStyle.Regular, GraphicsUnit.Pixel), new SolidBrush(Color.Black), 425, 180);
		}
		private void DrawMenu(Graphics G)
		{
			G.Clear(Color.White);
			G.DrawImage(new Bitmap("Background.bmp"), 0, 0);
			G.DrawImage(level.StartGame, this.ClientSize.Width / 2 - 200, 200);
			G.DrawImage(LoadGame, this.ClientSize.Width / 2 - 200, 225 + 100);
		}
		
		//////////////////////////////////////////////////////////////////////////////
	}

}
