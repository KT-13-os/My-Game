using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MessageText")]
public class MessageText : ScriptableObject
{
    public string[] KIKENparagraphs;
    [TextArea(2, 10)]
    public string speakerName;
    [TextArea(2, 10)]
    public string[] Titleparagraphs;
    [TextArea(2, 10)]
    public string[] Tutrialparagraphs;
    [TextArea(2, 10)]
    public string[] secretparagraphs;
    [TextArea(2, 10)]
    public string[] StoryParagraphs1;
}
