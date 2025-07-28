using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCallBack : MonoBehaviour
{
    public void UIAnimationComplete()
    {
        GridManager.Instance.GenerateGrid();
    }
}
