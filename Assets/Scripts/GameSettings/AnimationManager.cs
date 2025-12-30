using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System;
public class AnimationManager : MonoBehaviour
{
    [SerializeField, Header("背景画像")]
    private Sprite[] BackGloundSprite;
    [SerializeField, Header("背景")]
    private GameObject BackGlound;
    [SerializeField, Header("DIFFICULTY")]
    private Difficulty difficulty;
    private RectTransform rectTransform;
    private int ChildCount;
    void Start()
    {
    }
    public void StageBackGloundChange()
    {
        SpriteRenderer _spriteRenderer=BackGlound.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite=BackGloundSprite[difficulty.StageNum];
    }
    public IEnumerator ButtonMove(GameObject Buttons,String MODE,int X)
    {
        ChildCount=0;
        foreach(Transform Button in Buttons.GetComponentInChildren<Transform>())
        {
            ChildCount++;
            if(Button.name=="Text (TMP)")continue;
            rectTransform=Button.GetComponent<RectTransform>();
            StartCoroutine(buttonmove(MODE,X));
            yield return new WaitForSeconds(0.13f);
        }
        if(ChildCount>2)yield break;
        rectTransform=Buttons.GetComponent<RectTransform>();
        StartCoroutine(buttonmove(MODE,X));
    }
    private IEnumerator buttonmove(string MODE,int X)
    {
            if(MODE=="ENTER")
            {
                rectTransform.DOMove(new Vector3(X,rectTransform.position.y,0),1.3f);
                yield break;
            }
            else if(MODE=="EXIT")
            {
                rectTransform.DOMove(new Vector3(X,rectTransform.position.y,0),1.3f);
                yield break;
            }
    }
}
