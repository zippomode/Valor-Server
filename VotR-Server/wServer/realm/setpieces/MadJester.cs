﻿#region

using common.resources; using wServer.realm.worlds;
using System;
using wServer.realm.entities; using wServer.realm.worlds;
#endregion

namespace wServer.realm.setpieces
{
	internal class MadJester : ISetPiece
	{
		private readonly Random rand = new Random();

		private static byte[,] SetPiece //[Y, X]
		{
			get
			{
				return new byte[,]
				{
					{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
					{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
					{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
					{0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
					{0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0},
					{0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0},
					{0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0},
					{0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0},
					{0, 0, 0, 0, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0},
					{0, 0, 0, 0, 1, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1, 2, 1, 1, 1, 0, 0, 0, 0, 0},
					{0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0},
					{0, 0, 0, 1, 1, 1, 2, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 0, 0, 0, 0},
					{0, 0, 1, 1, 1, 1, 1, 2, 1, 2, 1, 2, 2, 2, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0},
					{0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 1, 1, 1, 1, 1, 0, 0, 0, 0},
					{0, 0, 0, 1, 1, 2, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 0, 0, 0},
					{0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0},
					{0, 0, 0, 0, 1, 1, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 0, 0},
					{0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 4, 2, 2, 2, 3, 2, 2, 2, 5, 2, 2, 1, 2, 1, 1, 2, 1, 1, 1, 1, 0, 0, 0},
					{0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0},
					{0, 0, 0, 0, 1, 1, 2, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 1, 1, 1, 1, 0, 0, 0, 0, 0},
					{0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0},
					{0, 0, 0, 0, 1, 1, 1, 2, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 2, 1, 1, 1, 1, 0, 0, 0, 0},
					{0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 1, 2, 1, 2, 1, 1, 1, 1, 1, 0, 0, 0, 0},
					{0, 0, 0, 1, 1, 1, 1, 1, 2, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0},
					{0, 0, 0, 1, 1, 2, 1, 2, 1, 1, 2, 1, 1, 1, 2, 2, 2, 1, 1, 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 1, 1, 1, 0, 0, 0},
					{0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0},
					{0, 0, 0, 0, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0},
					{0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 0, 0, 0, 0},
					{0, 0, 0, 1, 0, 0, 0, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0},
					{0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0},
					{0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
					{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
					{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
					{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
					{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
				};
			}
		}

		public int Size
		{
			get { return 35; }
		}

		public void RenderSetPiece(World world, IntPoint pos)
		{
			XmlData dat = world.Manager.Resources.GameData;
			for (int x = 0; x < Size; x++)
			{
				for (int y = 0; y < Size; y++)
				{
					if (SetPiece[y, x] == 1)
					{
						var tile = world.Map[x + pos.X, y + pos.Y].Clone();
						tile.TileId = dat.IdToTileType["Blue Grass"];
						tile.ObjType = 0;
						world.Map[x + pos.X, y + pos.Y] = tile;
					}
					else if (SetPiece[y, x] == 2)
					{
						var tile = world.Map[x + pos.X, y + pos.Y].Clone();
						tile.TileId = dat.IdToTileType["Cracked Purple Stone"];
						tile.ObjType = 0;
						world.Map[x + pos.X, y + pos.Y] = tile;
					}
					else if (SetPiece[y, x] == 3)
					{
						var tile = world.Map[x + pos.X, y + pos.Y].Clone();
						tile.TileId = dat.IdToTileType["Cracked Purple Stone"];
						tile.ObjType = 0;
						world.Map[x + pos.X, y + pos.Y] = tile;
						Entity statue = Entity.Resolve(world.Manager, "Mad Jester");
						statue.Move(pos.X + x + .5f, pos.Y + y + .5f);
						world.EnterWorld(statue);
					}
					else if (SetPiece[y, x] == 4)
					{
						var tile = world.Map[x + pos.X, y + pos.Y].Clone();
						tile.TileId = dat.IdToTileType["Cracked Purple Stone"];
						tile.ObjType = 0;
						world.Map[x + pos.X, y + pos.Y] = tile;
						Entity statue = Entity.Resolve(world.Manager, "White Mask");
						statue.Move(pos.X + x + .5f, pos.Y + y + .5f);
						world.EnterWorld(statue);
					}
					else if (SetPiece[y, x] == 5)
					{
						var tile = world.Map[x + pos.X, y + pos.Y].Clone();
						tile.TileId = dat.IdToTileType["Cracked Purple Stone"];
						tile.ObjType = 0;
						world.Map[x + pos.X, y + pos.Y] = tile;
						Entity statue = Entity.Resolve(world.Manager, "Dark Mask");
						statue.Move(pos.X + x + .5f, pos.Y + y + .5f);
						world.EnterWorld(statue);
					}
				}
			}
		}
	}
}