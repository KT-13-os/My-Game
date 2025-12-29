using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("KansuMove目的地X")]
    private float Targetx;
    [SerializeField, Header("KansuMove目的地Y")]
    private float Targety;
    [SerializeField, Header("中ボスなのか?")]
    protected bool _middle = false;
    [SerializeField, Header("DIFFICULTY")]
    protected Difficulty _difficulty;
    [SerializeField, Header("弾オブジェクト")]
    protected GameObject[] _bullet;
    [SerializeField, Header("弾を発射する時間")]
    protected float _shootTime;
    [SerializeField, Header("HP倍率")]
    private float _hpEXP=1;
    protected float _hp;
    protected float _Mhp;
    protected float _PhaseHP;
    [SerializeField, Header("待機する時間")]
    private float _WaitTime;
    private float _WaitCount;
    [SerializeField, Header("移動速度")]
    private float _Speed;
    [SerializeField, Header("敵の種類")]
    protected float _EnemyScale;
    [SerializeField, Header("ダメージエフェクトの時間")]
    protected float _damageEffectTime;
    [SerializeField, Header("ダメージ時の画像")]
    protected Sprite _damageSprite;
    [SerializeField, Header("ドロップアイテム")]
    protected GameObject[] _Item;
    [SerializeField, Header("死亡エフェクト")]
    protected GameObject _deadEffect;
    // [SerializeField, Header("無敵時間")]
    // protected float _mutekiTime;
    [SerializeField, Header("SE")]
    protected AudioClip[] _sound;
    [SerializeField, Header("スコア")]
    protected int _enemyScore;
    AudioSource _audioSource;
    protected bool _bAttack;
    protected GameObject _player;
    protected Rigidbody2D _rigid;
    protected float _shootCount;
    public static int _score;
    private SpriteRenderer _spriteRenderer;
    private Color defaultcolor;
    private Renderer _renderer;
    private Sprite _defaultSprite;
    private bool _tutorial = false;
    protected float _mutekiCount;
    protected bool _muteki;
    private float _mutekiTime;
    protected int RandomHP;
    int HpUp;
    float A;
    private float x;
    private float y;
    private float mX;
    private float mY;
    private float TX;
    private bool puras;
    enum MoveMode
    {
        down,
        left,
        light,
        migisita,
        hidarisita,
        none,
        Sdown,
        Jiguzagu,
    }
    private MoveMode _moveMode;
    private MoveMode _defaultMode;
    void Start()
    {
        if (_player = GameObject.Find("Player"))
        {
            _player = GameObject.Find("Player");
        }
        _WaitCount=0;
        _mutekiCount = 0;
        _audioSource = gameObject.GetComponent<AudioSource>();
        _renderer=GetComponent<Renderer>();
        defaultcolor=_renderer.material.color;;
        _shootCount = 0;
        _bAttack = false;
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
        _muteki = true;
        _mutekiTime = 0.5f;
        if (_EnemyScale >= 1.0f)
        {
            if (_EnemyScale < 2.0f)
            {
                Debug.Log("!!しょーわくせい!!");
            _mutekiTime = 1.2f;
                DifficultyHP(1);
            }
        }
        CheckDifficulty();
        Initialize();
        if (_tutorial == true) return;
        if (_EnemyScale == 0.0f || _EnemyScale == 1.0f || _EnemyScale == 2.0f || _EnemyScale == 3.0f || _EnemyScale == 4.0f || _EnemyScale == 5.0f|| _EnemyScale == 8.0f)
        {
            _moveMode = MoveMode.down;
        }
        else if (_EnemyScale == 0.1f || _EnemyScale == 1.1f || _EnemyScale == 2.1f || _EnemyScale == 3.1f || _EnemyScale == 4.1f || _EnemyScale == 5.1f|| _EnemyScale == 8.1f)
        {
            _moveMode = MoveMode.left;
        }
        else if (_EnemyScale == 0.2f || _EnemyScale == 1.2f || _EnemyScale == 2.2f || _EnemyScale == 3.2f || _EnemyScale == 4.2f || _EnemyScale == 5.2f|| _EnemyScale == 8.2f)
        {
            _moveMode = MoveMode.light;
        }
        else if (_EnemyScale == 0.3f || _EnemyScale == 1.3f || _EnemyScale == 2.3f || _EnemyScale == 3.3f || _EnemyScale == 4.3f || _EnemyScale == 5.3f|| _EnemyScale == 8.3f)
        {
            _moveMode = MoveMode.migisita;
        }
        else if (_EnemyScale == 0.4f || _EnemyScale == 1.4f || _EnemyScale == 2.4f || _EnemyScale == 3.4f || _EnemyScale == 4.4f || _EnemyScale == 5.4f|| _EnemyScale == 8.4f)
        {
            _moveMode = MoveMode.hidarisita;
        }
        else if (_EnemyScale == 0.5f || _EnemyScale == 1.5f || _EnemyScale == 2.5f || _EnemyScale == 3.5f || _EnemyScale == 4.5f || _EnemyScale == 5.5f || _EnemyScale == 6.5f || _EnemyScale == 7.5f|| _EnemyScale == 8.5f)
        {
            _moveMode = MoveMode.none;
        }
        else if (_EnemyScale == 0.6f || _EnemyScale == 1.6f || _EnemyScale == 2.6f || _EnemyScale == 3.6f || _EnemyScale == 4.6f || _EnemyScale == 5.6f || _EnemyScale == 6.6f || _EnemyScale == 7.6f)
        {
            _moveMode = MoveMode.Sdown;
        }
        else if (_EnemyScale == 0.7f || _EnemyScale == 1.7f || _EnemyScale == 2.7f || _EnemyScale == 3.7f || _EnemyScale == 4.7f || _EnemyScale == 5.7f || _EnemyScale == 6.7f || _EnemyScale == 7.7f||_EnemyScale==8.7f)
        {
            KansuMOVE("ENTER",Targetx,Targety,0);
            _moveMode = MoveMode.none;
            return;
        }
        else if (_EnemyScale == 0.8f || _EnemyScale == 1.8f || _EnemyScale == 2.8f || _EnemyScale == 3.8f || _EnemyScale == 4.8f || _EnemyScale == 5.8f || _EnemyScale == 6.8f || _EnemyScale == 7.8f||_EnemyScale==8.8f)
        {
            KansuMOVE("ENTER",Targetx,Targety,1);
            _moveMode=MoveMode.left;
            return;
        }
        _defaultMode=_moveMode;
    }
    protected virtual void DifficultyHP(int _HP)
    {
        if(_difficulty.StageNum==0)
        {
        if (_difficulty.DIFFICULTY == "Easy"||_difficulty.DIFFICULTY=="Tutorial"||_difficulty.DIFFICULTY=="VeryEasy")
        {
            _hp = _difficulty.easyEnemyHP[_HP]*_hpEXP;
        }
        else if (_difficulty.DIFFICULTY == "Normal")
        {
            _hp = _difficulty.normalEnemyHP[_HP]*_hpEXP;
        }
        else if (_difficulty.DIFFICULTY == "Hard")
        {
            _hp = _difficulty.hardEnemyHP[_HP]*_hpEXP;
        }
        else if(_difficulty.DIFFICULTY=="TEST")
        {
            _hp=_difficulty.testEnemyHP[_HP]*_hpEXP;
        }
        }
        else if(_difficulty.StageNum==1)
        {
        if (_difficulty.DIFFICULTY == "Easy"||_difficulty.DIFFICULTY=="Tutorial"||_difficulty.DIFFICULTY=="VeryEasy")
        {
            _hp = _difficulty.easyEnemyHP[_HP]*_hpEXP;
        }
        else if (_difficulty.DIFFICULTY == "Normal")
        {
            _hp = _difficulty.normalEnemyHP[_HP]*_hpEXP;
        }
        else if (_difficulty.DIFFICULTY == "Hard")
        {
            _hp = _difficulty.hardEnemyHP[_HP]*1f*_hpEXP;
        }
        else if(_difficulty.DIFFICULTY=="TEST")
        {
            _hp=_difficulty.testEnemyHP[_HP]*1.2f*_hpEXP;
        }
        }
        // Debug.Log(_difficulty.DIFFICULTY+"ENEMYHP"+_hp);
    }
    protected virtual void middleDifficultyHP(int _HP)
    {
        if(_difficulty.StageNum==0)
        {
        if (_difficulty.DIFFICULTY == "Easy"||_difficulty.DIFFICULTY=="VeryEasy")
        {
            _hp = _difficulty.easyBOSSHP[_HP];
        }
        else if (_difficulty.DIFFICULTY == "Normal")
        {
            _hp = _difficulty.normalBOSSHP[_HP];
        }
        else if (_difficulty.DIFFICULTY == "Hard")
        {
            _hp = _difficulty.hardBOSSHP[_HP];
        }
        else if(_difficulty.DIFFICULTY=="TEST")
        {
            _hp=_difficulty.testBOSSHP[_HP];
        }
        }
        else if(_difficulty.StageNum==1)
        {
        if (_difficulty.DIFFICULTY == "Easy"||_difficulty.DIFFICULTY=="VeryEasy")
        {
            _hp = _difficulty.easyBOSSHP[_HP]*4;
        }
        else if (_difficulty.DIFFICULTY == "Normal")
        {
            _hp = _difficulty.normalBOSSHP[_HP]*4;
        }
        else if (_difficulty.DIFFICULTY == "Hard")
        {
            _hp = _difficulty.hardBOSSHP[_HP]*4.2f;
        }
        else if(_difficulty.DIFFICULTY=="TEST")
        {
            _hp=_difficulty.testBOSSHP[_HP]*3;
        }
        }
    }
    protected virtual void CheckDifficulty()
    {
        if(_difficulty.DIFFICULTY=="VeryEasy")
        {
            RandomHP=1;
        }
        else if (_difficulty.DIFFICULTY == "Easy"||_difficulty.DIFFICULTY=="TEST")
        {
            RandomHP = 4;
        }
        else if (_difficulty.DIFFICULTY == "Normal")
        {
            RandomHP = 6;
        }
        else if (_difficulty.DIFFICULTY == "Hard")
        {
            RandomHP = 0;
            if(_hpEXP>=2)
            {
            RandomHP=1;
            }
        }
    }
    protected virtual void Initialize()
    {
    }
    void Update()
    {
        // _mutekiCount += Time.deltaTime;
        _WaitCount+=Time.deltaTime;
        if(_WaitCount<_WaitTime)return;
        Move();
        Atack();
    }
    protected virtual void Atack()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_bAttack == false) return;
        // if (_mutekiCount < _mutekiTime) return;
        if (collision.gameObject.tag == "Player")
        {
            if (_EnemyScale == 6.5)
            {
            Destroy(gameObject);
            Instantiate(_deadEffect, transform.position, Quaternion.identity);
            }
        }
        if (collision.gameObject.tag == "bullet2")
        {
            _hp -= 1;
                // _hp -= collision.gameObject.GetComponent<PlayerBullet>().GetPower();
                // _audioSource.PlayOneShot(_sound[0]);
                StartCoroutine(Damage());
            if (_hp <= 0)
            {
                GameObject GameManager = GameObject.Find("GameManager");
                GameManager gameManager = GameManager.GetComponent<GameManager>();
                gameManager.Score(_enemyScore);
                DeidAction();
                // _gamemanager.Score(_enemyScore);
                if (_middle == true)
                {
                    PhaseChange();
                    GameObject EnemySpawner = GameObject.Find("EnemySpawner");
                    EnemySpawner enemySpawner = EnemySpawner.GetComponent<EnemySpawner>();
                    enemySpawner.NumberChange(100);
                    GameObject HPUpItemObj = Instantiate(_Item[2]);
                    HPUpItemObj.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0, 3), gameObject.transform.position.y + Random.Range(0, 3));
                }
                Destroy(gameObject);
                Instantiate(_deadEffect, transform.position, Quaternion.identity);
                }
            }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_bAttack == false) return;
        // if (_mutekiCount < _mutekiTime) return;
        if (collision.gameObject.tag == "bullet")
        {
            // _hp -= 1;
            _hp -=collision.gameObject.GetComponent<Bullet>().GetPower();
            _audioSource.PlayOneShot(_sound[0]);
            StartCoroutine(Damage());
            if (_hp <= 0)
            {
                GameObject GameManager = GameObject.Find("GameManager");
                GameManager gameManager = GameManager.GetComponent<GameManager>();
                gameManager.Score(_enemyScore);
                DeidAction();
                // _gamemanager.Score(_enemyScore);
                if (_middle == true)
                {
                    PhaseChange();
                    GameObject EnemySpawner = GameObject.Find("EnemySpawner");
                    EnemySpawner enemySpawner = EnemySpawner.GetComponent<EnemySpawner>();
                    enemySpawner.NumberChange(100);
                    GameObject HPUpItemObj = Instantiate(_Item[2]);
                    HPUpItemObj.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0, 3), gameObject.transform.position.y + Random.Range(0, 3));
                }
                Instantiate(_deadEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        if(collision.gameObject.tag=="BOOM1")
        {
            _hp=_hp/3*2;
            _audioSource.PlayOneShot(_sound[0]);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BackGlound")
        {
            _mutekiCount += Time.deltaTime;
            if (_mutekiCount > _mutekiTime)
        {
            _bAttack = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BackGlound")
        {
            _bAttack = false;
        }
    }
private void DeidAction()
    {
                HpUp = Random.Range(0, RandomHP);
                if(_hpEXP>=2)
                {
                    for (int i = 0; i < Random.Range(2, 10); i++)
                {
                    GameObject powerItemObj = Instantiate(_Item[0]);
                    powerItemObj.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0, 3), gameObject.transform.position.y + Random.Range(0, 3));
                }
                }
                for (int i = 0; i < Random.Range(1, 4); i++)
                {
                    GameObject powerItemObj = Instantiate(_Item[0]);
                    powerItemObj.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0, 3), gameObject.transform.position.y + Random.Range(0, 3));
                }
                for (int i = 0; i < Random.Range(3, 6); i++)
                {
                    GameObject pointItemObj = Instantiate(_Item[1]);
                    pointItemObj.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0, 3), gameObject.transform.position.y + Random.Range(0, 3));
                }
                if (HpUp == 1||RandomHP==1)
                {
                    GameObject HPUpItemObj = Instantiate(_Item[2]);
                    HPUpItemObj.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0, 3), gameObject.transform.position.y + Random.Range(0, 3));
                }
    }
    protected void PhaseChange()
    {
    foreach(GameObject bullet in GameObject.FindGameObjectsWithTag("bullet"))
    {
        Destroy(bullet);
    }
    GameObject HPUpItemObj = Instantiate(_Item[2]);
    HPUpItemObj.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0, 3), gameObject.transform.position.y + Random.Range(0, 3));
    for (int i = 0; i < Random.Range(1, 9); i++)
    {
        GameObject powerItemObj = Instantiate(_Item[0]);
        powerItemObj.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0, 3), gameObject.transform.position.y + Random.Range(0, 3));
    }
    for (int i = 0; i < Random.Range(1, 6); i++)
    {
        GameObject pointItemObj = Instantiate(_Item[1]);
        pointItemObj.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0, 3), gameObject.transform.position.y + Random.Range(0, 3));
    }
    }
    protected virtual void Move()
    {
        // if(DontMove==true)return;
        switch (_moveMode)
        {
            case MoveMode.down: MoveDown(); break;
            case MoveMode.left: MoveLeft(); break;
            case MoveMode.light: MoveLight(); break;
            case MoveMode.migisita: MoveMigisita(); break;
            case MoveMode.hidarisita: Movehidarisita(); break;
            case MoveMode.none: NONE(); break;
            case MoveMode.Sdown: Sdown(); break;
        }
    }
    private void NONE()
    {
        _rigid.velocity = Vector2.down * 0;
    }
    private void MoveDown()
    {
        _rigid.velocity = Vector2.down * _Speed;
    }
    private void MoveLeft()
    {
        _rigid.velocity = Vector2.left * _Speed;
    }
    private void MoveLight()
    {
        _rigid.velocity = Vector2.right * _Speed;
    }
    private void MoveMigisita()
    {
        _rigid.velocity = Vector2.right * _Speed + Vector2.down * _Speed;
    }
    private void Movehidarisita()
    {
        _rigid.velocity = Vector2.left * _Speed + Vector2.down * _Speed;
    }
    protected void Sdown()
    {
        _rigid.velocity = Vector2.down * _Speed;
        if (transform.position.y <= 2f)
        {
            _moveMode = MoveMode.none;
        }
    }
    protected void KansuMOVE(string a,float AM,float BM,int MODE)
    {
        mX = transform.position.x;
        mY = transform.position.y;
        x = transform.position.x;
        if (AM > mX)
        {
            TX = 0.1f;
            puras = true;
        }
        else if(AM<mX)
        {
            TX = -0.1f;
            puras = false;
        }
        if (a == "ENTER")
        {
            StartCoroutine(ENTER(AM, BM,MODE));
        }
        else if (a == "EXIT")
        {
            StartCoroutine(EXIT(AM, BM));
        }
    }
    private IEnumerator ENTER(float A, float B,int MODE)
    {
        yield return new WaitForSeconds(_WaitTime);
        for (int i = 0; i < 10000; i++)
        {
            _shootCount=0;
            y = ((mY - B) / Mathf.Pow(mX-A, 2)) * Mathf.Pow(x - A, 2) + B;
            transform.DOMove(new Vector2(x, y), 0.5f);
            x += TX;
            if (puras == true)
            {
            if (x >= A) yield break;
            }
            else if(puras==false)
            {
            if (x <= A) yield break;
            }
            yield return new WaitForSeconds(0.015f);
        }
        if(MODE==0)
        {
        _shootCount=_shootTime;
        }
    }
    private IEnumerator EXIT(float A,float B)
    {
        for (int i=0; i < 10000; i++)
        {
            _shootCount=0;
            transform.DOMove(new Vector2(x, y), 0.1f);
            x += TX;
            y = ((B - mY) / Mathf.Pow(A - mX, 2)) * Mathf.Pow(x - mX, 2) + mY;
            if (puras == true)
            {
            if (x >= A) yield break;
            }
            else if(puras==false)
            {
            if (x <= A) yield break;
            }
            yield return new WaitForSeconds(0.015f);
        }
    }
    // private void OnWillRenderObject()
    // {
    //     if (Camera.current.name == "Main Camera")
    //     {
    //         _Count += Time.deltaTime;
    //         _bAttack = true;
    //         if (_Count < _mutekiTime) return;
    //         _muteki = false;
    //     }
    // }
    private IEnumerator Damage()
    {
        _renderer.material.color=new Color32(255,132,132,255);
        yield return new WaitForSeconds(_damageEffectTime);
        _renderer.material.color = defaultcolor;
    }
    public Transform Gettransform()
    {
        return gameObject.transform;
    }
public void MOVEchange(string _A)
    {
        if (_A == "down")
        {
            _moveMode = MoveMode.down;
        _tutorial = true;
        }
        else if (_A == "left")
        {
            _moveMode = MoveMode.left;
        _tutorial = true;
        }
        else if (_A == "right")
        {
            _moveMode = MoveMode.light;
        _tutorial = true;
        }
        else if(_A=="none")
        {
            _moveMode = MoveMode.none;
        }
    }
}
