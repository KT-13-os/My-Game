using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubGameManager : MonoBehaviour
{
    [SerializeField, Header("countTEXT")]
    private TMP_Text _countTEXT;
    [SerializeField, Header("BOSS")]
    private GameObject BOSS;
    private GameManager gameManager;
    private BOSSA BOSSA;
    private float _count;
    private bool COUNTDOWN = false;
    private int PhaseCount;
    [SerializeField, Header("DIFFICULTY")]
    protected Difficulty _difficulty;
    public void VeryEasyButton()
    {
        _difficulty.VeryEasy();
    }
    public void EasyButton()
    {
        _difficulty.Easy();
    }
    public void NormalButton()
    {
        _difficulty.Normal();
    }
    public void HardButton()
    {
        _difficulty.Hard();
    }
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
        PhaseCount = 0;
        _count = 80;
    }
    void Update()
    {
        if (COUNTDOWN == false) return;
        _count -= Time.deltaTime;
        if (_count < 0)
        {
            COUNTDOWN = false;
            BOSSA.ChangePhase(10);
            _countTEXT.text = "";
            return;
        }
        _countTEXT.text = _count.ToString("f1");
        if(_count<40)
        {
            if (PhaseCount >= 1) return;
            PhaseCount++;
            BOSSA.ChangePhase(1);
        }
    }
    public void Count()
    {
        BOSSA = BOSS.GetComponent<BOSSA>();
        COUNTDOWN = true;
    }
    public void CountStop()
    {
        _countTEXT.text = "";
        COUNTDOWN = false;
    }
    public void BOSSDIED()
    {
        gameManager.Score(100000);
    }
}
