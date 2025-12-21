using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class middlebossB : Enemy
{
    [SerializeField, Header("打つ弾の速さ")]
    private float A1BulletSpeed;
    [SerializeField, Header("円形に弾を発射する間隔")]
    private float _shootAngle;
    private int _BulletCount;
    enum AttackMode
    {
        A,
        A1,
        A2,
        A3,
    }
    private AttackMode _attackMode;
    protected override void Initialize()
    {
        _BulletCount=0;
        _shootTime=2;
        _difficulty.Phase=1;
        middleDifficultyHP(0);
        _Mhp = _hp;
        if (_difficulty.DIFFICULTY == "Easy" || _difficulty.DIFFICULTY == "Normal"||_difficulty.DIFFICULTY=="VeryEasy")
        {
            _PhaseHP = -100000000000000;
        }
        else if(_difficulty.DIFFICULTY=="Hard")
        {
            if(_difficulty.Phase==1)
            {
                _PhaseHP=_Mhp/3*2;
            }
            else
            {
            _PhaseHP = _Mhp / 2;
            }
        }
        _attackMode = AttackMode.A1;
    }
    protected override void Atack()
    {
        _shootCount += Time.deltaTime;
        if(_shootCount<_shootTime)return;
        switch (_attackMode)
        {
            case AttackMode.A: A(); break;
            case AttackMode.A1: A1(); break;
            case AttackMode.A2: A2(); break;
            case AttackMode.A3:A3();break;
        }
    }
    private void A()
    {
    }
    private void A1()
    {
        GURUGURU();
        MARU();
    }
    private void GURUGURU()
    {
        for(int i=0;i<2;i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            if(i==1)
            {
            float theta = angleRange / 1 * 1 - Mathf.Deg2Rad * (-_shootAngle + 720 / 2f);
            GameObject shootbullet = Instantiate(_bullet[0]);
            Bullet bullet = shootbullet.GetComponent<Bullet>();
            bullet.MoveChange(3,this.gameObject);
            bullet.KasokuCahange(A1BulletSpeed/2);
            bullet.Speed(A1BulletSpeed);
            shootbullet.transform.position = transform.position+new Vector3(-1.3f,1.6f,0);
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            shootbullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
            }
            else
            {
            float theta = angleRange / 1 * 1 - Mathf.Deg2Rad * (_shootAngle + 360f / 2f);
            GameObject shootbullet = Instantiate(_bullet[0]);
            Bullet bullet = shootbullet.GetComponent<Bullet>();
            bullet.MoveChange(3,this.gameObject);
            bullet.KasokuCahange(A1BulletSpeed/2);
            bullet.Speed(A1BulletSpeed);
            shootbullet.transform.position = transform.position+new Vector3(1.3f,1.6f,0);
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            shootbullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
            }
        }
            _BulletCount++;
            _shootAngle+=10;
            _shootCount=0;
            _shootTime=0.2f;
    }
    private void MARU()
    {
        if(_BulletCount>=5)
        {
        for(int I=0;I<2;I++)
        {
        for (int i = 0; i < 11; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / 11 * i - Mathf.Deg2Rad * (90f + 360f / 2f);
            GameObject shootbullet = Instantiate(_bullet[1]);
            Bullet bullet = shootbullet.GetComponent<Bullet>();
            bullet.MoveChange(6+I,this.gameObject);
            bullet.KasokuCahange(A1BulletSpeed/3);
            bullet.Speed(A1BulletSpeed/8);
            shootbullet.transform.position = transform.position;
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            shootbullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        }
        _BulletCount=0;
        }
    }
    private void A2()
    {
    }
    private void A3()
    {
    }
}
