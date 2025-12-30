using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class BOSS : MonoBehaviour
{
    [SerializeField, Header("DIFFICULTY")]
    protected Difficulty _difficulty;
    [SerializeField, Header("弾オブジェクト")]
    protected GameObject[] _bullet;
    protected float _hp;
    protected float _Mhp;
    protected float _KeepHP;
    protected float _PhaseHP;
    [SerializeField, Header("移動速度")]
    protected float _Speed;
    [SerializeField, Header("ダメージエフェクトの時間")]
    protected float _damageEffectTime;
    [SerializeField, Header("ダメージ時の画像")]
    protected Sprite _damageSprite;
    [SerializeField, Header("ドロップアイテム")]
    protected GameObject[] _Item;
    [SerializeField, Header("SE")]
    protected AudioClip[] _sound;
    [SerializeField, Header("死亡エフェクト")]
    protected GameObject _deadEffect;
    protected AudioSource _audioSource;
    protected SpriteRenderer _spriteRenderer;
    protected Sprite _defaultSprite;
    public static int _score;
    protected bool _bAttack;
    protected GameObject _player;
    protected Rigidbody2D _rigid;
    protected bool _attack;
    protected float _shootCount;
    [SerializeField, Header("Slider")]
    protected GameObject slider;
    protected Slider _slider;
    private Bullet _Bullet;
    protected PlayerScripts playerscripts;
    [SerializeField, Header("GameManager")]
    protected GameObject GameManager;
    protected GameManager gamemanager;
    protected SubGameManager subGameManager;
    private float _deadTime;
    private float x;
    private float y;
    private float mX;
    private float mY;
    private float TX;
    private bool puras;
    private Renderer _renderer;
    private Color defaultcolor;
    int _Gcount;
    void Awake()
    {
        _attack = false;
        // _Gcount = 0;
        _renderer=GetComponent<Renderer>();
        defaultcolor=_renderer.material.color;;
        _audioSource = gameObject.GetComponent<AudioSource>();
        gamemanager = GameManager.GetComponent<GameManager>();
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
        _slider = slider.GetComponent<Slider>();
        _score = 1000000;
        DifficultyHP(1);
    }
    void Start()
    {
        if (_player = GameObject.Find("Player"))
        {
            _player = GameObject.Find("Player");
        }
        playerscripts = _player.GetComponent<PlayerScripts>();
        subGameManager = GameManager.GetComponent<SubGameManager>();
        _Gcount = 0;
        Initialize();
    }
    protected virtual void DifficultyHP(int _HP)
    {
        if (_difficulty.DIFFICULTY == "Easy"||_difficulty.DIFFICULTY=="VeryEasy")
        {
            _hp = _difficulty.easyBOSSHP[_HP];
            _PhaseHP = _hp/2;
        }
        else if (_difficulty.DIFFICULTY == "Normal")
        {
            _hp = _difficulty.normalBOSSHP[_HP];
            _PhaseHP = (_hp/3)*2;
        }
        else if (_difficulty.DIFFICULTY == "Hard")
        {
            _hp = _difficulty.hardBOSSHP[_HP];
            _PhaseHP = (_hp/3)*2;
        }
        _Mhp = _hp;
    }
    protected virtual void Initialize()
    {
    }
    void Update()
    {
        // _attack += Time.deltaTime;
        if (_attack == false) return;
        if (_player = GameObject.Find("Player"))
        {
            _player = GameObject.Find("Player");
        }
        else
        {
            return;
        }
        Move();
        Attack();
        UpdateA();
        if (_hp <= 0)
        {
            if (_difficulty.DIFFICULTY == "Hard") return;
            if (_Gcount >= 1) return;
            _Gcount++;
            _attack = false;
            gamemanager.Score(_score);
            subGameManager.BOSSDIED();
            _slider.DESTROY();
            playerscripts.MUTEKI();
            gamemanager.ClearEffect();
            gameObject.SetActive(false);
        }
    }
    protected virtual void BossMOVE(string a, float AM, float BM)
    {
        mX = transform.position.x;
        mY = transform.position.y;
        x = transform.position.x;
        if (AM > mX)
        {
            TX = 0.1f;
            puras = true;
        }
        else if (AM < mX)
        {
            TX = -0.1f;
            puras = false;
        }
        if (a == "ENTER")
        {
            StartCoroutine(ENTER(AM, BM));
        }
        else if (a == "EXIT")
        {
            StartCoroutine(EXIT(AM, BM));
        }
    }
    private IEnumerator ENTER(float A, float B)
    {
        for (int i = 0; i < 10000; i++)
        {
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
            yield return new WaitForSeconds(0.01f);
        }
    }
    private IEnumerator EXIT(float A,float B)
    {
        for (int i=0; i < 10000; i++)
        {
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
            yield return new WaitForSeconds(0.05f);
        }
    }
    protected virtual void DEAD()
    {
        _deadTime += Time.deltaTime;
        if (_deadTime < 0.1f) return;
        Instantiate(_deadEffect, transform.position*Random.Range(-0.5f,1.5f), Quaternion.identity);
        _deadTime = 0;
    }
    protected virtual void UpdateA()
    {
    }
    protected virtual void Move()
    {
    }
    protected virtual void Attack()
    {
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet2")
        {
            // PlayerBullet _playerBullet = collision.gameObject.GetComponent<PlayerBullet>();
            // float _power = _playerBullet.GetPower();
            float _power = 1;
            _slider.BeInjured(_power);
            _hp -= collision.gameObject.GetComponent<PlayerBullet>().GetPower();
            StartCoroutine(Damage());
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            _slider = slider.GetComponent<Slider>();
            _Bullet = collision.gameObject.GetComponent<Bullet>();
            _audioSource.PlayOneShot(_sound[0]);
            float _power = _Bullet.GetPower();
            _slider.BeInjured(_power);
            _hp -= _power;
            StartCoroutine(Damage());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BackGlound")
        {
            _bAttack = true;
        }
    }
    // protected void OnWillRenderObject()
    // {
    //     if (Camera.current.name == "Main Camera")
    //     {
    //         _bAttack = true;
    //     }
    // }
    protected IEnumerator Damage()
    {
        _renderer.material.color=new Color32(255,132,132,255);
        yield return new WaitForSeconds(_damageEffectTime);
        _renderer.material.color = defaultcolor;
    }
protected void Phase()
{
    for (int i = 0; i < Random.Range(10, 15); i++)
    {
    GameObject powerItemObj = Instantiate(_Item[0]);
    powerItemObj.transform.position = new Vector2(transform.position.x + Random.Range(0, 3), transform.position.y + Random.Range(0, 3));
    }
    for (int i = 0; i < 2; i++)
    {
        GameObject HPUpItemObj = Instantiate(_Item[2]);
        HPUpItemObj.transform.position = new Vector2(transform.position.x + Random.Range(0, 3), transform.position.y + Random.Range(0, 3));
    }
    _difficulty.Phase++;
    Debug.Log(_difficulty.Phase);
}
    public void AttackOK()
    {
        _attack = true;
        gameObject.SetActive(true);
    }
    public float GetHp()
    {
        return _hp;
    }
    public void START()
    {
        if(_difficulty.StageNum==1)
        {
        gameObject.GetComponent<BOSSA>().enabled=false;
        gameObject.GetComponent<BOSSB>().enabled=true;
        }
        else if(_difficulty.StageNum==2)
        {
            gameObject.GetComponent<BOSSB>().enabled=false
;        }
    }
}
