using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class TEXTPANEL : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float _moveCount;
    private float _moveTime;
    private bool _MOVE;
    private float _speed;
    private Rigidbody2D _rg;
    private float _gravity;
    public bool _Tutorial = false;
    public bool _case = false;
    void Start()
    {
        _rg = gameObject.GetComponent<Rigidbody2D>();
        gameObject.SetActive(true);
        _moveTime = 0;
        _moveCount = 0;
        _gravity = 9.8f;
        _speed = 4.9f;
        _MOVE = false;
        if (_Tutorial == true)
        {
            // _speed = -9.8f;
            // _moveTime = 1.5f;
            // _gravity =-9.8f;
            // _MOVE = true;
            return;
        }
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (_MOVE == false) return;
        _moveCount += Time.deltaTime;
        _speed = _speed - _gravity * Time.deltaTime;
        _rg.velocity = Vector2.up * _speed;

        if (_moveCount > _moveTime)
        {
            _MOVE = false;
            _rg.velocity = Vector2.zero;
            _moveCount = 0;
        }
    }
    public void MOVE(string A,float SPEED,float TIME,float GLAVITY,float X,float Y,float MOVETIME)
    {
        if (A == "DOWN")
        {
            _speed = 4.9f;
            _moveTime = 1.5f;
            _MOVE = true;
        }
        else if (A == "UP")
        {
            transform.position = new Vector2(-2, -3.65f);
            // _speed = 9.8f;
            // _moveTime = 1.5f;
        }
        else if (A == "CUSTOM")
        {
            _speed = SPEED;
            _moveTime = TIME;
            _gravity = GLAVITY;
            _MOVE = true;
        }
        else if(A=="SLIDE")
        {
            gameObject.transform.DOMove(new Vector2(X, Y), MOVETIME);
        }
        Debug.Log(A);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_case == true) return;
        if (collision.gameObject.tag == "Player")
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.3f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_case == true) return;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
    }
}
