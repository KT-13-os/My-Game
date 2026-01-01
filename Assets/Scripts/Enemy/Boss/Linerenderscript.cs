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
    void Start()
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
    public IEnumerator TargetLine(int BEAMnum,Vector3 Pos,GameObject TARGET,GameObject TargetObj)
    {
        float X=0;
        float Y=0;
        float StartTime=Time.time;
        while(Time.time-StartTime<1f)
        {
        if(TargetObj.transform.position!=TARGET.transform.position)
        TargetObj.transform.position=TARGET.transform.position;
        lineNUM=0;
        if(TargetObj.transform.position.y<=Pos.y)
        {
            Y=Pos.y-TargetObj.transform.position.y;
        }
        else
        {
            Y=(TargetObj.transform.position.y-Pos.y)*-1;
        }
        if(TargetObj.transform.position.x<=Pos.x)
        {
            X=(Pos.x-TargetObj.transform.position.x)*-1;
        }
        else
        {
            X=TargetObj.transform.position.x-Pos.x;
        }
        gameObject.transform.position=new Vector3(Pos.x+1.1f,Pos.y+0.4f,0);
        _lineRenderer.positionCount=BEAMnum*3-1;
        _lineRenderer.SetPosition(lineNUM,this.gameObject.transform.position);
        for(int i=0;i<BEAMnum;i++)
        {
            _lineRenderer.SetPosition(lineNUM++,new Vector3(TargetObj.transform.position.x+(-1.1f+(2.5f/BEAMnum)*i)+X,TargetObj.transform.position.y-Y,0));
            _lineRenderer.SetPosition(lineNUM++,this.gameObject.transform.position);
            if(i>=BEAMnum-1)
            {
                break;
            }
            gameObject.transform.position-=new Vector3(2.5f/BEAMnum,0,0);
            _lineRenderer.SetPosition(lineNUM++,this.gameObject.transform.position);
        }
        yield return null;
        }
        yield return new WaitForSeconds(0.6f);
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
