using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField, Header("背景画像")]
    private Sprite[] BackGloundSprite;
    [SerializeField, Header("背景")]
    private GameObject BackGlound;
    [SerializeField, Header("DIFFICULTY")]
    private Difficulty difficulty;
    void Start()
    {
    }
    public void StageBackGloundChange()
    {
        SpriteRenderer _spriteRenderer=BackGlound.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite=BackGloundSprite[difficulty.StageNum];
    }
}
