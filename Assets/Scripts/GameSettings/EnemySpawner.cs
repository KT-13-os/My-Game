using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Header("Debugボタン")]
    private bool _Debug;
    [SerializeField, Header("EnemysManager")]
    private EnemysManager _enemysManager;
    [SerializeField, Header("DIFFICULTY")]
    protected Difficulty _difficulty;
    [SerializeField, Header("GameManager")]
    private GameObject _gamemanager;
    [SerializeField, Header("プレイヤー")]
    private GameObject _player;
    private PlayerScripts _playerScripts;
    private GameManager _GameManager;
    [SerializeField, Header("敵オブジェクトTUTORIAL")]
    private GameObject[] _enemy;
    [SerializeField, Header("敵オブジェクトDEBUG")]
    private GameObject[] _TESTenemy;
    [SerializeField, Header("敵オブジェクトEASY")]
    private GameObject[] _EASYenemy;
    [SerializeField, Header("敵オブジェクトNORMAL")]
    private GameObject[] _NORMALenemy;
    [SerializeField, Header("敵オブジェクトHARD")]
    private GameObject[] _HARDenemy;
    [SerializeField, Header("生成時間EASY")]
    private float[] _EASYspawnTimes;
    [SerializeField, Header("生成時間NORMAL")]
    private float[] _NORMALspawnTimes;
    [SerializeField, Header("生成時間HARD")]
    private float[] _HARDspawnTimes;
    [SerializeField, Header("BOSS")]
    private GameObject _Boss;
    [SerializeField, Header("スライダー")]
    private GameObject slider;
    [SerializeField, Header("中ボスの出る番号")]
    private int Number;
    [SerializeField, Header("TutorialInputField")]
    private GameObject _inputfield;
    private InputField _InputField;
    private GameObject[] _spawnEnemys;
    public bool _tutorial = false;
    private float _spawnCount;
    private int _spawnNum;
    private float _Count;
    private float _summonTime;
    private float _summonX;
    private float _summonY;
    private float _OriginalY;
    // private float PrasuX;
    private float PrasuY;
    private int _summonNum;
    private GameObject ENEMY;
    private Enemy _ENEMY;
    private int _B;
    bool _Not;
    BOSS _boss;
    Slider _slider;
    enum SpawnMode
    {
        S1,
        S2,
        S3,
        S4,
    }
    private SpawnMode _spawnmode;
    private GameObject _OLDobjects;
    void Start()
    {
        if(_Debug==true)
        {
            _difficulty.DIFFICULTY="TEST";
        }
        START();
        if (_tutorial == true)
        {
            _Not = true;
            _InputField = _inputfield.GetComponent<InputField>();
            _GameManager = _gamemanager.GetComponent<GameManager>();
            return;
        }
        _OLDobjects=null;
        _playerScripts = _player.GetComponent<PlayerScripts>();
        _Not = false;
        _GameManager = _gamemanager.GetComponent<GameManager>();
        _boss = _Boss.GetComponent<BOSS>();
        _slider = slider.GetComponent<Slider>();
    }
    public void START()
    {
        _difficulty.Phase=0;
        _spawnCount = 0.0f;
        _spawnNum = 0;
        _Count = 0;
        StageChange();
        _Boss.transform.position=new Vector3(-3,8,0);
        _Boss.SetActive(false);
        slider.SetActive(false);
    }
    public void StageChange()
    {
        if(_difficulty.StageNum==0)
        {
        if(_difficulty.DIFFICULTY=="Easy")
            {
                _spawnEnemys=_enemysManager._EASYenemys1;
            }
        else if(_difficulty.DIFFICULTY=="Normal")
        {
            _spawnEnemys=_enemysManager._NORMALenemys1;
        }
        else if(_difficulty.DIFFICULTY=="Hard")
        {
            _spawnEnemys=_enemysManager._HARDenemys1;
        }
        }
        else if(_difficulty.StageNum==1)
        {
        if(_difficulty.DIFFICULTY=="Easy")
        {
            _spawnEnemys=_enemysManager._EASYenemys2;
        }
        else if(_difficulty.DIFFICULTY=="Normal")
        {
            _spawnEnemys=_enemysManager._NORMALenemys2;
        }
        else if(_difficulty.DIFFICULTY=="Hard")
        {
            _spawnEnemys=_enemysManager._HARDenemys2;
        }
            }
    }
    void Update()
    {
        _spawnCount += Time.deltaTime;
        if (_tutorial == true) return;
        if (_spawnNum == Number + 1) return;
        // _boss.AttackOK();
        // _slider.AttackOk();
        // _GameManager.ChangeBackGlound();
        if (_difficulty.DIFFICULTY == "Easy"||_difficulty.DIFFICULTY=="VeryEasy")
        {
            SpawnEASY();
        }
        else if (_difficulty.DIFFICULTY == "Normal")
        {
            SpawnNORMAL();
        }
        else if (_difficulty.DIFFICULTY == "Hard")
        {
            SpawnHARD();
        }
        else if(_difficulty.DIFFICULTY=="TEST")
        {
            SpawnTEST();
        }
    }
    private void SpawnTEST()
    {
        if (_Not == true) return;
        if (_spawnNum >= _TESTenemy.Length)
        {
            _Count += Time.deltaTime;
            if (_Count < 6) return;
            _boss.AttackOK();
            _slider.AttackOk();
            if (_Count >= 6.01f) return;
            _GameManager.ChangeBackGlound(0);
            _difficulty.Phase=1;
            return;
        }
        // if (_spawnCount >= _TESTenemy.Length - 1) return;
            if(_spawnNum>=1)
            {
                if(_OLDobjects!=null)return;
            }
            if(_spawnCount<0.8f)return;
            _OLDobjects=Instantiate(_TESTenemy[_spawnNum]);
            _spawnNum++;
            _spawnCount=0;
    }
    private void SpawnEASY()
    {
        if (_Not == true) return;
        if (_spawnNum >= _spawnEnemys.Length)
        {
            _Count += Time.deltaTime;
            if (_Count < 6) return;
            _boss.AttackOK();
            _slider.AttackOk();
            if (_Count >= 6.01f) return;
            _GameManager.ChangeBackGlound(0);
            _difficulty.Phase=1;
            return;
        }
        // if (_spawnCount >= _TESTenemy.Length - 1) return;
            if(_spawnNum>=1)
            {
                if(_OLDobjects!=null)return;
            }
            if(_spawnCount<1.2f)return;
            _OLDobjects=Instantiate(_spawnEnemys[_spawnNum]);
            _spawnNum++;
            _spawnCount=0;
    }
    private void SpawnNORMAL()
    {
        if (_Not == true) return;
        if (_spawnNum >= _spawnEnemys.Length)
        {
            _Count += Time.deltaTime;
            if (_Count < 6) return;
            _boss.AttackOK();
            _slider.AttackOk();
            if (_Count >= 6.01f) return;
            _GameManager.ChangeBackGlound(0);
            _difficulty.Phase=1;
            return;
        }
        // if (_spawnCount >= _TESTenemy.Length - 1) return;
            if(_spawnNum>=1)
            {
                if(_OLDobjects!=null)return;
            }
            if(_spawnCount<1.2f)return;
            _OLDobjects=Instantiate(_spawnEnemys[_spawnNum]);
            _spawnNum++;
            _spawnCount=0;
    }
    private void SpawnHARD()
    {
        if (_Not == true) return;
        if (_spawnNum >= _spawnEnemys.Length)
        {
            _Count += Time.deltaTime;
            if (_Count < 6) return;
            _boss.AttackOK();
            _slider.AttackOk();
            if (_Count >= 6.01f) return;
            _GameManager.ChangeBackGlound(0);
            _difficulty.Phase=1;
            return;
        }
        // if (_spawnCount >= _TESTenemy.Length - 1) return;
            if(_spawnNum>=1)
            {
                if(_OLDobjects!=null)return;
            }
            if(_spawnCount<1.2f)return;
            _OLDobjects=Instantiate(_spawnEnemys[_spawnNum]);
            _spawnNum++;
            _spawnCount=0;
    }
    public void OWARI()
    {
        _Not = true;
    }
    public void NumberChange(int A)
    {
        _difficulty.Phase=0;
        Number = A;
    }
    public void SUMMON(int _A)
    {
        if (string.IsNullOrEmpty(_InputField.text))
        {
            _GameManager.KIKEN(2);
            return;
        }
        else if (int.Parse(_InputField.text) > 10)
        {
            _GameManager.KIKEN(0);
            return;
        }
        else if (int.Parse(_InputField.text) <= 0)
        {
            _GameManager.KIKEN(1);
            return;
        }
        else if (int.Parse(_InputField.text) <= 4)
        {
            _spawnmode = SpawnMode.S1;
        }
        else if (int.Parse(_InputField.text) <= 8)
        {
            _spawnmode = SpawnMode.S1;
        }
        else if (int.Parse(_InputField.text) <= 10)
        {
            _spawnmode = SpawnMode.S1;
        }
        _summonTime = 2;
        // PrasuX = 0;
        PrasuY = 0;
        _summonNum = int.Parse(_InputField.text);
        _B = Random.Range(0, 3);
        if(_B==0)
        {
        _summonX = 0;
        _summonY = 6;
        }
        StartCoroutine(SUMMONENEMY(_A));
    }
    private IEnumerator SUMMONENEMY(int _A)
    {
        for (int i = 0; i < _summonNum; i++)
        {
            yield return new WaitForSeconds(_summonTime);
            ENEMY = Instantiate(_enemy[_A]);
            _ENEMY = ENEMY.GetComponent<Enemy>();
            if (_B == 0)
            {
                _summonX = -5;
                _summonY = 6;
                _OriginalY = _summonY;
                _summonTime = 0;
                _ENEMY.MOVEchange("down");
            switch(_spawnmode)
            {
            case SpawnMode.S1: S1(i); break;
            case SpawnMode.S2: S2(); break;
            case SpawnMode.S3: S3(); break;
            case SpawnMode.S4: S4(); break;
            }
            }
            else if (_B == 1)
            {
                _summonX = 9;
                _summonY = 3;
                _ENEMY.MOVEchange("left");
            }
            else if (_B == 2)
            {
                _summonX = -9;
                _summonY = 3;
                _ENEMY.MOVEchange("right");
            }
            ENEMY.transform.position = new Vector2(_summonX, _summonY);
        }
    }
    private void S1(int i)
    {
        _summonY += PrasuY;
        PrasuY += 2;
        if(_summonY>=_OriginalY+_summonNum)
        {
            _summonX += 4;
            if (i > 1) return;
            _summonY = _OriginalY;
        }
    }
    private void S2()
    {
    }
    private void S3()
    {
    }
    private void S4()
    {
    }
}
