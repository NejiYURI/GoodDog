using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightSwitch : HitInteractiveObject
{
    [SerializeField]
    private Camera MainCam;

    [SerializeField]
    private Color color;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite OffSprite;
    [SerializeField]
    private TextMeshProUGUI TitleText;
    protected override void OnPlayerHit()
    {
        if (MainCam) MainCam.backgroundColor = color;
        if (spriteRenderer) spriteRenderer.sprite = OffSprite;
        if (TitleText) TitleText.color = Color.white;
        if (GameEventManager.instance)
            GameEventManager.instance.StageClear.Invoke();
    }
}
