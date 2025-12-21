using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BackGround : MonoBehaviour
{
    [SerializeField, Header("スクロール速度")]
    private float _speed;
    [SerializeField, Header("補正")]
    private float _offset;
    private Vector2 _cameraMinPos;
    private Vector2 _cameraMaxPos;
    void Start()
    {
        _cameraMinPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
        _cameraMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }
    void Update()
    {
        Scroll();
    }
    private void Scroll()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        float cameraOutPosY = _cameraMinPos.y * 3f;
        if (transform.position.y <= cameraOutPosY)
        {
            float resetPosY = _cameraMaxPos.y * 3f;
            transform.position = new Vector3(0f, resetPosY - _offset);
        }
    }
}
