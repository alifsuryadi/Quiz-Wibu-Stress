using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionObject : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string question = "Enter new question is here";
    [SerializeField] string[] answer = new string[4];
    [SerializeField] int correctAnswerIndex;


    public string getQuestion()
    {
        return question;
    }

    public string getAnswer(int index)
    {
        return answer[index];
    }

    public int getCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

}
