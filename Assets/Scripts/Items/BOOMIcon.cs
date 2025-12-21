using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOOMIcon : MonoBehaviour
{
    [SerializeField, Header("BOOMIcon")]
    private GameObject _boomicon;
    [SerializeField, Header("DIFFICULTY")]
    private Difficulty _difficulty;
    private  PlayerScripts _playerscripts;
    public GameObject _player;
    private int _beforeBOOM;
    private List<GameObject> _boomiconList;
    void Start()
    {
        _playerscripts = _player.GetComponent<PlayerScripts>();
        _beforeBOOM = _playerscripts.GetBOOM();
        _boomiconList = new List<GameObject>();
        CreatBOOMIcon();
    }
    private void CreatBOOMIcon()
    {
        for (int i = 0; i < _playerscripts.GetBOOM(); i++)
        {
            GameObject icon = Instantiate(_boomicon);
            icon.transform.SetParent(transform);
            _boomiconList.Add(icon);
        }
    }
    void Update()
    {
        ShowBOOMIcon();
    }
    private void ShowBOOMIcon()
    {
        if (_beforeBOOM == _playerscripts.GetBOOM()) return;
        for (int i = 0; i < _boomiconList.Count; i++)
        {
            _boomiconList[i].SetActive(i < _playerscripts.GetBOOM());
        }
        _beforeBOOM = _playerscripts.GetBOOM();
    }
    public void BOOMUp()
    {
        if(_difficulty.PlayerInfinity=="INFINITY")return;
            GameObject icon = Instantiate(_boomicon);
            icon.transform.SetParent(transform);
            _boomiconList.Add(icon);
    }
}
