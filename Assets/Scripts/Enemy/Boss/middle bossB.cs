using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class middlebossB : Enemy
{
    enum AttackMode
    {
        A,
        A1,
        A2,
    }
    private AttackMode _attackMode;
    protected override void Initialize()
    {
        _difficulty.Phase=1;
        middleDifficultyHP(0);
        _Mhp = _hp;
        if (_difficulty.DIFFICULTY == "Easy" || _difficulty.DIFFICULTY == "Normal"||_difficulty.DIFFICULTY=="VeryEasy")
        {
            _PhaseHP = -100000000000000;
        }
        else if(_difficulty.DIFFICULTY=="Hard")
        {
            _PhaseHP = _Mhp / 3*2;
        }
        _attackMode = AttackMode.A1;
    }
    protected override void Atack()
    {
        _shootCount += Time.deltaTime;
        switch (_attackMode)
        {
            case AttackMode.A: A(); break;
            case AttackMode.A1: A1(); break;
            case AttackMode.A2: A2(); break;
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
