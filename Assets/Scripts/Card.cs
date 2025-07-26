using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Header("Front Side")]
    public GameObject frontContainer;
    public Image frontImage;
    public Image frontSign;

    [Header("Back Side")]
    public GameObject backContainer;
    public Image backImage;
    public Image backSign;

    private bool isFliping = false;
    private bool isMatching = false;

    void Start()
    {
        ShowBack();
    }

    public void SetCardFace(Sprite bgSprite, Sprite signSprite)
    {
        frontImage.sprite = bgSprite;
        frontSign.sprite = signSprite;
    }

    public void OnCardClik()
    {
        if (isMatching || isFliping) return;

        CardFlip();
    }

    public void CardFlip()
    {
        isFliping = !isFliping;

        if (isFliping)
        {
            ShowFront();
        }
        else
        {
            ShowBack();
        }
    }

    public void ShowFront()
    {
        backContainer.SetActive(false);
        frontContainer.SetActive(true);
    }

    public void ShowBack()
    {
        backContainer.SetActive(true);
        frontContainer.SetActive(false);
    }

    public void CardMatch()
    {
        isMatching = true;
    }

}
