using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Timer : MonoBehaviour
{

    //Timer
    [SerializeField] float timeForQuestionGiven = 30f; 
    [SerializeField] float timeForAnswerViewing = 10f;
    public bool playerAnsweringQuestion = false;
    private float timeLeft; 

    //Timer image
    public float fillFraction = 1;

    //contact quiz script
    public bool loadNextQuestion; //set true in this script, set false in other script

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        setTimerFillAmount();
        Debug.Log("Time Left: " + timeLeft + "Fill Fraction: " + fillFraction);
    }

    private void setTimerFillAmount(){
        if(timeLeft > 0) {
            float timeToDivideBy = playerAnsweringQuestion ? timeForQuestionGiven: timeForAnswerViewing;
            fillFraction = timeLeft / timeToDivideBy; //ratio of time left on clock
        }
    }

    public void CancelTimer(){
        timeLeft = 0;
    }

    private void UpdateTimer(){
        timeLeft -= Time.deltaTime;//timer counts down in real time

        //if player is answering question when timer = 0 then switch to answer view timer or vice versa
        if (timeLeft <= 0 && playerAnsweringQuestion){
            playerAnsweringQuestion = false;
            timeLeft = timeForAnswerViewing;
        }
        else if (timeLeft <= 0 && !playerAnsweringQuestion){
            playerAnsweringQuestion = true;
            timeLeft = timeForQuestionGiven;
            loadNextQuestion = true;
        }
    }
}
