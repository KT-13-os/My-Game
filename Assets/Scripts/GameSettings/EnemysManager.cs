using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
[CreateAssetMenu(menuName = "EnemysManager")]
public class EnemysManager : ScriptableObject
{
    [SerializeField, Header("Stage1EASY")]
    public GameObject[] _EASYenemys1;
    [SerializeField, Header("Stage1Normal")]
    public GameObject[] _NORMALenemys1;
    [SerializeField, Header("Stage1HARD")]
    public GameObject[] _HARDenemys1;
    [SerializeField, Header("Stage2EASY")]
    public GameObject[] _EASYenemys2;
    [SerializeField, Header("Stage2Normal")]
    public GameObject[] _NORMALenemys2;
    [SerializeField, Header("Stage2HARD")]
    public GameObject[] _HARDenemys2;
}
