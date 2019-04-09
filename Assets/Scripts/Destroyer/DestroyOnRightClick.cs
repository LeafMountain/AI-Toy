using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnRightClick : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
            Destroy(gameObject);
    }
}
