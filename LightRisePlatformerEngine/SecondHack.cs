using LightRise.BaseClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightRise.Main
{
    class SecondHack : HackScreen
    {
        public SecondHack(SpriteFont font, SpriteBatch spriteBatch, Texture2D terminal, List<TextObject> items) : base (font, spriteBatch, terminal)
        {
            HackScreen.TextContainer word = new HackScreen.TextContainer(new Rectangle(50, 50, 358, 124));
            TextObject textObj = new TextObject(font, "void OpenDoor() {\n  if (AccessAllowed ==          )\n    Door.Open;\n }");
            word.textObject = textObj;
            Words.Add(word);
            word = new HackScreen.TextContainer(new Rectangle(330, 80, 57, 31));
            textObj = new TextObject(font, "true");
            textObj.color = Color.Red;
            word.textObject = textObj;
            textObj.Draggable = true;
            Words.Add(word);
            this.Items = items;
        }

        public void Update(GameTime gameTime, StepState State)
        {
        }

        public void Draw(SpriteBatch spriteBatch, Camera cam)
        {
        }
    }
}
