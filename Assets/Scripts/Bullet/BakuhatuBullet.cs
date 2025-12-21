using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BakuhatuBullet : MonoBehaviour
{
    [SerializeField, Header("弾オブジェクト")]
    protected GameObject[] _bullet;
    [SerializeField, Header("爆発する時間")]
    protected float _bakuhatuTime;
    [SerializeField, Header("円形の段数")]
    private int _circleBulletNum;
    [SerializeField, Header("弾を出す回数")]
    private int _circleCount;
    private float _countDown;
    private bool CircleRotate;
    private float CirclerotateaNum;
    private float CircleAngle;
    private float _circleCountTime;
    private int CountA;
    bool _Bound;
    Vector2 _position;
    // Start is called before the first frame update
    void Start()
    {
        _countDown = 0;
        CircleAngle = 90f;
        CountA = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Stop();
        BAKUHATU();
        if (_countDown < _bakuhatuTime) return;
        _circleCountTime += Time.deltaTime;
        if (_circleCountTime < 0.2f) return;
            BAKUHATU2();
            _circleCountTime = 0;
    }
    private void BAKUHATU()
    {
        if (CountA >= 1) return;
        _countDown += Time.deltaTime;
        if (_countDown < _bakuhatuTime) return;
            for (int i = 0; i < _circleBulletNum; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / _circleBulletNum * i - Mathf.Deg2Rad * (CircleAngle + 360f / 2f);
            GameObject shootbullet = Instantiate(_bullet[0]);
            shootbullet.transform.position = transform.position;
            Bullet bullet = shootbullet.GetComponent<Bullet>();
            bullet.Speed(2);
            bullet.KasokuCahange(0.5f);
            bullet.MoveChange(1,this.gameObject);
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            shootbullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        if (CircleRotate == true)
        {
            CircleAngle += CirclerotateaNum;
            CountA++;
            _position = gameObject.transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void BAKUHATU2()
    {
        for (int i = 0; i < _circleBulletNum; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / _circleBulletNum * i - Mathf.Deg2Rad * (CircleAngle + 360f / 2f);
            GameObject shootbullet = Instantiate(_bullet[0]);
            shootbullet.transform.position = transform.position;
            Bullet bullet = shootbullet.GetComponent<Bullet>();
            bullet.Speed(2);
            bullet.KasokuCahange(0.5f);
            bullet.MoveChange(1,this.gameObject);
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            shootbullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        CircleAngle += CirclerotateaNum;
        CountA++;
        if (CountA >= _circleCount)
        {
            Destroy(gameObject);
        }
    }
    private void Stop()
    {
        if (CountA >= 1)
        {
            transform.position = _position;
        }
    }
    public void ChangeCircleBuletNum(int A)
    {
        _circleBulletNum = A;
    }
    public void ChangeBakuhatuTime(float A)
    {
        _bakuhatuTime = A;
    }
    public void ChangeCircleCount(int A)
    {
        _circleCount = A;
    }
    public void ChangeBound(bool A)
    {
        _Bound = A;
    }
    public void ChangeCircleRotate(bool A)
    {
        CircleRotate = A;
    }
    public void ChangeCiecleRotateNum(float A)
    {
        CirclerotateaNum = A;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            _countDown = _bakuhatuTime;
        }
    }
}
