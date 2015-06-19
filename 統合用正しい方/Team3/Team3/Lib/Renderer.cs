using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Team3
{
   public class Renderer
    {
        private ContentManager contentManager;                  
        private GraphicsDevice graphicsDevice;                  
        private SpriteBatch spriteBatch;
                 
        private Dictionary<string, Texture2D> textures
         = new Dictionary<string, Texture2D>();

        private Font font;

        public Renderer(ContentManager content, GraphicsDevice graphics)
        {
            contentManager = content;
            contentManager.RootDirectory = "Content";
            graphicsDevice = graphics;
            spriteBatch = new SpriteBatch(graphics);
            font = new Font(contentManager, spriteBatch);
        }

        // 画像の読み込み

        public void LoadTexture(string name,string path = "./")
        {
            textures[name] = contentManager.Load<Texture2D>(path + name);
        }

        public void LoadFont(string name)
        {
            font.Add(name);
        }

        // 画像の解放
        public void Unload()
        {
            textures.Clear();
            contentManager.Unload();
        }
       
        public void Begin(){spriteBatch.Begin();}

        public void End(){spriteBatch.End();}

        public void DrawTexture(string name, Vector2 position, float alpha = 1.0f)
        {
            spriteBatch.Draw(textures[name], position, Color.White * alpha);
        }
        public void DrawTexture2(string name, Vector2 position, float alpha = 1.0f, float x = 1.0f)
        {
            spriteBatch.Draw(textures[name], position, null, Color.White * alpha, 0.0f, Vector2.Zero, new Vector2(x, 1), SpriteEffects.None, 0.0f);
        }
        public void DrawSpin(string name, Vector2 position, float rotate, Vector2 origin, float alpha = 1.0f)
        {
            spriteBatch.Draw(textures[name], position, null, Color.White * alpha, rotate, origin, 1, SpriteEffects.None, 0);
        }
        public void DrawSpin2(string name, Vector2 position, float rotate, Vector2 origin, float alpha = 1.0f)
        {
            spriteBatch.Draw(textures[name], position, null, Color.White * alpha, MathHelper.ToRadians(rotate), origin, 1, SpriteEffects.None, 0);
        }
        public void DrawText(Vector2 position, string text, Color color, float alpha = 1.0f)
        {
            font.Draw("ResultFont", position, text, color, alpha);
        }

        public Vector2 MeasureString(string text)
        {
            return font.MeasureString("ResultFont", text);
        }

        public void DrawTextureW(string name, Vector2 position, Color color, float rotate = 0, float size = 1, bool originFlg = false)//name position color rotate size
        {
            spriteBatch.Draw(textures[name], position, null, color, MathHelper.ToRadians(rotate), originFlg ? Radius(name) : Vector2.Zero, size, SpriteEffects.None, 0.0f);
        }

        public Vector2 Radius(string name)
        {
            return new Vector2(textures[name].Width / 2, textures[name].Height / 2);
        }
    }
}
