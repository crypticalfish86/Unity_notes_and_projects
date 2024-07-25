using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Question", fileName = "New Question")]
public class QuestionScriptableObject : ScriptableObject
{
    [TextArea(2, 6)] [SerializeField] private string question = "Enter new question here";
    [TextArea(2, 6)] [SerializeField] private string[] answers = new string[4];
    [SerializeField] private int correctAnswerIndex = 0;

    public string GetQuestion(){
        return question;
    }

    public string GetAnswer(int index){
        return answers[index];
    }

    public int GetCorrectAnswerIndex(){
        return correctAnswerIndex;
    }

}
