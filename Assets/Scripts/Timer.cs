using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float AnswerQuestion = 30f;
    [SerializeField] float ReviewQuestion = 10f;

    public bool LoadNextQuestion;
    public bool isAnsweringQuestion;
    public float FillFraction;

    float TimeValue;

    void Update()
    {
        SetTimer();
    }

    public void CancelQuestion()
    {
        TimeValue = 0;
    }

    void SetTimer()
    {
        TimeValue -= Time.deltaTime; //Time.deltaTime = mulai dari 0

        if (!isAnsweringQuestion)
        {
            if(TimeValue > 0) //Step 4
            {
                FillFraction = TimeValue / ReviewQuestion;
            }
            else //Step 1
            {
                TimeValue = AnswerQuestion;
                isAnsweringQuestion = true;
                LoadNextQuestion = true;
            }
        }

        else
        {
            if(TimeValue > 0) //Step 2
            {
                FillFraction = TimeValue / AnswerQuestion;
            }
            else //Step 3
            {
                TimeValue = ReviewQuestion;
                isAnsweringQuestion = false;
            }
        }
        

    }
}
