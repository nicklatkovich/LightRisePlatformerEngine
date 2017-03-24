using System;

namespace LightRisePlatformerEngine {
    class Map {

        private UInt32 _width;
        private UInt32 _height;

        public enum CellType {
            Empty = 0,
            Wall,
        }

        public UInt32 Width { get { return _width; } }
        public UInt32 Height { get { return _height; } }

        private CellType[ ][ ] Grid;

        public CellType this[UInt32 i, UInt32 j] { get { return Grid[i][j]; } }

        public Map(UInt32 width, UInt32 height) {
            _width = width;
            _height = height;
            Grid = SimpleUtils.Create2DArray(Width, Height, CellType.Empty);
            for (UInt32 i = 0; i < Width; i++) {
                for (UInt32 j = 0; j < Height; j++) {
                    Grid[i][j] = SimpleUtils.Choose(new CellType[ ] { CellType.Empty, CellType.Wall });
                }
            }
        }

    }
}
