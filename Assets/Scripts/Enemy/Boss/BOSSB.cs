using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BOSSB : BOSS
{
    [SerializeField, Header("Playerを補足するオブジェクト")]
    private GameObject TARGETobject;
    private GameObject TARGET;
    [SerializeField, Header("予告線を引くオブジェクト")]
    private GameObject Lineobject;
    private GameObject Line;
    private GameObject Beam;
    private int _BulletNum;
    private float _circleBulletAngle;
    private float _Bulletspeed;
    private float _shootTime;
    private int _beamNum;
    private float _beamAngle;
    private int _shootnum;
    float MHP;
    enum AttackMode
    {
        A,
        NORMAL,
        NORMAL2,
        SPELL1,
        SPELL2,
        SPELL3,
        SPELL4,
        SPELL5,
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
    protected override void Initialize()
    {
        _shootnum=0;
        TARGET=Instantiate(TARGETobject);
        Line=Instantiate(Lineobject);
        _attack=true;
        _moveMode = MoveMode.M;
        _attackMode = AttackMode.NORMAL2;
        MHP = _hp;
        _Bulletspeed=4;
        _shootCount=0;
        _shootTime=3f;
        _circleBulletAngle=90;
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
            _moveMode = MoveMode.M1;
            _attackMode = AttackMode.A;
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
            _attackMode = AttackMode.A;
        }
    }
    private void M2()
    {
        if (_hp <= MHP * 1 / 3)
        {
            _moveMode = MoveMode.M2;
            _attackMode = AttackMode.A;
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
            case AttackMode.NORMAL: NORMAL(); break;
            case AttackMode.NORMAL2: NORMAL2(); break;
            case AttackMode.SPELL1: SPELL1(); break;
            case AttackMode.SPELL2: SPELL2(); break;
            case AttackMode.SPELL3: SPELL3(); break;
            case AttackMode.SPELL4: SPELL4(); break;
            case AttackMode.SPELL5: SPELL5(); break;
        }
    }
    private void A()
    {
    }
    private void NORMAL()
    {
        _BulletNum=13;
        MARU();
    }
    private void MARU()
    {
        for (int i = 0; i < _BulletNum; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / (_BulletNum-1) * i - Mathf.Deg2Rad * (_circleBulletAngle + 360f / 2f);
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
        _circleBulletAngle+=360/_BulletNum;
        _shootTime=0.3f;
        if(_Bulletspeed>=4.8f)
        {
            _Bulletspeed=4;
            _shootTime=1.5f;
            _circleBulletAngle+=(360/_BulletNum)*3;
        }
        _shootCount=0;
        _Bulletspeed+=0.2f;
    }
    private void NORMAL2()
    {
        _attackMode=AttackMode.SPELL1;
        _beamNum=6;
        _beamAngle=45;
        StartCoroutine(CROSSBEAM(27,15));
        _shootTime=6f;
        return;
        _shootTime=2f;
        _BulletNum=4;
        _beamNum=5;
        _shootCount=0;
        SakuretuOugi();
        StartCoroutine(PlayerBEAM());
    }
    private IEnumerator PlayerBEAM()
    {
        if(_shootnum<2)yield break;
        _shootnum=0;
        Linerenderscript linerenderscript=Line.GetComponent<Linerenderscript>();
        StartCoroutine(linerenderscript.TargetLine(_beamNum,this.gameObject.transform.position,_player,TARGET));
        yield return new WaitForSeconds(1.6f);
        for (int i=0;i<_beamNum;i++)
        {
            GameObject bullet = Instantiate(_bullet[1]);
            Beem _beem = bullet.GetComponent<Beem>();
            StartCoroutine(_beem.BEEMSUMMON(1));
            bullet.transform.position = TARGET.transform.position;
            Vector3 dir = new Vector3(transform.position.x+1.1f-(2.5f/_beamNum)*i,transform.position.y+0.4f,0) - TARGET.transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
            bullet.transform.rotation=Quaternion.Euler(0,0,bullet.transform.rotation.eulerAngles.z+90);
        }
    }
    private void SakuretuOugi()
    {
        _shootnum++;
        for (int i = 0; i < _BulletNum; i++)
        {
            float angleRange = Mathf.Deg2Rad * 120;
            Vector2 Dis=transform.position-_player.transform.position;
            float Angle=Mathf.Atan2(Dis.x,Dis.y)*Mathf.Rad2Deg;
            float theta = angleRange / (_BulletNum - 1) * i - Mathf.Deg2Rad * (90+Angle+ 120 / 2f);
            GameObject bullet = Instantiate(_bullet[2]);
            Bullet shootbullet = bullet.GetComponent<Bullet>();
            BakuhatuBullet bakuhatuBullet=bullet.GetComponent<BakuhatuBullet>();
            bakuhatuBullet.ChangeCircleBuletNum(5);
            bakuhatuBullet.ChangeBakuhatuTime(2);
            bakuhatuBullet.ChangeCircleCount(1);
            bakuhatuBullet.ChangeCircleAngle(45*i);
            bakuhatuBullet.ChangeCircleRotate(false);
            bakuhatuBullet.ChangeBound(false);
            shootbullet.Speed(4);
            shootbullet.MoveChange(1,this.gameObject);
            bullet.transform.position = transform.position;
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
    }
    private void SPELL1()
    {
        _shootnum++;
        _shootCount=0;
        if(_shootnum>15)
        {
        _beamNum=4;
        _beamAngle=60;
        _shootTime=2.5f;
        if(_shootnum==26)
        {
         _attackMode=AttackMode.NORMAL;
         _shootnum=0;
         return;
        }
        StartCoroutine(CROSSBEAM(1f,-6));
        return;
        }
        if(_shootnum==15)
        {
        _shootTime=2.5f;
        _beamNum=6;
        _beamAngle=90;
        StartCoroutine(CROSSBEAM(28,20));
        return;
        }
        _shootTime=2;
        _beamNum=4;
        _beamAngle=45;
        StartCoroutine(CROSSBEAM(1.6f,24));
    }
    private IEnumerator CROSSBEAM(float TIME,float RotationSpeed)
    {
        Vector2 Dis=transform.position-_player.transform.position;
        float Angle=Mathf.Atan2(Dis.x,Dis.y)*Mathf.Rad2Deg;
        Line.transform.position=new Vector3(-2.5f,0);
        Linerenderscript linerenderscript=Line.GetComponent<Linerenderscript>();
        StartCoroutine(linerenderscript.CircleLine(_beamNum,_beamAngle+Angle));
        yield return new WaitForSeconds(0.8f);
        for(int i=0;i<_beamNum;i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / _beamNum * i - Mathf.Deg2Rad * (_beamAngle +Angle+ 360f / 2f);
            Beam = Instantiate(_bullet[1]);
            Beem _beem = Beam.GetComponent<Beem>();
            StartCoroutine(_beem.BEEMSUMMON(TIME));
            StartCoroutine(_beem.BEAMRotation(TIME,RotationSpeed,Line));
            Beam.transform.position = Line.transform.position+new Vector3(Mathf.Cos(theta)*12.6f,Mathf.Sin(theta)*12.6f,0);
            Vector3 dir = Line.transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - Line.transform.position;
            Beam.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
            Beam.transform.rotation=Quaternion.Euler(0,0,Beam.transform.rotation.eulerAngles.z+90);
        }
    }
    private void SPELL2()
    {
    }
    private void SPELL3()
    {
    }
    private void SPELL4()
    {
    }
    private void SPELL5()
    {
    }
}
