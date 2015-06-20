﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Team3
{
   public class Player : Character
    {

       public Player(string name, Sound sound)
           : base(name, sound)
       {
           position = Vector2.Zero;
           defaultPosition = position;
       }

       public Player(string name, Sound sound,Status status)
           : base(name, sound,status)
       {
           position = Vector2.Zero;
           defaultPosition = position;
       }

       public override void Initialize(int count, int no)
       {
           float y = this.HeightCalc(count, no);

           position = new Vector2(Screen.Width - 120, y);
           defaultPosition = position;
           attackDirection = -1;
       }

       public override void Updata(bool oneRotation)
       {

           if (attackMotion || oneRotation)
           {
               Attack(oneRotation);
           }
       }

       public override void Draw(Renderer renderer)
       {
           //ポジションに画像の左上が来るようになってます
           renderer.DrawTexture(name, position);
       }
    }
}
