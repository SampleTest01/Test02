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

        private List<Player> playerList;
        private List<Enemy> enemyList;

        /// <summary>
        /// 現在マウスの左ボタンを押しているか
        /// </summary>
        private bool nowMouseButtonPushed = false;

        /// <summary>
        /// 前回マウスの左ボタンを押しているか
        /// </summary>
        private bool previousMouseButtonPushed = false;

        public Battle(InputState input)
        {
            this.input = input;
            playerList = new List<Player>();

            playerList.Add(new Player("weak_enemy_0_0", null));
            playerList.Add(new Player("weak_enemy_1_0", null));
            playerList.Add(new Player("weak_enemy_2_0", null));

            enemyList = new List<Enemy>();

            enemyList.Add(new Enemy("weak_enemy_2_0", null));
            enemyList.Add(new Enemy("weak_enemy_1_0", null));
            enemyList.Add(new Enemy("weak_enemy_2_0", null));
            
        }

        public void Initialize()
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                playerList[i].Initialize(playerList.Count, i);
            }
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Initialize(enemyList.Count, i);
            }
        }

        public void Update()
        {
            MouseUpdate();

            for (int i = 0; i < playerList.Count; i++)
            {
                playerList[i].Updata(mouseclick());
            }
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Updata(mouseclick());
            }
        }

        public void Draw(Renderer graphics)
        {
            graphics.DrawTexture("main_battle_bg",Vector2.Zero);

            for (int i = 0; i < playerList.Count; i++)
            {
                playerList[i].Draw(graphics);
            }
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Draw(graphics);
            }

            graphics.DrawTexture2("enemy_HP", new Vector2(20, 250), 1, 1);
            graphics.DrawTexture2("Player_HP", new Vector2(530, 250), 1, 1);

        }

        public void MouseUpdate()
        {
            //前回のマウスボタンの押下状態を記録
            this.previousMouseButtonPushed = this.nowMouseButtonPushed;

            //マウスの状態を取得
            MouseState mouseState = Mouse.GetState();

            //現在のマウスボタンの押下状態を記憶
            this.nowMouseButtonPushed = mouseState.LeftButton == ButtonState.Pressed;
        }
        public bool mouseclick()
        {
            if (nowMouseButtonPushed)
            {
                return true;
            }
            return false;
        }
    }
}
