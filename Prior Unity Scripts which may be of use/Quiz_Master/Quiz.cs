using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{

    //QuestionScriptableObject
    [Header("QuestionSO")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionScriptableObject> questions = new List<QuestionScriptableObject>();
    QuestionScriptableObject currentQuestion;
    [SerializeField] GameObject[] answerButtons = new GameObject[4];
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    //Sprites
    [Header("Sprites")]
    [SerializeField] Sprite defaultAnswerButtonSprite;
    [SerializeField] Sprite correctAnswerButtonSprite;

    //timer
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    //score
    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    private void Start(){
        timer = FindObjectOfType<Timer>();//finds timer script
        scoreKeeper = FindObjectOfType<ScoreKeeper>();//finds scorekeeper script
        scoreKeeper.CalculateScore();//intializes score to 0 
        FillQuestionBox();
        FillAnswerButtons(currentQuestion);
    }

    private void Update(){
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion){
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.playerAnsweringQuestion){
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    //processes answer selection
    public void OnAnswerSelected(int index){
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    private void DisplayAnswer(int index) {
        Image buttonImage; //button image put up here for performance

        //if correct answer is clicked, then will inform player its correct, otherwise gives the player the correct answer
        if (index == correctAnswerIndex){
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerButtonSprite;
            buttonImage.color = new Color (255, 209, 148, 255);
            scoreKeeper.IncrementCorrectAnswers();
        }
        else{
            string questionBoxRevalingcorrectAnswer = "Incorrect, correct answer was:\n" + currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = questionBoxRevalingcorrectAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerButtonSprite;
            buttonImage.color = new Color (255, 209, 148, 255);
        }
    }

    //Get the next question after finishing the previous question(feature currently unavailable/untested)
    private void GetNextQuestion(){
        if(questions.Count > 0){
            SetButtonState(true);
            ResetButtonSprites();
            GetRandomQuestion();
            FillQuestionBox();
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    private void GetRandomQuestion(){
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        FillAnswerButtons(currentQuestion);

        if (questions.Contains(currentQuestion)) {
            questions.Remove(currentQuestion);
        }
    }
    
    //switches the buttons on or off depending on the state passed in
    private void SetButtonState(bool state){
        for (int i = 0; i < answerButtons.Length; i++){
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    //resets button sprites back to default sprite
    private void ResetButtonSprites(){
        Image buttonImage; //button image initalised here for performance

        //resets all buttons to the default sprite
        for (int i = 0; i < answerButtons.Length; i++){
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerButtonSprite;
        }
    }

    //Fills the question box with a question
    private void FillQuestionBox(){
        SetButtonState(true);
        questionText.text = currentQuestion.GetQuestion();
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
    }

    //goes through all button objects, finds the first textmeshpro child component and prints the question answer at that array index on that button
    private void FillAnswerButtons(QuestionScriptableObject question){
        for (int i = 0; i < answerButtons.Length; i++){
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }


}
