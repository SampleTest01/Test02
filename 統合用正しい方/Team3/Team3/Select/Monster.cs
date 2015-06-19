using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Team3
{
    public class Monster
    {
        Vector2 position;
        Vector2 firstPosition, anchorPosition, endPosition;
        float bezietimer = 0;

        string name;
        float alpha;
        bool rightroll, leftroll;
        int count, anmcount;
        int Monster1;

        // InputState input,, Sound sound
        public Monster(string name,Vector2 position,float alpha)
        {
            this.name = name;
            this.position = position;
            this.alpha = alpha;
        }

        public void Initialize()
        {
            rightroll = false;
            leftroll = false;
            count = 0;
            anmcount = 0;

            //回転の初期
            firstPosition = Vector2.Zero;
            anchorPosition = Vector2.Zero;
            endPosition = Vector2.Zero;

        }

        public void Update()
        {
            if (rightroll)
                UpRightroll();

            if (leftroll)
                UpLeftroll();
            anmcount++;
        }

        //右回転
        public void UpRightroll()
        {
            if (count < 20)
            {
                firstPosition = (1 - bezietimer) * (1 - bezietimer) * firstPosition +
                                    2 * (1 - bezietimer) * bezietimer * anchorPosition +
                                        bezietimer * bezietimer * endPosition;

                if (bezietimer < 1) { bezietimer +=0.1f; }
            }
            else
            {
                bezietimer = 0;
                if (Monster1 == 0)
                {
                    position = new Vector2(150,180); alpha = 0.8f;
                }
                else if (Monster1 == 1)
                {
                    position = new Vector2(550,180); alpha = 0.8f;
                }
                else if (Monster1 == 2)
                {
                    position = new Vector2(350,260); alpha = 1.0f;
                }

            }
            count++;
        }

        public int GetTime()
        {
            return count;
        }

        //左回転
        public void UpLeftroll()
        {
            if (count < 20)
            {
                firstPosition = (1 - bezietimer) * (1 - bezietimer) * firstPosition +
                                    2 * (1 - bezietimer) * bezietimer * anchorPosition +
                                        bezietimer * bezietimer * endPosition;

                if (bezietimer < 1) { bezietimer += 0.02f; }
            }
            else
            {
                if (Monster1 == 0)
                {
                    position = new Vector2(150,180); alpha = 0.5f;
                }
                else if (Monster1 == 1)
                {
                    position = new Vector2(550,180); alpha = 0.5f;
                }
                else if (Monster1 == 2)
                {
                    position = new Vector2(350,260); alpha = 0.8f;
                }

            }
            count++;
        }


        public void RightrollFlag(int i)
        {
            rightroll = true;
            bezietimer = 0;
            Monster1 = i;

            firstPosition = position;

            if(i == 0)
            {
                endPosition = new Vector2(550, 180); anchorPosition = new Vector2((firstPosition.X + endPosition.X) /2, (firstPosition.Y + endPosition.Y) / 2);
            }
            else if(i == 1)
            {
                endPosition = new Vector2(350, 260); anchorPosition = new Vector2((firstPosition.X + endPosition.X) / 2, (firstPosition.Y + endPosition.Y) / 2);
            }
            else if (i == 2)
            {
                endPosition = new Vector2(150, 180); anchorPosition = new Vector2((firstPosition.X + endPosition.X) / 2, (firstPosition.Y + endPosition.Y) / 2);
            }
        }


        public void LeftrollFlag(int i)
        {
            leftroll = true;
            bezietimer = 0;
            Monster1 = i;

            firstPosition = position;

            if (i == 0)
            {
                endPosition = new Vector2(350, 260); anchorPosition = new Vector2((firstPosition.X + endPosition.X) / 2, (firstPosition.Y + endPosition.Y) / 2);
            }
            else if (i == 1)
            {
                endPosition = new Vector2(550, 180); anchorPosition = new Vector2((firstPosition.X + endPosition.X) / 2, (firstPosition.Y + endPosition.Y) / 2);
            }
            else if (i == 2)
            {
                endPosition = new Vector2(150, 180); anchorPosition = new Vector2((firstPosition.X + endPosition.X) / 2, (firstPosition.Y + endPosition.Y) / 2);
            }
        }

        public void Draw(Renderer renderer)
        {
            //renderer.DrawTexture("ant", firstPosition);
            //renderer.DrawTexture("ant", new Vector2(600.0f, 200.0f));
            //renderer.DrawTexture("ant2", new Vector2(400.0f, 270.0f));
            renderer.DrawTexture(name, position, alpha);
        }

        public void Defflag()
        {
            rightroll = false;
            leftroll = false;
            count = 0;
        }

        public bool GetRightroll()
        {
            return rightroll;
        }
        public bool GetLeftroll()
        {
            return leftroll;
        }
        public string Getname()
        {
            return name;
        }

    }
}

