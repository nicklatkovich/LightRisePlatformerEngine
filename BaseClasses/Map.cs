using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.IO;

namespace LightRise.BaseClasses {
    public class Map {

        protected UInt32 _width;
        protected UInt32 _height;

        public const uint EMPTY = 0;
        public const uint WALL = 1;

        public UInt32 Width { get { return _width; } }
        public UInt32 Height { get { return _height; } }

        protected uint[ ][ ] Grid;

        public uint this[UInt32 i, UInt32 j] {
            get { return Grid[i][j]; }
            set { Grid[i][j] = value; }
        }

        public Map(UInt32 width, UInt32 height) {
            _width = width;
            _height = height;
            Grid = SimpleUtils.Create2DArray(Width, Height, EMPTY);
            //for (UInt32 i = 0; i < Width; i++) {
            //    for (UInt32 j = 0; j < Height; j++) {
            //        Grid[i][j] = EMPTY;
            //    }
            //}
        }

        // TODO: Delete this function when it is not needed for testing
        public void Randomize( ) {
            for (uint i = 0; i < Width; i++) {
                for (uint j = 0; j < Height; j++) {
                    Grid[i][j] = SimpleUtils.Choose(new Tuple<uint, float>[ ] {
                        new Tuple<uint, float>(EMPTY, 9),
                        new Tuple<uint, float>(WALL, 1)});
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
                    if (this[i, j] == WALL) {
                        spriteBatch.Draw(SimpleUtils.WhiteRect, new Rectangle(cam.WorldToWindow(new Vector2(i, j)), cam.Scale.CeilingToPoint( )), Color.Red);
                    }
                }
            }
        }

        public void Add(Map map, Point position) {
            if (position.X < 0 || position.Y < 0) {
                throw new NegativeValueException( );
            }
            for (uint i = 0; i < map.Width; i++) {
                for (uint j = 0; j < map.Height; j++) {
                    this[i + (uint)position.X, j + (uint)position.Y] = map[i, j];
                }
            }
        }

    }
}
