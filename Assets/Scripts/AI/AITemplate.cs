using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Template")]
public class AITemplate : ScriptableObject {

	// The maximum speed
	public float maxSpeed;
	// How good is it at turning
	public float maxForce;

	public AIBeahvior[] behaviors;
}
