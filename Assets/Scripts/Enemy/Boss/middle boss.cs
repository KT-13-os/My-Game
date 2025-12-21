using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class middleboss : Enemy
{
    [SerializeField, Header("打つ弾の速さ")]
    private float _Pspeed;
    [SerializeField, Header("円形2の段数")]
    private int _circleBulletNum2;
    [SerializeField, Header("円形に弾を発射する間隔2")]
    private float _circleBuletTime2;
    [SerializeField, Header("Maru2Attackでずらす角度")]
    private float Maru2Angle;
    private float _circle2Angle;
    private int _circle3count;
    private int _circle3Mode;
    enum AttackMode
    {
        A,
        A1,
        A2,
    }
    private AttackMode _attackMode;
    protected override void Initialize()
    {
        _difficulty.Phase=1;
        middleDifficultyHP(0);
        _Mhp = _hp;
        if (_difficulty.DIFFICULTY == "Easy" || _difficulty.DIFFICULTY == "Normal"||_difficulty.DIFFICULTY=="VeryEasy")
        {
            _PhaseHP = -100000000000000;
        }
        else if(_difficulty.DIFFICULTY=="Hard")
        {
            _PhaseHP = _Mhp / 2;
        }
        _attackMode = AttackMode.A1;
        _circle2Angle = 180;
    }

    // Update is called once per frame
    // protected override void UpdateA()
    // {
    //     if (_hp <= 0)
    //     {
    //         _attackMode = AttackMode.A;
    //         // for (int i = 0; i < Random.Range(1, 9); i++)
    //         // {
    //         //     GameObject powerItemObj = Instantiate(_Item[0]);
    //         //     powerItemObj.transform.position = new Vector2(transform.position.x + Random.Range(0, 3), transform.position.y + Random.Range(0, 3));
    //         // }
    //         // for (int i = 0; i < Random.Range(1, 6); i++)
    //         // {
    //         //     GameObject pointItemObj = Instantiate(_Item[1]);
    //         //     pointItemObj.transform.position = new Vector2(transform.position.x + Random.Range(0, 3), transform.position.y + Random.Range(0, 3));
    //         // }
    //         // GameObject HPUpItemObj = Instantiate(_Item[2]);
    //         // HPUpItemObj.transform.position = new Vector2(transform.position.x + Random.Range(0, 3), transform.position.y + Random.Range(0, 3));
    //         // Destroy(gameObject);
    //     }
    // }
    protected override void Atack()
    {
        _shootCount += Time.deltaTime;
        switch (_attackMode)
        {
            case AttackMode.A: A(); break;
            case AttackMode.A1: A1(); break;
            case AttackMode.A2: A2(); break;
        }
    }
    private void A()
    {
    }
    private void A1()
    {
        Maru2Attack();
        if (_hp < _PhaseHP)
        {
            _attackMode = AttackMode.A2;
            _circle3Mode = 1;
        }
    }
    private void Maru2Attack()
    {
        if (_shootCount < _circleBuletTime2) return;
        for (int i = 0; i < _circleBulletNum2; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / _circleBulletNum2 * i - Mathf.Deg2Rad * (_circle2Angle + 360f / 2f);
            GameObject bullet = Instantiate(_bullet[0]);
            Bullet shootbullet = bullet.GetComponent<Bullet>();
            shootbullet.Speed(_Pspeed);
        shootbullet.KasokuCahange(-0.01f);
        shootbullet.MoveChange(1);
            bullet.transform.position = transform.position;
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        _circle2Angle -= Maru2Angle;
        if (_circle2Angle <= -90f)
        {
            Maru2Angle *= -1;
            _circle2Angle = 90f;
        }
        if (_circle2Angle >= 270f)
        {
            Maru2Angle *= -1;
            _circle2Angle = 90f;
        }
        _shootCount = 0f;
    }
    private void A2()
    {
        MaruAttack3();
    }
    private void MaruAttack3()
    {
        if (_shootCount < _circleBuletTime2) return;
        for (int i = 0; i < _circleBulletNum2; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / _circleBulletNum2 * i - Mathf.Deg2Rad * (_circle2Angle + 360f / 2f);
            GameObject bullet = Instantiate(_bullet[0]);
            Bullet shootbullet = bullet.GetComponent<Bullet>();
            if (_circle3Mode == 1)
            {
                shootbullet.Speed(2);
        shootbullet.KasokuCahange(-0.01f);
        shootbullet.MoveChange(1);
            }
            else if (_circle3Mode == 2)
            {
                shootbullet.Speed(5);
                shootbullet.MoveChange(1);
                shootbullet.KasokuCahange(1);
            }
            bullet.transform.position = transform.position;
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        _circle2Angle -= Maru2Angle;
        _circleBuletTime2 = 0.1f;
        if (_circle2Angle <= -90f)
        {
            Maru2Angle *= -1;
            _circle2Angle = 90f;
            _circleBuletTime2 = 1;
            _circle3Mode = 2;
        }
        if (_circle2Angle >= 270f)
        {
            Maru2Angle *= -1;
            _circle2Angle = 90f;
            _circleBuletTime2 = 1;
            _circle3Mode = 1;
        }
        _shootCount = 0f;
    }
}
