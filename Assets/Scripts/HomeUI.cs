using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUI : MonoBehaviour
{
    int col;
    int row;
    int backBG;
    int score = 0;
    public TextMeshProUGUI scoreTxt;
    public enum Mode
    {
        Easy,
        Medium,
        Hard
    }

    void Start()
    {
        AudioManager.Instance.PlayMusic(0);
        int savedScore = PlayerPrefs.GetInt("Score",0);
        score += savedScore;
        scoreTxt.text = score.ToString(); 
    }

    public void GameScene()
    {
        SceneManager.LoadScene(1);
    }


    // Modes Settings
    public void SetEasy()
    {
        SetGridSize(Mode.Easy);
    }
    public void SetMedium()
    {
        SetGridSize(Mode.Medium);
    }
    public void SetHard()
    {
        SetGridSize(Mode.Hard);
    }

    void SetGridSize(Mode mode)
    {
        switch (mode)
        {
            case Mode.Easy:
                col = 2;
                row = 2;
                backBG = 0;
                break;
            case Mode.Medium:
                col = 3;
                row = 2;
                backBG = 1;
                break;
            case Mode.Hard:
                col = 6;
                row = 5;
                backBG = 2;
                break;
        }
        PlayerPrefs.SetInt("Columns", col);
        PlayerPrefs.SetInt("Rows", row);
        PlayerPrefs.SetInt("BackBg", backBG);
    }

    public void PlayButtonSfx()
    {
        AudioManager.Instance.PlaySfx(2);
    }

}
