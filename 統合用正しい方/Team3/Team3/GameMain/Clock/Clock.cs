using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Timers;

namespace Team3
{
    public class Clock
    {

        float shortRotate, longRotate;
        float huriko;
        float hurikospeed;
        float speed;
        float spinNum;
        bool missFlg;
        //動ける角度によって動く速さも角度も変わる。
        Sound sound;
        InputState input;
        DataClass data;
        public Clock(InputState input,Sound sound, DataClass data)
        {
            this.input = input;
            this.data = data;
            this.sound = sound;
        }

        public void Initialize()
        {
            shortRotate = DateTime.Now.Hour * 30 + DateTime.Now.Minute / 2;
            speed = 1.2f;
            hurikospeed = 1.2f;
            missFlg = false;
        }

        public void Update()
        {
          
            SpinHand(speed);
            Huriko();
            ClickCheck();
            if (missFlg)
            {
                speed += 0.2f;
                missFlg = false;
            }
           
            if (input.mouseRightClick())
            {
                data.PendulumClick = true;
            }


        }

        //時計の長い方の針の回転、何回周ったかをカウント
        private void SpinHand(float rotateSpeed)
        {
            longRotate += rotateSpeed;
            if (longRotate > 360)
            {
                longRotate = 0;
                spinNum++;
                sound.PlaySE("clock_one_rotation",1);
            }
        }

        private void Huriko()
        {
            huriko += hurikospeed;
            if (huriko < -30)
            {
                hurikospeed = speed;
                sound.PlaySE("pendulum",1);
            }
            else if (huriko > 30)
            {
                hurikospeed = -speed;
               sound.PlaySE("pendulum",1);
            }
        }
        //汚いソースの見本です
        private void ClickCheck()
        {
            if (input.mouseRightClick())
            {
                if (huriko >= 25 && huriko <= 30 || huriko <= -25 && huriko >= -30)
                {
                    data.PendulumClick = true;
                    sound.PlaySE("hand_change_true",1);
                }
                else
                {
                    missFlg = true;
                    sound.PlaySE("hand_change_false",1);  
                }

            }
            else
            {
                data.PendulumClick = false;
            }
          
        }


        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("main_clock_bg", new Vector2(0.0f, 0.0f));
            renderer.DrawSpin2("main_clock_swing", new Vector2(207.0f, 200.0f), huriko, new Vector2(14.0f, 0.0f)); //振り子
            renderer.DrawTexture("main_clock", new Vector2(105.0f, 5.0f));
            renderer.DrawSpin2("main_clock_short_hand", new Vector2(207.0f, 107.0f), shortRotate, new Vector2(9f, 58f)); //短い方
            renderer.DrawSpin2("main_clock_long_hand", new Vector2(205f, 105.0f), longRotate, new Vector2(13.5f, 85.0f)); //分針



        }
    }

}