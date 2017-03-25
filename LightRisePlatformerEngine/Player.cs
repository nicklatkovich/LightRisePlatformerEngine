using LightRise.BaseClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;

namespace LightRise.Main {
    class Player : IInstance {

        public const float WIDTH = 0.8f;
        public const float HEIGHT = 1.6f;
        public const float GRAVITY = 0.05f;
        public const float SIT_HEIGHT = 0.9f;

        public const uint WALK_TIME = 20;
        public const uint TO_SIT_TIME = 10;
        public const uint FROM_SIT_TIME = 15;
        public const uint SQUAT_TIME = 40;
        public enum ACTIONS {
            STAND,
            WALK_LEFT,
            WALK_RIGHT,
            FALL,
            TO_SIT,
            SIT,
            FROM_SIT,
            SQUAT_LEFT,
            SQUAT_RIGHT,
        }
        public ACTIONS Action = ACTIONS.STAND;

        public uint Alarm = 0;
        public float VSpeed = 0f;
        public float VSize = HEIGHT;

        private Point _gridPosition;
        public Point GridPosition {
            get { return _gridPosition; }
            set {
                if (value == null) {
                    throw new NullReferenceException( );
                }
                _gridPosition = value;
                Position = GridPosition.ToVector2( );
            }
        }

        public Vector2 Position;

        public Player(Point position) {
            if (Collision(position)) {
                throw new Exception("Incorrect initial coordinates of the player");
            }
            GridPosition = position;
        }

        public static Map Map { get { return Program.MainThread.Map; } }

        public static bool Collision(Point position) {
            return
                Map[position] == Map.WALL;
        }

        public void Draw(SpriteBatch surface, Camera camera) {
            surface.Draw(SimpleUtils.WhiteRect, new Rectangle(camera.WorldToWindow(Position.Add(0.5f) - new Vector2(WIDTH / 2f, VSize - 1.5f)), (new Vector2(WIDTH, VSize) * camera.Scale).ToPoint( )), Color.Green);
        }

        public void OnAlarm(StepState state) {
            switch (Action) {
            case ACTIONS.WALK_LEFT:
                Action = ACTIONS.STAND;
                GridPosition += new Point(-1, 0);
                break;
            case ACTIONS.WALK_RIGHT:
                Action = ACTIONS.STAND;
                GridPosition += new Point(1, 0);
                break;
            case ACTIONS.TO_SIT:
                Action = ACTIONS.SIT;
                VSize = SIT_HEIGHT;
                break;
            case ACTIONS.FROM_SIT:
                Action = ACTIONS.STAND;
                VSize = HEIGHT;
                break;
            case ACTIONS.SQUAT_LEFT:
                Action = ACTIONS.SIT;
                GridPosition += new Point(-1, 0);
                break;
            case ACTIONS.SQUAT_RIGHT:
                Action = ACTIONS.SIT;
                GridPosition += new Point(1, 0);
                break;
            }
        }

        public void Step(StepState state) {
            if (Alarm > 0) {
                Alarm--;
                if (Alarm == 0) {
                    OnAlarm(state);
                }
            }
            if (Action == ACTIONS.WALK_LEFT) {
                Position += new Vector2(-1f / WALK_TIME, 0);
            }
            if (Action == ACTIONS.WALK_RIGHT) {
                Position += new Vector2(1f / WALK_TIME, 0);
            }
            if (Action == ACTIONS.STAND) {
                if (!Collision(GridPosition + new Point(0, 2))) {
                    Action = ACTIONS.FALL;
                } else if (state.Keyboard.IsKeyDown(Keys.A) && !Collision(GridPosition + new Point(-1, 0))) {
                    Action = ACTIONS.WALK_LEFT;
                    Alarm = WALK_TIME;
                } else if (state.Keyboard.IsKeyDown(Keys.D) && !Collision(GridPosition + new Point(1, 0))) {
                    Action = ACTIONS.WALK_RIGHT;
                    Alarm = WALK_TIME;
                } else if (state.Keyboard.IsKeyDown(Keys.S)) {
                    Action = ACTIONS.TO_SIT;
                    Alarm = TO_SIT_TIME;
                }
            }
            if (Action == ACTIONS.TO_SIT) {
                VSize -= (HEIGHT - SIT_HEIGHT) / TO_SIT_TIME;
            }
            if (Action == ACTIONS.SIT) {
                if (!Collision(GridPosition + new Point(0, 2))) {
                    Action = ACTIONS.FALL;
                } else if (state.Keyboard.IsKeyDown(Keys.W) && !Collision(GridPosition)) {
                    Action = ACTIONS.FROM_SIT;
                    Alarm = FROM_SIT_TIME;
                } else if (state.Keyboard.IsKeyDown(Keys.A) && !Collision(GridPosition + new Point(-1, 1))) {
                    Action = ACTIONS.SQUAT_LEFT;
                    Alarm = SQUAT_TIME;
                } else if (state.Keyboard.IsKeyDown(Keys.D) && !Collision(GridPosition + new Point(1, 1))) {
                    Action = ACTIONS.SQUAT_RIGHT;
                    Alarm = SQUAT_TIME;
                }
            }
            if (Action == ACTIONS.FALL) {
                VSpeed = Math.Min(2, VSpeed + GRAVITY);
                Position += new Vector2(0f, VSpeed);
                if (Position.Y - GridPosition.Y > 1f) {
                    Vector2 pos = Position;
                    GridPosition += new Point(0, 1);
                    if (Collision(GridPosition + new Point(0, 2))) {
                        VSize = HEIGHT;
                        Action = ACTIONS.STAND;
                        VSpeed = 0f;
                    } else {
                        Position = pos;
                    }
                }
            }
            if (Action == ACTIONS.SQUAT_LEFT) {
                Position += new Vector2(-1f / SQUAT_TIME, 0);
            }
            if (Action == ACTIONS.SQUAT_RIGHT) {
                Position += new Vector2(1f / SQUAT_TIME, 0);
            }
            if (Action == ACTIONS.FROM_SIT) {
                VSize += (HEIGHT - SIT_HEIGHT) / FROM_SIT_TIME;
            }
        }
    }
}
