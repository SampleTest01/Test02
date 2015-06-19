using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Team3
{
  public  class Title :Scene
    {
      EScene nextScene = EScene.Select;

      bool gamef = false;   //"gamestart"の点滅を繰り返すフラグ
      float galpha;  //"gamestart"のalpha値

      bool titlef = false;  //シーン切り替え時のフェードアウトのフラグ
      float talpha;  //"title"のalpha値

      bool rotatef = false; //rotate値を動かす時のフラグ
      float rotate;    //"gamestart"のrotate値
      float heightr;   //"gamestart"のHeightを揺らす

      Random rnd;
      float shakingwidth;   //揺れ幅

      public Title(InputState input, Sound sound,DataClass data)
          : base(input,sound,data)
      {
      }

      public override void Initialize()
      {
          nextScene = EScene.Select;

          rnd = new Random();
          galpha = 1.0f;
          talpha = 1.0f;
          rotate = 0f;
          heightr = 0f;

          isEnd = false;
          titlef = false;
      }

      public override void Update()
      {
          if (input.mouseClick())
          {
              titlef = true;
          }

          if (titlef == true)
          {
              talpha = talpha - 0.02f;
              galpha = galpha - 0.02f;
          }

          if (talpha <= 0)
          {
              isEnd = true;
          }

          shakingwidth = rnd.Next(20, 36);

          if (gamef == false && titlef == false)
          {
              galpha = galpha + 0.01f;
              if (galpha >= 1)
              {
                  gamef = true;
              }
          }
          else if (gamef == true)
          {
              galpha = galpha - 0.01f;
              if (galpha <= 0)
              {
                  gamef = false;
              }
          }

          if (rotatef == false)
          {
              rotate++;
              if (rotate >= shakingwidth)
              {
                  rotatef = true;
              }
          }
          else if (rotatef == true)
          {
              rotate--;
              if (rotate <= -shakingwidth)
              {
                  rotatef = false;
              }
          }
          rotate = rotate * 0.96f;

          heightr = heightr + rotate / 6;
          if (heightr > 160)
          {
              rotatef = true;
          }
          else if (heightr < 10)
          {
              rotatef = false;
          }
      }

      public override void Draw(Renderer renderer)

      {
          renderer.DrawTexture("title_bg_0", new Vector2(0.0f, 0.0f), talpha);
          renderer.DrawTexture("title_logo", new Vector2(200.0f, 50.0f), talpha);
          renderer.DrawSpin("title_click_start", new Vector2(Screen.Width / 2, Screen.Height - 80 - heightr),MathHelper.ToRadians(rotate), new Vector2(215.0f, 32.5f), galpha);
      }

      public override EScene Next()
      {
          return nextScene;
      }

    }
}
