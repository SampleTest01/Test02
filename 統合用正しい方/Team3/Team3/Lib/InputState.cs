using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Team3
{
    public class InputState
    {
        //Title
        private bool currentStartKey = false;
        private bool previosStartKey = false;

        //GameMain
        private KeyboardState currentKey;   // 現在（最新）のキー
        private KeyboardState previousKey;  // 一つ前に押されたキー
        private Vector2 velocity = Vector2.Zero; // 移動速度

        private MouseState mouseState = Mouse.GetState();
        bool press;

        public InputState()
        {
        }

        //Title
        public bool IsStartButtenDown()
        {
            return currentStartKey && !previosStartKey;
        }

        private void UpdateStartKey(KeyboardState KeyStart1)
        {
            previosStartKey = currentStartKey;
            currentStartKey = KeyStart1.IsKeyDown(Keys.Space);
        }

        //GameMain
        public bool IsKeyDown(Keys key)
        {
            bool current = currentKey.IsKeyDown(key);
            bool previous = previousKey.IsKeyDown(key);
            return current && !previous;
        }
        private void UpdateKey(KeyboardState KeyState)
        {
            previousKey = currentKey;
            currentKey = KeyState;
        }

        //更新
        public void Update()
        {
            var KeyState = Keyboard.GetState(); // キーボードの状態
            UpdateKey(KeyState);                // 押されているキーの更新
            UpdateStartKey(KeyState);
            this.mouseState = Mouse.GetState();
        }

        public bool mouseClick()
        {
            if (press)
                if (mouseState.LeftButton == ButtonState.Released)
                {
                    press = false;
                    return true;
                }
                else
                    return false;
            else
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    press = true;

                return false;
            }

        }

        public bool mouseRightClick()
        {
            if (press)
                if (mouseState.RightButton == ButtonState.Released)
                {
                    press = false;
                    return true;
                }
                else
                    return false;
            else
            {
                if (mouseState.RightButton == ButtonState.Pressed)
                    press = true;

                return false;
            }

        }
        public bool MouseLong()
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }

        public int mouseX()
        {
            int x = Mouse.GetState().X;
            if (x < 0)
            {
                x = 0;
            }

            return x;
        }

        public int mouseY()
        {
            int y = Mouse.GetState().Y - 250;
            if (y > 600 + 35)
                y = 600 + 35;
            return y;
        }
    }
}


