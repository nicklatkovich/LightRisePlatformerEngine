﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LightRise.BaseClasses;

namespace LightRise.Main {
    public class FirstComp : Comp {

        public FirstComp(Point position, GraphicsDevice device) : base(position, device) {
        }

        public override void Complete( ) {
        }

        public override void Connect( ) {
            base.Connect( );
            Program.MainThread.HackScreen = new FirstHack(Program.MainThread.HackFont, Program.MainThread.SpriteBatch, Program.MainThread.Terminal);
        }

        public override void Draw(SpriteBatch surface, Camera camera) {
            surface.Draw(SimpleUtils.WhiteRect, new Rectangle(camera.WorldToWindow(GridPosition.ToVector2( ) + new Vector2(0.1f)), (camera.Scale * 0.8f).ToPoint( )), Color.Purple);
        }

        public override void Update(StepState state) {
        }
    }
}
