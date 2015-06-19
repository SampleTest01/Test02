using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Team3
{
    public class Font
    {
        private ContentManager contentManager;
        private SpriteBatch spriteBatch;

        private Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        public Font(ContentManager content, SpriteBatch spriteBatch)
        {
            contentManager = content;

            this.spriteBatch = spriteBatch;
        }

        public void Add(string name)
        {
            fonts[name] = contentManager.Load<SpriteFont>(name);
        }

        public void Unload()
        {
            fonts.Clear();
        }

        public void Draw(string name, Vector2 position, string text, Color color,float alpha = 1.0f)
        {
            spriteBatch.DrawString(fonts[name], text, position, color*alpha);
        }
        public Vector2 MeasureString(string name, string text)
        {
            return fonts[name].MeasureString(text);
        }
    }
}
