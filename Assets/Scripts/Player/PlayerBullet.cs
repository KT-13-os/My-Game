using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerBullet : Bullet
{
    Vector3 velocity;

    // 追尾レーザーの初期位置
    Vector3 position;

    Vector3 acceleration;

    // ターゲットが存在しない時の目標位置
    Vector3 randomPos;

    Transform target;

    // 追尾レーザーの初期位置に最も近いオブジェクト
    GameObject searchNearObj;
    float _Count;

    // 着弾までの時間
    [SerializeField] float period;

    void Start()
    {
        position = transform.position;
        searchNearObj = FindClosestEnemy();
        // ターゲットが存在すれば位置を取得
        if (searchNearObj != null)
        {
            target = searchNearObj.transform;
        }
        randomPos = new Vector3(Random.Range(-2.0f, 2.0f), 6, 0);
        // プレイヤーの斜め後ろにレーザーを発射
        velocity = new Vector3(Random.Range(-6.0f, 6.0f), Random.Range(-8.0f, 8.0f), 0);
        period += Random.Range(-1f, 0.5f);
    }

    void Update()
    {
        _Count += Time.deltaTime;
        if (_Count >= 5f)
        {
            Destroy(gameObject);
        }
            acceleration = Vector3.zero;
        if (searchNearObj != null)
        {
            Vector3 diff = target.position - position;
            acceleration += (diff - velocity * period) * 2f / (period * period);
        }
        else
        {
            Vector3 diff = randomPos - position;
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
        // if (period<=0)
        // {
        //     gameObject.GetComponent<Enemy>().DamageA(_power);
        //     Destroy(gameObject);
        // }
    }

    public GameObject FindClosestEnemy()
    {
        // EnemyのTagを持つゲームオブジェクトの配列
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        // 最も近い位置に存在するEnemy
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        // 最も近かったEnemyを返す
        return closest;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }
    }
}