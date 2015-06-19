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
        private float ts = 0;
        private float tm = 0;
        private float tms = 0;

        int speed;
        
        int s = DateTime.Now.Second;  //秒針
        int m = DateTime.Now.Minute;  //分針
        int h = DateTime.Now.Hour;    //時針
        float ms = DateTime.Now.Millisecond;
        int huriko = 27;

        public Clock(InputState input)
        {
        }

        public void Initialize()
        {   
        }

        public void Update()
        {
            this.s = DateTime.Now.Second;                   //ウィンドウの時間 second
            this.ts = this.s * 6 + 180;                     // 秒針の角度
            this.m = DateTime.Now.Minute;                   //ウィンドウの時間 minite
            this.tm = this.m * 6 + 180;                     //分針の角度
            this.ms = DateTime.Now.Millisecond;             //ウィンドウの時間 minite
            this.tms = this.ms * 0.036f + ts * 6 + 180;     //分針の角度

            this.huriko += speed;    // 振り子が動ける角度。振り子の基本位置より大きくなければ動くない。
            if (huriko < 40)　　　　
                speed++;
            else if (huriko > 140)
                speed--;
    
        }
     
        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("main_clock", new Vector2(156.0f,0.0f));    // 時計
            renderer.DrawSpin("clock_long_hand", new Vector2(256.0f, 100.0f),  this.tms, new Vector2(12.5f,0.0f)); //分針
            renderer.DrawSpin("clock_short_hand", new Vector2(256.0f, 100.0f), this.tm, new Vector2(12.5f,0.0f)); //秒針
            renderer.DrawSpin("main_clock_swing", new Vector2(256.0f, 200.0f), this.huriko, new Vector2(62.0f, 40.0f)); //振り子

        }
    }
    
    }