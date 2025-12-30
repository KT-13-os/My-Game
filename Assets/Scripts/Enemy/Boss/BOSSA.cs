using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random =  UnityEngine.Random;

public class BOSSA : BOSS
{
    [SerializeField, Header("wall")]
    private GameObject[] _wall;
    [SerializeField, Header("BB")]
    private GameObject[] _BB;
    [SerializeField, Header("円形の段数")]
    private int _circleBulletNum;
    [SerializeField, Header("円形攻撃の時間")]
    private float _circleShootTime;
    [SerializeField, Header("円形に弾を発射する間隔")]
    private float _circleBuletTime;
    float _ashootCount;
    float _bshootCount;
    float _Time;
    float _mutekiCount;
    int _a;
    float _bTime;
    private float _circleShootCount;
    private float _OLDcircleBulletTime;
    private float _OLDcircleBulletTime2;
    private float MaruAngle;
    private float MaruAngle2;
    private int _circleBulletNum2;
    private float _circleBuletTime2;
    private float _circleAngle;
    private float _circleAngle2;
    private float _shootCount2;
    private float _A;
    private int _B;
    private float _C;
    private float _D;
    private float _E;
    private int _F;
    private float _G;
    private float _H;
    private string _String;
    private float A1BulletSpeed;
    private float _BakuhatushootTime;
    private float ChangeCount;
    private float _boundshootTime;
    enum AttackMode
    {
        A,
        A1,
        A2,
        A3,
        A4,
    }
    private AttackMode _attackMode;
    enum MoveMode
    {
        M,
        M1,
        M2,
        M3,
        M4,
    }
    private MoveMode _moveMode;
    protected override void Initialize()
    {
        _score = 100000;
        _mutekiCount = 0;
        _bTime = 0.4f;
        _a = 0;
        _A = 4f;
        _B = 5;
        _D = 1f;
        _F = 5;
        ChangeCount = 0;
            if (_difficulty.DIFFICULTY == "Easy"||_difficulty.DIFFICULTY=="VeryEasy")
        {
            A1BulletSpeed = 2;
            _circleBulletNum = 15;
            _circleBuletTime = 0.6f;
            _OLDcircleBulletTime = _circleBuletTime;
            _BakuhatushootTime = 3f;
            _boundshootTime = 4f;
        }
            else if (_difficulty.DIFFICULTY == "Normal")
        {
            A1BulletSpeed = 4;
            _circleBulletNum = 19;
            _circleBuletTime = 0.5f;
            _OLDcircleBulletTime = _circleBuletTime;
            _BakuhatushootTime = 3f;
            _boundshootTime = 4f;
        }
            else if(_difficulty.DIFFICULTY == "Hard")
        {
            A1BulletSpeed = 4;
            _circleBulletNum = 25;
            _circleBuletTime = 0.4f;
            _OLDcircleBulletTime = _circleBuletTime;
            _BakuhatushootTime = 3f;
            _boundshootTime = 4f;
        }
        _moveMode = MoveMode.M;
        _attackMode = AttackMode.A;
    }
    protected override void UpdateA()
    {
    }
    protected override void Move()
    {
        switch (_moveMode)
        {
            case MoveMode.M: M(); break;
            case MoveMode.M1: M1(); break;
            case MoveMode.M2: M2(); break;
            case MoveMode.M3: M3(); break;
            case MoveMode.M4: M4(); break;
        }
    }
    private void M()
    {
        for (int i = 0; i < 4; i++)
        {
            _BB[i].SetActive(true);
        }
        if (transform.position.y <= 3.5f)
        {
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
        if (_hp <= _PhaseHP)
        {
            if (_difficulty.DIFFICULTY == "Easy"||_difficulty.DIFFICULTY=="VeryEasy")
            {
                _PhaseHP = -100000000000;
            }
            else if (_difficulty.DIFFICULTY == "Normal")
            {
                _PhaseHP = _Mhp / 2;
            }
            else if(_difficulty.DIFFICULTY == "Hard")
            {
                _PhaseHP = (_Mhp / 5) * 2;
            }
            _moveMode = MoveMode.M2;
            _attackMode = AttackMode.A2;
            playerscripts.MUTEKI();
            for (int i = 0; i < 4; i++)
            {
                _wall[i].SetActive(true);
            }
            Phase();
            GameObject bulletSummoner = Instantiate(_bullet[4]);
            bulletSummoner.transform.position = new Vector3(4.3f, -2, 0);
            GameObject bulletSummoner1 = Instantiate(_bullet[4]);
            bulletSummoner1.transform.position = new Vector3(4.3f, 2, 0);
            GameObject bulletSummoner2 = Instantiate(_bullet[5]);
            bulletSummoner2.transform.position = new Vector3(-5, -5, 0);
            GameObject bulletSummoner3 = Instantiate(_bullet[5]);
            bulletSummoner3.transform.position = new Vector3(-1, -5, 0);
        }
    }
    private void M2()
    {
        var x = Mathf.PingPong(Time.time, 6);

        // y座標を往復させて上下運動させる
        transform.position = new Vector2(x - 6, 2.5f);
        if (_hp <= _PhaseHP)
        {
            if (_difficulty.DIFFICULTY == "Normal"||_difficulty.DIFFICULTY == "Hard")
            {
                _PhaseHP = -10000000000;
            }
            _KeepHP = _hp;
            _moveMode = MoveMode.M3;
            _circleBuletTime = 3.5f;
            _shootCount = 0;
            _attackMode = AttackMode.A3;
            playerscripts.MUTEKI();
            for (int i = 0; i < 4; i++)
            {
                _wall[i].SetActive(false);
            }

            GameObject bulletSummoner = Instantiate(_bullet[9]);
            bulletSummoner.transform.position = new Vector3(4.3f, -2, 0);
            GameObject bulletSummoner1 = Instantiate(_bullet[9]);
            bulletSummoner1.transform.position = new Vector3(4.3f, 2, 0);
            GameObject bulletSummoner2 = Instantiate(_bullet[8]);
            bulletSummoner2.transform.position = new Vector3(-5, -5, 0);
            GameObject bulletSummoner3 = Instantiate(_bullet[8]);
            bulletSummoner3.transform.position = new Vector3(-1, -5, 0);
            Phase();
        }
        _mutekiCount += Time.deltaTime;
        if (_mutekiCount < 1) return;
        playerscripts.MUTEKIOFF();
    }
    private void M3()
    {
        var y = Mathf.PingPong(Time.time / 2, 2);

        // y座標を往復させて上下運動させる
        transform.position = new Vector2(-3, y + 2.5f);
        _mutekiCount += Time.deltaTime;
        if (_mutekiCount < 1) return;
        playerscripts.MUTEKIOFF();
    }
    private void M4()
    {
        if (transform.position.y >= 8f)
        {
            _rigid.velocity = Vector2.zero;
        }
        else
        {
            _rigid.velocity = Vector2.up * 2;
        }
    }
    protected override void Attack()
    {
        switch (_attackMode)
        {
            case AttackMode.A: A(); break;
            case AttackMode.A1: A1(); break;
            case AttackMode.A2: A2(); break;
            case AttackMode.A3: A3(); break;
            case AttackMode.A4: A4(); break;
        }
    }
    private void A()
    {
    }
    int B = 0;
    private void A1()
    {
        MaruAttack();
        float _ShootTime = Random.Range(1.5f, 2);
        _ashootCount += Time.deltaTime;
        if (_ashootCount < _ShootTime) return;
        normalAttack();
    }
    private void MaruAttack()
    {
        _shootCount += Time.deltaTime;
        if (B >= 2)
        {
            _circleBuletTime = _OLDcircleBulletTime;
            B = 0;
        }
        if (_shootCount < _circleBuletTime) return;
        for (int i = 0; i < _circleBulletNum; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / _circleBulletNum * i - Mathf.Deg2Rad * (90f + 360f / 2f);
            GameObject shootbullet = Instantiate(_bullet[3]);
            Bullet bullet = shootbullet.GetComponent<Bullet>();
            bullet.MoveChange(3,this.gameObject);
            bullet.KasokuCahange(A1BulletSpeed/2);
            bullet.Speed(A1BulletSpeed);
            shootbullet.transform.position = transform.position;
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            shootbullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        B++;
        _circleBuletTime = _OLDcircleBulletTime/2;
        _shootCount = 0f;
    }
    private void normalAttack()
    {
        GameObject bulletObj = Instantiate(_bullet[2]);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.Speed(4);
        bullet.KasokuCahange(-0.5f);
            bullet.MoveChange(1,this.gameObject);
        bulletObj.transform.position = transform.position;
        Vector3 dir = _player.transform.position - transform.position;
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        _ashootCount = 0;
    }
    private void A2()
    {
        _shootCount += Time.deltaTime;
        _bshootCount += Time.deltaTime;
        ChangeCount += Time.deltaTime;
        if(ChangeCount>=1)
        {
            if(_difficulty.DIFFICULTY=="Hard")
            {
                _BakuhatushootTime = 2f;
                _boundshootTime = 2f;
            }
        }
        BakuhatuAttack();
        if (_bshootCount < _bTime) return;
        BoundAttack();
    }
    private void BakuhatuAttack()
    {
        if (_shootCount < _BakuhatushootTime) return;
        GameObject bulletObj = Instantiate(_bullet[7]);
        Bullet shootbullet = bulletObj.GetComponent<Bullet>();
        BakuhatuBullet bakuhatuBullet = bulletObj.GetComponent<BakuhatuBullet>();
        shootbullet.BoundChange(false);
        shootbullet.Speed(2);
        shootbullet.MoveChange(3,this.gameObject);
        shootbullet.KasokuCahange(0.1f);
        bakuhatuBullet.ChangeCircleBuletNum(4);
        bakuhatuBullet.ChangeBakuhatuTime(Random.Range(1,2.5f));
        bakuhatuBullet.ChangeCircleCount(3);
        bakuhatuBullet.ChangeCircleRotate(true);
        bakuhatuBullet.ChangeCiecleRotateNum(45f);
        bakuhatuBullet.ChangeBound(false);
        bulletObj.transform.position = transform.position;
        Vector3 dir = _player.transform.position - transform.position;
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        _shootCount = 0;
    }
    private void BoundAttack()
    {
        if (_a >= 10)
        {
            _bTime = _boundshootTime;
            _a = 0;
        }
        else
        {
            _bTime = 0.5f;
            _a++;
        }
        GameObject boundObj = Instantiate(_bullet[6]);
        Bullet bullet = boundObj.GetComponent<Bullet>();
        bullet.Speed(2);
        bullet.KasokuCahange(-0.1f);
        bullet.ChangeBoundNum(3);
        bullet.BoundChange(true);
        bullet.MoveChange(4,this.gameObject);
        boundObj.transform.position = transform.position;
        _bshootCount = 0;
        _a++;
    }
    private void A3()
    {
        MaruAttackB();
        TIME();
        if (_hp <= 0)
        {
            if (_difficulty.DIFFICULTY == "Easy" || _difficulty.DIFFICULTY == "Normal"|| _difficulty.DIFFICULTY == "VeryEasy")
            { _attackMode = AttackMode.A;return; }
            // MARUconfig(5, 5, 0.2f, 0.4f, 4, 6);
            //     _C = 3.6f;
            //     _E = 3f;
            //     _G = 1.5f;
            //     _H = 1f;
            // _attackMode = AttackMode.A4;
            _attackMode = AttackMode.A;
            foreach(GameObject bullet in GameObject.FindGameObjectsWithTag("bullet"))
            {
            Destroy(bullet);
            }
            subGameManager = GameManager.GetComponent<SubGameManager>();
            subGameManager.Count();
            _moveMode = MoveMode.M4;
            ChangePhase(0);
            Phase();
        }
    }
    private void MaruAttackB()
    {
        _shootCount += Time.deltaTime;
        if (_shootCount < _circleBuletTime) return;
        _shootCount = 0f;
        for (int i = 0; i < 30; i++)
        {
            int A = Random.Range(10, 12);
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / 30 * i - Mathf.Deg2Rad * (90f + 360f / 2f);
            GameObject bullet = Instantiate(_bullet[A]);
            Bullet shootbullet = bullet.GetComponent<Bullet>();
            if (A == 10)
            {
                shootbullet.KasokuCahange(-4);
                shootbullet.Speed(6);
                shootbullet.MoveChange(1,this.gameObject);
            }
            else if (A == 11)
            {
                shootbullet.KasokuCahange(1.5f);
                shootbullet.Speed(3);
                shootbullet.MoveChange(3,this.gameObject);
            }
            bullet.transform.position = transform.position;
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
    }
    private void A4()
    {
        _shootCount += Time.deltaTime;
        _shootCount2 += Time.deltaTime;
        MARU();
        MARU2();
    }
    private void MARU()
    {
        if (_shootCount < _circleBuletTime) return;
        for (int i = 0; i < _circleBulletNum; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / _circleBulletNum * i - Mathf.Deg2Rad * (_circleAngle + 360f / 2f);
            GameObject bullet = Instantiate(_bullet[12]);
            Bullet shootbullet = bullet.GetComponent<Bullet>();
            shootbullet.Speed(_A);
            shootbullet.MoveChange(_B,this.gameObject);
            shootbullet.RcfSSTIME(_C);
            shootbullet.RcfStopTime(_D);
            bullet.transform.position = new Vector2(-3,0);
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        _circleAngle -= MaruAngle;
        _circleBuletTime = _OLDcircleBulletTime;
        // if (_circleAngle <= -180f)
        // {
        //     _circleAngle = 90f;
        // }
        if (_circleAngle > 360f)
        {
            _circleAngle = 90f;
        }
        _shootCount = 0;
    }
    private void MARU2()
    {
        if (_shootCount2 < _circleBuletTime2) return;
        for (int i = 0; i < _circleBulletNum2; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / _circleBulletNum2 * i - Mathf.Deg2Rad * (_circleAngle2 + 360f / 2f);
            GameObject bullet = Instantiate(_bullet[13]);
            Bullet shootbullet = bullet.GetComponent<Bullet>();
            shootbullet.Speed(_E);
            shootbullet.MoveChange(_F,this.gameObject);
            shootbullet.RcfSSTIME(_G);
            shootbullet.RcfStopTime(_H);
            bullet.transform.position = new Vector2(-3,0);
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        _circleAngle2 -= -MaruAngle2;
        _circleBuletTime2 = _OLDcircleBulletTime2;
        if (_circleAngle2 <= -180f)
        {
            _circleAngle2 = 90f;
        }
        // if (_circleAngle > 360f)
        // {
        //     _circleAngle = 90f;
        // }
        _shootCount2 = 0;
    }
    private void MARUconfig(int Num, int Num2, float Time, float Time2, float Angle, float Angle2,float BulletTime,float BulletTime2)
    {
        _circleBulletNum = Num;
        _circleBulletNum2 = Num2;
        _circleBuletTime = BulletTime;
        _circleBuletTime2 = BulletTime2;
        MaruAngle = Angle;
        MaruAngle2 = Angle2;
        _OLDcircleBulletTime = Time;
        _OLDcircleBulletTime2 = Time2;
        _shootCount = 0;
        _shootCount2 = 0;
    }
    private void TIME()
    {
        if (_hp < _KeepHP / 2)
        {
            _circleBuletTime = 0.8f;
            if (_hp < _KeepHP / 3)
            {
                _circleBuletTime = 0.4f;
            }
        }
        if (_hp < _KeepHP / 2) return;
        if (_hp < (_KeepHP / 6) * 5)
        {
            _circleBuletTime = 3f;
            if (_hp < (_KeepHP / 4) * 3)
            {
                _circleBuletTime = 2f;
                if (_hp < (_KeepHP / 12) * 7)
                {
                    _circleBuletTime = 1.2f;
                }
            }
        }
    }
public void ChangePhase(int INT)
    {
        if (INT == 0)
        {
            _attackMode =AttackMode.A4;
            MARUconfig(3, 6, 0.25f, 0.3f, 12, 5,2,2);
            _E = 4f;
            _G = 1.5f;
            _H = 1f;
        }
        else if (INT == 1)
        {
            _attackMode =AttackMode.A4;
            MARUconfig(5, 5, 0.2f, 0.42f, 4, 6,5,5);
            _C = 3.6f;
            _E = 3f;
            _G = 1.5f;
            _H = 1f;
        }
        else if(INT==10)
        {
            _attackMode = AttackMode.A;
            _attack = false;
            gamemanager.Score(_score);
            _slider.DESTROY();
            playerscripts.MUTEKI();
            gamemanager.ClearEffect();
            gameObject.SetActive(false);
        }
    }
}
