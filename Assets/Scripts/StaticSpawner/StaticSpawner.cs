using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSpawner : MonoBehaviour
{
    public void Instantiate(GameObject go)
    {
        GameObject.Instantiate(go, Vector3.zero, Quaternion.identity);
    }
}
