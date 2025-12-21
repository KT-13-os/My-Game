using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class StageTitle : MonoBehaviour
{
    [SerializeField, Header("STAGETITLE")]
    private TMP_Text stagetitle;
    [SerializeField, Header("MessageText")]
    private MessageText messageText;
    private float TEXTColor;
    [SerializeField, Header("TextPanel")]
    private GameObject _TextPanel;
    private TEXTPANEL _textpanel;
    private int index;
    void Start()
    {
        Tutorial();
        TEXTColor = 1;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Tutorial()
    {
        gameObject.transform.DOScale(new Vector3(1, 0.2f, 1), 1f).SetDelay(1);
        StartCoroutine(DisplaySimple(messageText.Titleparagraphs[index]));
        gameObject.transform.DOScale(new Vector3(0, 0, 1), 1f).SetDelay(5);
        StartCoroutine(NoneSimple());
    }
    private IEnumerator DisplaySimple(string TEXT)
    {
        yield return new WaitForSeconds(1.2f);
        string DisplayTEXT = "";
        int colorIndex = 0;
        foreach (char Pra in TEXT)
        {
            colorIndex++;
            stagetitle.text = TEXT;
            DisplayTEXT = stagetitle.text.Insert(colorIndex, "<color=#00000000>");
            stagetitle.text = DisplayTEXT;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator NoneSimple()
    {
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 10; i++)
        {
            TEXTColor -= 0.1f;
            stagetitle.color = new Color(220, 255, 143, TEXTColor);
            yield return new WaitForSeconds(0.05f);
        }
        if(index==0)
        {
            _textpanel = _TextPanel.GetComponent<TEXTPANEL>();
            _textpanel.MOVE("SLIDE", 0, 0, 0,0,0,1.4f);
        }
    }
}
