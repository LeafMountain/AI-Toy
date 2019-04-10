using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSpawner : MonoBehaviour
{
    public GameObject prefab;

    Camera cam;
    Vector3 lastClickPos;
    float mouseDragToSpawnDistance = 2;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && prefab)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (Vector3.Distance(lastClickPos, hit.point) > mouseDragToSpawnDistance || Input.GetMouseButtonDown(0))
                {
                    Vector3 spawnPos = hit.point;
                    Instantiate(prefab, spawnPos, Quaternion.identity, transform);
                    lastClickPos = spawnPos;
                }
            }
        }
    }

    RaycastHit GetMouseRayHitPosition()
    {
        RaycastHit hit;
        Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit);
        return hit;
    }
}
