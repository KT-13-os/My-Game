using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBoss : BOSS
{
    // Start is called before the first frame update
    // [SerializeField, Header("StoryManager")]
    // private GameObject storymanager;
    // private StoryManager _storyManager;
    [SerializeField, Header("Player")]
    private GameObject Player;
    public GameObject _WALL;
    private PlayerScripts _playerScripts;
    private float X;
    private float Y;
    enum Tutorial
    {
        T,
        T1,
    }
    private Tutorial _tutorial;
    enum MoveMode
    {
        M,
        M1,
    }
    private MoveMode _moveMode;
    void Start()
    {
        _playerScripts = Player.GetComponent<PlayerScripts>();
        _playerScripts.STOP();
        _moveMode = MoveMode.M;
        _tutorial = Tutorial.T;
        // _storyManager = storymanager.GetComponent<StoryManager>();
        X = 3;
        // float FLOAT=Mathf.Pow(X+3,2);
        // Y = FLOAT / 8 + 5 / 2;
        // StartCoroutine(STARTMOVE());
    }
    private IEnumerator STARTMOVE()
    {
        yield return new WaitForSeconds(5);
        for (int i = 0; i < 60; i++)
        {
            transform.DOMove(new Vector2(X, Y), 1f);
            X -= 0.1f;
            float FLOAT = Mathf.Pow(X + 3, 2);
            Y = FLOAT / 8 + 5 / 2;
            yield return new WaitForSeconds(0.02f);
        }
        _WALL.SetActive(true);
        yield return new WaitForSeconds(2f);
        // _storyManager.PanelStart();
    }
    // Update is called once per frame
    protected override void Move()
    {
        switch (_moveMode)
        {
            case MoveMode.M: M(); break;
            case MoveMode.M1: M1(); break;
        }
        switch (_tutorial)
        {
            case Tutorial.T: T(); break;
            case Tutorial.T1: T1(); break;
        }
    }
    private void M()
    {
    }
    private void M1()
    {
    }
    private void T()
    {
    }
    private void T1()
    {
    }
    void Update()
    {

    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}