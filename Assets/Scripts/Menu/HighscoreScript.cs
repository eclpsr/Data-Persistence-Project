using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] highscoreText;
    public GameObject highscorePanel;

    private void OnEnable()
    {
        UpdateHighscore();
    }

    private void UpdateHighscore()
    {
        for(int i = 0; i < DataManager.Instance.highscores_.highScoreEntryList.Count; i++)
        {
            
            highscoreText[i].text = i+1 + ". " + DataManager.Instance.highscores_.highScoreEntryList[i].name + " : " + DataManager.Instance.highscores_.highScoreEntryList[i].score;
        }
    }

    public void ExitHighscores()
    {
        highscorePanel.SetActive(false);
    }
}
