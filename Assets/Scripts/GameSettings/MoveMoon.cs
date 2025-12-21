using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMoon : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float _Speed;
    Rigidbody2D _rigid;
    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 1.25)
        {
            _rigid.velocity = Vector2.zero;
            return;
        }
        _rigid.velocity = Vector2.down * _Speed;
    }
}
