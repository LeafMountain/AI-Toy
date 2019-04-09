using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Sensors/Radar")]
public class AIRadar : AISensor {

	public float range;

	Collider[] colliders = new Collider[10];

	Vector3 currentPos;
	
	public override Collider[] GetObjects(AIComponent boid){
		currentPos = boid.position;

		int numberOfColliders = Physics.OverlapSphereNonAlloc(boid.position, range, colliders, layerMask);

		for (int i = 0; i < colliders.Length; i++) {
			// Remove boid from array			
			if(i < numberOfColliders) {
				if(colliders[i].transform == boid.transform){
					colliders[i] = null;
				}
			}
			// Reset the rest of the slots
			else{
				colliders[i] = null;				
			}
		}

		return colliders;
	}
}
