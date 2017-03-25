using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LightRise.BaseClasses {
    public class MapWithSelector : Map {

        public MapWithSelector(UInt32 width, UInt32 height) : base(width, height) {

        }

        private Point _selector;

        public Point Selector {
            get { return _selector; }
            set {
                if (value == null) {
                    throw new NullReferenceException( );
                }
                _selector = value;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Camera cam) {
            base.Draw(spriteBatch, cam);

            spriteBatch.Draw(SimpleUtils.WhiteRect, new Rectangle(
                (Int32)((Selector.X - cam.Position.X) * cam.Scale.X),
                (Int32)((Selector.Y - cam.Position.Y) * cam.Scale.Y),
                (Int32)cam.Scale.X,
                (Int32)cam.Scale.Y), Color.Red);
        }

    }
}
