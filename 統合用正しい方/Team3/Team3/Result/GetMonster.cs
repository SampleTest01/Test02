using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Team3
{
    public class GetMonster
    {
        private List<float> alpha;
        private List<string> monster;
        private int No = 0;
        private bool drawEnd;

        public GetMonster(List<string> monster)
        {
            this.monster = monster;
            alpha = new List<float>();
            for (int i = 0; i < monster.Count; i++)
            { alpha.Add(0); }
            drawEnd = false;
        }

        public void Initialize(List<string> monster)
        {
            this.monster = monster;
            alpha = new List<float>();
            for (int i = 0; i < monster.Count; i++)
            { alpha.Add(0); }
            drawEnd = false;
            No = 0;
        }

        public void Update()
        {
            if (No < alpha.Count)
            {
                alpha[No] = alpha[No] + 0.03f;
                if (alpha[No] >= 1)
                { No = No + 1; }
            }
            else
            {
                drawEnd = true;
            }
        }

        public void Draw(Renderer renderer)
        {
            for (int i = 0; i < monster.Count; i++)
            {
                if (i > 4)
                {
                    renderer.DrawTexture(monster[i], new Vector2(60 + (i - 5) * 65, 400 + 70),alpha[i]);
                }
                else
                {
                    renderer.DrawTexture(monster[i], new Vector2(60 + i * 65, 400),alpha[i]);
                }
            }
        }
        public bool DrawEnd
        {
            get
            {
                return drawEnd;
            }
        }

        public void AllDraw()
        {
            for (int i = 0; i < alpha.Count; i++)
            {
                alpha[i] = 1;
            }
            drawEnd = true;
        }
    }
}
