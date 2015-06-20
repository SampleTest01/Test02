using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Team3
{
    public class Status
    {
        private int maxHP;
        private int hitPoint;
        private int attack;
        private int speed;

        /// <summary>
        /// 空のステータスを作る（全て0）
        /// </summary>
        public Status()
        {
            maxHP = 0;
            hitPoint= 0;
            attack = 0;
            speed = 0;
        }
        public Status(int maxHP, int atack, int speed)
        {
            this.maxHP = maxHP;
            this.hitPoint = maxHP;
            this.attack = atack;
            this.speed = speed;
        }

        public Status(int value)
        {
            this.maxHP = value;
            this.hitPoint = value;
            this.attack = value;
            this.speed = value;
        }

        /// <summary>
        /// MaxHP(最大ヒットポイント)を返す
        /// </summary>
        public int MaxHP
        { get { return maxHP; } }
        /// <summary>
        /// HitPoint(現在のヒットポイント)を返す
        /// </summary>
        public int HitPoint
        {get{ return hitPoint; }}
        /// <summary>
        /// Attack(攻撃力)を返す
        /// </summary>
        public int Attack
        { get { return attack; } }
        /// <summary>
        /// Speed（速さ）を返す
        /// </summary>
        public int Speed
        { get { return speed; } }


        /// <summary>
        /// MaxHPを設定する
        /// HitPoint(現在のヒットポイント)もMaxHPと同じ値にする
        /// </summary>
        /// <param name="maxHP">設定後の値</param>
        public void SetMaxHP(int maxHP)
        {
            this.maxHP = maxHP;
            hitPoint = this.maxHP;
        }
        /// <summary>
        /// Attackを設定する
        /// </summary>
        /// <param name="atack">設定後の値</param>
        public void SetAtack(int attack)
        {
            this.attack = attack;
        }
        /// <summary>
        /// Speedを設定する
        /// </summary>
        /// <param name="speed">設定後の値</param>
        public void SetSpeed(int speed)
        {
            this.speed = speed;
        }


        /// <summary>
        /// ダメージを受ける（HPを減らす）
        /// 0より下なら0にする
        /// </summary>
        /// <param name="damage">ダメージ量</param>
        public void Damage(int damage)
        {
            if (damage < 0)
            { damage = 0; }
            hitPoint = hitPoint - damage;
        }

        /// <summary>
        /// 回復する
        /// 0より下なら0にする
        /// </summary>
        /// <param name="recovery">回復量</param>
        public void Recovery(int recovery)
        {
            if (recovery < 0)
            { recovery = 0; }

            hitPoint = hitPoint + recovery;

            if (MaxHP < hitPoint)
            { hitPoint = MaxHP; }

        }
        
        /// <summary>
        /// 死んでいるか（HPが0以下か）
        /// </summary>
        /// <returns>bool（死んでいたらtrue）</returns>
        public bool IsDead()
        {
            if (hitPoint <= 0)
            { return true; }

            return false;
        }

    }
}
