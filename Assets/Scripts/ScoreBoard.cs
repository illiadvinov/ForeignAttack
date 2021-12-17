using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
   string scoreNum;
   int score;
   TMP_Text scoreText;
   
   void Start() 
   {
       scoreText = GetComponent<TMP_Text>();
       scoreText.text = "Shoot Enemies";
   }

   public void IncreaseScore(int amountToIncrease)
   {
       score +=  amountToIncrease;
       scoreText.text = score.ToString();
       
   }
}
