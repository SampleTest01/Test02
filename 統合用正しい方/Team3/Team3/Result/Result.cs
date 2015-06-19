using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Team3
{
    public class Result:Scene
    {
        private Gear grea;
        private float alpha;
        private int maxItem;
        private int item;
        private int maxExp;
        private int exp;

        private GetMonster monster;

        public Result(InputState input, Sound sound,DataClass data)
            : base(input, sound,data)
        {

        }

        public override void Initialize()
        {
            isEnd = false;
            grea = new Gear();
            alpha = 0.0f;
            maxItem = 00001000;
            item = 0;
            maxExp = 00000500;
            exp = 0;

            List<string> Smonster = new List<string>()
            {
                "weak_enemy_1_0","weak_enemy_2_0","weak_enemy_0_0","weak_enemy_1_0","weak_enemy_2_0","weak_enemy_0_0","weak_enemy_1_0","weak_enemy_2_0","weak_enemy_0_0","weak_enemy_2_0",
            };
            monster = new GetMonster(Smonster);
        }

        public override void Update()
        {
            grea.Update();

            if (alpha < 1)  //透明度上げる①
            {
                alpha = alpha + 0.01f;
            }
            else if (monster.DrawEnd == false)　//モンスター描画
            {
                monster.Update();
                if (input.mouseClick())
                {
                    monster.AllDraw();
                }
            }
            else if (maxExp > exp || maxItem > item) // 経験値とアイテムの数値増やす
            {
                if (maxExp > exp)
                    exp = exp + 1;

                if (maxItem > item)
                    item = item + 1;
                if (input.mouseClick())
                {
                    exp = maxExp;
                    item = maxItem;
                }
            }
            else
            {
                if (input.mouseClick())　//シーン移行
                {
                    isEnd = true;
                }
            }
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture("menubg", Vector2.Zero);

            //resultがわかりやすいように表示しているだけです。
            renderer.DrawTexture("result", new Vector2(0, 0));

            grea.Draw(renderer);

            //文字枠
            renderer.DrawTexture("result_word_frame", new Vector2(25, Screen.Height/2));
            renderer.DrawTexture("result_word_frame", new Vector2(Screen.Width - 400 - 25, Screen.Height / 2));
            //区切り線
            renderer.DrawTexture("result_separator_line", new Vector2(Screen.Width /2 - 25, 250));
            
            //文字列の幅の半分を取得
            float itemP = renderer.MeasureString("獲得アイテム").X / 2;
            float monsterP = renderer.MeasureString("獲得モンスター").X / 2;
            float questP = renderer.MeasureString("クエスト報酬").X/2;

            //文字列描画
            renderer.DrawText(new Vector2(225 - monsterP                    , Screen.Height / 2 + 25),   "獲得モンスター", Color.Black, alpha);
            renderer.DrawText(new Vector2(Screen.Width / 2 + 225 - itemP    , Screen.Height / 2 + 25),   "獲得アイテム", Color.Black, alpha);
            renderer.DrawText(new Vector2(Screen.Width / 2 - questP         , Screen.Height / 4),        "クエスト報酬", Color.Black, alpha);

            //アイテム数値
            renderer.DrawText(new Vector2(Screen.Width / 2 + 50, 420), item.ToString().PadLeft(15), Color.Black, alpha);
            renderer.DrawText(new Vector2(Screen.Width / 2 + 50, 470), exp.ToString().PadLeft(15), Color.Black, alpha);
            //経験値アイコン
            renderer.DrawTexture("result_expoint",new Vector2(Screen.Width/2+50,460),alpha);

            monster.Draw(renderer);
        }
        public override EScene Next()
        {
            return EScene.Select;
        }
    }
}
