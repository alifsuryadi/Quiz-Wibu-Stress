using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] TextMeshProUGUI questionLayout; //ambil letak teks (khusus teks)
    [SerializeField] List<QuestionObject> question = new List<QuestionObject>(); 
    QuestionObject currentQuestion; //ambil pertanyaan

    [Header("Answer")]
    [SerializeField] GameObject[] answerButton;  //ambil button dan teks

    [Header("Collor Button Answer")]
    [SerializeField] Sprite defaultSpriteNoAnswer; //ambil gambar dari inspector
    [SerializeField] Sprite correctSpriteAnswer;
    bool notAnswer = true;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;
    public bool isComplete;


    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Start()
    {        
        progressBar.maxValue = question.Count;
        progressBar.minValue = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.FillFraction;
        if (timer.LoadNextQuestion)
        {
            //Complete Quiz
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return; //supaya langsung di kembalikan jika tidak terpenuhi
            }

            notAnswer = false;
            GetNextQuestion();
            timer.LoadNextQuestion = false;
        }
        else if(!notAnswer && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetActiveButton(false);
        }
    }

    void GetNextQuestion()
    { 
        //Batasi jika pertanyaan habis
        if(question.Count > 0)
        {
            SetActiveButton(true);
            SetDefaultButtonSprite();

            GetRandomQuestion();
            DisplayQuestion();

            //Sum question
            scoreKeeper.IncrementTotalQuestion();

            progressBar.value++;
        }

    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, question.Count);
        currentQuestion = question[index];

        if (question.Contains(currentQuestion)) //untuk memastikan saja
        {
            question.Remove(currentQuestion);
        }
        
    }

    void DisplayQuestion()
    {
        //question
        questionLayout.text = currentQuestion.getQuestion();

        //button answer
        for (int i = 0; i < answerButton.Length; i++)
        {
            //ambil list jawaban
            //answerButton[i] = karena objectnya ada banyak yaitu 4 disini
            TextMeshProUGUI textLayout = answerButton[i].GetComponentInChildren<TextMeshProUGUI>(); //telak button
            textLayout.text = currentQuestion.getAnswer(i);
        }

    }


    //ON CLICK
    public void ImageSelected(int index)
    {
        notAnswer = true;
        DisplayAnswer(index);
        SetActiveButton(false);
        timer.CancelQuestion();

        //Show score
        scoreText.text = "Score : " + scoreKeeper.CalculateScore() + "%";
      
    }

    void DisplayAnswer(int index)
    {
        Image buttonAnswer;

        if (index == currentQuestion.getCorrectAnswerIndex())
        {
            questionLayout.text = "Sugoi";
            buttonAnswer = answerButton[index].GetComponent<Image>();
            buttonAnswer.sprite = correctSpriteAnswer;

            //Sum correct answer
            scoreKeeper.IncrementCorrectAnswer();
        }
        else
        {
            index = currentQuestion.getCorrectAnswerIndex();
            string correctAnswer = currentQuestion.getAnswer(index);
            questionLayout.text = "Gomennasai, the correct answer was :\n" + correctAnswer;

            buttonAnswer = answerButton[index].GetComponent<Image>();
            buttonAnswer.sprite = correctSpriteAnswer;
        }
    }

    void SetActiveButton(bool buttonActive)
    {
        for(int i = 0; i < answerButton.Length; i++)
        {
            Button button = answerButton[i].GetComponent<Button>();
            button.interactable = buttonActive;
        }       
    }
    void SetDefaultButtonSprite()
    {
        Image defaultButton;
        for (int i = 0; i < answerButton.Length; i++)
        {
            defaultButton = answerButton[i].GetComponent<Image>();
            defaultButton.sprite = defaultSpriteNoAnswer;
        }
    }

}
