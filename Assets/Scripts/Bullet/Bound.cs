using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    Vector2 _Vec;
    // Start is called before the first frame update
    void Start()
    {
        Bullet _bullet = gameObject.GetComponent<Bullet>();
        _Vec=_bullet.GettVec();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("AAAA");
            transform.Rotate(new Vector3(180, 0, 0));
        }
    }
}
