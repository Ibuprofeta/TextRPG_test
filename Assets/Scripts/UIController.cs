using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextRPG{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        Text playerStatsText, enemyStatsText, playerInventoryText;
        [SerializeField]
        Player player;
        public delegate void OnPlayerUpdateHandler();
        public static OnPlayerUpdateHandler OnPlayerStatChange;
        public static OnPlayerUpdateHandler OnPlayerInventoryChange;

        public delegate void OnEnemyUpdateHandler(Enemy enemy);
        public static OnEnemyUpdateHandler OnEnemyUpdate;

        void Start(){
            OnPlayerStatChange += UpdatePlayerStats;
            OnPlayerInventoryChange += UpdatePlayerInventory;
            OnEnemyUpdate += UpdateEnemyStats;
        }

        public void UpdatePlayerStats(){
            playerStatsText.text = string.Format("Player; {0} energy, {1} attack. {2} defense, {3}g",
            player.Energy, player.Attack, player.Defense, player.Gold);
        }

        public void UpdatePlayerInventory(){
            playerInventoryText.text = "Items: ";
            foreach (string item in player.Inventory){
                playerInventoryText.text += item + " / ";
            }
        }

        public void UpdateEnemyStats(Enemy enemy){
            if (enemy){
                enemyStatsText.text = string.Format("{0}: {1} energy, {2} attack, {3} defense",
                enemy.Description, enemy.Energy, enemy.Attack, enemy.Defense);
            } else{
                enemyStatsText.text = "";
            }
        }
    }
}


