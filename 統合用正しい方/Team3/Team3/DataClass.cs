using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Team3
{
   public class DataClass
    {
        bool pendulumClick;
        bool attackFlg;
        int eraseNum;
        public DataClass()
        { }


        public bool PendulumClick
        {
            set { this.pendulumClick = value; }
            get { return pendulumClick; }
 
        }

       //消した数を渡します
        public int EraseNum
        {
            set { this.eraseNum = value; }
            get { return eraseNum; }
        }


        public bool AttackFlg
        {
            set { this.attackFlg = value; }
            get { return attackFlg; }
        }


    }
}
