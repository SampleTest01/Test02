using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Team3
{

    public enum Direction
    {
        Up = 1, Down, Left, Right,DownLeft,DownRight,UpLeft,UpRight
    }

    class Puzzle
    {
        /// <summary>
        /// クラスに落とせないかな？
        /// </summary>


        Random rand;
        InputState input;
        DataClass data;

        int WIDTH = 7, HEIGHT = 5; //横縦
        int SIZE = 70;　　       //ボールの直径

        int[,] hairetu;
        int[,] standby;
        int[,] moveYArray;
        int[,] moveYStandby;
        bool[,] alphaArray;
        int[] eraseNum;


        //追加分

        int[] ChainX;
        int[] ChainY;
        int[] ChainDir;
        int ChainNumber;
        int mx;
        int my;

        int nowhairetu;

        int m_MoveY;
        float alpha;
        bool m_InputFlg;
        bool testFlg;
        bool changeArrayFlg;

        EffectManager em;

        public Puzzle(InputState input,DataClass data)
        {
            this.input = input;
            this.data = data;
        }

        public void Initialize()
        {

            rand = new Random();
            ArrayInit();    //配列初期

            em = new EffectManager(rand,800);
            em.Initialize();
            //追加分
            ChainX = new int[100];
            ChainY = new int[100];
            ChainDir = new int[100];
            //まとめて消す数の初期化
            ChainNumber = 0;

            m_MoveY = 0;
            alpha = 1f;
            m_InputFlg = true;
            testFlg = true;
            changeArrayFlg = false;
        }

        public void ArrayInit()
        {
            hairetu = new int[WIDTH, HEIGHT];
            standby = new int[WIDTH, HEIGHT];
            moveYArray = new int[WIDTH, HEIGHT];
            moveYStandby = new int[WIDTH, HEIGHT];
            alphaArray = new bool[WIDTH, HEIGHT];
            eraseNum = new int[2];

            for (var x = 0; x < WIDTH; x++)
            {
                for (var y = 0; y < HEIGHT; y++)
                {
                    hairetu[x, y] = rand.Next(1, 5);
                    standby[x, y] = rand.Next(1, 5);
                    moveYArray[x, y] = 0;
                    moveYStandby[x, y] = 0;
                    alphaArray[x, y] = false;
                }
            }

        }

        private void TestSpecial()
        {
            //A押したら特殊な矢印(仮で出してる状態)
            if (input.IsKeyDown(Keys.A))
            {
                hairetu[3, 2] = 5;//特殊向き
                //特殊向きに沿って消せるように配置している
                hairetu[2, 2] = 3;
                hairetu[1, 2] = 3;
                hairetu[3, 3] = 1;
                hairetu[3, 4] = 1;
            }
        }

        public void Update()
        {
            em.Update();


            if (input.IsKeyDown(Keys.Enter))
                Initialize();
            //特殊な矢印チェック
            TestSpecial();

            
                //マウス処理できるか？
                if (m_InputFlg&&!data.AttackFlg)
                {
                    Clear(); //消す処理
                }
                else
                {
                    CheckErase();//個数チェック
                    MovePuzzle();//パズル移動
                    ChangeArray(); //配列書き換え処理
                }
            
        }

        private void ChangeBlock()
        {
            for (var x = 0; x < WIDTH; x++)
            {
                for (var y = 0; y < HEIGHT; y++)
                {
                    if (alphaArray[x, y])
                    {
                        if (data.PendulumClick)
                        {
                            hairetu[x,y] = 5;
                        }
                    }
                }
            }
        }

        #region ブロック系の処理

        #region つなぐ判定
        private void ClearCheck()
        {
            mx = input.mouseX() / SIZE;
            my = input.mouseY() / SIZE;

            if (mx >= WIDTH) //画面外に出たら
                return;
            if (my >= HEIGHT)//画面外に出たら
                return;
            if (mx < 0)
                return;
            if (my < 0)
                return;

            nowhairetu = hairetu[mx, my];

            //一つもつながってない か 繋がってる　　　　　　
            if (ChainNumber == 0 || (ChainNumber > 0 && (ChainX[ChainNumber - 1] != mx || ChainY[ChainNumber - 1] != my)))
            {

                //if文の中身が見にくいので新しく書き直しました ↓

                bool CheckStart = false;
                //最初ならチエック開始
                if (ChainNumber == 0)
                {
                    CheckStart = true;
                }
                //2回目以降
                else
                {
                    //向きが同じの場合
                    if (ChainDir[0] == hairetu[mx, my])
                    {
                        CheckStart = true;
                    }

                    // "main_down_left_hand"
                    //特殊向きの場合(右から下)  
                    if (ChainDir[0] == 4 && hairetu[mx, my] == 5)
                    {
                        CheckStart = true;
                    }
                    //特殊向きの場合(上から左)
                    if (ChainDir[0] == 1 && hairetu[mx, my] == 5)
                    {
                        CheckStart = true;
                    }

                    ////////////////////////////////////////////////////////////////

                    // "main_down_right_hand"
                    //特殊向きの場合(左から下)
                    if (ChainDir[0] == 3 && hairetu[mx, my] == 6)
                    {
                        CheckStart = true;
                    }
                    //特殊向きの場合(上から右)
                    if (ChainDir[0] == 1 && hairetu[mx, my] == 6)
                    {
                        CheckStart = true;
                    }

                    ////////////////////////////////////////////////////////////////

                    // "main_up_left_hand"
                    //特殊向きの場合(右から上)
                    if (ChainDir[0] == 4 && hairetu[mx, my] == 7)
                    {
                        CheckStart = true;
                    }
                    //特殊向きの場合(下から左)
                    if (ChainDir[0] == 2 && hairetu[mx, my] == 7)
                    {
                        CheckStart = true;
                    }

                    ////////////////////////////////////////////////////////////////

                    // "main_up_right_hand"
                    //特殊向きの場合(左から上)
                    if (ChainDir[0] == 3 && hairetu[mx, my] == 8)
                    {
                        CheckStart = true;
                    }
                    //特殊向きの場合(下から右)
                    if (ChainDir[0] == 2 && hairetu[mx, my] == 8)
                    {
                        CheckStart = true;
                    }
                }

                //条件を満たしたらチェック開始
                if (CheckStart == true)
                {
                    bool dirOK; //向きが同じか
                    dirOK = false;

                    //最初の場合
                    if (ChainNumber == 0)
                    {
                        dirOK = true;
                    }
                    else//2つ目を通った場合
                    {
                        if (ChainDir[0] == 1)//上向き
                        {
                            //現在位置が前の位置よりひとつ上ならば
                            if (my + 1 == ChainY[ChainNumber - 1] && mx == ChainX[ChainNumber - 1])
                            {
                                dirOK = true;
                                alphaArray[mx, my] = true;
                                foreach (var i in System.Linq.Enumerable.Range(0,10)) em.Add("MyEffect1", 7, 3, new Vector2((mx + 1) * 70 - 35, (my+ 1) * 70 - 35), new Color(rand.Next(256), rand.Next(256), rand.Next(256)), 2, 0.4f, 0, 1, true);
                            }
                        }
                        else if (ChainDir[0] == 2)//下向き
                        {
                            //現在位置が前の位置よりひとつ下ならば
                            if (my - 1 == ChainY[ChainNumber - 1] && mx == ChainX[ChainNumber - 1])
                            {
                                dirOK = true;
                                alphaArray[mx, my] = true;
                                foreach (var i in System.Linq.Enumerable.Range(0, 10)) em.Add("MyEffect1", 7, 3, new Vector2((mx + 1) * 70 - 35, (my+ 1) * 70 - 35), new Color(rand.Next(256), rand.Next(256), rand.Next(256)), 2, 0.4f, 0, 1, true);
                            }
                        }
                        else if (ChainDir[0] == 3)//左向き
                        {
                            //現在位置が前の位置よりひとつ左ならば
                            if (mx + 1 == ChainX[ChainNumber - 1] && my == ChainY[ChainNumber - 1])
                            {
                                dirOK = true;
                                alphaArray[mx, my] = true;
                                foreach (var i in System.Linq.Enumerable.Range(0, 10)) em.Add("MyEffect1", 7, 3, new Vector2((mx + 1) * 70 - 35, (my+ 1) * 70 - 35), new Color(rand.Next(256), rand.Next(256), rand.Next(256)), 2, 0.4f, 0, 1, true);
                            }
                        }
                        else if (ChainDir[0] == 4)//右向き
                        {
                            //現在位置が前の位置よりひとつ右ならば
                            if (mx - 1 == ChainX[ChainNumber - 1] && my == ChainY[ChainNumber - 1])
                            {
                                dirOK = true;
                                alphaArray[mx, my] = true;
                                foreach (var i in System.Linq.Enumerable.Range(0, 10)) em.Add("MyEffect1", 7, 3, new Vector2((mx + 1) * 70 - 35, (my+ 1) * 70 - 35), new Color(rand.Next(256), rand.Next(256), rand.Next(256)), 2, 0.4f, 0, 1, true);
                            }
                        }
                    }

                    if (dirOK)//方向が同じなら
                    {
                        //削除
                        ChainX[ChainNumber] = mx;
                        ChainY[ChainNumber] = my;
                        ChainDir[ChainNumber] = hairetu[mx, my];
                        ChainNumber++;
                        alphaArray[mx, my] = true;//透過処理
                        foreach (var i in System.Linq.Enumerable.Range(0, 10)) em.Add("MyEffect1", 7, 3, new Vector2((mx + 1) * 70 - 35, (my+ 1) * 70 - 35), new Color(rand.Next(256), rand.Next(256), rand.Next(256)), 2, 0.4f, 0, 1, true);

                        //特殊向き "main_down_left_hand"
                        if (hairetu[mx, my] == 5)
                        {
                            //右向きなら下向きにする
                            if (ChainDir[0] == 4)
                            {
                                ChainDir[0] = 2;
                            }

                            //上向きなら左向きにする
                            if (ChainDir[0] == 1)
                            {
                                ChainDir[0] = 3;
                            }
                        }

                        //特殊向き "main_down_right_hand"
                        if (hairetu[mx, my] == 6)
                        {
                            //左向きなら下向きにする
                            if (ChainDir[0] == 3)
                            {
                                ChainDir[0] = 2;
                            }

                            //上向きなら右向きにする
                            if (ChainDir[0] == 1)
                            {
                                ChainDir[0] = 4;
                            }
                        }

                        //特殊向き "main_up_left_hand"
                        if (hairetu[mx, my] == 7)
                        {
                            //右向きなら上向きにする
                            if (ChainDir[0] == 4)
                            {
                                ChainDir[0] = 1;
                            }

                            //下向きなら左向きにする
                            if (ChainDir[0] == 2)
                            {
                                ChainDir[0] = 3;
                            }
                        }

                        //特殊向き "main_up_right_hand"
                        if (hairetu[mx, my] == 8)
                        {
                            //左向きなら上向きにする
                            if (ChainDir[0] == 3)
                            {
                                ChainDir[0] = 1;
                            }

                            //下向きなら右向きにする
                            if (ChainDir[0] == 2)
                            {
                                ChainDir[0] = 4;
                            }
                        }
                    }
                }
                //同じ向きの矢印で消してるときに別の向きの矢印を通った場合最初からやり直しにする
                //ここをコメントにすれば同じ方向の矢印は消えるようになります
                //else
                //{
                //    ChainNumber = 0;
                //}
            }
        }

        private void Check()
        {
            if (ChainNumber == 1)
                ChainNumber = 0;

            if (ChainNumber <= 1)
                return;

            for (int i = 0; i < ChainNumber; i++)
            {
                {
                    hairetu[ChainX[i], ChainY[i]] = 0;
                }
            }

            ChainNumber = 0;
        }

    #endregion
    
        #region チェックシリーズ →クラス化
        //消えた個数チェック
        private void CheckErase()
        {
            int count = 0;
            int count2 = 0;
            for (var x = 0; x < WIDTH; x++)
            {
                for (var y = 0; y < HEIGHT; y++)
                {
                    if (CheckArrayZero(x, y))//alphaArray[x, y])
                    {
                        count++;
                        count2++;
                    }
                    eraseNum[1] = count2;
                }
                if (eraseNum[0] < count) eraseNum[0] = count;
                count = 0;
            }
            count2 = 0;
            //何個消したかをデータで渡すよ
            data.EraseNum = eraseNum[1];
        }
        //0が何個あるか調べるやつ
        private int CheckZero(int checkX, int checkY, bool all)
        {
            int num = 0;
            int yMAX = HEIGHT - 1;
            if (all)//全部の０の数を求める
            {
                for (var y = yMAX; y >= 0; y--)
                {
                    if (hairetu[checkX, y] == 0)    num++;
                }
            }
            else
            {
                //入れたYから繰り返す回数を調べる
                for (var y = yMAX; y > checkY; y--)
                {
                    if (hairetu[checkX, y] == 0)
                        num++;
                }
            }
            return num;

        }
        //配列が０かを確認するメソッド
        private bool CheckArrayZero(int x, int y)
        {
            if (hairetu[x, y] == 0)
                return true;
            return false;
        }
        //どのぐらい動かすかを調べるメソッド
        private void CheckMoveNum()
        {
            int yMAX = HEIGHT - 1;
            for (var x = 0; x < WIDTH; x++)
            {
                for (var y = yMAX - 1; y >= 0; y--)
                {

                    if (CheckArrayZero(x, y + 1))//もし下が0ならば
                    {
                        moveYArray[x, y] = CheckZero(x, y, false);//ココに下に０が何個あるか調べるメソッドを入れる
                    }
                    else
                    {
                        moveYArray[x, y] = moveYArray[x, y + 1];
                    }

                }
                //待機陣は全部動く
                for (var y = yMAX; y >= 0; y--)
                    moveYStandby[x, y] = CheckZero(x, y, true);
            }

        }
        #endregion


        private void Clear()　//ブロックの消す処理
        {
            for (var x = 0; x < WIDTH; x++)
            {
                for (var y = 0; y < HEIGHT; y++)
                {
                    if (input.MouseLong())
                    {
                        ClearCheck();
                        testFlg = false;
                    }
                    else if (!input.MouseLong() && !testFlg)　//離した時に操作できなくして、見かけ上のやつを動かす
                    {
                        Check();
                        m_InputFlg = false;//操作できなくする
                        data.AttackFlg = true;
                    }
                }
            }
        }

        private void MovePuzzle()
        {
            CheckMoveNum();
            m_MoveY += 5;

            if (m_MoveY > SIZE)
            {
                changeArrayFlg = true;
                m_MoveY = 0;
            }
        }
        //配列の書き換え
        private void ChangeArray()
        {
            if (changeArrayFlg)
            {
                int yMAX = HEIGHT - 1;
                for (var i = 0; i < eraseNum[0]; i++)
                {
                    for (var x = 0; x < WIDTH; x++)
                    {
                        for (var y = yMAX; y >= 0; y--)
                        {
                            if (CheckArrayZero(x, y))
                            {
                                if (y != 0)
                                {
                                    hairetu[x, y] = hairetu[x, y - 1];
                                    hairetu[x, y - 1] = 0;

                                }
                                else if (y == 0)
                                {//一番上は待機の一番下で、大気の一番下は移動したから０
                                    hairetu[x, 0] = standby[x, yMAX];
                                    standby[x, yMAX] = 0;
                                }
                            }
                        }
                    }

                    for (var x = 0; x < WIDTH; x++)
                    {
                        for (var y = yMAX; y >= 0; y--)
                        {
                            if (standby[x, y] == 0)
                            {
                                if (y != 0)
                                {
                                    standby[x, y] = standby[x, y - 1];
                                    standby[x, y - 1] = 0;
                                }
                                else if (y == 0)
                                {
                                    standby[x, 0] = rand.Next(1, 5);
                                }
                            }
                            alphaArray[x, y] = false;
                            moveYArray[x, y] = 0;
                            moveYStandby[x, y] = 0;
                        }
                    }
                }
                testFlg = true;
                changeArrayFlg = false;
                m_InputFlg = true;
            }
        }
        #endregion

        #region 書き出しシリーズ

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("main_pazzle_bg_0", Vector2.Zero);
            for (var x = 0; x < WIDTH; x++)
            {
                for (var y = 0; y < HEIGHT; y++)
                {
                    if (alphaArray[x, y])
                        alpha = 0.5f;
                    else
                        alpha = 1f;

                    if (moveYArray[x, y] != 0)//動く奴ら
                    {
                        DrawPazzle(hairetu, x, y, m_MoveY * moveYArray[x, y], renderer);
                    }
                    else
                    {
                        DrawPazzle(hairetu, x, y, 0, renderer, alpha);
                    }

                    if (moveYStandby[x, y] != 0)//standby側
                    {
                        DrawStandby(standby, x, y, m_MoveY * moveYStandby[x, y], renderer);
                    }
                }
            }
            em.Draw(renderer);
        }


        private void DrawPazzle(int[,] array, int x, int y, float moveY, Renderer renderer, float alpha = 1.0f)
        {
            switch (array[x, y])
            {
                case 1:
                    renderer.DrawTexture("main_up_hand", new Vector2(SIZE * x, SIZE * y + moveY), alpha);
                    break;
                case 2:
                    renderer.DrawTexture("main_down_hand", new Vector2(SIZE * x, SIZE * y + moveY), alpha);
                    break;
                case 3:
                    renderer.DrawTexture("main_left_hand", new Vector2(SIZE * x, SIZE * y + moveY), alpha);
                    break;
                case 4:
                    renderer.DrawTexture("main_right_hand", new Vector2(SIZE * x, SIZE * y + moveY), alpha);
                    break;
                case 5:
                    renderer.DrawTexture("main_down_left_hand", new Vector2(SIZE * x, SIZE * y + moveY), alpha);
                    break;
                    
            }
        }

    
        private void DrawStandby(int[,] array, int x, int y, float moveY, Renderer renderer)
        {
            switch (array[x, y])
            {
                case 1:
                    renderer.DrawTexture("main_up_hand", new Vector2(SIZE * x, (-SIZE * HEIGHT) + SIZE * y + moveY));
                    break;
                case 2:
                    renderer.DrawTexture("main_down_hand", new Vector2(SIZE * x, (-SIZE * HEIGHT) + SIZE * y + moveY));
                    break;
                case 3:
                    renderer.DrawTexture("main_left_hand", new Vector2(SIZE * x, (-SIZE * HEIGHT) + SIZE * y + moveY));
                    break;
                case 4:
                    renderer.DrawTexture("main_right_hand", new Vector2(SIZE * x, (-SIZE * HEIGHT) + SIZE * y + moveY));
                    break;
            }
        }

        private void Print(int[,] m)
        {
            for (var i = 0; i < HEIGHT; i++)
            {
                for (var k = 0; k < WIDTH; k++)
                {
                    Console.Write(m[k, i] + "  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        #endregion



    }
}
