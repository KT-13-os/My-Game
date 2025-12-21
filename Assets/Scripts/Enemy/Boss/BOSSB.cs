using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BOSSB : BOSS
{
    float MHP;
    enum AttackMode
    {
        A,
        A1,
        A2,
        A3,
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
    [SerializeField, Header("wall")]
    private GameObject[] _wall;
    protected override void Initialize()
    {
        _moveMode = MoveMode.M;
        _attackMode = AttackMode.A;
        MHP = _hp;
    }
    protected override void UpdateA()
    {
        _shootCount += Time.deltaTime;
        if (_hp <= 0)
        {
            _attackMode = AttackMode.A;
        }
    }
    protected override void Move()
    {
        switch (_moveMode)
        {
            case MoveMode.M: M(); break;
            case MoveMode.M1: M1(); break;
            case MoveMode.M2: M2(); break;
            case MoveMode.M3: M3(); break;
        }
    }
    private void M()
    {
        if (transform.position.y <= 3.5f)
        {
            for (int i = 0; i < 4; i++)
            {
                _wall[i].SetActive(true);
            }
            _moveMode = MoveMode.M1;
            _attackMode = AttackMode.A1;
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
            _attackMode = AttackMode.A2;
        }
    }
    private void M2()
    {
        if (_hp <= MHP * 1 / 3)
        {
            _moveMode = MoveMode.M2;
            _attackMode = AttackMode.A2;
        }
    }
    private void M3()
    {
    }
    protected override void Attack()
    {
        switch (_attackMode)
        {
            case AttackMode.A: A(); break;
            case AttackMode.A1: A1(); break;
            case AttackMode.A2: A2(); break;
            case AttackMode.A3: A3(); break;
        }
    }
    private void A()
    {
    }
    private void A1()
    {
    }
    private void A2()
    {
    }
    private void A3()
    {
    }
}
