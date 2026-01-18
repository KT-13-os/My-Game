using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Vector3=UnityEngine.Vector3;

public class Linerenderscript : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private int lineNUM;
    void Awake()
    {
        _lineRenderer=gameObject.GetComponent<LineRenderer>();
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = Color.red;
        _lineRenderer.endColor = Color.red;
        _lineRenderer.startWidth=0.05f;
        _lineRenderer.endWidth=0.05f;
    }
    public IEnumerator CircleLine(int MARUnum,float Angle)
    {
        lineNUM=0;
        _lineRenderer.positionCount=MARUnum*2;
        _lineRenderer.SetPosition(lineNUM,this.gameObject.transform.position);
        for (int i = 0; i < MARUnum; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange /MARUnum * i - Mathf.Deg2Rad * (Angle + 360f / 2f);
            _lineRenderer.SetPosition(lineNUM++,this.gameObject.transform.position+new Vector3(Mathf.Cos(theta)*10,Mathf.Sin(theta)*10,0));
            _lineRenderer.SetPosition(lineNUM++,this.gameObject.transform.position);
        }
        yield return new WaitForSeconds(0.9f);
        _lineRenderer.positionCount=0;
    }
    public IEnumerator TargetLine(Vector3 Pos,GameObject TARGET,GameObject TARGETobject)
    {
        float X=0;
        float Y=0;
        float StartTime=Time.time;
        while(Time.time-StartTime<1.2f)
        {
        if(TARGETobject.transform.position==TARGET.transform.position)yield return null;
        TARGETobject.transform.position=TARGET.transform.position;
        _lineRenderer.positionCount=2;
        _lineRenderer.SetPosition(0,Pos);
        if(Pos.y>=TARGETobject.transform.position.y)
        {
            Y=(Pos.y-TARGETobject.transform.position.y)*-1;
        }
        else
        {
            Y=TARGETobject.transform.position.y-Pos.y;
        }
        if(Pos.x>=TARGETobject.transform.position.x)
        {
            X=(Pos.x-TARGETobject.transform.position.x)*-1;
        }
        else
        {
            X=TARGETobject.transform.position.x-Pos.x;
        }
        _lineRenderer.SetPosition(1,new Vector3(X*10-2.6f,Y*10,0));
        yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        _lineRenderer.positionCount=0;
    }
    public void STOPline()
    {
        StopAllCoroutines();
        _lineRenderer.positionCount=0;
        _lineRenderer.SetPosition(0,this.gameObject.transform.position);
        Destroy(this.gameObject);
    }
}
