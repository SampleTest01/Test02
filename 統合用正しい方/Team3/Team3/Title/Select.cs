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
       EScene nextScene = EScene.GameMain;
       public Select(InputState input,Sound sound)
          : base(input,sound)
      {
      }

       public override void Initialize()
       {
       }

       public override void Update()
       {
       }

       public override void Draw(Renderer renderer)
       {
       }

       public override EScene Next()
       {
           return nextScene;
       }
    }
}
