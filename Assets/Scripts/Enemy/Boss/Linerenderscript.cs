using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Linerenderscript : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private int MbossBlineNUM;
    void Start()
    {
        _lineRenderer=gameObject.GetComponent<LineRenderer>();
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = Color.red;
        _lineRenderer.endColor = Color.red;
        _lineRenderer.startWidth=0.1f;
        _lineRenderer.endWidth=0.1f;
    }
    public IEnumerator middleBossBLine(int MARUnum,int Angle)
    {
        MbossBlineNUM=0;
        _lineRenderer.positionCount=MARUnum*2;
        _lineRenderer.SetPosition(0,this.gameObject.transform.position);
        for (int i = 0; i < MARUnum; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange /MARUnum * i - Mathf.Deg2Rad * (Angle + 360f / 2f);
            _lineRenderer.SetPosition(MbossBlineNUM++,this.gameObject.transform.position+new Vector3(Mathf.Cos(theta)*10,Mathf.Sin(theta)*10,0));
            _lineRenderer.SetPosition(MbossBlineNUM++,this.gameObject.transform.position);
        }
        yield return new WaitForSeconds(1f);
        _lineRenderer.positionCount=0;
    }
}
