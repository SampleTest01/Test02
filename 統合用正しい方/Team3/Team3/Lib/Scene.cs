using System;
using Microsoft.Xna.Framework;

namespace Team3
{
    public abstract class Scene
    {
        protected GraphicsDeviceManager graphics;
        protected InputState input;
        protected Sound sound;
        protected DataClass data;
        protected bool isEnd = false;

        public Scene(InputState input, Sound sound,DataClass data)
        {
            this.input = input;
            this.sound = sound;
            this.data = data;
        }

        public abstract void Initialize();

        public abstract void Update();

        public abstract void Draw(Renderer renderer);

        public bool IsEnd() { return isEnd; }

        public abstract EScene Next();
    }
}
