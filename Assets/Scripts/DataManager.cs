using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string name_;
    public int score_;
    public Highscores highscores_;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    [System.Serializable]
    class SaveData
    {
        public string userName;
        public uint score;
    }

    [System.Serializable]
    public class HighscoreEntry
    {
        public int score;
        public string name;
    }
    
    public class Highscores
    {
        public List<HighscoreEntry> highScoreEntryList;
    }

    public void AddHighscoreEntry(int score, string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
        Instance.highscores_.highScoreEntryList.Add(highscoreEntry);
        Instance.Sort();
        Instance.Cut();
        Instance.Save();        
    }

    public void Sort() 
    {
        Highscores highscores = new Highscores();
        highscores = Instance.highscores_;

        for (int i = 0; i < highscores.highScoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highScoreEntryList.Count; j++)
            {
                if (highscores.highScoreEntryList[j].score > highscores.highScoreEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscores.highScoreEntryList[i];
                    highscores.highScoreEntryList[i] = highscores.highScoreEntryList[j];
                    highscores.highScoreEntryList[j] = tmp;
                }
            }
        }
        Instance.highscores_ = highscores;
    }

    public void Cut()
    {
        Highscores highscores = new Highscores();
        highscores = Instance.highscores_;
        if (highscores.highScoreEntryList.Count > 10)
        {
            for (int h = highscores.highScoreEntryList.Count; h > 10; h--)
            {
                highscores.highScoreEntryList.RemoveAt(10);
            }
        }
        Instance.highscores_ = highscores;
    }

    public void Save()
    {
        Highscores highscores = new Highscores();
        highscores = Instance.highscores_;
        string json = JsonUtility.ToJson(highscores);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Highscores highscores = JsonUtility.FromJson<Highscores>(json);
            Instance.highscores_ = highscores;
        }
        else
        {
            Highscores highscores = new Highscores()
            {
                highScoreEntryList = new List<HighscoreEntry>()
            };
            Instance.highscores_ = highscores;
        }

    }

    public string GetHighScore()
    {
        if (Instance.highscores_ != null && Instance.highscores_.highScoreEntryList.Count != 0)
        {
            return Instance.highscores_.highScoreEntryList[0].name + " : " + Instance.highscores_.highScoreEntryList[0].score;
        } 
        return "Empty!";
    }
}
