using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LightRise.BaseClasses {
    class FirstGUI : GUI {

        const uint WIDTH = 7;
        const uint HEIGHT = 7;

        Point Size = new Point(480);

        int[ ][ ] Map = SimpleUtils.Create2DArray(WIDTH, HEIGHT, 0);

        public FirstGUI(GraphicsDevice device, GraphicsDeviceManager graphics) : base(new Point(0, 0), device, graphics) {
            uint xstart = (uint)(SimpleUtils.Rand % WIDTH);
            uint ystart = (uint)(SimpleUtils.Rand % HEIGHT);
            Map[xstart][ystart] = -1;
            uint[ ] posses = new uint[WIDTH * HEIGHT];
            uint pos = 1;
            posses[0] = xstart * WIDTH + ystart;

            while (pos > 0) {
                uint r = (uint)SimpleUtils.Rand % pos;
                uint x = posses[r] / WIDTH;
                uint y = posses[r] % WIDTH;
                posses[r] = posses[--pos];
                if (Map[x][y] == -1) {
                    Map[x][y] = 1;
                    uint dirs = 0;
                    for (uint d = 0; d < 4; d++) {
                        int xx = (int)x + SimpleUtils.DX[d];
                        int yy = (int)y + SimpleUtils.DY[d];
                        if (xx >= 0 && xx < WIDTH && yy >= 0 && yy < HEIGHT) {
                            if (Map[xx][yy] == 0) {
                                Map[xx][yy] = -1;
                                posses[pos++] = (uint)(xx * WIDTH + yy);
                            } else if (Map[xx][yy] > 0) {
                                dirs++;
                            }
                        }
                    }
                    if (dirs > 0) {
                        int dir = SimpleUtils.Rand % (int)dirs;
                        for (uint d = 0; d < 4; d++) {
                            int xx = (int)x + SimpleUtils.DX[d];
                            int yy = (int)y + SimpleUtils.DY[d];
                            if (xx >= 0 && xx < WIDTH && yy >= 0 && yy < HEIGHT) {
                                if (Map[xx][yy] > 0) {
                                    dir--;
                                    if (dir < 0) {
                                        Map[x][y] *= (int)SimpleUtils.DS[d];
                                        Map[xx][yy] *= (int)SimpleUtils.DS[(d + 2) % 4];
                                        d = 4; // break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch surface, Camera cam) {
            surface.Draw(SimpleUtils.WhiteRect, new Rectangle(((new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight) / 2f) - Size.ToVector2( ) / 2f).ToPoint( ), Size), Color.CornflowerBlue);
            for (uint i = 0; i < WIDTH; i++) {
                for (uint j = 0; j < HEIGHT; j++) {
                    for (uint d = 0; d < 4; d++) {
                        if (Map[i][j] % SimpleUtils.DS[d] == 0) {
                            surface.Draw(SimpleUtils.WhiteRect, new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight) / 2f - Size.ToVector2( ) / 2f + new Vector2((i + 0.5f) * Size.X / WIDTH, (j + 0.5f) * Size.Y / HEIGHT), origin: new Vector2(0, 0.5f), rotation: -(float)Math.PI * d / 2, scale: new Vector2(Math.Min(Size.X / WIDTH, Size.Y / HEIGHT), 8f));
                        }
                    }
                }
            }
        }

        public override void Update(StepState state) {
            base.Update(state);

            if (state.Mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed) {
                Point pos = ((state.Mouse.Position.ToVector2( ) - (new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight) - Size.ToVector2( )) / 2f) / Math.Min(Size.X / WIDTH, Size.Y / HEIGHT)).ToPoint( );
                if (pos.X >= 0 && pos.X < WIDTH && pos.Y >= 0 && pos.Y < HEIGHT) {
                    uint new_value = 1;
                    for (uint d = 0; d < 4; d++) {
                        if (Map[pos.X][pos.Y] % SimpleUtils.DS[d] == 0) {
                            new_value *= SimpleUtils.DS[(d + 1) % 4];
                        }
                    }
                }
            }
        }
    }
}
