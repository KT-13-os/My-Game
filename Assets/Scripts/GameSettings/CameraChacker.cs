using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraChacker : MonoBehaviour
{
    enum Mode
    {
        None,
        Render,
        RenderOut,
    }
    private Mode _mode;
    void Start()
    {
        _mode = Mode.None;
    }
    void Update()
    {
        Dead();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BackGlound")
        {
            _mode = Mode.Render;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="BackGlound")
        {
        if(_mode == Mode.Render)
        {
            _mode = Mode.RenderOut;
        }
        }
    }
    private void Dead()
    {
        if (_mode == Mode.RenderOut)
        {
            Destroy(gameObject);
        }
    }
}
