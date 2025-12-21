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
        transform.DOScale(new Vector3(_ScaleX,_ScaleY,0),1f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(3f);
        transform.DOScale(new Vector3(0.1f,0.1f,0),1f).SetEase(Ease.Linear);
        this.gameObject.SetActive(false);
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
    // private void Way3()
    // {
    //     for (int i = 0; i < 3; i++)
    //     {
    //         float angleRange = Mathf.Deg2Rad * 60f;
    //         float theta = angleRange / 2 * i - Mathf.Deg2Rad * (30f / 2f);
    //         GameObject shootbullet = Instantiate(_bullet[0]);
    //         Bullet bullet = shootbullet.GetComponent<Bullet>();
    //         bullet.MoveChange(3);
    //         bullet.KasokuCahange(5);
    //         bullet.Speed(15);
    //         shootbullet.transform.position = transform.position;
    //         Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
    //         shootbullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
    //     }
    //     _ShootCount = 0;
    // }
}
