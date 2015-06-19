using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Team3
{
   public class Select:Scene
    {
       EScene nextScene = EScene.GameMain;//切り替え先

       //Clock clock;
       int step;      
       float alpha;

       //float scale;
       Vector2 position;
       int scale_flg;

       List<Monster> monster = new List<Monster>() { };
       public Select(InputState input,Sound sound,DataClass data)
          : base(input,sound,data)
       {
       }

       public override void Initialize()
       {
           nextScene = EScene.GameMain;

           position = new Vector2(350.0f,500.0f);
           alpha = 0;
           step = 0;
           //scale = 1.0f;
           monster.Clear();
           //clickAnime = false;
           scale_flg = 0;
           isEnd = false;
       }

       public bool Scale_flg()
       {
           if (scale_flg > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }

       public override void Update()
       {
           SelectScene nowScene = SelectScene.Main;

           switch (nowScene)
           {
               case SelectScene.Main:
                   break;
               case SelectScene.Sub:
                   break;
           }

           switch (step)
           {
               case 0:
                   {
                       foreach (var i in Enumerable.Range(0, 3))
                           monster.Add(Roll(i));
                       step++;
                       break;
                   }
               case 1:
                   {
                       if (input.IsStartButtenDown()) { isEnd = true; } //シーンの切り替え

                       if (position.X < 40 && position.Y < 50)
                       {
                           scale_flg = 1;
                       }

                       //マウスの左クリックで回転
                       if (Rolling())
                       {
                           if (input.mouseClick())        
                           {
                               for (var i = 0; i < monster.Count; i++)
                               {
                                   monster[i].LeftrollFlag(i);
                               }
                           }
                           if (input.mouseClick())
                           {
                               for (var i = 0; i < monster.Count; i++)
                               {
                                   monster[i].RightrollFlag(i);
                               }
                           }
                       }

                       if (monster[0].GetTime() > 20 && monster[0].GetRightroll())
                       {
                           monster.Add(monster[0]);
                           monster.RemoveAt(0);
                           monster.ForEach(s => s.Defflag());
                       }

                       if (monster[0].GetTime() > 20 && monster[0].GetLeftroll())
                       {
                           monster.Insert(0, monster[monster.Count - 1]);
                           monster.RemoveAt(monster.Count - 1);
                           monster.ForEach(s => s.Defflag());
                       }

                       monster.ForEach(s => s.Update());

                       break;
                   }
           }
       }

       public Monster Roll(int i)
       {
           float a = 0;
           if (i == 2)
               a = 100;

           if (i == 0 || i == 1)
               alpha = 1.5f;
           else if (i == 2)
               alpha = 0.5f;

           string name = "";
           if (i == 2)
               name = "ant2";
           if (i == 0)
               name = "ant";
           if (i == 1)
               name = "ant";
           Monster monster = new Monster(name, new Vector2( a,100 + i), alpha);
           return monster;
       }

       public bool Rolling()
       {
           bool state = true;
           foreach (var i in monster)
           {
               if (i.GetRightroll() || i.GetLeftroll())
                   state = false;
           }
           return state;
       }

       public override void Draw(Renderer renderer)
       {
           renderer.DrawTexture("menubg",Vector2.Zero);
           //if (input.mouseClick())
           //{
               if (Scale_flg() == false)
               {
                   renderer.DrawTexture("ant3", new Vector2(0, 0));
               }
           //}

           renderer.DrawTexture("h", new Vector2(350, 500));
           monster.ForEach(s => s.Draw(renderer));

       }

       public override EScene Next()
       {
           return nextScene;
       }
    }
}

public enum SelectScene
{
    Main,Sub
}