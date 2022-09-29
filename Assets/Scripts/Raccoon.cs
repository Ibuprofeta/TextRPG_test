using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG{
    public class Raccoon : Enemy
    {
        void Awake(){
            Energy = 10;
            Attack = 8;
            Defense = 3;
            Gold = 20;
            Description = "Raccoon";
            Inventory.Add("Bandit Mask");
            print("Raccoon");
        }
        
    }
}