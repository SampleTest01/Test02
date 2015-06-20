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
        private CharacterString CS = new CharacterString();
        private GetMonster monster;
        private NumericalData numericalData;

        public Result(InputState input, Sound sound,DataClass data)
            : base(input, sound,data)
        {

        }

        public override void Initialize()
        {
            isEnd = false;
            
            alpha = 0.0f;
            
            grea = new Gear(sound);
            CS = new CharacterString();
            
            numericalData = new NumericalData(sound);
            numericalData.Initialize();

            List<string> Smonster = new List<string>()
            {
                "weak_enemy_1_0","weak_enemy_2_0","weak_enemy_0_0","weak_enemy_1_0","weak_enemy_2_0",
                "weak_enemy_0_0","weak_enemy_1_0","weak_enemy_2_0","weak_enemy_0_0","weak_enemy_2_0",
            };
            monster = new GetMonster(Smonster,sound);
        }

        public override void Update()
        {
            grea.Update();
            sound.PlayBGM("result_bgm");

            if (alpha < 1)  //透明度上げる①
            {
                alpha = alpha + 0.01f;
            }
            else if (monster.DrawEnd == false)　//モンスター描画
            {
                monster.Update(input);
                
            }
            else if (!numericalData.Finish()) // 経験値とアイテムの数値増やす
            {
                numericalData.Updata(input);
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

            CS.Draw(renderer, alpha);

            numericalData.Draw(renderer, alpha);

            monster.Draw(renderer);
        }
        public override EScene Next()
        {
            return EScene.Select;
        }
    }
}
