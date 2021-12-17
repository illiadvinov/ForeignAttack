using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillScore : MonoBehaviour
{
    string killNum;
    int kills;
    TMP_Text killText;
     TMP_Text ed;
   
   void Start() 
   {
       killText = GetComponent<TMP_Text>();

   }

   public void IncreaseKillsScore(int amountToIncreaseKills)
   {
       kills +=  amountToIncreaseKills;
       killText.text = kills.ToString();
       
   }
}
