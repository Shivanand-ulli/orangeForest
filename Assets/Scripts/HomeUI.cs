using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUI : MonoBehaviour
{
    int col;
    int row;
    public enum Mode
    {
        Easy,
        Medium,
        Hard
    }

    void Start()
    {
        AudioManager.Instance.PlayMusic(0);   
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
                break;
            case Mode.Medium:
                col = 3;
                row = 2;
                break;
            case Mode.Hard:
                col = 6;
                row = 5;
                break;
        }
        PlayerPrefs.SetInt("Columns", col);
        PlayerPrefs.SetInt("Rows", row);
    }

    public void PlayButtonSfx()
    {
        AudioManager.Instance.PlaySfx(2);
    }

}
