using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Beem : MonoBehaviour
{
    private Sequence _sequence;
    void Start()
    {
        _sequence=DOTween.Sequence();
        _sequence.Append(transform.DOScale(new Vector3(gameObject.transform.localScale.x,0.8f,0),0.4f));
        _sequence.AppendInterval(1);
        _sequence.Append(transform.DOScale(new Vector3(gameObject.transform.localScale.x,0,0),0.6f));
        StartCoroutine(BEEMSUMMON());
    }
    private IEnumerator BEEMSUMMON()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
