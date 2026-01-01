using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Beem : MonoBehaviour
{
    private Sequence _sequence;
    public IEnumerator BEEMSUMMON(float TIME)
    {
        _sequence=DOTween.Sequence();
        _sequence.Append(transform.DOScale(new Vector3(gameObject.transform.localScale.x,0.8f,0),0.4f));
        _sequence.AppendInterval(TIME);
        _sequence.Append(transform.DOScale(new Vector3(gameObject.transform.localScale.x,0,0),0.6f));
        yield return new WaitForSeconds(1+TIME);
        Destroy(gameObject);
    }
    public IEnumerator BEAMRotation(float TIME,float RotationSpeed,GameObject O)
    {
            float Starttime=Time.time;
            while(Time.time-Starttime<TIME)
            {
            transform.RotateAround(O.transform.position,Vector3.forward,RotationSpeed*Time.deltaTime);
            yield return null;
            }
    }
}
