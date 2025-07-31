using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Feedback Items")]
    public TextMeshProUGUI turnsTxt;
    public TextMeshProUGUI matchesTxt;
    public TextMeshProUGUI scoreTxt;

    [Header("GameOver Panel")]
    public GameObject WinPanel;
    public TextMeshProUGUI winPanelTurns;
    public TextMeshProUGUI winPanelScore;

    [Header("Settings")]
    public Button settingBtn;
    public GameObject settingPanel;
    private int turns = 0;
    private int matches = 0;
    private int score = 0;
    private Animator UIAnimator;

    void OnEnable()
    {
        // Subscribe to the events
        GridManager.IncreaseTurns += SetTurns;
        GridManager.IncreaseMatches += SetMatches;
        GridManager.ShowWinPanel += HideTopPanel;
    }
    void OnDisable()
    {
        // Unsubscribe to the events
        GridManager.IncreaseTurns -= SetTurns;
        GridManager.IncreaseMatches -= SetMatches;
        GridManager.ShowWinPanel -= HideTopPanel;
    }

    void Start()
    {
        turnsTxt.text = "0";
        matchesTxt.text = "0";
        scoreTxt.text = "0";
        UIAnimator = GetComponent<Animator>();
    }

    // Set the turns of the cards
    public void SetTurns()
    {
        turns++;
        turnsTxt.text = turns.ToString();
    }

    // Set the matches and score of the cards
    public void SetMatches()
    {
        matches++;
        matchesTxt.text = matches.ToString();

        score = matches * 10;
        scoreTxt.text = score.ToString();

        int previousScore = PlayerPrefs.GetInt("Score", 0);
        int totalScore = previousScore + 10;
        PlayerPrefs.SetInt("Score", totalScore);
        PlayerPrefs.Save();
    }

    // Hide the HUD
    public void HideTopPanel()
    {
        UIAnimator.SetBool("out", true);
    }

    // Show the win feedback panel
    public void ShowWinPanel()
    {
        AudioManager.Instance.PlaySfx(3);
        CanvasGroup winPanel = WinPanel.GetComponent<CanvasGroup>();
        winPanel.alpha = 0;
        UIAnimator.SetBool("Appear", true);
        StartCoroutine(AnimateGameOverStatus()); 
    }

    IEnumerator AnimateGameOverStatus()
    {
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(AnimateTextCount(0, turns, winPanelTurns));
        yield return StartCoroutine(AnimateTextCount(0, score, winPanelScore));
    }

    // Animate the text num count
    IEnumerator AnimateTextCount(int startValue, int endValue, TextMeshProUGUI Text)
    {
        yield return new WaitForSeconds(0.7f);
        float duration = 0.5f;
        float currentTime = 0;
        AudioManager.Instance.musicSource.Play();
        AudioManager.Instance.PlayMusic(2);
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float progress = Mathf.Clamp01(currentTime / duration);
            int displayValue = (int)Mathf.Lerp(startValue, endValue, progress);
            Text.text = displayValue.ToString();
            yield return null;
        }
        AudioManager.Instance.musicSource.Stop();
    }

    // Play again current scene
    public void PlayAgain()
    {
        UIAnimator.SetBool("Disappear", true); // Play Disappear animation
        StartCoroutine(PlayCurrentScene());
    }

    IEnumerator PlayCurrentScene()
    {
        yield return new WaitForSeconds(0.1f);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    // Exit to home menu
    public void Exit()
    {
        UIAnimator.SetBool("Disappear", true); // Play Disappear animation 
        StartCoroutine(ExitGameScene());
    }

    IEnumerator ExitGameScene()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(0);
    }

    // Play button sfx
    public void PlayButtonSfx()
    {
        AudioManager.Instance.PlaySfx(2);
    }
}

