using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Sequence=DG.Tweening.Sequence;

public class BOSSB : BOSS
{
    [SerializeField, Header("Playerを補足するオブジェクト")]
    private GameObject TARGETobject;
    private GameObject TARGET;
    [SerializeField, Header("bossBOME")]
    private GameObject BOME;
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
    private string nextSPELL;
    private bool _Movenow;
    private Sequence MoveSequence;
    private int MoveNum;
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
        M4,
        M5,
        M6,
    }
    private MoveMode _moveMode;
    protected override void Initialize()
    {
        MoveNum=1;
        _Movenow=false;
        PlaySpell=false;
        _spellCount=0;
        _spellTime=60;
        _shootnum=0;
        TARGET=Instantiate(TARGETobject);
        // Line.transform.position=new Vector3(-2.5f,0);
        _moveMode = MoveMode.M;
        // SubGameManager subGameManager = GameManager.GetComponent<SubGameManager>();
        // subGameManager.Count(60);
        DifficultyHP(3);
        nextSPELL="SPELL1";
        _bulletdistance=3;
        _distanceNum=-0.1f;
        _Bulletspeed=4;
        _shootCount=0;
        _shootTime=3f;
        _circleBulletAngle=90;
    }
    private void DifficultyHP(int _HP)
    {
        if (_difficulty.DIFFICULTY == "Easy"||_difficulty.DIFFICULTY=="VeryEasy")
        {
            _hp = _difficulty.easyBOSSHP[_HP];
            _PhaseHP = (_hp/4)*3;
        }
        else if (_difficulty.DIFFICULTY == "Normal")
        {
            _hp = _difficulty.normalBOSSHP[_HP];
            _PhaseHP = (_hp/5)*4;
        }
        else if (_difficulty.DIFFICULTY == "Hard")
        {
            _hp = _difficulty.hardBOSSHP[_HP];
            _PhaseHP = (_hp/6)*5;
        }
        _Mhp = _hp;
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
            case MoveMode.M5: M5(); break;
            case MoveMode.M6: M6(); break;
        }
    }
    private void M()
    {
        if (transform.position.y <= 3.5f)
        {
            _rigid.velocity = Vector2.zero;
            _moveMode = MoveMode.M1;
            _attackMode=AttackMode.NORMAL;
        }
        else
        {
            _rigid.velocity = Vector2.down * 2;
        }
    }
    private void M1()//NORMAL
    {
        if (_hp <= _PhaseHP)
        {
            if(_difficulty.DIFFICULTY=="VeryEasy"||_difficulty.DIFFICULTY=="Easy")
            {
                _PhaseHP=_Mhp/4*2;
            }
            else if(_difficulty.DIFFICULTY=="Normal")
            {
                _PhaseHP=_Mhp/5*3;
            }
            else if(_difficulty.DIFFICULTY=="Hard")
            {
                _PhaseHP=_Mhp/6*4;
            }
            _beamNum=6;
            _beamAngle=45;
            _shootTime=6f;
            _moveMode = MoveMode.M2;
            StartCoroutine(SpellChange());
            SpriteRenderer spriteRenderer=GetComponent<SpriteRenderer>();
            spriteRenderer.color=new Color32(255,255,255,100);
            Collider2D collider2D=GetComponent<PolygonCollider2D>();
            collider2D.enabled=false;
        }
    }
    private void M2()//SPELL1
    {
    }
    private void M3()//SPELL4
    {
        if(_hp<=_PhaseHP)
        {
            if(_difficulty.DIFFICULTY=="VeryEasy"||_difficulty.DIFFICULTY=="Easy")
            {
                _PhaseHP=0;
            }
            else if(_difficulty.DIFFICULTY=="Normal")
            {
                _PhaseHP=_Mhp/5*1;
            }
            else if(_difficulty.DIFFICULTY=="Hard")
            {
                _PhaseHP=_Mhp/6*2;
            }
            _moveMode = MoveMode.M4;
            StartCoroutine(SpellChange());
        }
    }
    private void M4()//NORMAL2
    {
        if(_hp<=_PhaseHP)
        {
            if(_difficulty.DIFFICULTY=="Normal")
            {
                _PhaseHP=0;
            }
            else if(_difficulty.DIFFICULTY=="Hard")
            {
                _PhaseHP=_Mhp/6*1;
            }
            _moveMode = MoveMode.M5;
            StartCoroutine(SpellChange());
        }
        if(_Movenow==false)
        {
            Vector3 position=transform.position;
            _Movenow=true;
            MoveSequence=DOTween.Sequence().Append(transform.DOMove(new Vector3(position.x+4*MoveNum,position.y+Random.Range(-0.8f,0.8f),0),Random.Range(2.5f,5f)))
            .Append(transform.DOMove(position,Random.Range(2.5f,5f))).AppendInterval(0.3f).AppendCallback(() =>_Movenow=false);
            MoveNum*=-1;
        }
    }
    private void M5()//SPELL2
    {
        if(_hp<=_PhaseHP)
        {
            if(_difficulty.DIFFICULTY=="Normal")
            {
                _PhaseHP=0;
            }
            else if(_difficulty.DIFFICULTY=="Hard")
            {
                _PhaseHP=0;
            }
            _moveMode = MoveMode.M6;
            StartCoroutine(SpellChange());
            SpriteRenderer spriteRenderer=GetComponent<SpriteRenderer>();
            spriteRenderer.color=new Color32(255,255,255,100);
            Collider2D collider2D=GetComponent<PolygonCollider2D>();
            collider2D.enabled=false;
        }
    }
    private void M6()//SPELL3
    {
        if(_Movenow==false)
        {
            Vector3 position=transform.position;
            _Movenow=true;
            MoveSequence=DOTween.Sequence().Append(transform.DOMove(new Vector3(position.x+4*MoveNum,position.y+Random.Range(-0.8f,0.8f),0),Random.Range(2.5f,5f)))
            .Append(transform.DOMove(position,Random.Range(2.5f,5f))).AppendInterval(0.3f).AppendCallback(() =>_Movenow=false);
            MoveNum*=-1;
        }
        if(_hp<=_PhaseHP)
        {
            _attack = false;
            gamemanager.Score(_score);
            _slider.DESTROY();
            playerscripts.MUTEKI();
            gamemanager.ClearEffect();
            gameObject.SetActive(false);
            _moveMode = MoveMode.M;
            _attackMode=AttackMode.A;
        }
    }
    // private void M7()
    // {
    // }
    private IEnumerator SpellChange()
    {
        GameObject bome=Instantiate(BOME);
        BOOM boom=bome.GetComponent<BOOM>();
        bome.transform.position=gameObject.transform.position;
        boom.BOOMScale(8,8);
        // for(int i=0;i<3;i++)
        // {
        // GameObject HPUpItemObj = Instantiate(_Item[2]);
        // HPUpItemObj.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0, 3), gameObject.transform.position.y + Random.Range(0, 3));
        // }
        yield return new WaitForSeconds(2.3f);
            switch (nextSPELL)
            {
                case "NORMAL": _attackMode = AttackMode.NORMAL;nextSPELL="SPELL1"; break;//1
                case "SPELL1": _attackMode = AttackMode.SPELL1;nextSPELL="SPELL4";StartCoroutine(CROSSBEAM(28,15));subGameManager.Count(60); break;//2
                case "SPELL4": _attackMode = AttackMode.SPELL4;nextSPELL="NORMAL2"; break;//3
                // case "SPELL5": _attackMode = AttackMode.SPELL5;nextSPELL="NORMAL2"; break;//4
                case "NORMAL2": _attackMode = AttackMode.NORMAL2;nextSPELL="SPELL2"; break;//4
                case "SPELL2": _attackMode = AttackMode.SPELL2;nextSPELL="SPELL3";subGameManager.Count(60); break;//5
                case "SPELL3": _attackMode = AttackMode.SPELL3;_shootnum=0; break;//6
            }
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
        _shootCount=0;
        if(subGameManager.GetCount()<=30)
        {
        if(subGameManager.GetCount()<28)
        {
        _beamNum=4;
        _beamAngle=60;
        _shootTime=2.5f;
        if(subGameManager.CheckCount()==false)
        {
        if(_difficulty.DIFFICULTY=="VeryEasy"||_difficulty.DIFFICULTY=="Easy")
        {
            _hp-=_Mhp/4;
            _slider.BeInjured(_Mhp/4);
            _PhaseHP=_Mhp/4*1;
        }
        else if(_difficulty.DIFFICULTY=="Normal")
        {
            _hp-=_Mhp/5;
            _slider.BeInjured(_Mhp/5);
            _PhaseHP=_Mhp/5*2;
        }
        else if(_difficulty.DIFFICULTY=="Hard")
        {
            _hp-=_Mhp/7;
            _slider.BeInjured(_Mhp/7);
            _PhaseHP=_Mhp/7*4;
        }
        SpriteRenderer spriteRenderer=GetComponent<SpriteRenderer>();
        spriteRenderer.color=new Color32(255,255,255,255);
        Collider2D collider2D=GetComponent<PolygonCollider2D>();
        collider2D.enabled=true;
        _moveMode = MoveMode.M3;
        StartCoroutine(SpellChange());
         _shootnum=0;
         return;
        }
        StartCoroutine(CROSSBEAM(1f,-5));
        return;
        }
        _shootTime=2.5f;
        _beamNum=6;
        _beamAngle=Random.Range(60,105);
        StartCoroutine(CROSSBEAM(28,20));
        return;
        }
        _shootTime=1.5f;
        _beamNum=4;
        _beamAngle=Random.Range(45,90);
        StartCoroutine(CROSSBEAM(1.3f,24));
    }
    private IEnumerator CROSSBEAM(float TIME,float RotationSpeed)
    {
        Line=Instantiate(Lineobject);
        Line.transform.position=new Vector3(-3,0,0);
        Linerenderscript linerenderscript=Line.GetComponent<Linerenderscript>();
        StartCoroutine(linerenderscript.CircleLine(_beamNum,_beamAngle,TIME));
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
            _PhaseHP=0;
            _hp-=_Mhp/7;
            _slider.BeInjured(_Mhp/7);
            _PhaseHP=0;
            _moveMode = MoveMode.M6;
            StartCoroutine(SpellChange());
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
    private void SPELL3()//(SPELL)x=-8.9~4.1y=-5~5
    {
        _shootCount=0;
        if(_spellCount>=_spellTime)
        {
            _spellCount=0;
            PlaySpell=false;
            if(_difficulty.DIFFICULTY=="Hard")
            {
                _hp-=_Mhp/7;
                _PhaseHP=0;
            }
            SpellChange();
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
