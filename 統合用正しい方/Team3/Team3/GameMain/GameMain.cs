using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;


namespace Team3
{
  public  class GameMain :Scene
    {
      Viewport[] viewport;

      Puzzle puzzle;//追加
      Battle battle;
      Clock clock;
     

      public GameMain(InputState input,Sound sound,GraphicsDeviceManager graphics,DataClass data)
          : base(input,sound,data)
      {
          this.graphics = graphics;
      }

      public override void Initialize()
      {
          SetViewport();
         
          //追加
          puzzle = new Puzzle(input,data);
          battle = new Battle(input);
          clock = new Clock(input,data);
          puzzle.Initialize();
          battle.Initialize();
          clock.Initialize();

          isEnd = false;
      }

      private void SetViewport()
      {  //分割初期化
          viewport = new Viewport[4]; //分割戻しの為４つ

          //横、縦設定
          viewport[0].Width = 900;
          viewport[0].Height = 250;

          viewport[1].Width = 490;
          viewport[1].Height = 350;
          //tokei
          viewport[2].Width = 410;
          viewport[2].Height = 350;

          //ポジションの設定
          viewport[0].X = 0;
          viewport[0].Y = 0;

          viewport[1].X = 0;
          viewport[1].Y = 250;

          viewport[2].X = 490;
          viewport[2].Y = 250;


          viewport[3].Height = Screen.Height;
          viewport[3].Width = Screen.Width;
          viewport[3].X = viewport[3].Y = 0;

      }

      public override void Update()
      {
          if (input.IsStartButtenDown())
          {
              isEnd = true;
          }
          //追加
          puzzle.Update();
          battle.Update();
          clock.Update();
      }

      public override void Draw(Renderer renderer)
      {
          //配列０
          graphics.GraphicsDevice.Viewport = viewport[0];
          battle.Draw(renderer);


          renderer.End();
          renderer.Begin();

          //配列１
          graphics.GraphicsDevice.Viewport = viewport[1];
          puzzle.Draw(renderer);


          renderer.End();
          renderer.Begin();

          //配列２
          graphics.GraphicsDevice.Viewport = viewport[2];
          clock.Draw(renderer);

          renderer.End();

          renderer.Begin();
          graphics.GraphicsDevice.Viewport = viewport[3];
          

      }

      public override EScene Next()
      {
          return EScene.Result;
      }

    }
}
