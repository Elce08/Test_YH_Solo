using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player3UI : PlayerUIBase
{
    protected override void Awake()
    {
        base.Awake();
        portrait.sprite = gameManager.player3Sprite;
    }
}
