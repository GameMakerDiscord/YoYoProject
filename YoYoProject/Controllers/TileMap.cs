using System;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    // TODO Implement me
    public sealed class TileMap
    {
        private uint[,] tiles;
        
        internal TileMap()
        {
            tiles = new uint[0, 0];
        }

        internal GMTileMapModel Serialize()
        {
            int width = tiles.GetLength(0);
            int height = tiles.GetLength(1);
            uint[] tileData = new uint[width * height];
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                    tileData[y * width + x] = tiles[x, y];
            }

            return new GMTileMapModel
            {
                SerialiseWidth = width,
                SerialiseHeight = height,
                TileSerialiseData = tileData
            };
        }
    }
}