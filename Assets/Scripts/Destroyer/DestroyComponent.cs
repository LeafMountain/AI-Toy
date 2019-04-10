using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DestroyComponent : MonoBehaviour
{
    public Vector3 size;

    void Start()
    {
        GetComponent<BoxCollider>().size = size;
    }

    void OnTriggerEnter(Collider collider)
    {
        Destroy(collider.gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, size);
    }
}
