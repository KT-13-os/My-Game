using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BOSSB : BOSS
{
    [SerializeField, Header("Playerを補足するオブジェクト")]
    private GameObject TARGETobject;
    private GameObject TARGET;
    [SerializeField, Header("タレットくん")]
    private GameObject[] turret;
    [SerializeField, Header("予告線を引くオブジェクト")]
    private GameObject Lineobject;
    [SerializeField, Header("GlobaLight2D")]
    private GameObject GlobalLight2D;
    private GameObject Line;
    private GameObject Beam;
    private int _BulletNum;
    private float _circleBulletAngle;
    private float _Bulletspeed;
    private float _shootTime;
    private int _beamNum;
    private float _beamAngle;
    private int _shootnum;
    private float _spellTime;
    private float _spellCount;
    private bool PlaySpell;
    private float SummonX;
    private float SummonY;
    private float _bulletdistance;
    private float _distanceNum;
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
        PlaySpell=false;
        _spellCount=0;
        _spellTime=60;
        _shootnum=0;
        TARGET=Instantiate(TARGETobject);
        // Line.transform.position=new Vector3(-2.5f,0);
        _attack=true;
        _moveMode = MoveMode.M;
        _attackMode = AttackMode.SPELL4;
        // SubGameManager subGameManager = GameManager.GetComponent<SubGameManager>();
        // subGameManager.Count(60);
        _bulletdistance=3;
        _distanceNum=-0.1f;
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
        if(PlaySpell==true)_spellCount+=Time.deltaTime;
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
        // _attackMode=AttackMode.SPELL1;
        // _beamNum=6;
        // _beamAngle=45;
        // Line=Instantiate(Lineobject);
        // StartCoroutine(CROSSBEAM(27,15));
        // _shootTime=6f;
        // return;
        _shootTime=2f;
        _BulletNum=4;
        _beamNum=5;
        _shootCount=0;
        SakuretuOugi();
        StartCoroutine(TURRETBEAM(1));
    }
    private IEnumerator TURRETBEAM(float TIME)
    {
        if(_shootnum<3)yield break;
        _shootnum=0;
        for(int i=0;i<_BulletNum;i++)
        {
        float angleRange = Mathf.Deg2Rad * 360;
        float theta = angleRange / _BulletNum  * i - Mathf.Deg2Rad * (90+ 360 / 2f);
        GameObject TURRET=Instantiate(turret[0]);
        TURRET.transform.position=new Vector3(Mathf.Sin(theta)*1.3f+gameObject.transform.position.x,Mathf.Cos(theta)*1.3f+gameObject.transform.position.y,0);
        TurretScript archerScript=TURRET.GetComponent<TurretScript>();
        StartCoroutine(archerScript.BeamSummon(TIME,_player,TARGET));
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
            bakuhatuBullet.ChangeBakuhatuTime(1.6f);
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
    private void SPELL1()//回転するビームがうざいSPELL(耐久型)
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
        _beamAngle=Random.Range(60,105);
        StartCoroutine(CROSSBEAM(28,20));
        return;
        }
        _shootTime=2;
        _beamNum=4;
        _beamAngle=Random.Range(45,90);
        StartCoroutine(CROSSBEAM(1.6f,24));
    }
    private IEnumerator CROSSBEAM(float TIME,float RotationSpeed)
    {
        Linerenderscript linerenderscript=Line.GetComponent<Linerenderscript>();
        StartCoroutine(linerenderscript.CircleLine(_beamNum,_beamAngle));
        yield return new WaitForSeconds(0.8f);
        for(int i=0;i<_beamNum;i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / _beamNum * i - Mathf.Deg2Rad * (_beamAngle + 360f / 2f);
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
    private void SPELL2()//洗濯機ブラスターみたいなや～つ(耐久)
    {
        _shootCount=0;
        _shootnum++;
        if(_spellCount>=_spellTime)
        {
            _spellCount=0;
            PlaySpell=false;
            _attackMode=AttackMode.A;
            return;
        }
        PlaySpell=true;
        _beamNum=8;
        _beamAngle=90+4*_shootnum;
        StartCoroutine(CROSSBEAM(0.3f,0));
        if(_shootTime<=0.18f)
        {
        _shootTime=0.18f;
        return;
        }
        _shootTime=2-0.05f*_shootnum;
    }
    private void SPELL3()//(耐久)x=-8.9~4.1y=-5~5
    {
        _shootCount=0;
        if(_spellCount>=_spellTime)
        {
            _spellCount=0;
            PlaySpell=false;
            _attackMode=AttackMode.A;
            return;
        }
        PlaySpell=true;
        StartCoroutine(AMIME());
    }
    private IEnumerator BlackOut()
    {
        Light2D light2D=GlobalLight2D.GetComponent<Light2D>();
        for(int i=0;i<51;i++)
        {
            light2D.color-=new Color32(5,5,5,0);
            yield return new WaitForSeconds(0.05f);
        }
    }
    private IEnumerator BlackIn()
    {
        Light2D light2D=GlobalLight2D.GetComponent<Light2D>();
        for(int i=0;i<255;i++)
        {
            light2D.color+=new Color32(1,1,1,0);
            yield return new WaitForSeconds(0.013f);
        }
    }
    private IEnumerator AMIME()
    {
        StartCoroutine(BlackOut());
        SummonY=5;
        SummonX=-9;
        if(_shootnum>=4)StartCoroutine(BlackIn());yield break;
        if(_shootnum==0)
        {
        _shootTime=15;
        _BulletNum=6;
        }
        else if(_shootnum==1)
        {
        _shootTime=14;
        _BulletNum=7;
        }
        else if(_shootnum==2)
        {
        _shootTime=13;
            _BulletNum=7;
        }
        else if(_shootnum==3)
        {
            _shootTime=15;
            _BulletNum=8;
        }
        for(int i=0;i<Mathf.CeilToInt(13f/(10f/(_BulletNum-1))+1);i++)
        {
        GameObject TURRET=Instantiate(turret[0]);
        TurretScript turretScript=TURRET.GetComponent<TurretScript>();
        TURRET.transform.position=new Vector3(SummonX,SummonY,0);
        StartCoroutine(turretScript.straightBeam(new Vector3(SummonX,-5,0),4.5f-0.5f*i,5.2f));
        for(int I=0;I<_BulletNum;I++)
        {
        if(i==0)
        {
        GameObject TURRET1=Instantiate(turret[0]);
        TurretScript turretScript1=TURRET1.GetComponent<TurretScript>();
        TURRET1.transform.position=new Vector3(SummonX,SummonY,0);
        TURRET1.transform.rotation=Quaternion.Euler(0,0,TURRET1.transform.rotation.eulerAngles.z-90);
        StartCoroutine(turretScript1.straightBeam(new Vector3(4,SummonY,0),4.5f-0.3f*I,5.2f));
        }
        GameObject bullet=Instantiate(_bullet[0]);
        Bullet bulletsc=bullet.GetComponent<Bullet>();
        bulletsc.SetNumber(1);
        bullet.transform.position=new Vector3(SummonX,SummonY,0);
        SummonY-=10f/(_BulletNum-1);
        yield return new WaitForSeconds(0.06f);
        }
        SummonY=5;
        SummonX+=13f/Mathf.CeilToInt(13f/(10f/(_BulletNum-1)));
        }
        yield return new WaitForSeconds(6f);
        GameObject[] Bullets=GameObject.FindGameObjectsWithTag("bullet");
        foreach(GameObject BULLET in Bullets)
        {
            Bullet bulletsc=BULLET.GetComponent<Bullet>();
            if(bulletsc==null)continue;
            if(bulletsc.GetNumber()==1)
            {
                bulletsc.Speed(0.3f);
                bulletsc.KasokuCahange(0.1f);
                bulletsc.MoveChange(3,gameObject);
                BULLET.transform.rotation=Quaternion.FromToRotation(transform.up,BULLET.transform.position-_player.transform.position);
                BULLET.transform.rotation=Quaternion.Euler(0,0,BULLET.transform.rotation.eulerAngles.z+Random.Range(-240,-90));
            }
        }
        _shootnum++;
        yield return new WaitForSeconds(4f);
        StartCoroutine(BlackIn());
    }
    private void SPELL4()//(SPELL)
    {
        StartCoroutine(PerforatedCircleBullet());
        PlayerMARU();
    }
    private IEnumerator PerforatedCircleBullet()
    {
        if(_shootnum==30||_shootnum==60)
        {
        _BulletNum=4;
        for(int I=0;I<_BulletNum;I++)
        {
        for (int i = 0; i < 4; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / 4 * i - Mathf.Deg2Rad * (_circleBulletAngle + 360f / 2f);
            GameObject bullet = Instantiate(_bullet[3]);
            Bullet shootbullet = bullet.GetComponent<Bullet>();
            shootbullet.Speed(3);
            shootbullet.KasokuCahange(-3/3);
            shootbullet.MoveChange(1,this.gameObject);
            bullet.transform.position = transform.position;
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        _circleBulletAngle+=(360/(5*2))/_BulletNum;
        }
        }
        if(_shootnum>=90)
        {
            _shootnum=15;
            for(int i=0;i<3;i++)
            {
            Line=Instantiate(Lineobject);
            Linerenderscript linerenderscript=Line.GetComponent<Linerenderscript>();
            StartCoroutine(linerenderscript.TargetLine(transform.position,_player,TARGET=Instantiate(TARGETobject),1f));
            StartCoroutine(BEAM());
            yield return new WaitForSeconds(1.5f);
            }
        }
    }
    private IEnumerator BEAM()
    {
        yield return new WaitForSeconds(1.5f);
        Beam = Instantiate(_bullet[1]);
        Beem _beem = Beam.GetComponent<Beem>();
        StartCoroutine(_beem.BEEMSUMMON(0.6f));
        Beam.transform.position = TARGET.transform.position;
        Vector3 dir = TARGET.transform.position-transform.position;
        Beam.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        Beam.transform.rotation=Quaternion.Euler(0,0,Beam.transform.rotation.eulerAngles.z+90);
    }
    private void PlayerMARU()
    {
        _shootTime=0.1f;
        _BulletNum=6;
        _shootCount=0;
        _shootnum++;
        for(int I=0;I<2;I++)
        {
        for (int i = 0; i < _BulletNum; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / _BulletNum * i - Mathf.Deg2Rad * (_circleBulletAngle+(-90*I) + 360f / 2f);
            GameObject bullet = Instantiate(_bullet[0]);
            Bullet shootbullet = bullet.GetComponent<Bullet>();
            shootbullet.SetNumber(10);
            shootbullet.Speed(0);
            shootbullet.KasokuCahange(0f);
            shootbullet.MoveChange(0,this.gameObject);
            bullet.transform.position = _player.transform.position+new Vector3(Mathf.Cos(theta)*_bulletdistance,Mathf.Sin(theta)*_bulletdistance,0);
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        _circleBulletAngle+=3;
        _BulletNum=4;
        _bulletdistance+=1.5f;
        }
        _bulletdistance-=3;
        if(_bulletdistance>4f)
        {
            _distanceNum*=-1;
        }
        else if(_bulletdistance<1.3f)
        {
            _distanceNum*=-1;
        }
        _bulletdistance+=_distanceNum;
    }
    private void SPELL5()//(SPELL)
    {
    }
}
