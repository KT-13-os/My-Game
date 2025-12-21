using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUP : MonoBehaviour
{
    Vector3 velocity;
    Vector3 position;
    Vector3 acceleration;
    GameObject _player;
    private Rigidbody2D _rigid;
        [SerializeField] float period;
    void Start()
    {
        if (_player = GameObject.Find("Player"))
        {
            _player = GameObject.Find("Player");
        }
        velocity = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-6.0f, 6.0f), 0);
        period += Random.Range(-0.5f, 0.5f);
    }
    void Update()
    {
        acceleration = Vector3.zero;
        if (_player != null)
        {
            Vector3 diff = _player.transform.position - position;
            acceleration += (diff - velocity * period) * 2f / (period * period);
        }
         period -= Time.deltaTime;
        if (period < 0f)
        {
            return;
        }
        //いじらない
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;
        transform.position = position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScripts>().HPUp();
            Destroy(this.gameObject);
        }
    }
}
