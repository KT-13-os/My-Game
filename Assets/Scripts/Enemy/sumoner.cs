using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sumoner : MonoBehaviour
{
    [SerializeField, Header("弾オブジェクト")]
    protected GameObject[] _bullet;
    [SerializeField, Header("移動速度")]
    private float _Speed;
    [SerializeField, Header("弾を召喚する間隔")]
    private float _circleBuletTime;
    [SerializeField, Header("動く向き")]
    private int _move;
    protected Rigidbody2D _rigid;
    private float _count;
    private float _shootCount;
    enum MoveMode
    {
        up,
        left,
    }
    private MoveMode _moveMode;
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        if(_move==0)
        {
            _moveMode=MoveMode.up;
        }
        else if(_move==1)
        {
            _moveMode=MoveMode.left;
        }
    }
    void Update()
    {
        _shootCount+=Time.deltaTime;
        Move();
        Summon();
    }
    private void Summon()
    {
        if (_shootCount < _circleBuletTime) return;
        GameObject bulletObj = Instantiate(_bullet[0]);
        bulletObj.transform.position = transform.position;
        _shootCount =0;
    }
    protected virtual void Move()
    {
        switch (_moveMode)
        {
            case MoveMode.up: MoveUp(); break;
            case MoveMode.left: MoveLeft(); break;
        }
    }
    private void MoveUp()
    {
        _rigid.velocity = Vector2.up * _Speed;
    }
    private void MoveLeft()
    {
        _rigid.velocity = Vector2.left * _Speed;
    }
}
