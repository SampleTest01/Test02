using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Team3
{
    /*-----------------------------------------------------------------------------------------------------*/
    /*---------------------------------------- EffectManager Class ----------------------------------------*/
    /*-----------------------------------------------------------------------------------------------------*/
    class EffectManager
    {
        private int effectMax , nowEffectAdd;
        private Particle[] particles;
        private Random rnd;

        public EffectManager(Random rnd ,int effectMax = 800)
        {
            this.effectMax = effectMax;
            this.rnd = rnd;
            nowEffectAdd = 0;
            particles = new Particle[effectMax];
        }

        public int teee()
        {
            return nowEffectAdd;
        }

        public void Initialize()
        {
            particles = new Particle[effectMax];
            nowEffectAdd = 0;
        }

        public void Add(string name,int mode , float speed , Vector2 pos , Color color ,  float delTime = -900 , float alpha = 0.6f , float wait = 0 , float size = 1 , bool rotateFlg = false)//粒子追加用関数
        {
            if (delTime != -900) delTime *= 60;
            particles[nowEffectAdd] = new Particle(name,this,mode, speed , pos , rnd , color , delTime , alpha , wait , size , rotateFlg);
            nowEffectAdd++;
            if (nowEffectAdd >= effectMax) nowEffectAdd = 0;
        }

        public void Update()//アップデート
        {
            for (var i = 0; i < particles.Length; i++)
            {
                if(particles[i] != null && !particles[i].IsDead()) particles[i].Update();
            }
        }

        public void Draw(Renderer renderer)
        {
            for (var i = 0; i < particles.Length; i++)
            {
                if (particles[i] != null && !particles[i].IsDead()) particles[i].Draw(renderer);
            }
        }
    }
    /*------------------------------------------------------------------------------------------------*/
    /*---------------------------------------- Particle Class ----------------------------------------*/
    /*------------------------------------------------------------------------------------------------*/
    class Particle
    {
        private string name;
        private EffectManager em;
        private int mode;
        private Vector2 position , velocity;
        private float GRAVITY;//粒子の落下速度
        private Color color;
        private float alpha, speed , size , rotate = 0;
        private bool isDead = false;
        private Random rnd;


        float aa = 0;

        private bool sideFlg , sizeFlg = false , rotateFlg;
        private float ChangeTime = 0.5f , delTime , wait;

        public Particle(string name , EffectManager em , int mode, float speed , Vector2 position, Random rnd, Color color , float delTime ,  float alpha , float wait , float size , bool rotateFlg)
        {
            this.name = name;
            this.em = em;
            this.speed = speed;
            this.mode = mode;
            this.position = position;
            this.rnd = rnd;
            this.velocity = -GetVelocity(mode);
            this.delTime = delTime;
            this.alpha = alpha;
            this.wait = wait * 60;
            this.size = size;
            this.rotateFlg = rotateFlg;

            sideFlg = velocity.X >= 0 ? true : false;

            this.color = color;
            GRAVITY = Setting.Gravity[mode];
        }

        public void Update()
        {
            if(rotateFlg) rotate += 5;

            position += velocity;
            if (wait <= 0) velocity.Y += GRAVITY;
            else { wait--; velocity.Y += GRAVITY / 4; }

            if (mode == 2)
            {
                velocity += -GetVelocity(mode) / 100;
            }
            //雪だけ動きが独特な感じになるねん
            else if (mode == 3 || mode == 5)
            {
                if (Math.Abs(velocity.X) > 0.5 + ChangeTime)
                {
                    velocity.Y = -GetVelocity(mode).Y;
                    ChangeTime = (float)rnd.NextDouble();
                    sideFlg = !sideFlg;
                }

                velocity.X += sideFlg ? 0.01f : -0.01f;
            }
            //はなび
            else if (mode == 6)
            {
                em.Add("MyEffect2", 8 , speed , position , color , 1.0f, 0.4f, 0 , 1, true);
            }
            else if (mode == 7)
            {
                em.Add("MyEffect2", 8, speed, position, color, 0.7f, alpha - 0.3f, 900 , 1 , true);
            }
            //風っぽい感じに
            else if (mode == 9)
            {
                if (size > 5) sizeFlg = true;
                else if (size <= 2) sizeFlg = false;
                size += sizeFlg ? -0.03f : 0.08f;
                velocity.X = -speed - rotate / 100;
                position.Y += (float)Math.Sin(aa * Math.PI / 180) * speed + 1;
                position.X += (float)Math.Cos(aa * Math.PI / 180) * speed + 2;
                aa += 5;
                em.Add("MyEffect3", 8, speed, position, color, 1f , 0.12f , 0 , size);
            }
            else if (mode == 11)
            {
                position.X += speed;
            }
            //デリート処理
            DeadCheck();
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTextureW(name, position , color * alpha , rotate , size , true);
        }

        public void DeadCheck()
        {
            if (mode == 1 || mode == 6 || mode == 10)
            {
                if (position.Y > Setting.height)
                {
                    isDead = true;
                }
            }
            else if (mode == 2)
            {
                if (position.Y < -100 || position.Y >= Setting.height + 100 || position.X < -100 || position.X > Setting.width + 100)
                {
                    isDead = true;
                }
            }
            else if (mode == 3 || mode == 5)
            {
                if (position.Y < -100 || position.Y >= Setting.height || position.X < 0 || position.X > Setting.width)
                {
                    isDead = true;
                }
            }
            else if (mode == 7)
            {
                if (position.Y < 0 || position.Y >= Setting.height || position.X < 0 || position.X > Setting.width)
                {
                    isDead = true;
                }
            }
            else if (mode == 9)
            {
                if (position.X < 0) isDead = true;
            }
            else
            {
                if (position.Y < -200 || position.Y >= Setting.height + 200 || position.X < -200 || position.X > Setting.width + 200)
                {
                    isDead = true;
                }
            }

            if (delTime != -900)
            {
                delTime--;
                if (alpha >= delTime / 100) alpha -= alpha / 50;
                if (delTime <= 0)
                {
                    if (mode == 6)
                    {
                        List<Color> cs = new List<Color>() { Color.Red , Color.Yellow , Color.SkyBlue , Color.Crimson , Color.Orange , Color.Green , Color.Violet };
                        int c = rnd.Next(cs.Count);
                        foreach (var i in Enumerable.Range(0, 20)) em.Add("MyEffect1" , 2, 2 , position, Color.Yellow , 1 , 0.5f);
                        foreach (var i in Enumerable.Range(10, 30)) em.Add("MyEffect2", 7, 2 + (float)rnd.NextDouble(), position, cs[c] , -900, 0.7f, 55 / i, 1, true);//cs[rnd.Next(cs.Count)] new Color(rnd.Next(256),rnd.Next(256),rnd.Next(256))
                    }
                    isDead = true;
                }
            }
        }

        public Vector2 GetVelocity(int mode)
        {
            Vector2 velocity = Vector2.Zero;
            float angle = 0;

            switch (mode)
            {
                case 0:
                    angle = MathHelper.ToRadians(rnd.Next(360));
                    velocity = new Vector2(0, (float)Math.Sin(angle));
                    velocity *= speed;
                    return velocity;

                case 1:
                    angle = MathHelper.ToRadians(rnd.Next(70, 110));
                    velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    velocity.Y *= speed * 4;
                    velocity.X *= speed;
                    return velocity;

                case 2:
                    angle = MathHelper.ToRadians(rnd.Next(360));
                    velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    velocity *= speed;
                    return velocity;

                case 3:
                    angle = MathHelper.ToRadians(rnd.Next(210, 330));
                    velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    velocity.X *= speed;
                    return velocity;

                case 4:
                    angle = MathHelper.ToRadians(rnd.Next(30, 150));
                    velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    velocity.X *= speed;
                    return velocity;

                case 5:
                    angle = MathHelper.ToRadians(rnd.Next(30, 150));
                    velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    velocity.X *= speed;
                    return velocity;

                case 6:
                    angle = MathHelper.ToRadians(rnd.Next(85, 95));
                    velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    velocity.Y *= speed;
                    velocity.X *= speed;
                    return velocity;

                case 7:
                    angle = MathHelper.ToRadians(rnd.Next(360));
                    velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    velocity *= speed;
                    return velocity;

                case 9:
                    angle = MathHelper.ToRadians(rnd.Next(170,180));
                    velocity = -new Vector2((float)Math.Cos(angle), (float)Math.Sin(MathHelper.ToRadians(rnd.Next(-1,2))));
                    velocity *= speed;
                    return velocity;

                case 10:
                    angle = MathHelper.ToRadians(rnd.Next(360));
                    velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    velocity *= speed;
                    return velocity;

                default: return velocity;
            }
        }

    }
    /*-----------------------------------------------------------------------------------------------*/
    /*---------------------------------------- Setting Class ----------------------------------------*/
    /*-----------------------------------------------------------------------------------------------*/
    static class Setting
    {
        static public int width = Screen.Width;
        static public int height = Screen.Height;

        static public List<float> Gravity;

        static Setting()
        {
            Gravity = new List<float>()
            {
                0.05f,// 0
                0.22f , // 1
                0.01f , // 2
                0.000001f , // 3
                -0.01f, // 4
                -0.000001f, // 5
                -0.02f,// 6
                0.05f, // 7
                0,// 8
                0,// 9
                0.07f,// 10
                0 , //11
            };
        }
    }
}
/*
 *      パーティクル説明
 * 0.ただの落下玉
 * 1.噴水
 * 2.全方向発射
 * 3.ダウン雪
 * 4.アップライト
 * 5.アップ雪
 * 6.花火上昇玉
 * 7.花火拡散玉
 * 8.花火弾道
 * 9.風
*/