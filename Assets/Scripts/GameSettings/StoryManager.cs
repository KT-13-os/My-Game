using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StoryManager : MonoBehaviour
{
    [SerializeField,Header("MessageText")] private MessageText messageText;
    [SerializeField,Header("TEXTpanel")] private GameObject panel;
    private TEXTPANEL textpanel;
    [SerializeField, Header("名前TEXT")] private TextMeshProUGUI speakerNameText;
    [SerializeField, Header("表示TEXT")]
    private TMP_Text tmpText;
    [SerializeField, Header("Player")]
    private GameObject _player;
    [SerializeField, Header("PlatForm")]
    private GameObject _platform;
    [SerializeField, Header("TutorialBoss")]
    private GameObject _tutorialBoss;
    private TutorialBoss _tutorialBOSS;
    private PlatForm platForm;
    private PlayerScripts _playerScripts;
    private int index = 0;
    private bool isTyping;
    private Coroutine displaysimple;
    private GameObject _panel;
    private float T;
    private float T2;
    private bool not;
    void Start()
    {
        platForm = _platform.GetComponent<PlatForm>();
        not = true;
        panel.SetActive(true);
        _tutorialBOSS = _tutorialBoss.GetComponent<TutorialBoss>();
    }
    public void PanelStart()
    {
        // textpanel = panel.GetComponent<TEXTPANEL>();
        not = false;
        _playerScripts = _player.GetComponent<PlayerScripts>();
        _panel = Instantiate(panel);
        textpanel = _panel.GetComponent<TEXTPANEL>();
        _panel.transform.position = transform.position;
        speakerNameText.text = messageText.speakerName;
        displaysimple = StartCoroutine(DisplaySimple(messageText.Tutrialparagraphs[index]));
    }
    private void Display()
    {
        // if (index >= 1)
        // {
        //     if (!isTyping)
        //     {
        //         // T = -0.5f * index;
        //         // T2 = -0.8f * index;
        //         _panel.transform.position = new Vector2(-2f + -0.6f, -3.65f + -0.3f);
        //         if (index < 2)
        //         {
        //             _panel2 = Instantiate(panel);
        //             _panel2.transform.position = transform.position;
        //         }
        //     }
        // }
        // パネルが非表示なら表示する
        if (!panel.activeSelf)
        {
            panel.SetActive(true);
        }
        if (not == true) return;
        if (messageText.Tutrialparagraphs.Length > index)
        {
            if (!isTyping)
            {
            if (index == 1)
            {
            _playerScripts.TutorialStart();
            }
                if (index == 6 || index == 10)
                {
                    textpanel.MOVE("DOWN",0,0,0,0,0,0);
                    STOP();
                    if (index == 6)
                    {
                        platForm.MOVE("ENTER", 7 / 2, 2);
                    }
                    if (index == 10)
                    {
                        _playerScripts.STOPOFF();
                    }
                    return;
                }
                displaysimple = StartCoroutine(DisplaySimple(messageText.Tutrialparagraphs[index]));
            }
            else
            {
                stopTyping();
            }
        }
        else
        {
            // 会話が終了したためパネルを非表示にする
            speakerNameText.text = "";
            tmpText.text = "";
            textpanel.MOVE("DOWN",0,0,0,0,0,0);
        }
    }
    private IEnumerator DisplaySimple(string TEXT)
    {
        string DisplayTEXT = "";
        isTyping = true;
        int colorIndex = 0;
        foreach (char Pra in TEXT)
        {
            colorIndex++;
            tmpText.text = TEXT;
            DisplayTEXT = tmpText.text.Insert(colorIndex, "<color=#00000000>");
            tmpText.text = DisplayTEXT;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
        index++; //次の会話文インデックス
    }
    private void stopTyping()
    {
        StopCoroutine(displaysimple);
        tmpText.text = messageText.Tutrialparagraphs[index];
        isTyping = false;
        index++;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Display();
        }
    }
    private void STOP()
    {
    speakerNameText.text = "";
        tmpText.text = "";
        isTyping = false;
        not = true;
    }
    public void ReStart()
    {
        index++;
        not = false;
        textpanel.MOVE("UP",0,0,0,0,0,0);
        panel.SetActive(true);
        StartCoroutine(RESTART());
    }
    private IEnumerator RESTART()
    {
        yield return new WaitForSeconds(0.5f);
        speakerNameText.text = messageText.speakerName;
        Debug.Log(index);
        displaysimple = StartCoroutine(DisplaySimple(messageText.Tutrialparagraphs[index]));
    }
}
