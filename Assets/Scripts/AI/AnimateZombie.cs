using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateZombie : MonoBehaviour
{
    [Range(0, 1)]
    public float smoothing = .5f;

    Animator animator;
    Rigidbody rigidbody;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        // velocity = GetComponent<Velocity>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 velo = rigidbody.velocity;
        velo = transform.InverseTransformDirection(velo);

        animator.SetFloat("velocityX", velo.x, smoothing, Time.deltaTime);
        animator.SetFloat("velocityY", velo.z, smoothing, Time.deltaTime);
    }
}
