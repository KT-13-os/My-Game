using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Status")]
public class Status : ScriptableObject
{
    [SerializeField, Header("Playerの弾の状態")]
    public string PBulletStatus;
}
