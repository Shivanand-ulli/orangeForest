using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("References")]
    public GameObject cardPrefab;
    public RectTransform gridParent;
    public GridLayoutGroup gridLayoutGroup;

    [Header("Card Settings")]
    public List<Sprite> frontSprites;
    public Sprite frontBg;
    public Sprite backBg;
    public Sprite backSign;

    [Header("Layout")]
    public int col = 6;
    public int row = 5;

    private List<Card> createdCards = new List<Card>();
    private Card firstCard;
    private Card secondCard;
    private int totalMatchesRequired = 0;
    private int currentMatch = 0;
    int totalCards;

    public static event Action IncreaseTurns;
    public static event Action IncreaseMatches;
    public static event Action ShowWinPanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void GenerateGrid()
    {
        UpdateGridLayout(row, col);

        // Clear existing cards
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);
        createdCards.Clear();

        totalCards = col * row;
        int pairCount = totalCards / 2;
        totalMatchesRequired = pairCount;

        // Create pairs
        List<Sprite> selectedSprites = new List<Sprite>();
        for (int i = 0; i < pairCount; i++)
        {
            selectedSprites.Add(frontSprites[i]);
            selectedSprites.Add(frontSprites[i]);
        }

        ShuffleSprites(selectedSprites);

        StartCoroutine(ShowCardSequentially(selectedSprites));
    }

    IEnumerator ShowCardSequentially(List<Sprite> sprites)
    {
        for (int i = 0; i < totalCards; i++)
        {
            GameObject cardGO = Instantiate(cardPrefab, gridParent);
            Card card = cardGO.GetComponent<Card>();
            card.SetCardFace(frontBg, sprites[i]);
            createdCards.Add(card);

            // Add and setup CanvasGroup
            CanvasGroup cg = cardGO.GetComponent<CanvasGroup>();
            if (cg == null) cg = cardGO.AddComponent<CanvasGroup>();
            cg.alpha = 0f;

            StartCoroutine(FadeInCard(cg, 0.3f));

            yield return new WaitForSeconds(0.05f); 
        }
    }


    IEnumerator FadeInCard(CanvasGroup canvasGroup, float duration)
    {

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    void UpdateGridLayout(int rows, int cols)
    {
        float parentWidth = gridParent.rect.width;
        float parentHeight = gridParent.rect.height;

        // Define spacing percentage (can tweak)
        float spacingPercent = 0.1f;

        // Calculate cell size and spacing
        float rawCellWidth = parentWidth / (cols + (cols - 1) * spacingPercent);
        float rawCellHeight = parentHeight / (rows + (rows - 1) * spacingPercent);
        float cellSize = Mathf.Min(rawCellWidth, rawCellHeight);

        float spacingX = cellSize * spacingPercent;
        float spacingY = cellSize * spacingPercent;

        // Debug log for testing
        Debug.Log($"Grid: {rows}x{cols}, Cell: {cellSize}, Spacing: {spacingX}");

        // Apply to GridLayout
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = cols;
        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
        gridLayoutGroup.spacing = new Vector2(spacingX, spacingY);
    }

    public void CardRevealed(Card card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null)
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.3f);

        bool isMatch = firstCard.frontSign.sprite == secondCard.frontSign.sprite;

        if (isMatch)
        {
            firstCard.CardMatch();
            secondCard.CardMatch();
            IncreaseMatches?.Invoke();
            currentMatch++;
            if (currentMatch == totalMatchesRequired)
            {
                StartCoroutine(ShowWinWithDelay());
            }
        }
        else
        {
            firstCard.CardFlip();
            secondCard.CardFlip();
        }

        IncreaseTurns?.Invoke();
        firstCard = null;
        secondCard = null;
    }

    IEnumerator ShowWinWithDelay()
    {
        yield return new WaitForSeconds(1);
        ShowWinPanel?.Invoke();
    }

    void ShuffleSprites(List<Sprite> sprites)
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            int rand = UnityEngine.Random.Range(i, sprites.Count);
            (sprites[i], sprites[rand]) = (sprites[rand], sprites[i]);
        }
    }
}
