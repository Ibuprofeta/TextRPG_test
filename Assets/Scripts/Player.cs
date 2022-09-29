using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG{
    public class Player : Character
    {
        public int Floor {get; set;}
        public Room Room {get; set;}

        public World world;

        [SerializeField]
        Encounter encounter;

        void Start(){
            Floor = 0;
            Energy = 30;
            Attack = 10;
            Defense = 5;
            Gold = 0;
            Inventory = new List<string>();
            RoomIndex = new Vector2(2, 2);
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            this.Room.Empty = true;
            UIController.OnPlayerStatChange();
            UIController.OnPlayerInventoryChange();
        }

        public void Move(int direction){
           if (this.Room.Enemy){
            return;
           }
           if (direction == 0 && RoomIndex.y > 0){
            RoomIndex -= Vector2.up;
           }
           if (direction == 1 && RoomIndex.x < world.Dungeon.GetLength(0) - 1){
            RoomIndex += Vector2.right;
           }
           if (direction == 2 && RoomIndex.y < world.Dungeon.GetLength(1) - 1){
            RoomIndex -= Vector2.down;
           }
           if (direction == 3 && RoomIndex.x > 0){
            RoomIndex += Vector2.left;
           }
           if (this.Room.RoomIndex != RoomIndex){
            Investigate();
           }
        }

        public void Investigate(){
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];

            Debug.Log(RoomIndex);
            encounter.ResetDynamicControls();

            if (this.Room.Empty){
                Journal.Instance.Log("You find yourself in an empty room");
            } else if (this.Room.Chest != null){
                Journal.Instance.Log("You've found a chest! What would you like to do?");
                encounter.StartChest();
            } else if (this.Room.Enemy != null){
                Journal.Instance.Log("You are jumped by " + Room.Enemy.Description + "! What would you like to do?");
                encounter.StartCombat();
            } else if (this.Room.Exit){
                Journal.Instance.Log("You've found the exit to the next floor. Would you like to continue?");
                encounter.StartExit();
            }
        }

        public void AddItem(string item){
            Journal.Instance.Log("You were given item: " + item);
            Inventory.Add(item);
            UIController.OnPlayerInventoryChange();
        }

        public void AddItem(int item){
            Inventory.Add(ItemDatabase.Instance.Items[item]);
            UIController.OnPlayerInventoryChange();
        }

        public override void TakeDamage(int amount){
            Debug.Log("Player took damage");
            base.TakeDamage(amount);
            UIController.OnPlayerStatChange();
        }

        public override void Die(){
            Debug.Log("Player died. Game over!");
            base.Die();
        }
    }
}