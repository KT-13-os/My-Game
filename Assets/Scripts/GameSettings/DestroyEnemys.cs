using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemys : MonoBehaviour
{
    void FixedUpdate()
    {
        GetChildren(this.gameObject);
    }
    private void GetChildren(GameObject obj)
    {
	Transform children = obj.GetComponentInChildren < Transform > ();
	if (children.childCount == 0)
    {
		Destroy(gameObject);
	}
}
}
