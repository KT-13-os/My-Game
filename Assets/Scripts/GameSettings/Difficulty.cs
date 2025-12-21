using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
[CreateAssetMenu(menuName = "Difficulty")]
public class Difficulty : ScriptableObject
{
    public string DIFFICULTY;
    [SerializeField, Header("現在のPhase")]
    public float Phase;
    [SerializeField, Header("現在のSTAGE")]
    public int StageNum;
    [SerializeField, Header("DEBUG")]
    public float[] testBOSSHP;
    public float[] testEnemyHP;
    [SerializeField, Header("EASY")]
    public float[] easyBOSSHP;
    public float[] easyEnemyHP;
    [SerializeField, Header("NORMAL")]
    public float[] normalBOSSHP;
    public float[] normalEnemyHP;
    [SerializeField, Header("HARD")]
    public float[] hardBOSSHP;
    public float[] hardEnemyHP;
    public string PlayerInfinity;
    public void VeryEasy()
    {
        DIFFICULTY="VeryEasy";
    }
    public void Easy()
    {
        DIFFICULTY = "Easy";
    }
    public void Normal()
    {
        DIFFICULTY = "Normal";
    }
    public void Hard()
    {
        DIFFICULTY = "Hard";
    }
    public void TEST()
    {
        DIFFICULTY="TEST";
    }
}
