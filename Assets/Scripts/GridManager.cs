using System.Collections;
using System.Collections.Generic;
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

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        UpdateGridLayout(row, col);

        // Clear old cards
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
        createdCards.Clear();

        int totalCards = col * row;
        int pairCount = totalCards / 2;

        // Select pairs of sprites
        List<Sprite> selectedSprites = new List<Sprite>();
        for (int i = 0; i < pairCount; i++)
        {
            selectedSprites.Add(frontSprites[i]);
            selectedSprites.Add(frontSprites[i]);
        }

        ShuffleSprites(selectedSprites);

        // Instantiate cards
        for (int i = 0; i < totalCards; i++)
        {
            GameObject cardGO = Instantiate(cardPrefab, gridParent);
            Card card = cardGO.GetComponent<Card>();
            card.SetCardFace(frontBg, selectedSprites[i]);
            createdCards.Add(card);
        }
    }

    void UpdateGridLayout(int rows, int cols)
    {
        float cellSize = 100f;
        float spacing = 20f;

        // Set based on known layouts
        if (rows == 2 && cols == 2 || rows == 2 && cols == 3)
        {
            cellSize = 350f;
        }
        else if (rows == 5 && cols == 6)
        {
            cellSize = 150f;
        }

        // Apply to grid layout
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = cols;
        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
        gridLayoutGroup.spacing = new Vector2(spacing, spacing);
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
        yield return new WaitForSeconds(0.2f);

        if (firstCard.frontSign.sprite == secondCard.frontSign.sprite)
        {
            firstCard.CardMatch();
            secondCard.CardMatch();
        }
        else
        {
            firstCard.CardFlip();
            secondCard.CardFlip();
        }

        firstCard = null;
        secondCard = null;
    }

    void ShuffleSprites(List<Sprite> sprites)
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            Sprite temp = sprites[i];
            int rand = Random.Range(i, sprites.Count);
            sprites[i] = sprites[rand];
            sprites[rand] = temp;
        }
    }
}
