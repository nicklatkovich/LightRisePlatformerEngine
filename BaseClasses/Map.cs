using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace LightRise.BaseClasses {
    public class Map {

        protected UInt32 _width;
        protected UInt32 _height;

        public enum CellType {
            Empty = 0,
            Wall,
        }

        public UInt32 Width { get { return _width; } }
        public UInt32 Height { get { return _height; } }

        protected CellType[ ][ ] Grid;

        public CellType this[UInt32 i, UInt32 j] { get { return Grid[i][j]; } }

        public Map(UInt32 width, UInt32 height) {
            _width = width;
            _height = height;
            Grid = SimpleUtils.Create2DArray(Width, Height, CellType.Empty);
            for (UInt32 i = 0; i < Width; i++) {
                for (UInt32 j = 0; j < Height; j++) {
                    Grid[i][j] = CellType.Empty;
                }
            }
        }

        // TODO: Delete this function when it is not needed for testing
        public void Randomize() {
            for (uint i = 0; i < Width; i++) {
                for (uint j = 0; j < Height; j++) {
                    Grid[i][j] = SimpleUtils.Choose(new Tuple<CellType, float>[ ] {
                        new Tuple<CellType, float>(CellType.Empty, 9),
                        new Tuple<CellType, float>(CellType.Wall, 1)});
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Camera cam) {
            for (UInt32
                i = (UInt32)Math.Max(0, (Int32)Math.Floor(cam.Position.X)),
                iTo = Math.Min(Width, (UInt32)Math.Ceiling(cam.Position.X + spriteBatch.GraphicsDevice.PresentationParameters.BackBufferWidth / cam.Scale.X));
                i < iTo; i++) {
                for (UInt32
                    j = (UInt32)Math.Max(0, (Int32)Math.Floor(cam.Position.Y)),
                    jTo = Math.Min(Height, (UInt32)Math.Ceiling(cam.Position.Y + spriteBatch.GraphicsDevice.PresentationParameters.BackBufferHeight / cam.Scale.Y));
                    j < jTo; j++) {
                    if (this[i, j] == CellType.Wall) {
                        spriteBatch.Draw(SimpleUtils.WhiteRect, new Rectangle(
                            (Int32)((i - cam.Position.X) * cam.Scale.X),
                            (Int32)((j - cam.Position.Y) * cam.Scale.Y),
                            (Int32)cam.Scale.X,
                            (Int32)cam.Scale.Y), Color.Red);
                    }
                }
            }

        }
    }
}
