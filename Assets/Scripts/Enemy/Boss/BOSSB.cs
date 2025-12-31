using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BOSSB : BOSS
{
    private int _circleBulletNum;
    private float _circleBulletAngle;
    private float _Bulletspeed;
    private float _InSpeed;
    private float _shootTime;
    private int _shootNum;
    float MHP;
    enum AttackMode
    {
        A,
        A1,
        A2,
        A3,
        A4,
        A5,
        A6,
        A7,
    }
    private AttackMode _attackMode;
    enum MoveMode
    {
        M,
        M1,
        M2,
        M3,
    }
    private MoveMode _moveMode;
    [SerializeField, Header("wall")]
    private GameObject[] _wall;
    protected override void Initialize()
    {
        _attack=true;
        _moveMode = MoveMode.M;
        _attackMode = AttackMode.A1;
        MHP = _hp;
        _InSpeed=1;
        _Bulletspeed=4;
        _shootNum=0;
        _shootCount=0;
        _shootTime=0.3f;
        _circleBulletNum=13;
        _circleBulletAngle=90;
    }
    protected override void UpdateA()
    {
        _shootCount += Time.deltaTime;
        if (_hp <= 0)
        {
            _attackMode = AttackMode.A;
        }
    }
    protected override void Move()
    {
        // switch (_moveMode)
        // {
        //     case MoveMode.M: M(); break;
        //     case MoveMode.M1: M1(); break;
        //     case MoveMode.M2: M2(); break;
        //     case MoveMode.M3: M3(); break;
        // }
    }
    private void M()
    {
        if (transform.position.y <= 3.5f)
        {
            for (int i = 0; i < 4; i++)
            {
                _wall[i].SetActive(true);
            }
            _moveMode = MoveMode.M1;
            _attackMode = AttackMode.A1;
        }
        else
        {
            _rigid.velocity = Vector2.down * 2;
        }
    }
    private void M1()
    {
        var y = Mathf.PingPong(Time.time, 1);

        // y座標を往復させて上下運動させる
        transform.position = new Vector2(-3, y + 2.5f);
        if (_hp <= MHP * 2 / 3)
        {
            _moveMode = MoveMode.M2;
            _attackMode = AttackMode.A2;
        }
    }
    private void M2()
    {
        if (_hp <= MHP * 1 / 3)
        {
            _moveMode = MoveMode.M2;
            _attackMode = AttackMode.A2;
        }
    }
    private void M3()
    {
    }
    protected override void Attack()
    {
        _shootCount+=Time.deltaTime;
        if(_shootCount<_shootTime)return;
        switch (_attackMode)
        {
            case AttackMode.A: A(); break;
            case AttackMode.A1: A1(); break;
            case AttackMode.A2: A2(); break;
            case AttackMode.A3: A3(); break;
            case AttackMode.A4: A4(); break;
            case AttackMode.A5: A5(); break;
            case AttackMode.A6: A6(); break;
            case AttackMode.A7: A7(); break;
        }
    }
    private void A()
    {
    }
    private void A1()
    {
        MARU();
    }
    private void MARU()
    {
        for (int i = 0; i < _circleBulletNum; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / (_circleBulletNum-1) * i - Mathf.Deg2Rad * (_circleBulletAngle + 360f / 2f);
            GameObject bullet = Instantiate(_bullet[0]);
            Bullet shootbullet = bullet.GetComponent<Bullet>();
            shootbullet.Speed(_Bulletspeed);
            shootbullet.KasokuCahange(_Bulletspeed/2);
            shootbullet.MoveChange(5,this.gameObject);
            shootbullet.RcfSSTIME(7f);
            shootbullet.RcfStopTime(0f);
            bullet.transform.position = transform.position;
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        _circleBulletAngle+=360/_circleBulletNum;
        _shootTime=0.3f;
        if(_Bulletspeed>=4.8f)
        {
            _Bulletspeed=4;
            _shootTime=1.5f;
            _circleBulletAngle+=(360/_circleBulletNum)*3;
        }
        _shootCount=0;
        _Bulletspeed+=0.2f;
    }
    private void A2()
    {
    }
    private void A3()
    {
    }
    private void A4()
    {
    }
    private void A5()
    {
    }
    private void A6()
    {
    }
    private void A7()
    {
    }
}
