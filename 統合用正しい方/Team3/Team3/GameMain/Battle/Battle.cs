using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input; //make input can be use
using Microsoft.Xna.Framework;

namespace Team3
{
    class Battle 
    {
        private InputState input;
        
        private string[] enemy = { "weak_enemy_1_0", "weak_enemy_2_0", "boss_0" };
        //private string[] player = {"Player","Player2","Player3","Player4" };
        private int attackMode;
        private int playerX;
        private int playerY;


        /// <summary>
        /// 現在マウスの左ボタンを押しているか
        /// </summary>
        private bool nowMouseButtonPushed = false;

        /// <summary>
        /// 前回マウスの左ボタンを押しているか
        /// </summary>
        private bool previousMouseButtonPushed = false;


        private float alpha = 1.0f;
        private float x = 1.0f;
        private float y = 1.0f;
        private int enemyNo = 0;
        //private float vy = 0;
        //private bool attack = false;


        public Battle(InputState input)
        {
            this.input = input;
        }

        public void Initialize()
        {
            playerX = 750;
            attackMode = 5;
            enemyNo = 0;
            y = 1;
            x = 1;
            
        }

        public void Update()
        {
            //前回のマウスボタンの押下状態を記録
            this.previousMouseButtonPushed = this.nowMouseButtonPushed;

            //マウスの状態を取得
            MouseState mouseState = Mouse.GetState();

            //現在のマウスボタンの押下状態を記憶
            this.nowMouseButtonPushed = mouseState.LeftButton == ButtonState.Pressed;

            //攻撃モーション(マウスを押したら動く)
            switch (attackMode)
            {
                case 0:

                    playerY -= 5;
                    if (playerY <= -30)
                    {
                        attackMode = 1;
                    }

                    break;
                case 1:
                    playerY += 5;
                    if (playerY >= 0)
                    {
                        attackMode = 2;
                    }
                    break;
                case 2:
                    playerX -= 5;
                    if (playerX <= 700)
                    {
                        x = x - 0.1f;
                        attackMode = 3;
                    }
                    break;
                case 3:
                    playerX += 5;
                    if (playerX >= 750)
                    {
                        attackMode = 5;
                    }
                    break;
                default:
                    if (this.nowMouseButtonPushed && !this.previousMouseButtonPushed)
                    {
                        attackMode = 0;
                    }
                    break;

            }


            //２を押すと味方のHPが減る。
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                y = y - 0.01f;


            }


            //敵のHPがなくなったら敵のNOが変わる
            if (x <= 0)
            {
                alpha = alpha - 0.1f;
                if (alpha <= 0)
                {
                    x = 1.0f;
                    enemyNo = enemyNo + 1;
                    alpha = 1;
                }
            }
            

            //敵を全て倒したら 又は　味方のHPが無くなったら
            if (enemyNo >= enemy.Length || y <= 0)
            {
                //if (input.mouseClick()) { isEnd = true; }
                //Gameを終了
                //this.isEnd = true;
            }


        }

        public void Draw(Renderer graphics)
        {
            graphics.DrawTexture("main_battle_bg",Vector2.Zero);
            graphics.DrawTexture(enemy[enemyNo], new Vector2(60, 30), alpha);
            graphics.DrawTexture("weak_enemy_0_0", new Vector2(playerX, playerY + 15));
            graphics.DrawTexture("weak_enemy_0_0", new Vector2(750, 65));
            graphics.DrawTexture("weak_enemy_1_0", new Vector2(750, 115));
            graphics.DrawTexture("weak_enemy_2_0", new Vector2(750, 165));
            graphics.DrawTexture2("enemy_HP", new Vector2(20, 250), 1, x);
            graphics.DrawTexture2("Player_HP", new Vector2(530, 250), 1, y);

        }

    }
}
