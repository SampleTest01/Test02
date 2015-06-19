using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Team3
{
    public class Gear
    {
        float rotate = 0.0f;
        float angle = 0.0f;
        Vector2 origin = new Vector2(142.0f, 142.0f);
        public Gear()
        {

        }

        public void Initialize()
        {

        }

        public void Update()
        {
            angle = angle + 1.0f;
            if (angle % 360 == 0 && angle != 0)
            { angle = 0; }

            rotate = angle * (float)Math.PI / 180;

        }
        
        public void Draw(Renderer renderer)
        {
            renderer.DrawSpin("result_gear", new Vector2(100, 100), -rotate, origin);
            renderer.DrawSpin("result_gear", new Vector2(Screen.Width - 100, 100), rotate, origin);
        }
    }
}
