using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Behaviors/Avoid")]
public class AIAvoid : AIBeahvior {

	public AISensor sensor;

	Vector3 force;

	public override Vector3 DoBehavior (AIComponent boid) {
		Vector3 force = Vector3.zero;
		Collider[] obstacles = GetObstacles(boid);

		Vector3[] positions = new Vector3[obstacles.Length];

		for (int i = 0; i < obstacles.Length; i++) {
			if(obstacles[i]){
				positions[i] = boid.position - obstacles[i].transform.position;
			}
		}

		Vector3 averagePosition = GetAverageVector (positions);

		if(averagePosition != Vector3.zero){
			force = averagePosition;
		}

		Debug.DrawRay(boid.transform.position, force.normalized, Color.red);

		return force.normalized;
	}

	Collider[] GetObstacles(AIComponent boid){
		return sensor.GetObjects(boid);
	}

	Vector3 GetAverageVector (Vector3[] positions) {
		Vector3 sum = Vector3.zero;

		// Get the sum of all the positions
		for (int i = 0; i < positions.Length; i++)
		{
			sum += positions[i];
		}
		return sum / positions.Length;
	}
}
