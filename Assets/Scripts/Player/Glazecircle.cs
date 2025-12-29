using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glazecircle : MonoBehaviour
{
    public GameObject _gamemanager;
    public GameObject _player;
    private GameManager _GameManager;
    private float _count;
    private float _Time;
    private Rigidbody2D _rigidbody2D;
    void Start()
    {
        _Time = 0.08f;
        _count = 0;
        _GameManager = _gamemanager.GetComponent<GameManager>();
        _rigidbody2D=gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == _player.transform.position) return;
        transform.position = _player.transform.position;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        _count += Time.deltaTime;
        if (_count < _Time) return;
        if (collision.gameObject.tag == "bullet")
        {
            _rigidbody2D.WakeUp();
            _GameManager.Glaze(1);
            _count=0;
        }
    }
}
