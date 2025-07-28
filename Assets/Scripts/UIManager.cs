using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    private bool soundSettingPanel = false;
    private Animator UIAnimator;

    void OnEnable()
    {
        GridManager.IncreaseTurns += SetTurns;
        GridManager.IncreaseMatches += SetMatches;
        GridManager.ShowWinPanel += ShowWinPanel;
    }
    void OnDisable()
    {
        GridManager.IncreaseTurns -= SetTurns;
        GridManager.IncreaseMatches -= SetMatches;
        GridManager.ShowWinPanel -= ShowWinPanel;
    }

    void Start()
    {
        turnsTxt.text = "0";
        matchesTxt.text = "0";
        scoreTxt.text = "0";
        WinPanel.SetActive(false);

        if (settingBtn != null)
            settingBtn.onClick.AddListener(ShowSoundSetting);

        UIAnimator = GetComponent<Animator>();
    }

    public void ShowSoundSetting()
    {
        soundSettingPanel = !soundSettingPanel;
        settingPanel.SetActive(soundSettingPanel);
    }

    public void SetTurns()
    {
        turns++;
        turnsTxt.text = turns.ToString();
    }

    public void SetMatches()
    {
        matches++;
        matchesTxt.text = matches.ToString();

        score = matches * 10;
        scoreTxt.text = score.ToString();
    }

    public void ShowWinPanel()
    {
        UIAnimator.SetBool("out", true);
        StartCoroutine(AnimateGameOverStatus());
    }

    IEnumerator AnimateGameOverStatus()
    {
        yield return new WaitForSeconds(0.5f);
        WinPanel.SetActive(true);
        yield return StartCoroutine(AnimateTextCount(0, turns, winPanelTurns));
        yield return StartCoroutine(AnimateTextCount(0, score, winPanelScore));
    }

    IEnumerator AnimateTextCount(int startValue, int endValue, TextMeshProUGUI Text)
    {
        yield return new WaitForSeconds(0.7f);
        float duration = 0.5f;
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float progress = Mathf.Clamp01(currentTime / duration);
            int displayValue = (int)Mathf.Lerp(startValue, endValue, progress);
            Text.text = displayValue.ToString();
            yield return null;
        }
    }
}
