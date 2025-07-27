using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    // Front Face Container
    [Header("Front Side")]
    public GameObject frontContainer;
    public Image frontImage;
    public Image frontSign;

    // Back Face Container
    [Header("Back Side")]
    public GameObject backContainer;
    public Image backImage;
    public Image backSign;

    // Flags to check
    private bool isFlipped = false;
    private bool isMatched = false;
    
    private Button button;  

    void Awake()
    {
        // Reference of the button
        button = GetComponent<Button>();
        if(button != null)
            button.onClick.AddListener(OnCardClik);
    }

    void Start()
    {
        // Initially show backface of the card
        ShowBack();
    }

    // Set the front card face and image
    public void SetCardFace(Sprite bgSprite, Sprite signSprite)
    {
        frontImage.sprite = bgSprite;
        frontSign.sprite = signSprite;
    }

    // Button listener func to the flip card
    public void OnCardClik()
    {
        if (isMatched || isFlipped) return;

        CardFlip();
        GridManager.Instance.CardRevealed(this);
    }

    /*Call the ShowFront and ShowBack method 
    according to the isFlipped flag*/
    public void CardFlip()
    {
        isFlipped = !isFlipped;

        if (isFlipped)
        {
            ShowFront();
        }
        else
        {
            ShowBack();
        }
    }

    // Show front face of the card
    public void ShowFront()
    {
        backContainer.SetActive(false);
        frontContainer.SetActive(true);
    }

    // Show back face of the card
    public void ShowBack()
    {
        backContainer.SetActive(true);
        frontContainer.SetActive(false);
    }

    /* Turn off the button interaction and 
     StartCoroutine after both card matches.*/
    public void CardMatch()
    {
        isMatched = true;
        button.interactable = false;
        StartCoroutine(HideMatchCard());
    }

    // Hide matched card using coroutine
    private IEnumerator HideMatchCard()
    {
        yield return new WaitForSeconds(0.3f);
        backContainer.SetActive(false);
        frontContainer.SetActive(false);
    }

}
