using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
public class PlayerScripts : MonoBehaviour
{
    [SerializeField, Header("GlobalLight2D")]
    private GameObject _GlobalLight2D;
    [SerializeField, Header("glaze circle")]
    private GameObject _glazecircle;
    private Glazecircle _GlazeCircle;
    [SerializeField, Header("DIFFICULTY")]
    protected Difficulty _difficulty;
    [SerializeField, Header("移動速度")]
    private float _speed;
    [SerializeField, Header("弾オブジェクト")]
    private GameObject[] _bullet;
    [SerializeField, Header("BOOM")]
    private GameObject _BOOM;
    [SerializeField, Header("弾を発射する時間")]
    private float _shootTime;
    [SerializeField, Header("残機")]
    private int _hp;
    [SerializeField, Header("点滅時間")]
    private float _damageTime;
    [SerializeField, Header("点滅周期")]
    private float _damageCycle;
    [SerializeField, Header("死亡エフェクト")]
    private GameObject _deadEffect;
    [SerializeField, Header("ボムの数")]
    private int _boom;
    [SerializeField, Header("ホーミング弾の間隔")]
    private float _BshootMaxCount;
    [SerializeField, Header("GameManager")]
    private GameObject _GameManager;
    [SerializeField, Header("PlayerBullet")]
    private GameObject _playerbullet;
    [SerializeField, Header("HPIcon")]
    private GameObject _hpIcon;
    [SerializeField, Header("SE")]
    private AudioClip[] _sound;
    AudioSource _audioSource;
    private HPIcon _hpicon;
    private Vector2 _inputVelocity;
    private Rigidbody2D _rigid;
    private SpriteRenderer _spriteRenderer;
    private GameManager _gameManager;
    Bullet bullet;
    private float _shootCount;
    private float _lowspeed;
    private float _damageTimeCount;
    private bool _bdamage;
    private float _powercount;
    private int _boomCount;
    private float _BshootCount;
    private float _levelPower;
    private float _levelPowerburst;
    // private bool _bshoot;
    private bool not;
    private float _PowerLevel;
    private int _power;
    private GameObject[] Item;
    private bool Muteki;
    private float X;
    private float Y;
    private bool _notshoot;
    private bool _infinity = false;
    private bool _infinityBOOM=false;
    public bool Tutorial = false;
    private bool _BOOMstatus;
    enum ShootingMode
    {
        A1,
        A2,
    }
    private ShootingMode _shootingMode;
    void Start()
    {
        //初期化
        _GlazeCircle = _glazecircle.GetComponent<Glazecircle>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        Muteki = false;
        _shootingMode = ShootingMode.A1;
        _powercount = 0;
        _PowerLevel = 0;
        _boom = 2;
        _power = 1;
        not = false;
        _inputVelocity = Vector2.zero;
        _hpicon = _hpIcon.GetComponent<HPIcon>();
        bullet = _playerbullet.GetComponent<Bullet>();
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameManager = _GameManager.GetComponent<GameManager>();
        _shootCount = 0;
        _lowspeed = _speed / 2.5f;
        _damageTimeCount = 0;
        // _BOOMstatus = false;
        _bdamage = false;
        // _bshoot = false;
        _notshoot = false;
        _BshootCount = _BshootMaxCount;
        _levelPower = 10;
        bullet.ResetA(_power);
        if (_difficulty.DIFFICULTY == "Easy"||_difficulty.DIFFICULTY == "Tutorial"||_difficulty.DIFFICULTY=="VeryEasy"||_difficulty.DIFFICULTY=="TEST")
        {
            _levelPowerburst = 1.4f;
        }
        else if (_difficulty.DIFFICULTY == "Normal")
        {
            _levelPowerburst = 1.3f;
        }
        else if(_difficulty.DIFFICULTY=="Hard")
        {
            _levelPowerburst = 1.3f;
        }
        if (Tutorial == true||_difficulty.PlayerInfinity=="INFINITY")
        {
            Infinity();
        }
        TutorialStart();
    }
    void Update()
    {
        if (not == true) return;
        Move();
        Damage();
        if (_notshoot == true) return;
        if (Input.GetKey(KeyCode.Z))
        {
            // Shooting();
        switch (_shootingMode)
        {
            case ShootingMode.A1: Shooting(); break;
            case ShootingMode.A2:A2Shooting(); break;
        }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_BOOMstatus == true) return;
            BOMB();
        }
    }
    //Playerの移動
    private void Move()
    {
        if (Input.GetKey(KeyCode.X))
        {
            _rigid.velocity = _inputVelocity * _lowspeed;
            return;
        }
        _rigid.velocity = _inputVelocity * _speed;
    }
    //入力を感知して_inoutVelocityに入力された方向を代入
    public void OnMove(InputAction.CallbackContext context)
    {
        _inputVelocity = context.ReadValue<Vector2>();
    }
    private void Shooting()
    {
        _shootCount += Time.deltaTime;
        if (_shootCount < _shootTime) return;
        _audioSource.PlayOneShot(_sound[0]);
        GameObject bulletObj = Instantiate(_bullet[0]);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.Speed(15);
        bullet.MoveChange(3,this.gameObject);
        bulletObj.transform.position = transform.position + new Vector3(0f, transform.lossyScale.y, 0f);
        _shootCount = 0.0f;
    }
    private void BShooting(int A,int Bullet)
    {
        _BshootCount += Time.deltaTime;
        if (_BshootCount >= _BshootMaxCount)
        {
            for (int i = 0; i < A; i++)
            {
                GameObject bulletObjb = Instantiate(_bullet[Bullet]);
                bulletObjb.transform.position = transform.position + new Vector3(0f, transform.lossyScale.y / 1.0f, 0f);
                _BshootCount = 0;
            }
        }
    }
    private void A2Shooting()
    {
        Shooting();
        BShooting(10,1);
    }
    //弾に当たった時
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Muteki == true) return;
        if (not == true) return;
        if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "Enemy")
        {
            Hit(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (not == true) return;
        if (Muteki == true) return;
        if (collision.gameObject.tag == "bullet"|| collision.gameObject.tag == "Enemy")
        {
            Hit(collision.gameObject);
        }
    }
    private void Damage()
    {
        if (!_bdamage) return;
        _damageTimeCount += Time.deltaTime;
        float value = Mathf.Repeat(_damageTimeCount, _damageCycle);
        _spriteRenderer.enabled = value >= _damageCycle * 0.5f;
        if (_damageTimeCount >= _damageTime)
        {
            _damageTimeCount = 0;
            _spriteRenderer.enabled = true;
            _bdamage = false;
        }
    }
    private void Hit(GameObject hitObj)
    {
        if (Muteki == true) return;
        if (not == true) return;
        if (_bdamage == true) return;
        _bdamage = true;
        if (_infinity == true) return;
        if (hitObj.tag == "bullet")
        {
            _hp--;
            // StartCoroutine(DamageEffect());
        }
        else if (hitObj.tag == "Enemy")
        {
            _hp--;
            // StartCoroutine(DamageEffect());
        }
        if (_hp <= 0)
        {
            _GlazeCircle.enabled=false;
            Destroy(gameObject);
            Instantiate(_deadEffect, transform.position, Quaternion.identity);
            _gameManager.DeadEffect();
            Shake();
        }
    }
    // private IEnumerator DamageEffect()
    // {
    //     _GlobalLight2D.GetComponent<Light2D>().color=new Color32(255,100,100,0);
    //     yield return new WaitForSeconds(0.001f);
    //     for(int i=0;i<30;i++)
    //     {
    //     _GlobalLight2D.GetComponent<Light2D>().color+=new Color32(0,9,9,0);
    //     yield return new WaitForSeconds(0.001f);
    //     }
    // }
    public void Powerup1()
    {
        if (_PowerLevel > 8) return;
        _powercount++;
        // _gameManager.LEVELPOWER(_powercount, _levelPower);
        if (_powercount >= _levelPower)
        {
            _PowerLevel += 1;
            _power++;
            // bullet.PowerUp(_power);
            _levelPower *= _levelPowerburst;
            _powercount -= _levelPower;
            _gameManager.POWERLEVEL(_power);
            if (_powercount < 0)
            {
                _powercount = 0;
            }
            if (_PowerLevel >= 11f) return;
            if (_PowerLevel >= 3)
            {
                _shootingMode = ShootingMode.A2;
                // _bshoot = true;
            }
            if (_PowerLevel >= 4)
            {
                _BshootMaxCount = (_BshootMaxCount*5)/8;
            }
        }
    }
    public void Boomup()
    {
        _boomCount++;
        if (_boomCount >= 50)
        {
            _boomCount = 0;
            if (_boom >= 3) return;
            _boom++;
        }
    }
    public void HPUp()
    {
        if (_difficulty.DIFFICULTY == "Tutorial") return;
        if(_hp>=8)return;
        _hp++;
        _hpicon.HpUp();
    }
    public int GetHP()
    {
        return _hp;
    }
    public int GetBOOM()
    {
        return _boom;
    }
    private void BOMB()
    {
        if (_boom <= 0) return;
        _BOOMstatus = true;
        StartCoroutine(BOOMCountDown());
        _BOOM.transform.position=gameObject.transform.position;
        _BOOM.SetActive(true);
        if(_difficulty.Phase<=0)
        {
            BOOM boomScripts=_BOOM.GetComponent<BOOM>();
            boomScripts.BOOMScale(2.5f,2.5f);
        }
        else
        {
            BOOM boomScripts=_BOOM.GetComponent<BOOM>();
            boomScripts.BOOMScale(1,1);
        }
        if (_infinityBOOM == true) return;
        _boom--;
    }
    private IEnumerator BOOMCountDown()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1);
        }
        _BOOMstatus = false;
    }
    public bool Isdamage()
    {
        return _bdamage;
    }
    public void Shake()
    {
        var impulseSource = GetComponent<CinemachineImpulseSource>();
        impulseSource.GenerateImpulse();
    }
    public void MUTEKI()
    {
        Muteki = true;
    }
    public void MUTEKIOFF()
    {
        Muteki = false;
    }
    public void STOP()
    {
        _notshoot = true;
        not = true;
    }
    public void STOPOFF()
    {
        _notshoot = false;
        not = false;
    }
    public void TutorialStart()
    {
        X = transform.position.x;
        float FLOAT = Mathf.Pow(X + 9, 2);
        Y = FLOAT / 8 + 5 / 2;
        _power = 0;
        StartCoroutine(TutorialMove());
    }
    private IEnumerator TutorialMove()
    {
        for (int i = 0; i < 60; i++)
        {
            transform.DOMove(new Vector2(X, Y), 1f);
            X += 0.1f;
            float FLOAT = Mathf.Pow(X + 9, 2);
            Y = FLOAT * 5 / 36 - 6;
            yield return new WaitForSeconds(0.02f);
        }
        not = false;
    }
    public void Infinity()
    {
        _infinityBOOM = true;
        _infinity = true;
        _hp = 1;
    }
}
