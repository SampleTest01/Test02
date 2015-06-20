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
       protected Vector2 defaultPosition;
       protected string name;

       protected Sound sound;
       protected Status status;
       protected int attackDirection;
       
       protected bool attackMotion;
       protected Vector2 velocity;

       public Character(string name, Sound sound)
       {
           this.sound = sound;
           status = new Status();
           this.name = name;
           attackMotion = false;
       }

       public Character(string name, Sound sound, Status status)
       {
           this.name = name;
           this.sound = sound;
           this.status = status;
           attackMotion = false;
       }

       public abstract void Initialize(int count, int no);

       public abstract void Updata(bool flag);

       public abstract void Draw(Renderer renderer);

       public virtual void Dead(int DeadEffectFlg = 0) { }

       public bool IsDead()
       {
           return status.IsDead();
       }

       public Vector2 Position
       {
           get { return position; }
       }

       /// <summary>
       /// 引数で受け取ったステータスにする
       /// </summary>
       /// <param name="status"></param>
       public void SetStatus(Status status)
       {
           this.status = status;
       }

       /// <summary>
       /// ステータスを返す
       /// 基本「継承物.Status.○○」でステータスを変えたりするつもり
       /// </summary>
       public Status Status
       { get { return status; } }

       /// <summary>
       /// 高さ計算
       /// </summary>
       /// <param name="count">合計モンスター数</param>
       /// <param name="no">上から何体目か</param>
       /// <returns>高さ</returns>
       public float HeightCalc(int count, int no)
       {
           if (count == 1) //1キャラのみなら中心を返す
           { return 125; }

           //上下にスペース計算（元が250で、モンスター描画に200利用して、余りで上下に分けるので/2する）
           float monsterSpace = 200;
           float plus = (250 - monsterSpace) / 2;

           //countはリストの番号利用するので1からスタートするように+1
           float y = (monsterSpace / count) * no  + plus;

           return y;
       }

       public void Attack(bool oneRotation)
       {
           if (!attackMotion) //初回のみ
           {
               velocity = new Vector2(4*attackDirection, -10);
               position += velocity;
               attackMotion = true;
           }
           else if (position.Y < defaultPosition.Y) //ジャンプ軌道
           {
               position += velocity;
               velocity.Y += 1;

               if (!(position.Y < defaultPosition.Y))
               {
                   //ダメージ与える





                   position.Y = defaultPosition.Y;//高さを初期位置に
               }
           }
           else //バック
           {
               velocity = new Vector2(-2 * attackDirection, 0);
               position += velocity;
               
               if (position.X > defaultPosition.X && attackDirection == -1) //プレイヤー用 初期位置を過ぎていたら戻ってフラグをfalseに
               {
                   position = defaultPosition;
                   attackMotion = false;
                   oneRotation = false; ;
               }
               else if (position.X < defaultPosition.X && attackDirection == 1)　//エネミー用　初期位置に戻ってフラグをfalseに
               {
                   position = defaultPosition;
                   attackMotion = false;
               }
           }
       }
    }
}
