using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // Flags to check
    private bool isFlipped = false;
    private bool isMatched = false;
    private Animator anim;

    private Button button;
    public ParticleSystem sparkelEffect;

    void Awake()
    {
        // Reference of the button
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnCardClik);
    }

    void Start()
    {
        // Initially show backface of the card
        anim = GetComponent<Animator>();
        
        // Coroutine to set particle size based on card width
        StartCoroutine(AdjustParticleSize());
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
        if (isMatched || isFlipped || !GridManager.Instance.CanReveal()) return;

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
            anim.SetBool("FlipFront", isFlipped);
        }
        else
        {
            anim.SetBool("FlipFront", isFlipped);
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
        anim.SetTrigger("Dissappear");
    }

    // Play SparkelEffect when card disappear
    public void PlayEffect()
    {
        sparkelEffect.Play();
    }

    IEnumerator AdjustParticleSize()
    {
        yield return null;
        RectTransform rt = GetComponent<RectTransform>();
        float width = rt.rect.width;

        float baseSize = 380;
        float scaleFactor = width / baseSize;

        if (sparkelEffect != null)
        {
            sparkelEffect.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        }
    }
}
