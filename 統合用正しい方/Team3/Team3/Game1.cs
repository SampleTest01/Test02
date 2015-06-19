using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Team3
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        InputState input;
        Renderer renderer;
        Sound sound;
        DataClass data;

        Dictionary<EScene, Scene> scenes = new Dictionary<EScene, Scene>();
        Scene currentScene = null;//現在のシーン

        Vector2 positionCursor;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Screen.Width;
            graphics.PreferredBackBufferHeight = Screen.Height;
            Content.RootDirectory = "Content";
        }

        //初期化
        protected override void Initialize()
        {
            renderer = new Renderer(Content, GraphicsDevice);
            sound = new Sound(Content);
            input = new InputState();
            data = new DataClass();
            positionCursor = Vector2.Zero;
            //シーン定義
            scenes[EScene.Title] = new Title(input, sound,data);
            scenes[EScene.Select] = new Select(input, sound,data);
            scenes[EScene.GameMain] = new GameMain(input, sound, graphics,data);
            scenes[EScene.Result] = new Result(input, sound,data);

            currentScene = scenes[EScene.Title];

            currentScene.Initialize();
            base.Initialize();
        }


        //LoadContent
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            List<string> title = new List<string>()
            {   //たいとる
                "title_bg_0","title_click_start","title_logo",
            };
            List<string> select = new List<string>()
            { 
                //セレクト
                "ant","ant2","ant3","h","menubg"
            };
            List<string> puzzle = new List<string>()
            {
                //パズル部分の素材
                "main_up_hand","main_down_hand","main_left_hand","main_right_hand","main_pazzle_bg_0",
                "main_up_right_hand","main_up_left_hand","main_down_right_hand","main_down_left_hand",
            };
            List<string> battle = new List<string>()
            {
                //せんとー
                "weak_enemy_0_0", "weak_enemy_1_0","weak_enemy_2_0","boss_0",
                "enemy_HP","Player_HP","main_battle_bg"
            };
            List<string> clock = new List<string>()
            {
                //時計
                "main_clock_long_hand","main_clock_short_hand","main_clock","main_clock_swing","main_clock_bg",
            };
            List<string> result = new List<string>()
            {
                //リザルト
                "result_expoint","result_gear","result_separator_line","result_word_frame","result"
            };
            LoadTexture(title, "./title/");
            LoadTexture(select, "./select/");
            LoadTexture(puzzle, "./puzzle/");
            LoadTexture(battle, "./Battle/");
            LoadTexture(clock, "./Clock/");
            LoadTexture(result, "./Result/");

            renderer.LoadTexture("MyEffect1");
            renderer.LoadTexture("MyEffect2");

            renderer.LoadTexture("cursor");

            renderer.LoadFont("ResultFont");
        }

        private void LoadTexture(List<string> contentName,string path)
        {
            foreach (var s in contentName) renderer.LoadTexture(s,path);
        }

        protected override void UnloadContent()
        {
        }


        //更新
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) { this.Exit(); }

            MouseState m = Mouse.GetState();
            positionCursor = new Vector2(m.X,m.Y);

            input.Update();
            currentScene.Update();

            if (currentScene.IsEnd())//Scene end check
            {
                currentScene = scenes[currentScene.Next()];//Next scene
                currentScene.Initialize();
            }
            base.Update(gameTime);
        }


        //描画
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: ここに描画コードを追加します。
            renderer.Begin();
            currentScene.Draw(renderer);
            renderer.DrawTexture("cursor",positionCursor);
            renderer.End();

            base.Draw(gameTime);
        }
    }
}
