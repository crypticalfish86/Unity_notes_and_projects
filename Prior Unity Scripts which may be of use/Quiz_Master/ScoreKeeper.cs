using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    private int correctAnswers = 0;
    private int questionsSeen = 0;
    // Start is called before the first frame update
    

    public int GetCorrectAnswers(){
        return correctAnswers;
    }
    public void IncrementCorrectAnswers(){
        correctAnswers++;
    }



    public int GetQuestionsSeen(){
        return questionsSeen;
    }

    public void IncrementQuestionsSeen(){
        questionsSeen++;
    }

    public float CalculateScore(){
        if (questionsSeen != 0 && correctAnswers != 0){
            return Mathf.RoundToInt((float) correctAnswers / (float) questionsSeen * 100);
        }
        else {
            return 0f;
        }
    }

}
