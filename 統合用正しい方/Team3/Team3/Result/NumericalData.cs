using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Team3
{
    public class NumericalData
    {

        private int maxItem;
        private int item;
        private int maxExp;
        private int exp;
        private Sound sound;

        public NumericalData(Sound sound)
        {

            maxItem = 00001000;
            item = 0;
            maxExp = 00000500;
            exp = 0;
            this.sound = sound;
        }
        public NumericalData(Sound sound,int maxItem,int maxExp)
        {

            this.maxItem = maxItem;
            this.maxExp = maxExp;
            item = 0;
            exp = 0;
            this.sound = sound;
        }

        public void Initialize()
        {
            maxItem = 1000;
            item = 0;
            maxExp = 500;
            exp = 0;
        }

        public void Initialize(int maxItem,int maxExp)
        {
            this.maxItem = maxItem;
            item = 0;
            this.maxExp = maxExp;
            exp = 0;
        }

        public void Updata(InputState input)
        {
            int dExp = exp;
            int dItem = item;
            if (maxExp > exp)
                exp = exp + 1;

            if (maxItem > item)
                item = item + 1;

            if(dExp != exp || dItem == item)
                sound.PlaySE("result_num_up", 0.01f);

            if (input.mouseClick())
            {
                exp = maxExp;
                item = maxItem;
            }
        }

        public void Draw(Renderer renderer,float alpha)
        {
            //アイテム数値
            renderer.DrawText(new Vector2(Screen.Width / 2 + 50, 420), item.ToString().PadLeft(15), Color.Black, alpha);
            renderer.DrawText(new Vector2(Screen.Width / 2 + 50, 470), exp.ToString().PadLeft(15), Color.Black, alpha);

        }

        public bool Finish()
        {
            if (!(maxExp > exp || maxItem > item))
            {
                return true;
            }
            return false;
        }
    }
}
