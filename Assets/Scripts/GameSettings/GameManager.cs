using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
// using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Random =  UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("TutorialInputField")]
    private GameObject _inputfield;
    private InputField _InputField;
    [SerializeField, Header("ゲームタイトル")]
    protected GameObject _Title;
    [SerializeField, Header("Infinity Button")]
    protected GameObject _infinityButton;
    [SerializeField, Header("Infinity Panel")]
    protected GameObject _infinityPanel;
    [SerializeField, Header("Infinity yes")]
    protected GameObject _infinityYesButton;
    [SerializeField, Header("Infinity no")]
    protected GameObject _infinityNoButton;
    [SerializeField, Header("MessageText")]
    private MessageText messageText;
    [SerializeField, Header("Player")]
    private GameObject _player;
    private PlayerScripts _playerScripts;
    [SerializeField, Header("遅くなる時間")]
    private float _deadEffectTimeScale;
    [SerializeField, Header("スローモードにする時間")]
    private float _deadEffectTime;
    [SerializeField, Header("GameOver image")]
    private GameObject _gameover;
    [SerializeField, Header("GameOver Score")]
    private Text _Gscoretext;
    [SerializeField, Header("GameOver Glaze")]
    private Text _Gglazetext;
    [SerializeField, Header("GameOver PowerLevel")]
    private Text _GpowerLeveltext;
    [SerializeField, Header("Gameclear image")]
    private GameObject _gameclear;
    [SerializeField, Header("Gameclear Score")]
    private Text _GGscoretext;
    [SerializeField, Header("Gameclear Glaze")]
    private Text _GGglazetext;
    [SerializeField, Header("Gameclear HP")]
    private Text _GGhptext;
    // [SerializeField, Header("Gameclear Time")]
    // private Text _GGtimetext;
    [SerializeField, Header("Gameclear PowerLevel")]
    private Text _GGpowerLeveltext;
    [SerializeField, Header("Enemy Spowner")]
    private GameObject _enemyspowner;
    [SerializeField, Header("SE")]
    private AudioClip[] _sound;
    [SerializeField, Header("pause画面")]
    private GameObject _pauseMenuUI;
    [SerializeField, Header("explanation")]
    private GameObject _explanation;
    [SerializeField, Header("SummonEnemyTEXT")]
    private GameObject _summonEnemyTEXT;
    [SerializeField, Header("EnemyButton")]
    private GameObject _enemyBUTTON;
    [SerializeField, Header("注意TEXT")]
    private TMP_Text _KIKENtext;
    [SerializeField, Header("注意BOX")]
    private GameObject _KIKEN;
    [SerializeField, Header("difficultyButtons")]
    private GameObject _difficulty;
    [SerializeField, Header("Buttons")]
    private GameObject _Buttons;
    private GameObject[] _Enemy;
    private TEXTPANEL _textpanel;
    [SerializeField, Header("DIFFICULTY")]
    protected Difficulty _Sdifficulty;
    [SerializeField, Header("BackGlound")]
    private GameObject _backGlound;
    [SerializeField, Header("BackGlound2")]
    private GameObject _backGlound2;
    [SerializeField, Header("Hider")]
    private GameObject _hider;
    private SpriteRenderer _hidersprite;
    [SerializeField, Header("BGM")]
    private AudioSource[] BGM;
    AudioSource _audioSource;
    // [SerializeField, Header("BGMSource")]
    // private AudioSource _BGMSource;
    // [SerializeField, Header("SESource")]
    // private AudioSource _SESource;
    // [SerializeField, Header("AudioMixer")]
    // private AudioMixer _audioMixer;
    // [SerializeField, Header("SEGroup")]
    // private AudioMixerGroup _SEGroup;
    // [SerializeField, Header("BGMGroup")]
    // private AudioMixerGroup _BGMGroup;
    // [SerializeField, Header("SEの大きさ")]
    // private float _SEvalue;
    // [SerializeField, Header("BGMの大きさ")]
    // private float _BGMvalue;
    EnemySpawner _EnemySpawner;
    public Text _scoretext;
    public Text _powertext;
    public Text _Glazetext;
    private int _glaze;
    private int _glazePoint;
    public static GameManager instance;
    private int _npower;
    private int _powerPoint;
    private int _nscore;
    private int _Bscore;
    private bool _CountScore;
    public bool _tutorial = false;
    private string Gamesituation;
    private float HP;
    private int HPburst;
    private float GLAZE;
    private int GLAZEburst;
    private float POWER;
    private int POWERburst;
    private float SCORE;
    private int _playerHP;
    private int _hpPoint;
    Sequence CountnUpScore;
    private SubGameManager subGameManager;
    private AnimationManager animationManager;
    bool A;
    private float TIME;
    // private int _timePoint;
    private int TEXTcount;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        A = true;
    }
    void Start()
    {
        TIME = 0;
        _audioSource = gameObject.GetComponent<AudioSource>();
        subGameManager = gameObject.GetComponent<SubGameManager>();
        animationManager=gameObject.GetComponent<AnimationManager>();
        if(SceneManager.GetActiveScene().name == "MAIN")
        {
        _EnemySpawner = _enemyspowner.GetComponent<EnemySpawner>();
        }
        // _BGMSource.outputAudioMixerGroup = _BGMGroup;
        // _SESource.outputAudioMixerGroup = _SEGroup;
        // _audioMixer.SetFloat("VolumeParam_BGM",_BGMvalue);
        // _audioMixer.SetFloat("VolumeParam_SE",_SEvalue);
        _Sdifficulty.StageNum=0;
        _nscore = 0;
        _npower = 1;
        _glaze = 0;
        GLAZE = 0;
        POWER = 0;
        SCORE = 0;
        HP = 0;
        if (_Sdifficulty.DIFFICULTY == "Easy"||_Sdifficulty.DIFFICULTY=="VeryEasy"||_Sdifficulty.DIFFICULTY=="TEST")
        {
            HPburst = 100;
            POWERburst = 10;
            GLAZEburst = 1000;
        }
        else if (_Sdifficulty.DIFFICULTY == "Normal")
        {
            HPburst = 500;
            POWERburst = 25;
            GLAZEburst = 2500;
        }
        else if(_Sdifficulty.DIFFICULTY=="Hard")
        {
            HPburst = 1000;
            POWERburst = 50;
            GLAZEburst = 10000;
        }
        else if(_Sdifficulty.DIFFICULTY=="Tutorial")
        {
            _InputField=_inputfield.GetComponent<InputField>();
            _InputField.text="1";
        }
        if (_enemyspowner != null)
        {
            A = false;
        }
    }
    void Update()
    {
        TIME += Time.deltaTime;
        if (_CountScore == true)
        {
            if (Gamesituation == "Play")
            {
                _scoretext.text = _Bscore.ToString("f0");
            }
            else if (Gamesituation == "GAMEOVER")
            {
                _Gglazetext.text = GLAZE.ToString("f0");
                _GpowerLeveltext.text = POWER.ToString("f0");
                _Gscoretext.text = SCORE.ToString("f0");
            }
            else if(Gamesituation=="GameClear")
            {
                _GGpowerLeveltext.text = POWER.ToString("f0");
                _GGhptext.text = POWER.ToString("f0");
                _GGglazetext.text = GLAZE.ToString("f0");
                _GGscoretext.text = SCORE.ToString("f0");
            }
        }
        if (A == true) return;
        Pause();
    }
    public void NEXT()
    {
        _playerScripts.START();
        _Sdifficulty.StageNum++;
        _EnemySpawner.enabled=true;
        _EnemySpawner.StageChange();
        Gamesituation = "Play";
        _EnemySpawner.START();
        _gameclear.SetActive(false);
        _backGlound.SetActive(true);
        _backGlound2.SetActive(false);
        _nscore = 0;
        _npower = 1;
        _glaze = 0;
        GLAZE = 0;
        POWER = 0;
        SCORE = 0;
        _scoretext.text="0";
        _Glazetext.text="0";
        _powertext.text="1";
        ChangeBackGlound(1);
    }
    public void summonA()
    {
        _Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (_Enemy.Length>10)
        {
            KIKEN(3);
        }
        else
        {
            _EnemySpawner.SUMMON(0);
        }
    }
    public void summonB()
    {
        _Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (_Enemy.Length>10)
        {
            KIKEN(3);
        }
        else
        {
            _EnemySpawner.SUMMON(1);
        }
    }
    public void summonC()
    {
        _Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (_Enemy.Length>10)
        {
            KIKEN(3);
        }
        else
        {
            _EnemySpawner.SUMMON(2);
        }
    }
    public void summonD()
    {
        _Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (_Enemy.Length>10)
        {
            KIKEN(3);
        }
        else
        {
            _EnemySpawner.SUMMON(3);
        }
    }
    public void summonE()
    {
        _Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (_Enemy.Length>8)
        {
            KIKEN(3);
        }
        else
        {
            _EnemySpawner.SUMMON(4);
        }
    }
    public void ENEMY()
    {
        _summonEnemyTEXT.SetActive(true);
    }
    public void ENEMYOFF()
    {
        _summonEnemyTEXT.SetActive(false);
    }
    public void SETExplanation()
    {
        _explanation.SetActive(true);
        StartCoroutine(animationManager.ButtonMove(_infinityButton,"EXIT",1100));
        StartCoroutine(animationManager.ButtonMove(_Buttons,"EXIT",-225));
        _Title.SetActive(false);
    }
    public void OKExplanation()
    {
        _explanation.SetActive(false);
        if (_tutorial == true)
        {
            _textpanel = _enemyBUTTON.GetComponent<TEXTPANEL>();
            return;
        }
        StartCoroutine(animationManager.ButtonMove(_infinityButton,"ENTER",645));
        _Title.SetActive(true);
        StartCoroutine(animationManager.ButtonMove(_Buttons,"ENTER",80));
    }
    public void StartButton()
    {
        _Buttons.SetActive(false);
        _difficulty.SetActive(true);
        Time.timeScale = 1;
    }
    public void BackStart()
    {
        _Buttons.SetActive(true);
        _difficulty.SetActive(false);
    }
    public void MenuSTART()//Menu画面で使う用
    {
        StartCoroutine(animationManager.ButtonMove(_Buttons,"EXIT",-225));
        StartCoroutine(animationManager.ButtonMove(_difficulty,"ENTER",390));
        StartCoroutine(animationManager.ButtonMove(_infinityButton,"EXIT",1100));
        Time.timeScale = 1;
    }
    public void MenuBackStart()//Menu画面で使う用
    {
        StartCoroutine(animationManager.ButtonMove(_Buttons,"ENTER",80));
        StartCoroutine(animationManager.ButtonMove(_difficulty,"EXIT",-225));
        StartCoroutine(animationManager.ButtonMove(_infinityButton,"ENTER",645));
        Time.timeScale = 1;
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("Mnue");
        _Sdifficulty.DIFFICULTY=" ";
        Time.timeScale = 1;
    }
    public void MAINButton()
    {
        SceneManager.LoadScene("MAIN");
        Time.timeScale = 1;
    }
    public void MAIN2Button()
    {
        SceneManager.LoadScene("MAIN2");
        Time.timeScale = 1;
    }
    public void Tutorial()
    {
        _Sdifficulty.DIFFICULTY = "Tutorial";
        SceneManager.LoadScene("Tutorial");
        Time.timeScale = 1;
    }
    public void OpenConfig()
    {
    }
    public void ClosePause()
    {
        _pauseMenuUI.SetActive(false);
    }
    public void PlayerInfinityOn()
    {
        _infinityPanel.SetActive(true);
        StartCoroutine(animationManager.ButtonMove(_infinityButton,"EXIT",1100));
        StartCoroutine(animationManager.ButtonMove(_Buttons,"EXIT",-225));
    }
    public void PlayerInfinityYes()
    {
        _Sdifficulty.PlayerInfinity="INFINITY";
        _infinityPanel.SetActive(false);
        StartCoroutine(animationManager.ButtonMove(_infinityButton,"ENTER",645));
        StartCoroutine(animationManager.ButtonMove(_Buttons,"ENTER",80));
    }
    public void PlayerInfinityNo()
    {
        _Sdifficulty.PlayerInfinity=" ";
        _infinityPanel.SetActive(false);
        StartCoroutine(animationManager.ButtonMove(_infinityButton,"ENTER",645));
        StartCoroutine(animationManager.ButtonMove(_Buttons,"ENTER",80));
    }
    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                PAUSE();
            }
            else if (Time.timeScale == _deadEffectTimeScale) return;
            else
            {
                RESUME();
            }
        }
    }
    private void PAUSE()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }
    private void RESUME()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }
    public void ChangeBackGlound(int MODE)
    {
        _hidersprite = _hider.GetComponent<SpriteRenderer>();
        StartCoroutine(CHANGEBACKGLOUND(MODE));
    }
    private IEnumerator CHANGEBACKGLOUND(int MODE)
    {
        BGM[0].Stop();
        if (_Sdifficulty.DIFFICULTY == "Easy" || _Sdifficulty.DIFFICULTY == "Normal")
        {
            BGM[1].Play();
        }
        else if(_Sdifficulty.DIFFICULTY=="Hard")
        {
            BGM[2].Play();
        }
        for(int i=0;i<80;i++)
        {
            _hidersprite.color=_hidersprite.color+new Color32(0,0,0,3);
            yield return new WaitForSeconds(0.01f);
        }
        if(MODE==0)
        {
        _backGlound.SetActive(false);
        _backGlound2.SetActive(true);
        }
        else if(MODE==1)
        {
        animationManager.StageBackGloundChange();
        }
        for(int i=0;i<80;i++)
        {
            _hidersprite.color=_hidersprite.color-new Color32(0,0,0,3);
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void Score(int _score)
    {
        Gamesituation = "Play";
        _Bscore = _nscore;
        _nscore += _score;
        if (_CountScore == true)
        {
            CountnUpScore.Kill(true);
        }
        // CountUpAnime();
        CountUpAnime();
    }
    private void CountUpAnime()
    {
        _CountScore = true;
        CountnUpScore = DOTween.Sequence().Append(DOTween.To(() => _Bscore, num => _Bscore = num, _nscore, 0.25f)).AppendInterval(0.1f).AppendCallback(() => _CountScore = false);
    }
    private void GGCountHP()
    {
        _CountScore = true;
        DOTween.Sequence().Append(DOTween.To(() => HP, num => HP = num, _playerHP, 0.5f)).AppendInterval(0.1f).AppendCallback(() => _CountScore = false);
    }
    private void GGCountGLAZE()
    {
        _CountScore = true;
        DOTween.Sequence().Append(DOTween.To(() => GLAZE, num => GLAZE = num, _glaze, 1f)).AppendInterval(0.1f).AppendCallback(() => _CountScore = false);
    }
    private void GGCountPOWER()
    {
        _CountScore = true;
        DOTween.Sequence().Append(DOTween.To(() => POWER, num => POWER = num, _npower, 0.5f)).AppendInterval(0.1f).AppendCallback(() => _CountScore = false);
    }
    private void GGCountSCORE()
    {
        _CountScore = true;
        DOTween.Sequence().Append(DOTween.To(() => SCORE, num => SCORE = num, _nscore, 10f)).AppendInterval(0.1f).AppendCallback(() => _CountScore = false);
    }

    public void POWERLEVEL(int _power)
    {
        _audioSource.PlayOneShot(_sound[0]);
        _npower = _power;
        _powertext.text = _power.ToString("f0");
    }
    public void DeadEffect()
    {
        StartCoroutine(Slow());
    }
    IEnumerator Slow()
    {
        Time.timeScale = _deadEffectTimeScale;
        yield return new WaitForSecondsRealtime(_deadEffectTime);
        Time.timeScale = 1.0f;
        GameOver();
    }
    private void GameOver()
    {
        subGameManager.CountStop();
        Gamesituation = "GAMEOVER";
        _glazePoint = _glaze * GLAZEburst;
        _powerPoint = _npower * POWERburst;
        _EnemySpawner.enabled = false;
        _gameover.SetActive(true);
        _nscore = _nscore+_glazePoint+_powerPoint;
        StartCoroutine(GGscore());
    }
    public void Gameclear()
    {
        Gamesituation = "GameClear";
        _playerScripts = _player.GetComponent<PlayerScripts>();
        _playerHP = _playerScripts.GetHP();
        _hpPoint = _playerHP * HPburst;
        _glazePoint = _glaze * GLAZEburst;
        _powerPoint = _npower * POWERburst;
        _EnemySpawner.enabled = false;
        _gameclear.SetActive(true);
        _nscore = _nscore + _glazePoint + _powerPoint+_hpPoint;
        StartCoroutine(GGscore());
    }
    private IEnumerator GGscore()
    {
        for(int i=0;i<4;i++)
        {
            if (i == 0)
            {
                GGCountPOWER();
                yield return new WaitForSeconds(0.6f);
            }
            else if(i==1)
            {
                if (Gamesituation == "GAMEOVER") yield return new WaitForSeconds(0.01f);
                GGCountHP();
                yield return new WaitForSeconds(0.6f);
            }
            else if (i == 2)
            {
                GGCountGLAZE();
                yield return new WaitForSeconds(1.1f);
            }
            else if (i == 3)
            {
                GGCountSCORE();
            }
        }
    }
    public void ClearEffect()
    {
        StartCoroutine(Slow2());
    }
    public void Glaze(int A)
    {
        _glaze += A;
        _Glazetext.text = _glaze.ToString("f0");
    }
    IEnumerator Slow2()
    {
        Time.timeScale = _deadEffectTimeScale;
        yield return new WaitForSecondsRealtime(_deadEffectTime);
        Time.timeScale = 1.0f;
        Gameclear();
    }
    public void KIKEN(int _A)
    {
        // _KIKENtext.transform.position = new Vector2(400, 200);
        _KIKENtext.text = messageText.KIKENparagraphs[_A];
        _KIKEN.SetActive(true);
        StartCoroutine(KIKENTEXT());
    }
    private IEnumerator KIKENTEXT()
    {
        yield return new WaitForSeconds(3f);
        _KIKENtext.text = "";
        _KIKEN.SetActive(false);
        // _KIKENtext.transform.position = new Vector2(800,800);
    }
}