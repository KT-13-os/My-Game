using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPIcon : MonoBehaviour
{
    [SerializeField, Header("HPIcon")]
    private GameObject _hpicon;
    [SerializeField, Header("DIFFICULTY")]
    private Difficulty _difficulty;
    private  PlayerScripts _playerscripts;
    public GameObject PlayerObject;
    private int _beforeHP;
    private List<GameObject> _hpiconList;
    void Start()
    {
        _playerscripts = PlayerObject.GetComponent<PlayerScripts>();
        _beforeHP = _playerscripts.GetHP();
        _hpiconList = new List<GameObject>();
        CreatHPIcon();
    }
    private void CreatHPIcon()
    {
        for (int i = 0; i < _playerscripts.GetHP(); i++)
        {
            GameObject icon = Instantiate(_hpicon);
            icon.transform.SetParent(transform);
            _hpiconList.Add(icon);
        }
    }
    void Update()
    {
        ShowHPIcon();
    }
    private void ShowHPIcon()
    {
        if (_beforeHP == _playerscripts.GetHP()) return;
        for (int i = 0; i < _hpiconList.Count; i++)
        {
            _hpiconList[i].SetActive(i < _playerscripts.GetHP());
        }
        _beforeHP = _playerscripts.GetHP();
    }
    public void HpUp()
    {
        if(_difficulty.PlayerInfinity=="INFINITY")return;
            GameObject icon = Instantiate(_hpicon);
            icon.transform.SetParent(transform);
            _hpiconList.Add(icon);
    }
}
