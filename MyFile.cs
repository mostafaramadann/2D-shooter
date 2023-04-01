using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Multimedia_Project
{
	class MyFile
	{
		FileStream file;
		public MyFile()
		{

		}
		public void Load(Player player, int[] xscroll,int []yscroll)
		{
			using (file = new FileStream("Game.txt", FileMode.Open, FileAccess.Read))
			{
				using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
				{
					string line = reader.ReadLine();
					if (line == "Player")
					{
						string x, y, lifes, left, coincount, zxscroll, zyscroll;
						x = reader.ReadLine();
						y = reader.ReadLine();
						lifes = reader.ReadLine();
						left  = reader.ReadLine();
						coincount = reader.ReadLine();
						zxscroll  = reader.ReadLine();
						zyscroll  = reader.ReadLine();
						for (int xx = 0; xx <= 2800; xx++)
						{
							if (xx.ToString() == zxscroll)
								xscroll[0] = xx;
							if (xx.ToString() == x)
								player.x = xx;
						}
						for (int yy = 0; yy <= 1125; yy++)
						{
							if (yy.ToString() == zyscroll)
								yscroll[0] = yy;
							if (yy.ToString() == y)
								player.y = yy;
						}
						for (int l = 1; l <= 3; l++)
						{
							if (l.ToString() == lifes)
								player.lifes = l;
						}
						for (int cc = 0; cc <= 1000; cc++)
						{
							if (cc.ToString() == coincount)
								player.coincount =cc;
						}

						bool leftf = false;
						if (leftf.ToString() == left)
							player.left = leftf;
						else
							player.left = true;

					}
				}
			}
		}
		public void Save(Player player, int[] xscroll,int []yscroll)
		{
			using (file = new FileStream("Game.txt", FileMode.Create, FileAccess.Write))
			{
				using (StreamWriter writer = new StreamWriter(file, Encoding.UTF8))
				{
					writer.WriteLine("Player");
					writer.WriteLine(player.x);
					writer.WriteLine(player.y);
					writer.WriteLine(player.lifes);
					writer.WriteLine(player.left);
					writer.WriteLine(player.coincount);
					writer.WriteLine(xscroll[0]);
					writer.WriteLine(yscroll[0]);
					writer.WriteLine(".........");

				}
			}
		}
	}
}
