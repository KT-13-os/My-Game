using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BulletEnemy : Enemy
{
    [SerializeField, Header("炸裂弾を使うか")]
    private bool Sakuretu=false;
    [SerializeField, Header("LR時間")]
    private float _LRAttackTime;
    [SerializeField, Header("LR間隔")]
    private float _LRShootTime;
    [SerializeField, Header("LR幅")]
    private float _LRRange;
    [SerializeField, Header("LR速度")]
    private float _LRSpeed;
    [SerializeField, Header("円形の段数")]
    private int _circleBulletNum;
    [SerializeField, Header("円形攻撃の時間")]
    private float _circleShootTime;
    [SerializeField, Header("円形に弾を発射する間隔")]
    private float _circleBuletTime;
    [SerializeField, Header("扇形の段数")]
    private int _ougiBulletNum;
    [SerializeField, Header("扇の角度")]
    private int _ougiangle;
    [SerializeField, Header("扇の攻撃回数")]
    private int _ougiAttackCountMax;
    [SerializeField, Header("偶数弾の弾数")]
    private int _gusuBulletNum;
    [SerializeField, Header("偶数弾の角度")]
    private int _gusuangle;
    [SerializeField, Header("円形2の段数")]
    private int _circleBulletNum2;
    [SerializeField, Header("円形2に弾を発射する間隔")]
    private float _circleBuletTime2;
    [SerializeField, Header("Maru2Attackでずらす角度")]
    private float Maru2Angle;
    private float _circle2Angle;
    private int _ougiAttackCount;
    [SerializeField, Header("打つ弾の速さのプラス値")]
    private float _Pspeed;
    private float _rotateZ;
    private float _LRAttackCount;
    // private float _circleShootCount;
    private int _circleBulletCountB;
    private int _circleBulletCount;
    private int _SakuretuNum;
    private int _SakuretuCount;
    private float _SakuretuTime;
    private int B = 0;
    private float _Maru3Num;
    private float _Maru3Angle;
    private int _HomeCount;
    Vector3 velocity;

    // 追尾レーザーの初期位置
    Vector3 position;

    Vector3 acceleration;

    // ターゲットが存在しない時の目標位置
    Vector3 randomPos;

    Transform target;

    // 追尾レーザーの初期位置に最も近いオブジェクト
    GameObject searchNearObj;
    [SerializeField, Header("自爆着弾時間")] float period;
    enum AttackMode
    {
        normalAttack,
        sanrenAttack,
        MaruAttack,
        Ougi2Attack,
        ougiAttack,
        jibaku,
        Maru2Attack,
        BossAttack,
        Maru3Attack,
    }
    private AttackMode _attackMode;
    public static BulletEnemy instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    protected override void Atack()
    {
        if(_attackMode==AttackMode.jibaku)
        {
            JIBAKUAttack();
            return;
        }
        if (_bAttack == false) return;
        if (_player == null || !_bAttack) return;
        _shootCount += Time.deltaTime;
        if (_shootCount < _shootTime) return;
        switch (_attackMode)
        {
            case AttackMode.normalAttack: normalAttack(); break;
            case AttackMode.MaruAttack: MaruAttack(); break;
            case AttackMode.Ougi2Attack: Ougi2Attack(); break;
            case AttackMode.ougiAttack: OugiAttack(); break;
            case AttackMode.sanrenAttack: SanrenAttack(); break;
            // case AttackMode.jibaku: JIBAKUAttack(); break;
            case AttackMode.Maru2Attack: Maru2Attack(); break;
            case AttackMode.Maru3Attack: Maru3Attack(); break;
            case AttackMode.BossAttack: BossAttack(); break;
        }
    }
    protected override void Initialize()
    {
        _HomeCount=0;
        _rotateZ = 0f;
        _LRAttackCount = 0f;
        // _circleShootCount = 0f;
        B = 0;
        if (_difficulty.DIFFICULTY == "Easy"||_difficulty.DIFFICULTY=="Tutorial"||_difficulty.DIFFICULTY=="VeryEasy"||_difficulty.DIFFICULTY=="TEST")
        {
            _circleBulletCountB = 2;
            _circleBulletCount = 3;
            _SakuretuNum = 3;
            _SakuretuCount = 1;
            _SakuretuTime = 1;
        }
        else if (_difficulty.DIFFICULTY == "Normal")
        {
            _circleBulletCountB = 3;
            _circleBulletCount = 4;
            _SakuretuNum = 4;
            _SakuretuCount = 1;
            _SakuretuTime = 1;
        }
        else if (_difficulty.DIFFICULTY == "Hard")
        {
            _circleBulletCountB = 4;
            _circleBulletCount = 4;
            _SakuretuNum = 5;
            _SakuretuCount = 1;
            _SakuretuTime = 2;
        }
        //8のやつ
        if (_EnemyScale >= 8.0f)
        {
            if (_EnemyScale >= 9.0f) return;
            _Maru3Angle=0;
            _Maru3Num=4;
            _shootTime=1.6f;
            _Pspeed=1;
            _attackMode = AttackMode.Maru3Attack;
            return;
        }
        //7のやつ
        if (_EnemyScale >= 7.0f)
        {
            if (_EnemyScale >= 8.0f) return;
            _attackMode = AttackMode.Maru2Attack;
            return;
        }
        //6のやつ
        if (_EnemyScale >= 6.0f)
        {
            if (_EnemyScale >= 7.0f) return;
            position = transform.position;
            period += Random.Range(0f, 1f);
            _attackMode = AttackMode.jibaku;
            searchNearObj = FindClosestPlayer();
            // ターゲットが存在すれば位置を取得
            if (searchNearObj != null)
            {
                target = searchNearObj.transform;
            }
            return;
        }
            _shootCount = _shootTime;
        //5のやつ
        if (_EnemyScale >= 5.0f)
        {
            if (_EnemyScale >= 6.0f) return;
            _attackMode = AttackMode.sanrenAttack;
            DifficultyHP(0);
            return;
        }        //_EnemyScaleが4、4.1、4.2のやつ
        if (_EnemyScale >= 4.0f)
        {
            if (_EnemyScale >= 5.0f) return;
            _attackMode = AttackMode.MaruAttack;
            DifficultyHP(3);
            return;
        }
        //_EnemyScaleが3、3.1、3.2のやつ
        // if (_EnemyScale >= 3.0f)
        // {
        //     if (_EnemyScale >= 4.0f) return;
        //     _attackMode = AttackMode.MaruAttack;
        //     return;
        // }
        //_EnemyScaleが2、2.1、2.2のやつ
        if (_EnemyScale >= 2.0f)
        {
            if (_EnemyScale >= 3.0f) return;
            _attackMode = AttackMode.Ougi2Attack;
            DifficultyHP(2);
            return;
        }
        // if (_EnemyScale >= 1.0f)
        // {
        //     if (_EnemyScale >= 2.0f) return;
        //     DifficultyHP(1);
        //     return;
        // }
        //_EnemyScaleが0、0.1、0.2のやつ
        if (_EnemyScale >= 0)
        {
            if (_EnemyScale >= 1.0f) return;
            _attackMode = AttackMode.normalAttack;
            DifficultyHP(0);
            return;
        }
    }
    private void MaruAttack()
    {
        if (B == _circleBulletCountB)
        {
            _shootTime = 3;
            B = 0;
            return;
        }
        else
        {
            _shootTime = 0.3f;
        }
        for (int i = 0; i < _circleBulletNum; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / _circleBulletNum * i - Mathf.Deg2Rad * (90f + 360f / 2f);
            GameObject bullet = Instantiate(_bullet[0]);
            Bullet shootbullet = bullet.GetComponent<Bullet>();
            shootbullet.Speed(_Pspeed);
            shootbullet.KasokuCahange(-0.01f);
            shootbullet.MoveChange(1,this.gameObject);
            bullet.transform.position = transform.position;
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        // _circleBuletTime = 0.25f;
        B++;
        _shootCount = 0f;
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
        shootbullet.MoveChange(1,this.gameObject);
            bullet.transform.position = transform.position;
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        _circle2Angle -= Maru2Angle;
        if (_circle2Angle <= 0f)
        {
            Maru2Angle *= -1;
            _circle2Angle = 90f;
        }
        if (_circle2Angle >= 180f)
        {
            Maru2Angle *= -1;
            _circle2Angle = 90f;
        }
        _circleBuletTime2 = 0.2f;
        _shootCount = 0f;
    }
    private void normalAttack()
    {
        GameObject bulletObj = Instantiate(_bullet[0]);
        Bullet shootbullet = bulletObj.GetComponent<Bullet>();
        shootbullet.Speed(_Pspeed);
        shootbullet.KasokuCahange(-0.01f);
        shootbullet.MoveChange(1,this.gameObject);
        if (Sakuretu == true)
        {
        shootbullet.Speed(4);
        shootbullet.KasokuCahange(-0.01f);
        shootbullet.MoveChange(1,this.gameObject);
            shootbullet.BoundChange(false);
            BakuhatuBullet bakuhatuBullet = bulletObj.GetComponent<BakuhatuBullet>();
            bakuhatuBullet.ChangeCircleBuletNum(_SakuretuNum);
            bakuhatuBullet.ChangeBakuhatuTime(_SakuretuTime);
            bakuhatuBullet.ChangeCircleCount(_SakuretuCount);
            bakuhatuBullet.ChangeCiecleRotateNum(45);
            bakuhatuBullet.ChangeCircleRotate(false);
            bakuhatuBullet.ChangeBound(false);
        }
        bulletObj.transform.position = transform.position;
        Vector3 dir = _player.transform.position - transform.position;
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        // _shootTime=
        _shootCount = 0.0f;
    }
    private void Ougi2Attack()
    {
        _LRAttackCount += Time.deltaTime;
        if (_shootCount < _LRShootTime) return;
        _rotateZ += _LRSpeed;
        for(int i=0;i<2;i++)
        {
        GameObject bulletObj = Instantiate(_bullet[0]);
        Bullet shootbullet = bulletObj.GetComponent<Bullet>();
        shootbullet.Speed(_Pspeed);
        shootbullet.KasokuCahange(-0.01f);
        shootbullet.MoveChange(1,this.gameObject);
        bulletObj.transform.position = transform.position;
        bulletObj.transform.eulerAngles = new Vector3(0f, 0f, -180f*i + _rotateZ);
        }
        _shootCount = 0f;
    }
    private void OugiAttack()
    {
        if (_ougiAttackCount >= _ougiAttackCountMax) return;
        for (int i = 0; i < _ougiBulletNum; i++)
        {
            float angleRange = Mathf.Deg2Rad * _ougiangle;
            float theta = angleRange / (_ougiBulletNum - 1) * i - Mathf.Deg2Rad * (90f + _ougiangle / 2f);
            GameObject bullet = Instantiate(_bullet[0]);
        Bullet shootbullet = bullet.GetComponent<Bullet>();
            shootbullet.Speed(_Pspeed);
        shootbullet.KasokuCahange(-0.01f);
        shootbullet.MoveChange(1,this.gameObject);
            bullet.transform.position = transform.position;
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        _ougiAttackCount++;
        _shootCount = 0f;
    }
    private void SanrenAttack()
    {
        if (B == _circleBulletCount)
        {
            _shootTime = 0.4f;
            B = 0;
            return;
        }
        else
        {
            _shootTime = 0.15f;
        }
        GameObject bulletObj = Instantiate(_bullet[0]);
        Bullet shootbullet = bulletObj.GetComponent<Bullet>();
        shootbullet.Speed(_Pspeed);
        shootbullet.KasokuCahange(-0.01f);
        shootbullet.MoveChange(1,this.gameObject);
        bulletObj.transform.position = transform.position;
        Vector3 dir = _player.transform.position - transform.position;
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        B++;
        _shootCount = 0;
    }
    private void JIBAKUAttack()
    {
        searchNearObj = FindClosestPlayer();
        // ターゲットが存在すれば位置を取得
        if (searchNearObj != null)
        {
            target = searchNearObj.transform;
        }
        acceleration = Vector3.zero;
        if (searchNearObj != null)
        {
            Vector3 diff = target.position - position;
            acceleration += (diff - velocity * period) * 2f / (period * period);
        }
        else
        {
            Vector3 diff = randomPos - position;
            acceleration += (diff - velocity * period) * 2f / (period * period);
        }
        if (acceleration.magnitude > 10f)
        {
            acceleration = acceleration.normalized * 10f;
        }
        period -= Time.deltaTime;
        if (period < 0f)
        {
            period = 1;
            return;
        }
        //いじらない
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;
        transform.position = position;
    }
    public GameObject FindClosestPlayer()
    {
        // EnemyのTagを持つゲームオブジェクトの配列
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        // 最も近い位置に存在するEnemy
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        // 最も近かったEnemyを返す
        return closest;
    }
    private void Maru3Attack()
    {
        if (B == 1)
        {
            _Pspeed=1;
            _HomeCount++;
            B = 0;
            if(_HomeCount>=5)
            {
                KansuMOVE("EXIT",6,8,0);
            }
            return;
        }
        else
        {
            _Pspeed=4;
        }
        for(int I=0;I<10;I++)
        {
        for (int i = 0; i < _Maru3Num; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / _Maru3Num * i - Mathf.Deg2Rad * (_Maru3Angle + 360f / 2f);
            GameObject bullet = Instantiate(_bullet[0]);
            Bullet shootbullet = bullet.GetComponent<Bullet>();
            shootbullet.Speed(_Pspeed);
            shootbullet.KasokuCahange(-_Pspeed/3);
            shootbullet.MoveChange(1,this.gameObject);
            bullet.transform.position = transform.position;
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        _Maru3Angle+=4.5f;
        }
        _shootCount=0;
        B++;
    }
    private void BossAttack()
    {
        GameObject bulletObj = Instantiate(_bullet[1]);
        Bullet shootbullet = bulletObj.GetComponent<Bullet>();
        shootbullet.Speed(1);
        shootbullet.KasokuCahange(-0.01f);
        shootbullet.MoveChange(1,this.gameObject);
        bulletObj.transform.position = transform.position;
        Vector3 dir = _player.transform.position - transform.position;
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        _shootCount = 0.0f;
    }
    public void AttackChange(int POINT,float TIME)
    {
        if(POINT==0)
        {
            _attackMode = AttackMode.BossAttack;
            _shootTime = TIME;
        }
    }
}