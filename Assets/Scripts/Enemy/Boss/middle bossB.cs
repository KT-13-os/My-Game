using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using Random =  UnityEngine.Random;

public class middlebossB : Enemy
{
    [SerializeField, Header("打つ弾の速さ")]
    private float A1BulletSpeed;
    [SerializeField, Header("円形に弾を発射する間隔")]
    private float _shootAngle;
    [SerializeField, Header("円形に弾を発射する間隔")]
    private GameObject TARGETobject;
    private GameObject TARGET;
    private int _BulletCount;
    //プレイヤーを中心に呼び出す円の半径
    private float _bulletdistance;
    //trueの間はその種類の攻撃をストップさせる
    private bool PlayBullet;
    private Transform Target;
    //弾を何回撃ったのか
    private int PlayerMaruNum;
    private int _beemNum;
    private int _beemCountmax;
    private int _beemAngle;
    //BEEMを呼び出した回数
    private int _beemCount;
    private int GURUGURUNum;
    private int MARUcountnum;
    private int PLAYERMARUnummax;
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
        PlayBullet=false;
        TARGET=Instantiate(TARGETobject);
        DIFFICULTYcheak();
        _bulletdistance=3;
        _BulletCount=0;
        _shootTime=3;
        _difficulty.Phase=1;
        middleDifficultyHP(0);
        _Mhp = _hp;
        if (_difficulty.DIFFICULTY == "Easy" ||_difficulty.DIFFICULTY=="VeryEasy")
        {
            _PhaseHP = _Mhp/2;
        }
        else if(_difficulty.DIFFICULTY=="Hard"|| _difficulty.DIFFICULTY == "Normal")
        {
                _PhaseHP=_Mhp/3*2;
        }
        _attackMode = AttackMode.A1;
    }
    private void DIFFICULTYcheak()
    {
        if(_difficulty.DIFFICULTY=="Easy"||_difficulty.DIFFICULTY=="VeryEasy")
        {
        GURUGURUNum=1;
        MARUcountnum=10;
        PLAYERMARUnummax=1;
        }
        else if(_difficulty.DIFFICULTY=="Normal")
        {
        GURUGURUNum=1;
        MARUcountnum=5;
        PLAYERMARUnummax=2;
        _beemNum=13;
        _beemAngle=90;
        _beemCount=3;
        _beemCountmax=3;
        }
        else if(_difficulty.DIFFICULTY=="Hard")
        {
        GURUGURUNum=2;
        MARUcountnum=5;
        PLAYERMARUnummax=3;
        _beemNum=13;
        _beemAngle=90;
        _beemCount=3;
        _beemCountmax=2;
        }
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
        if(_hp<=_PhaseHP)
        {
            _attackMode=AttackMode.A2;
            if(_difficulty.DIFFICULTY=="Easy"||_difficulty.DIFFICULTY=="VeryEasy")
            {
                _PhaseHP=-1000000000000000;
            }
            else{
            _PhaseHP=_Mhp/3;
            }
            PhaseChange();
            return;
        }
        GURUGURU();
        MARU();
    }
    private void GURUGURU()
    {
        if(_shootCount<_shootTime)return;
        for(int i=0;i<2;i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            if(i==1)
            {
            for(int I=0;I<GURUGURUNum;I++)
            {
            float theta = angleRange / GURUGURUNum * I - Mathf.Deg2Rad * (-_shootAngle + 720 / 2f);
            GameObject shootbullet = Instantiate(_bullet[0]);
            Bullet bullet = shootbullet.GetComponent<Bullet>();
            bullet.MoveChange(3,this.gameObject);
            bullet.KasokuCahange(A1BulletSpeed/2);
            bullet.Speed(A1BulletSpeed);
            shootbullet.transform.position = transform.position+new Vector3(-1.3f,1.6f,0);
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            shootbullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
            }
            }
            else
            {
            for(int I=0;I<GURUGURUNum;I++)
            {
            float theta = angleRange / GURUGURUNum * I - Mathf.Deg2Rad * (_shootAngle + 360f / 2f);
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
        }
            _BulletCount++;
            _shootAngle+=10;
            _shootCount=0;
            _shootTime=0.2f;
    }
    private void MARU()
    {
        if(_BulletCount>=MARUcountnum)
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
        if(_hp<=_PhaseHP)
        {
            if(PlayBullet==true)
            {
                StopCoroutine(PlayerMARU(PlayerMaruNum));
            }
            _attackMode=AttackMode.A3;
            PhaseChange();
            return;
        }
        if(_player==null)
        {
            StopCoroutine(PlayerMARU(0));
            return;
        }
        if(PlayBullet==true)return;
        StartCoroutine(PlayerMARU(PLAYERMARUnummax));
    }
    // private void NormalShoot()
    // {
    //     if(_shootCount<_shootTime)return;
    //     if (BulletCount == 3)
    //     {
    //         _shootTime = 3f;
    //         BulletCount = 0;
    //         return;
    //     }
    //     else
    //     {
    //         _shootTime = 0.8f;
    //     }
    //     GameObject bulletObj = Instantiate(_bullet[2]);
    //     Bullet shootbullet = bulletObj.GetComponent<Bullet>();
    //     shootbullet.Speed(5);
    //     shootbullet.KasokuCahange(-0.5f);
    //     shootbullet.MoveChange(3,this.gameObject);
    //     bulletObj.transform.position = transform.position;
    //     Vector3 dir = _player.transform.position - transform.position;
    //     bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, -dir);
    //     BulletCount++;
    //     _shootCount = 0;
    // }
    private IEnumerator PlayerMARU(int MARUnum)
    {
        PlayerMaruNum=6;
        TARGET.transform.position=_player.transform.position;
        Target=TARGET.transform;
        PlayBullet=true;
        for(int i1=0;i1<MARUnum;i1++)
        {
        for(int I=0;I<2;I++)
        {
        for (int i = 0; i < PlayerMaruNum; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / PlayerMaruNum * i - Mathf.Deg2Rad * (90+360/(PlayerMaruNum*2)*I + 360f / 2f);
            GameObject bullet = Instantiate(_bullet[Random.Range(4,10)]);
            Bullet shootbullet = bullet.GetComponent<Bullet>();
            shootbullet.SetNumber(I);
            shootbullet.Speed(1);
            shootbullet.KasokuCahange(0.6f);
            shootbullet.MoveChange(0,this.gameObject);
            bullet.transform.position = Target.position+new Vector3(Mathf.Cos(theta)*_bulletdistance,Mathf.Sin(theta)*_bulletdistance,0);
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
            yield return new WaitForSeconds(0.01f);
        }
        }
        Target.transform.position=new Vector3(Random.Range(-9,9),3,0);
        PlayerMaruNum=7;
        }
        for(int i=0;i<2;i++)
        {
        yield return new WaitForSeconds(1.3f*(i+1));
        GameObject[] _bullets=GameObject.FindGameObjectsWithTag("bullet");
        foreach(GameObject bullet in _bullets)
        {
            Bullet shootbullet=bullet.GetComponent<Bullet>();
            if(shootbullet==null)continue;
            if(shootbullet.GetNumber()==i)
            {
                shootbullet.MoveChange(3,this.gameObject);
                shootbullet.SetNumber(100);
                if(_player==null)yield break;
                bullet.transform.rotation=Quaternion.FromToRotation(transform.up,bullet.transform.position-_player.transform.position);
            }
        }
        }
        yield return new WaitForSeconds(2f);
        PlayBullet=false;
    }
    private void A3()
    {
        if(_hp<=0)
        {
            _attackMode=AttackMode.A;
            Linerenderscript linerenderscript=TARGET.GetComponent<Linerenderscript>();
            linerenderscript.STOPline();
            return;
        }
        _shootCount=0;
        _shootTime=3;
        StartCoroutine(BEEM());
        if(_beemCount>=_beemCountmax)
        {
        _beemCount=0;
        if(_player==null)
            {
                return;
            }
        StartCoroutine(PlayerMARU(1));
        }
    }
    private IEnumerator BEEM()
    {
        TARGET.transform.position=gameObject.transform.position;
        Linerenderscript linerenderscript=TARGET.GetComponent<Linerenderscript>();
        StartCoroutine(linerenderscript.middleBossBLine(_beemNum,_beemAngle));
        yield return new WaitForSeconds(1f);
        for (int i=0;i<_beemNum;i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / _beemNum * i - Mathf.Deg2Rad * (_beemAngle + 360f / 2f);
            GameObject bullet = Instantiate(_bullet[3]);
            Beem _beem = bullet.GetComponent<Beem>();
            bullet.transform.position = transform.position+new Vector3(Mathf.Cos(theta)*6.2f,Mathf.Sin(theta)*6.2f,0);
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
            bullet.transform.rotation=Quaternion.Euler(0,0,bullet.transform.rotation.eulerAngles.z+90);
        }
        _beemCount++;
        _beemAngle+=30;
    }
}
