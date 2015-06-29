using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Team3
{
    class SearchPannel
    {
        int WIDTH, HEIGHT;
        bool[,] erasePattern;
        bool specialFlg;
        bool changeFlg;

        public SearchPannel(int WIDTH, int HEIGHT)
        {
            this.WIDTH = WIDTH;
            this.HEIGHT = HEIGHT;
        }

        #region 最短探し
        //ココをチェックさせる

        public void Initialize()
        {
            erasePattern = new bool[WIDTH, HEIGHT];
            for (var xx = 0; xx < WIDTH; xx++)
            {
                for (var yy = 0; yy < HEIGHT; yy++)
                {
                    erasePattern[xx, yy] = false;
                }
            }
            specialFlg = false;
        }

        public void Update(int changeX, int changeY, int[,] hairetu)
        {
            changeFlg = false;
            UpStart(changeX, changeY, hairetu);
            DownStart(changeX, changeY, hairetu);
            LeftStart(changeX, changeY, hairetu);
            RightStart(changeX, changeY, hairetu);
            ChangePattern(changeX, changeY, hairetu);
            specialFlg = false;
        }
        private void UpStart(int cx, int cy, int[,] hairetu)
        {
            if (cy == 0 || changeFlg)
                return;
            //上が↓の場合
            if (hairetu[cx, cy - 1] == 2 )
            {
                //どのパターンでもない場合は抜ける
                if (cx == 0 && (hairetu[cx + 1, cy] != 4 && hairetu[cx + 1, cy] != (int)Dir.UpLeft && hairetu[cx + 1, cy] != (int)Dir.DownLeft)
                    || cx == WIDTH - 1 && (hairetu[cx - 1, cy] != 3) && hairetu[cx - 1, cy] != (int)Dir.UpRight && hairetu[cx - 1, cy] != (int)Dir.DownRight)
                    return;

                //一番端側の完全確定パターン
                if (cx == 0 && cy == HEIGHT - 1 && hairetu[cx + 1, cy] == 4)
                {
                    hairetu[cx, cy] = 8;
                    changeFlg = true;
                    return;
                }
                else if (cx == WIDTH - 1 && cy == HEIGHT - 1 && hairetu[cx - 1, cy] == 3)
                {
                    hairetu[cx, cy] = 7;
                    changeFlg = true;
                    return;
                }
                XStart(1, cx, cy, hairetu, false);
            }
            else if (hairetu[cx, cy - 1] == (int)Dir.DownLeft || hairetu[cx, cy - 1] == (int)Dir.DownRight)
            {
                //どのパターンでもない場合は抜ける
                if (cx == 0 && hairetu[cx + 1, cy] != 4 || cx == WIDTH - 1 && hairetu[cx - 1, cy] != 3)
                    return;

                //一番端側の完全確定パターン
                if (cx == 0 && cy == HEIGHT - 1 && hairetu[cx + 1, cy] == 4)
                {
                    hairetu[cx, cy] = 8;
                    changeFlg = true;
                    return;
                }
                else if (cx == WIDTH - 1 && cy == HEIGHT - 1 && hairetu[cx - 1, cy] == 3)
                {
                    hairetu[cx, cy] = 7;
                    changeFlg = true;
                    return;
                }

                XStart(1, cx, cy, hairetu, true);
            }
        }
        private void DownStart(int cx, int cy, int[,] hairetu)
        {
            if (cy == HEIGHT - 1 || changeFlg)
                return;

            //下が↑の場合
            if (hairetu[cx, cy + 1] == 1)
            {
                //どのパターンでもない場合は抜ける
                if (cx == 0 && (hairetu[cx + 1, cy] != 4 && hairetu[cx + 1, cy] != (int)Dir.UpLeft && hairetu[cx + 1, cy] != (int)Dir.DownLeft)
                    || cx == WIDTH - 1 && (hairetu[cx - 1, cy] != 3) && hairetu[cx - 1, cy] != (int)Dir.UpRight && hairetu[cx - 1, cy] != (int)Dir.DownRight)
                    return;


                //完全確定パターン
                //一番端側の完全確定パターン
                if (cx == 0 && cy == 0 && hairetu[cx + 1, cy] == 4)
                {
                    hairetu[cx, cy] = 6;
                    changeFlg = true;
                    return;
                }
                else if (cx == WIDTH - 1 && cy == 0 && hairetu[cx - 1, cy] == 3)
                {
                    hairetu[cx, cy] = 5;
                    changeFlg = true;
                    return;
                }
                XStart(2, cx, cy, hairetu, false);
            }
            else if (hairetu[cx, cy + 1] == (int)Dir.UpLeft || hairetu[cx, cy + 1] == (int)Dir.UpRight)
            {
                //どのパターンでもない場合は抜ける
                if (cx == 0 && hairetu[cx + 1, cy] != 4 || cx == WIDTH - 1 && hairetu[cx - 1, cy] != 3)
                    return;

                //一番端側の完全確定パターン
                if (cx == 0 && cy == 0 && hairetu[cx + 1, cy] == 4)
                {
                    hairetu[cx, cy] = 6;
                    changeFlg = true;
                    return;
                }
                else if (cx == WIDTH - 1 && cy == 0 && hairetu[cx - 1, cy] == 3)
                {
                    hairetu[cx, cy] = 5;
                    changeFlg = true;
                    return;
                }
                else
                    XStart(2, cx, cy, hairetu, true);

            }
        }
        private void XStart(int dir, int cx, int cy, int[,] hairetu, bool special)
        {
            if (cx == 6)
            {
                if (hairetu[cx - 1, cy] == (int)Dir.DownRight || hairetu[cx - 1, cy] == (int)Dir.UpRight)
                {
                    specialFlg = true;
                    changeFlg = true;
                    if (dir == 1)
                        hairetu[cx, cy] = (int)Dir.UpLeft;
                    else
                        hairetu[cx, cy] = (int)Dir.DownLeft;
                }
                else if (cx == WIDTH - 1 && hairetu[cx - 1, cy] == 3)
                {
                    //左抜けパターン
                    erasePattern[dir, 3] = true;
                }
            }
            else if (cx == 0)
            {
                //最優先
                if (hairetu[cx + 1, cy] == (int)Dir.DownLeft || hairetu[cx + 1, cy] == (int)Dir.UpLeft)
                {
                    changeFlg = true;
                    specialFlg = true;
                    if (dir == 1)
                        hairetu[cx, cy] = (int)Dir.UpRight;
                    else
                        hairetu[cx, cy] = (int)Dir.DownRight;
                }
                else if (cx == 0 && hairetu[cx + 1, cy] == 4)
                {
                    erasePattern[dir, 4] = true;
                }

            }
            else if (hairetu[cx - 1, cy] == (int)Dir.DownRight || hairetu[cx - 1, cy] == (int)Dir.UpRight)
            {
                changeFlg = true;
                specialFlg = true;
                if (dir == 1)
                    hairetu[cx, cy] = (int)Dir.UpLeft;
                else
                    hairetu[cx, cy] = (int)Dir.DownLeft;
            }
            else if (hairetu[cx + 1, cy] == (int)Dir.DownLeft || hairetu[cx + 1, cy] == (int)Dir.UpLeft)
            {
                changeFlg = true;
                specialFlg = true;
                if (dir == 1)
                    hairetu[cx, cy] = (int)Dir.UpRight;
                else
                    hairetu[cx, cy] = (int)Dir.DownRight;
            }
            //比較される可能性があるパターン 右抜けパターン
            else if (hairetu[cx - 1, cy] != 3 && hairetu[cx + 1, cy] == 4)
            {
                erasePattern[dir, 4] = true;
            }
            else if (hairetu[cx + 1, cy] != 4 && hairetu[cx - 1, cy] == 3)
            {
                //左抜けパターン
                erasePattern[dir, 3] = true;
            }

            if (special && cx != 6 && cx != 0)
            {
                if (hairetu[cx - 1, cy] == 3 && hairetu[cx + 1, cy] == 4)
                {
                    if (Left(cx - 1, cy, hairetu) > Right(cx + 1, cy, hairetu))
                    {
                        if (dir == 1)
                            hairetu[cx, cy] = (int)Dir.UpLeft;
                        else
                            hairetu[cx, cy] = (int)Dir.DownLeft;
                    }
                    else
                    {
                        if (dir == 1)
                            hairetu[cx, cy] = (int)Dir.UpRight;
                        else
                            hairetu[cx, cy] = (int)Dir.DownRight;
                    }
                    changeFlg = true;
                }
            }
            else if (!special && cx != 6 && cx != 0)
            {
                if (hairetu[cx - 1, cy] == 3 && hairetu[cx + 1, cy] == 4)
                    erasePattern[dir, 0] = true;
            }
        }

        private void LeftStart(int cx, int cy, int[,] hairetu)
        {
            //3パターンチェック
            if (cx == 0 || changeFlg)
                return;
            if (hairetu[cx - 1, cy] == 4)
            {
                //一番上かつ、下が↓じゃなけりゃ終わり
                if (cy == 0 && (hairetu[cx, cy + 1] != 2 && hairetu[cx, cy + 1] != (int)Dir.DownRight && hairetu[cx, cy + 1] != (int)Dir.DownLeft)
        || cy == HEIGHT - 1 && (hairetu[cx, cy - 1] != 1 && hairetu[cx, cy - 1] != (int)Dir.UpRight && hairetu[cx, cy - 1] != (int)Dir.UpLeft))
                    return;


                if (cx == WIDTH - 1 && cy == HEIGHT - 1 && hairetu[cx, cy - 1] == 1)
                {
                    hairetu[cx, cy] = 7;
                    changeFlg = true;
                    return;
                }
                else if (cx == WIDTH - 1 && cy == 0 && hairetu[cx, cy + 1] == 2)
                {
                    hairetu[cx, cy] = 5;
                    changeFlg = true;
                    return;
                }
                YStart(3, cx, cy, hairetu, false);
            }
            else if (hairetu[cx - 1, cy] == (int)Dir.DownRight || hairetu[cx - 1, cy] == (int)Dir.UpRight)
            {
                if (cx == WIDTH - 1 && cy == HEIGHT - 1 && hairetu[cx, cy - 1] == 1)
                {
                    hairetu[cx, cy] = 7;
                    changeFlg = true;
                    return;
                }
                else if (cx == WIDTH - 1 && cy == 0 && hairetu[cx, cy + 1] == 2)
                {
                    hairetu[cx, cy] = 5;
                    changeFlg = true;
                    return;
                }

                YStart(3, cx, cy, hairetu, true);

            }
        }
        private void RightStart(int cx, int cy, int[,] hairetu)
        {
            //3パターンチェック
            if (cx == WIDTH - 1|| changeFlg)
                return;
            if (hairetu[cx + 1, cy] == 3)
            {
                //一番上かつ、下が↓じゃなけりゃ終わり
                if (cy == 0 && (hairetu[cx, cy + 1] != 2 && hairetu[cx, cy + 1] != (int)Dir.DownRight && hairetu[cx, cy + 1] != (int)Dir.DownLeft)
        || cy == HEIGHT - 1 && (hairetu[cx, cy - 1] != 1 && hairetu[cx, cy - 1] != (int)Dir.UpRight && hairetu[cx, cy - 1] != (int)Dir.UpLeft))
                    return;


                //完全確定パターン
                if (cx == 0 && cy == 0 && hairetu[cx, cy + 1] == 2)
                {
                    hairetu[cx, cy] = 6;
                    changeFlg = true;
                    return;
                }
                else if (cx == 0 && cy == HEIGHT - 1 && hairetu[cx, cy - 1] == 1)
                {
                    hairetu[cx, cy] = 8;
                    changeFlg = true;
                    return;
                }
                YStart(4, cx, cy, hairetu, false);
            }
            else if (hairetu[cx + 1, cy] == (int)Dir.DownLeft || hairetu[cx + 1, cy] == (int)Dir.UpLeft)
            {
                //一番上かつ、下が↓じゃなけりゃ終わり
                if (cy == 0 && hairetu[cx, cy + 1] != 2 || cy == HEIGHT - 1 && hairetu[cx, cy - 1] != 1)
                    return;

                //完全確定パターン
                if (cx == 0 && cy == 0 && hairetu[cx, cy + 1] == 2)
                {
                    hairetu[cx, cy] = 6;
                    changeFlg = true;
                    return;
                }
                else if (cx == 0 && cy == HEIGHT - 1 && hairetu[cx, cy - 1] == 1)
                {
                    hairetu[cx, cy] = 8;
                    changeFlg = true;
                    return;
                }
                YStart(4, cx, cy, hairetu, true);

            }
        }
        private void YStart(int dir, int cx, int cy, int[,] hairetu, bool special)
        {
            if (cy == 0)
            {
                if (hairetu[cx, cy + 1] == (int)Dir.UpLeft || hairetu[cx, cy + 1] == (int)Dir.UpRight)
                {
                    changeFlg = true;
                    specialFlg = true;
                    if (dir == 3)
                        hairetu[cx, cy] = (int)Dir.DownLeft;
                    else
                        hairetu[cx, cy] = (int)Dir.DownRight;
                }
                else if (hairetu[cx, cy + 1] == 2)
                {
                    erasePattern[dir, 2] = true;
                }
            }
            else if (cy == 4)
            {
                if (hairetu[cx, cy - 1] == (int)Dir.DownRight || hairetu[cx, cy - 1] == (int)Dir.DownLeft)
                {
                    changeFlg = true;
                    specialFlg = true;
                    if (dir == 3)
                        hairetu[cx, cy] = (int)Dir.UpLeft;
                    else
                        hairetu[cx, cy] = (int)Dir.UpRight;
                }
                else if (hairetu[cx, cy - 1] == 1)
                {
                    erasePattern[dir, 1] = true;
                }
            }
            else if (hairetu[cx, cy - 1] == (int)Dir.DownRight || hairetu[cx, cy - 1] == (int)Dir.DownLeft)
            {
                changeFlg = true;
                specialFlg = true;
                if (dir == 3)
                    hairetu[cx, cy] = (int)Dir.UpLeft;
                else
                    hairetu[cx, cy] = (int)Dir.UpRight;
            }
            else if (hairetu[cx, cy + 1] == (int)Dir.UpLeft || hairetu[cx, cy + 1] == (int)Dir.UpRight)
            {
                changeFlg = true;
                specialFlg = true;
                if (dir == 3)
                    hairetu[cx, cy] = (int)Dir.DownLeft;
                else
                    hairetu[cx, cy] = (int)Dir.DownRight;
            }
            //↓のみつながってる場合は個数を確認する
            else if (hairetu[cx, cy - 1] != 1 && hairetu[cx, cy + 1] == 2)
            {
                erasePattern[dir, 2] = true;
            }
            //↑のみつながってる場合は
            else if (hairetu[cx, cy + 1] != 2 && hairetu[cx, cy - 1] == 1)
            {
                erasePattern[dir, 1] = true;
            }


            if (special && cy != 4 && cy != 0)
            {
                if (hairetu[cx, cy + 1] == (int)Dir.Down && hairetu[cx, cy - 1] == (int)Dir.Up)
                {
                    if (Up(cx, cy - 1, hairetu) > Down(cx, cy + 1, hairetu))
                    {
                        if (dir == 3)
                            hairetu[cx, cy] = (int)Dir.UpLeft;
                        else
                            hairetu[cx, cy] = (int)Dir.UpRight;
                    }
                    else
                    {
                        if (dir == 3)
                            hairetu[cx, cy] = (int)Dir.DownLeft;
                        else
                            hairetu[cx, cy] = (int)Dir.DownRight;
                    }
                }
                changeFlg = true;
            }
            else if (!special && cy != 4 && cy != 0)
            {
                if (hairetu[cx, cy + 1] == 2 && hairetu[cx, cy - 1] == 1)
                {
                    erasePattern[dir, 0] = true;
                }
            }


        }
        private void ChangePattern(int cx, int cy, int[,] hairetu)
        {
            if (!specialFlg)
            {
                if (erasePattern[1, 4] && !erasePattern[3, 2])
                {
                    hairetu[cx, cy] = 8;
                }
                else if (!erasePattern[1, 4] && erasePattern[3, 2])
                {
                    hairetu[cx, cy] = 5;
                }
                else if (erasePattern[1, 3] && !erasePattern[4, 2])
                {
                    hairetu[cx, cy] = 7;
                }
                else if (!erasePattern[1, 3] && erasePattern[4, 2])
                {
                    hairetu[cx, cy] = 6;
                }
                else if (erasePattern[2, 4] && !erasePattern[3, 1])
                {
                    hairetu[cx, cy] = 6;
                }
                else if (!erasePattern[2, 4] && erasePattern[3, 1])
                {
                    hairetu[cx, cy] = 7;
                }
                else if (erasePattern[2, 3] && !erasePattern[4, 1])
                {
                    hairetu[cx, cy] = 5;
                }
                else if (!erasePattern[2, 3] && erasePattern[4, 1])
                {
                    hairetu[cx, cy] = 8;
                }
                else
                {
                    CheckDoublePattern(cx, cy, hairetu);
                }
            }
        }
        private void CheckDoublePattern(int cx, int cy, int[,] hairetu)
        {
            int checkcLeftX = cx - 1;
            int checkRightX = cx + 1;
            int checkUpY = cy - 1;
            int checkcDownY = cy + 1;
            //複合パターンの確認
            if (erasePattern[1, 0])
            {
                //上入の左右パターン
                if (Left(checkcLeftX, cy, hairetu) > Right(checkRightX, cy, hairetu))
                    hairetu[cx, cy] = (int)Dir.UpLeft;
                else
                    hairetu[cx, cy] = (int)Dir.UpRight;

            }
            else if (erasePattern[2, 0])
            {
                //下入りの左右パターン
                if (Left(checkcLeftX, cy, hairetu) > Right(checkRightX, cy, hairetu))
                    hairetu[cx, cy] = (int)Dir.DownLeft;
                else
                    hairetu[cx, cy] = (int)Dir.DownRight;

            }
            else if (erasePattern[3, 0])
            {
                //左入りの上下パターン
                if (Down(cx, checkcDownY, hairetu) > Up(cx, checkUpY, hairetu))
                    hairetu[cx, cy] = (int)Dir.DownLeft;
                else
                    hairetu[cx, cy] = (int)Dir.UpLeft;

            }
            else if (erasePattern[4, 0])
            {
                //右入り上下パターン

                if (Down(cx, checkcDownY, hairetu) > Up(cx, checkUpY, hairetu))
                    hairetu[cx, cy] = (int)Dir.DownRight;
                else
                    hairetu[cx, cy] = (int)Dir.UpRight;

            }
            //ここから複合ダブりパターン、単体バージョン
            else if (erasePattern[1, 4] && erasePattern[3, 2])
            {

                if (Right(checkRightX, cy, hairetu) > Down(cx, checkcDownY, hairetu))
                    hairetu[cx, cy] = 8;
                else
                    hairetu[cx, cy] = 5;
            }
            else if (erasePattern[1, 3] && erasePattern[4, 2])
            {
                //下と左
                if (Left(checkcLeftX, cy, hairetu) > Down(cx, checkcDownY, hairetu))
                    hairetu[cx, cy] = 7;
                else
                    hairetu[cx, cy] = 6;
            }
            else if (erasePattern[2, 4] && erasePattern[3, 1])
            {
                //上と右  
                if (Up(cx, checkUpY, hairetu) > Right(checkRightX, cy, hairetu))
                    hairetu[cx, cy] = 7;
                else
                    hairetu[cx, cy] = 6;
            }
            else if (erasePattern[2, 3] && erasePattern[4, 1])
            {
                //上と左
                if (Up(cx, checkUpY, hairetu) > Left(checkcLeftX, cy, hairetu))
                    hairetu[cx, cy] = 8;
                else
                    hairetu[cx, cy] = 5;
            }

        }
        #region 何個あるか見るやつ
        //上の数を見る
        private int Up(int cx, int checky, int[,] hairetu)
        {
            int up = 0;
            int cy = checky;
            while (hairetu[cx, cy] == 1)
            {
                up++;
                cy--;
                if (cy < 0)
                    break;
            }
            return up;
        }
        //下の数を見る
        private int Down(int cx, int checky, int[,] hairetu)
        {
            int down = 0;
            int cy = checky;
            while (hairetu[cx, cy] == 2)
            {
                cy++;
                down++;
                if (cy > HEIGHT - 1)
                    break;
            }
            return down;
        }
        //左に行く
        private int Left(int checkx, int cy, int[,] hairetu)
        {
            int left = 0;
            int cx = checkx;
            while (hairetu[cx, cy] == 3)
            {
                cx--;
                left++;
                if (cx < 0)
                    break;
            }
            return left;
        }
        //右に行く
        private int Right(int checkx, int cy, int[,] hairetu)
        {
            int right = 0;
            int cx = checkx;
            while (hairetu[cx, cy] == 4)
            {
                cx++;
                right++;
                if (cx > WIDTH - 1)
                    break;

            }
            return right;
        }

        #endregion

        #endregion
    }
}
