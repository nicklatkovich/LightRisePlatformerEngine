using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LightRise.BaseClasses {
    class FirstGUI : GUI {

        public FirstGUI(GraphicsDevice device, GraphicsDeviceManager graphics) : base(new Point(0, 0), device, graphics) {
        }

        public override void Draw(SpriteBatch surface, Camera cam) {
            surface.Draw(SimpleUtils.WhiteRect, new Rectangle((new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight) / 2f).Add(-240f).ToPoint( ), new Point(480)), Color.CornflowerBlue);
        }
    }
}
