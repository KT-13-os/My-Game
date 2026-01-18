using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
public class TurretScript : MonoBehaviour
{
    [SerializeField, Header("弾オブジェクト")]
    private GameObject[] _bullet;
    [SerializeField, Header("予告線引くオブジェクト")]
    private GameObject _line;
    [SerializeField, Header("死亡エフェクト")]
    protected GameObject _deadEffect;
    Vector3 velocity;

    Vector3 acceleration;

    Transform target;
    public void BulletSummon(int BulletNumber,float BulletSpeed,float Bulletacceleration,int MoveMode,GameObject gameobject)
    {
        Bullet BULLET=_bullet[BulletNumber].GetComponent<Bullet>();
        BULLET.Speed(BulletSpeed);
        BULLET.KasokuCahange(Bulletacceleration);
        BULLET.MoveChange(MoveMode,gameobject);
        _bullet[BulletNumber].transform.rotation=gameObject.transform.rotation;
    }
    public IEnumerator BeamSummon(float TIME,GameObject TARGET,GameObject TARGETobject)
    {
        GameObject Line=Instantiate(_line);
        Linerenderscript linerenderscript=Line.GetComponent<Linerenderscript>();
        StartCoroutine(linerenderscript.TargetLine(gameObject.transform.position,TARGET,TARGETobject));
        StartCoroutine(RotateTurret(1.5f,TARGET));
        yield return new WaitForSeconds(1.6f);
        GameObject bullet = Instantiate(_bullet[1]);
        Beem _beem = bullet.GetComponent<Beem>();
        StartCoroutine(_beem.BEEMSUMMON(TIME));
        bullet.transform.position = TARGETobject.transform.position;
        Vector3 dir = gameObject.transform.position - TARGETobject.transform.position;
        bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        bullet.transform.rotation=Quaternion.FromToRotation(Vector3.up,-dir);
        bullet.transform.rotation=Quaternion.Euler(0,0,bullet.transform.rotation.eulerAngles.z+90);
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(ExitMove());
    }
    private IEnumerator ExitMove()
    {
        Vector3 scale=transform.localScale;
        SpriteRenderer spriteRenderer=GetComponent<SpriteRenderer>();
        transform.DOScale(scale/3,1.8f).SetEase(Ease.OutBounce);
        spriteRenderer.DOColor(Color.cyan,1);
        yield return new WaitForSeconds(0.6f);
        Instantiate(_deadEffect,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private IEnumerator RotateTurret(float TIME,GameObject TARGET)
    {
        float StartTime=Time.time;
        while(Time.time-StartTime<TIME)
        {
            Vector3 dir=transform.position - TARGET.transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, -dir);
            yield return null;
        }
    }
}
