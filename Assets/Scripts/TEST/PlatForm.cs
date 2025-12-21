using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatForm : MonoBehaviour
{
    [SerializeField, Header("チュートリアルかどうか")]
    private bool Tutorial = false;
    private float x;
    private float y;
    private float mX;
    private float mY;
    private float TX;
    private bool puras;
    private int TutorialCount;
    void Start()
    {
        TutorialCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Tutorial == true)
            {
                if (TutorialCount >= 1) return;
                TutorialCount++;
                GameObject Manager = GameObject.Find("StoryManager");
                StoryManager manager = Manager.GetComponent<StoryManager>();
                manager.ReStart();
                MOVE("EXIT", 6, 8);
            }
            else
            {
            }
        }
    }
    // public void TutorialMove()
    // {
    //     X = transform.position.x;
    //     float FLOAT = Mathf.Pow(X - 7 / 2, 2);
    //     Y = FLOAT * 24 / 169 + 2;
    //     StartCoroutine(TutorialMOVE());
    // }
    public void MOVE(string a,float AM,float BM)
    {
        mX = transform.position.x;
        mY = transform.position.y;
        x = transform.position.x;
        if (AM > mX)
        {
            TX = 0.1f;
            puras = true;
        }
        else if(AM<mX)
        {
            TX = -0.1f;
            puras = false;
        }
        if (a == "ENTER")
        {
            StartCoroutine(ENTER(AM, BM));
        }
        else if (a == "EXIT")
        {
            StartCoroutine(EXIT(AM, BM));
        }
    }
    private IEnumerator ENTER(float A, float B)
    {
        for (int i = 0; i < 10000; i++)
        {
            y = ((mY - B) / Mathf.Pow(mX-A, 2)) * Mathf.Pow(x - A, 2) + B;
            transform.DOMove(new Vector2(x, y), 0.5f);
            x += TX;
            if (puras == true)
            {
            if (x >= A) yield break;
            }
            else if(puras==false)
            {
            if (x <= A) yield break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    private IEnumerator EXIT(float A,float B)
    {
        for (int i=0; i < 10000; i++)
        {
            transform.DOMove(new Vector2(x, y), 0.1f);
            x += TX;
            y = ((B - mY) / Mathf.Pow(A - mX, 2)) * Mathf.Pow(x - mX, 2) + mY;
            if (puras == true)
            {
            if (x >= A) yield break;
            }
            else if(puras==false)
            {
            if (x <= A) yield break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
    // private IEnumerator TutorialMOVE()
    // {
    //     for (int i = 0; i < 65; i++)
    //     {
    //         transform.DOMove(new Vector2(X, Y), 0.5f);
    //         X += 0.1f;
    //         float FLOAT = Mathf.Pow(X - 7 / 2, 2);
    //         Y = FLOAT * 24 / 169 + 2;
    //         yield return new WaitForSeconds(0.01f);
    //     }
    // }
    // private IEnumerator TutorialMOVE2()
    // {
    //     for (int i = 0; i < 65; i++)
    //     {
    //         transform.DOMove(new Vector2(X, Y), 0.5f);
    //         X -= 0.1f;
    //         float FLOAT = Mathf.Pow(X -7/2, 2);
    //         Y = FLOAT*24/169 + 2;
    //         yield return new WaitForSeconds(0.01f);
    //     }
    // }
}
