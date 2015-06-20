using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Team3
{
    class CharacterString
    {
        public CharacterString()
        {


        }
        public void Draw(Renderer renderer,float alpha)
        {
            //文字枠
            renderer.DrawTexture("result_word_frame", new Vector2(25, Screen.Height / 2));
            renderer.DrawTexture("result_word_frame", new Vector2(Screen.Width - 400 - 25, Screen.Height / 2));
            //区切り線
            renderer.DrawTexture("result_separator_line", new Vector2(Screen.Width / 2 - 25, 250));

            //文字列の幅の半分を取得
            float itemP = renderer.MeasureString("獲得アイテム").X / 2;
            float monsterP = renderer.MeasureString("獲得モンスター").X / 2;
            float questP = renderer.MeasureString("クエスト報酬").X / 2;

            //文字列描画
            renderer.DrawText(new Vector2(225 - monsterP, Screen.Height / 2 + 25), "獲得モンスター", Color.Black, alpha);
            renderer.DrawText(new Vector2(Screen.Width / 2 + 225 - itemP, Screen.Height / 2 + 25), "獲得アイテム", Color.Black, alpha);
            renderer.DrawText(new Vector2(Screen.Width / 2 - questP, Screen.Height / 4), "クエスト報酬", Color.Black, alpha);

            //経験値アイコン
            renderer.DrawTexture("result_expoint", new Vector2(Screen.Width / 2 + 50, 460), alpha);
        }
    }
}
