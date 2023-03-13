using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text bestScore;
    public GameObject highscorePanel;

    private void Start()
    {
        bestScore.text = "Best Score : " + DataManager.Instance.GetHighScore();
    }
    public void StartNew()
    {
        DataManager.Instance.name_ = inputField.text;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void ShowHighScores()
    {
        highscorePanel.SetActive(true);
    }
}
