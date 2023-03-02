using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int answerCorrect;
    int totalQuestion;


    public int GetAnswerCorret()
    {
        return answerCorrect;
    }

    public void IncrementCorrectAnswer()
    {
        answerCorrect++;
    }

    public int GetTotalQuestion()
    {
        return totalQuestion;
    }

    public void IncrementTotalQuestion()
    {
        totalQuestion++;
    }

    public int CalculateScore()
    {
        // di c# 100 = 100%
        // 3/4 * 100 = 0.75, bukan 75
        // maka gunakan float, supaya tidak di ambil 0 saja
        // Mathf.RoundToInt supaya di bulatkan ke int

        return Mathf.RoundToInt(answerCorrect / (float)totalQuestion * 100);
    }
}
