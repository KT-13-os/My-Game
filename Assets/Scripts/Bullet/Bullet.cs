using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
// using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float _speed;
    [SerializeField, Header("Status")]
    protected Status _Status;
    protected float _OLDspeed;
    [SerializeField, Header("識別番号")]
    protected float _Number;
    [SerializeField, Header("弾の加速度")]
    protected float _Pspeed;
    [SerializeField, Header("加速か減速か")]
    protected int _a;
    [SerializeField, Header("弾の威力")]
    protected float _power;
    [SerializeField, Header("ドロップアイテム")]
    protected GameObject[] _Item;
    [SerializeField, Header("跳ね返るかどうか")]
    protected bool _Bound = true;
    private float ItemCount;
    protected float _Time;
    protected int _boundCountNum;
    protected float _Mspeed;
    protected float CountTime;
    protected float _SSTime;
    protected float _StopTime;
    protected Rigidbody2D _rigid;
    public static Bullet instance;
    protected Vector2 _moveVec;
    int _boundCount;
    private float _MutekiCount;
    GameObject _player;
    private SpriteRenderer spriteRenderer;
    [SerializeField, Header("ダメージSprite")]
    private Sprite _defaultSprite;
    private Sprite _damageSprite;
    protected bool StopMove = false;
    private GameObject Summoner;
    private int _number;
    private MoveMode _moveMode;
    enum MoveMode
    {
        none,
        M,
        M1,
        M2,
        M3,
        M4,
        M5,
        M6,
    }
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _number=100;
    }
    void Start()
    {
        if (_player = GameObject.Find("Player"))
        {
            _player = GameObject.Find("Player");
        }
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _defaultSprite = spriteRenderer.sprite;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
        _rigid = GetComponent<Rigidbody2D>();
        _Time = 0.5f;
        _Mspeed = 0f;
        _MutekiCount = 0;
        _boundCount = 0;
        CountTime = 0;
        CheckNumber();
        StartA();
        if (_Bound == true)
        {
            _moveVec = new Vector2(Random.Range(-8.6f, 4f), Random.Range(-4.5f, 4.4f));
            _moveMode = MoveMode.M;
            return;
        }
    }
    protected virtual void StartA()
    {
    }
    void Update()
    {
        _MutekiCount += Time.deltaTime;
        Move();
        UpdateA();
    }
    protected virtual void UpdateA()
    {
    }
    protected virtual void Move()
    {
        if (StopMove == true) return;
        switch (_moveMode)
        {
            case MoveMode.none: NONE(); break;
            case MoveMode.M: MoveBound(); break;
            case MoveMode.M1: Move1(); break;
            case MoveMode.M2: Move2(); break;
            case MoveMode.M3: Move3(); break;
            case MoveMode.M4:RETURN();break;
            case MoveMode.M5:Move5();break;
            case MoveMode.M6:Move6();break;
        }
    }
    //ローカル座標上方向に_speed分進む
    private void NONE()
    {
        _rigid.velocity = Vector2.zero;
    }
    private void MoveBound()
    {
        _rigid.velocity = _moveVec * _speed / 2;
    }
    protected virtual void Move1()
    {
        _Time += Time.deltaTime;
        _Mspeed = _speed + _Pspeed * _Time;
        if (_Mspeed <= _speed / 4)
        {
            _Mspeed = _speed / 4;
        }
        _rigid.velocity = transform.up * _Mspeed;
    }
    private void Move2()
    {
        _rigid.velocity = transform.up * _Mspeed;
    }
    private void Move3()
    {
        _Time += Time.deltaTime;
        _Mspeed = _speed + _Pspeed * _Time;
        if (_Mspeed <= _speed / 4)
        {
            _Mspeed = _speed / 4;
        }
        _rigid.velocity = transform.up * _Mspeed;
    }
    private void RETURN()
    {
        CountTime += Time.deltaTime;
        _rigid.velocity = transform.up * _speed;
        if (CountTime < _SSTime)
        {
            _speed = _OLDspeed / Mathf.Pow(_SSTime, 2) * Mathf.Pow(CountTime, 2) - 2 * (_OLDspeed / _SSTime) * CountTime + _OLDspeed;
            return;
        }
        else if (CountTime < _SSTime + _StopTime)
        {
            _speed = 0;
            return;
        }
        else if (CountTime < 2*(_SSTime + _StopTime))
        {
            _speed = -_OLDspeed / (Mathf.Pow(_SSTime, 2) + 2 * (_SSTime * _StopTime) + Mathf.Pow(_StopTime, 2)) * Mathf.Pow(CountTime, 2) - 2 * (-_OLDspeed / Mathf.Pow(_SSTime + _StopTime, 2)) * (_SSTime + _StopTime) * CountTime - _OLDspeed;
            return;
        }
        else
        {
            _speed = -_OLDspeed;
        }
    }
    private void Move5()
    {
        Move1();
        transform.RotateAround(Summoner.transform.position,Vector3.forward,40*Time.deltaTime);
    }
    private void Move6()
    {
        Move1();
        transform.RotateAround(Summoner.transform.position,Vector3.back,40*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerScripts>().Isdamage()) return;
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
        // if (collision.gameObject.tag == "bullet")
        // {
        //     spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.2f);
        // }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BOOM1")
        {
            if(_MutekiCount<0.2f)return;
            SummonItems();
            Destroy(gameObject);
        }
    }
    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     // spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1.0f);
    // }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_Bound == false) return;
        if (collision.gameObject.tag == "wall")
        {
            if (_boundCount >= _boundCountNum)
            {
                Destroy(gameObject);
            }
            _moveVec *= Random.Range(-0.5f, -1.5f);
            _boundCount++;
        }
    }
    public float GetPower()
    {
        return _power;
    }
    public void ChangePower(float A)
    {
        _power = A;
    }
    public void PowerUp(float _Apower)
    {
        _power = _Apower;
        Debug.Log(_Apower);
    }
    public Vector2 GettVec()
    {
        return _moveVec;
    }
    public void ResetA(float _Bpower)
    {
        _power = _Bpower;
    }
    public void Speed(float Pspeed)
    {
        _speed = Pspeed;
        _Mspeed = _speed;
        _OLDspeed = _speed;
    }
    public void KasokuCahange(float _pspeed)
    {
        _Pspeed = _pspeed;
    }
    public void BoundChange(bool _b)
    {
        if (_b == true)
        {
            _Bound = true;
        }
        else { _Bound = false; }
    }
    public void Boundcount(int BoundCount)
    {
        _boundCountNum = BoundCount;
    }
    public void MoveChange(int A,GameObject _gameObject)
    {
        if (A == 3)
        {
            _moveMode = MoveMode.M1;
            StopMove = false;
        }
        else if (A == 1)
        {
            _moveMode = MoveMode.M3;
            StopMove = false;
        }
        else if (A == 2)
        {
            _Mspeed = _speed;
            _moveMode = MoveMode.M2;
            StopMove = false;
        }
        else if (A == 4)
        {
            _moveVec = new Vector2(Random.Range(-8.6f, 4f), Random.Range(-4.5f, 1f));
            _moveMode = MoveMode.M;
            StopMove = false;
        }
        else if (A == 5)
        {
            _moveMode = MoveMode.M4;
            StopMove = false;
        }
        else if(A==6)
        {
            _moveMode=MoveMode.M5;
            StopMove = false;
        }
        else if(A==7)
        {
            _moveMode=MoveMode.M6;
            StopMove = false;
        }
        else if (A == 0)
        {
            StopMove = true;
        }
        Summoner=_gameObject;
    }
    public void RcfSSTIME(float a)
    {
        _SSTime = a;
    }
    public void RcfStopTime(float a)
    {
        _StopTime = a;
    }
    public void ChangeBoundNum(int A)
    {
        _boundCountNum = A;
    }
    private void CheckNumber()
    {
        if (_Number == 0)
        {
            ItemCount = 1;
        }
        else if (_Number == 1)
        {
            ItemCount = 1;
        }
        else if (_Number == 2)
        {
            ItemCount = 1;
        }
        else if (_Number == 3)
        {
            ItemCount = 1;
        }
        else
        {
            ItemCount = 0;
        }
    }
    private void SummonItems()
    {
    for (int i = 0; i < ItemCount; i++)
    {
        GameObject powerItemObj = Instantiate(_Item[0]);
        powerItemObj.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0, 3), gameObject.transform.position.y + Random.Range(0, 3));
    }
    }
public void SetNumber(int number)
    {
        _number=number;
    }
    public int GetNumber()
    {
        return _number;
    }
}
