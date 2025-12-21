using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TESTBOSS : BOSS
{
    [SerializeField, Header("ENEMY")]
    private GameObject[] _enemy;
    [SerializeField, Header("円形の段数")]
    private int _circleBulletNum;
    [SerializeField, Header("円形に弾を発射する間隔")]
    private float _circleBuletTime;
    private float _OLDcircleBulletTime;
    private float _OLDcircleBulletTime2;
    [SerializeField, Header("MaruAttackでずらす角度")]
    private float MaruAngle;
    private int _circleBulletNum2;
    private float _circleBuletTime2;
    private float MaruAngle2;
    private float _circleAngle;
    private float _circleAngle2;
    private int _circlecount;
    private int _circleMode;
    private int TEST;
    private float A;
    private int B;
    private float C;
    private float D;
    private float E;
    private int F;
    private float G;
    private float H;
    private float X;
    private float Y;
    private float _shootCount2;
    private float CountUp;
    private int SummonCount;
    enum MARUmode
    {
        NONE,
        C1,
        C2,
        C3,
    }
    private MARUmode marumode;
    protected override void Initialize()
    {
        TEST = -1;
        marumode = MARUmode.NONE;
        subGameManager = GameManager.GetComponent<SubGameManager>();
        _shootCount = 0;
        _shootCount2 = 0;
        _circleAngle = 90;
        _circleAngle2 = 90;
        SummonCount = 0;
        B = 5;
        F = 5;
        _attack = true;
    }
    protected override void Attack()
    {
        switch(marumode)
        {
            case MARUmode.NONE: NONE(); break;
            case MARUmode.C1: C1(); break;
            case MARUmode.C2: C2(); break;
            case MARUmode.C3: C3(); break;
        }
    }
    protected override void UpdateA()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TEST++;
            if (TEST == 0)
            {
                marumode = MARUmode.C1;
                MARUconfig(5, 0, 0.3f, 0, 5, 0);
                A = 4f;
                C = 2f;
                D = 1f;
            }
            else if (TEST == 1)
            {
                marumode = MARUmode.C1;
                MARUconfig(6, 0, 0.3f, 0, 8, 0);
                A = 4f;
                C = 2f;
                D = 1f;
            }
            else if (TEST == 2)
            {
                marumode = MARUmode.C2;
                MARUconfig(0, 6, 0, 0.3f, 0, 4);
                E = 4f;
                G = 3f;
                H = 1f;
            }
            else if (TEST == 3)
            {
                marumode = MARUmode.C3;
                MARUconfig(3, 6, 0.25f, 0.3f, 12, 5);
                E = 4f;
                G = 1.5f;
                H = 1f;
            }
            else if(TEST==4)
            {
                // marumode = MARUmode.C3;
                // MARUconfig(5, 5, 0.2f, 0.4f, 4, 6);
                // C = 3.6f;
                // E = 3f;
                // G = 1.5f;
                // H = 1f;
                // CountUp = 0;
                subGameManager.Count();
                ChangePhase(0);
            }
            else
            {
                TEST = 0;
                marumode = MARUmode.NONE;
            }
                Debug.Log("TEST" + TEST);
        }
    }
    private void NONE()
    {
    }
    private void C1()
    {
        _shootCount += Time.deltaTime;
        MARU();
    }
    private void C2()
    {
        _shootCount2 += Time.deltaTime;
        MARU2();
    }
    private void C3()
    {
        _shootCount += Time.deltaTime;
        _shootCount2 += Time.deltaTime;
        MARU();
        MARU2();
        if(TEST==4)
        {
            CountUp += Time.deltaTime;
            if (CountUp < 6) return;
            if ( CountUp<50)
            {
                A = 4+CountUp/100 - 6 / 100;
                C =3.6f-CountUp / 100 + 6 / 100;
                Debug.Log("スピード" + A);
            }
            else if (CountUp >=60)
            {
                marumode = MARUmode.NONE;
                TEST = -1;
            }
        }
    }
    private void MARUconfig(int Num, int Num2, float Time, float Time2, float Angle, float Angle2)
    {
        _circleBulletNum = Num;
        _circleBulletNum2 = Num2;
        _circleBuletTime = 6;
        _circleBuletTime2 = 6;
        MaruAngle = Angle;
        MaruAngle2 = Angle2;
        _OLDcircleBulletTime = Time;
        _OLDcircleBulletTime2 = Time2;
        _shootCount = 0;
        _shootCount2 = 0;
    }
    private void MARU()
    {
        if (_shootCount < _circleBuletTime) return;
        for (int i = 0; i < _circleBulletNum; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / _circleBulletNum * i - Mathf.Deg2Rad * (_circleAngle + 360f / 2f);
            GameObject bullet = Instantiate(_bullet[0]);
            Bullet shootbullet = bullet.GetComponent<Bullet>();
            shootbullet.Speed(A);
            shootbullet.MoveChange(B,this.gameObject);
            shootbullet.RcfSSTIME(C);
            shootbullet.RcfStopTime(D);
            bullet.transform.position = transform.position;
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
            GameObject bullet = Instantiate(_bullet[1]);
            Bullet shootbullet = bullet.GetComponent<Bullet>();
            shootbullet.Speed(E);
            shootbullet.MoveChange(F,this.gameObject);
            shootbullet.RcfSSTIME(G);
            shootbullet.RcfStopTime(H);
            bullet.transform.position = transform.position;
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
    private void SUMMONenemy(int _C)
    {
        X = 4;
        Y = 4.6f;
        foreach (GameObject ENEMY in _enemy)
        {
            GameObject _ENEMY = Instantiate(ENEMY);
            Enemy enemy = _ENEMY.GetComponent<Enemy>();
            BulletEnemy bulletEnemy = _ENEMY.GetComponent<BulletEnemy>();
            enemy.MOVEchange("none");
            if (SummonCount == 1)
            {
                X = 4;
                Y = -4.5f;
            }
            else if (SummonCount == 2)
            {
                X = -8.7f;
                Y = 4.7f;
            }
            else if (SummonCount == 3)
            {
                X = -8.7f;
                Y = -4.6f;
            }
            if (_C == 0)
            // {
            // bulletEnemy.AttackChange(0,8);
            // }
            // else if(_C==1)
            // {
            // bulletEnemy.AttackChange(0,5);
            // }
            ENEMY.transform.position = new Vector2(X, Y);
            SummonCount++;
        }
    }
    public void ChangePhase(int INT)
    {
        if (INT == 0)
        {
            marumode = MARUmode.C3;
            MARUconfig(3, 6, 0.25f, 0.3f, 12, 5);
            E = 4f;
            G = 1.5f;
            H = 1f;
        }
        // else if (INT == 1)
        // {
        //     marumode = MARUmode.C3;
        //     MARUconfig(3, 6, 0.25f, 0.3f, 12, 5);
        //     E = 4f;
        //     G = 1.5f;
        //     H = 1f;
        //     SUMMONenemy(0);
        // }
        else if (INT == 1)
        {
            marumode = MARUmode.C3;
            MARUconfig(5, 5, 0.2f, 0.4f, 4, 6);
            C = 3.6f;
            E = 3f;
            G = 1.5f;
            H = 1f;
            CountUp = 0;
        }
        else if(INT==10)
        {
            marumode = MARUmode.NONE;
        }
        // else if(INT==3)
        // {
        //     marumode = MARUmode.C3;
        //     MARUconfig(5, 5, 0.2f, 0.4f, 4, 6);
        //     C = 3.6f;
        //     E = 3f;
        //     G = 1.5f;
        //     H = 1f;
        //     CountUp = 0;
        //     SUMMONenemy(1);
        // }
    }
}
