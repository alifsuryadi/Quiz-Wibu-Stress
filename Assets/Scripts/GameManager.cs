using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    QuizController quizController;
    EndScreen endScreen;

    void Awake()
    {
        quizController = FindObjectOfType<QuizController>();
        endScreen = FindObjectOfType<EndScreen>();
    }

    void Start()
    {
        quizController.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }


    void Update()
    {
        if (quizController.isComplete)
        {
            quizController.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
