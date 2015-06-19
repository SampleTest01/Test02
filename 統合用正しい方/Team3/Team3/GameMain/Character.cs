using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Team3
{
   public abstract class Character
    {
       protected Vector2 position;
       protected Vector2 radius;
       protected bool deadFlg = false, isDead;
       protected Sound sound;

       public Character(Vector2 position, Sound sound)
       {
           this.position = position;
           this.sound = sound;
           isDead = false;
       }

       public abstract void Initialize();

       public abstract void Updata();

       public abstract void Draw(Renderer renderer);

       public virtual void Dead(int DeadEffectFlg = 0) { }

       public bool IsDead()
       {
           return isDead;
       }

       public Vector2 Position
       {
           get { return position; }
       }

       public Vector2 Radius
       {
           get { return radius; }
       }

       public bool DeadFlg
       {
           get { return deadFlg; }
       }

    }
}
