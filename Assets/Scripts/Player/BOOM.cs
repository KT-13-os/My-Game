using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class BOOM : MonoBehaviour
{
    [SerializeField, Header("弾オブジェクト")]
    private GameObject[] _bullet;
    private float _ScaleX;
    private float _ScaleY;
    [SerializeField, Header("BOSS?")]
    private bool BOSS=false;
    enum BOOMMode
    {
        B1,
    }
    private BOOMMode _BOOMMode;
    void Start()
    {
        _BOOMMode = BOOMMode.B1;
    }
    void Update()
    {
        // _ShootCount += Time.deltaTime;
    }
    private void B1()
    {
        StartCoroutine(BOOM1());
    }
    private IEnumerator BOOM1()
    {
        if(BOSS==false)
        {
            transform.DOScale(new Vector3(_ScaleX,_ScaleY,0),1f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(3f);
            transform.DOScale(new Vector3(0.1f,0.1f,0),1f).SetEase(Ease.Linear);
            this.gameObject.SetActive(false);
        }
        else
        {
            SpriteRenderer _spriterenderer=gameObject.GetComponent<SpriteRenderer>();
            transform.DOScale(new Vector3(_ScaleX,_ScaleY,0),1f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(1f);
            _spriterenderer.DOFade(0,0.3f);
            Destroy(gameObject);
        }
    }
    public void BOOMScale(float A,float B)
    {
        _ScaleX=A;
        _ScaleY=B;
        switch (_BOOMMode)
        {
            case BOOMMode.B1: B1(); break;
        }
    }
}
