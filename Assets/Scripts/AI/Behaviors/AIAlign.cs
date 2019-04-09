using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Behaviors/Align")]
public class AIAlign : AIBeahvior {

	public AISensor sensor;
	public int neighborMinLimit = 2;

	public override Vector3 DoBehavior (AIComponent boid) {
		AIComponent[] neighbors = GetNeighbors (boid);

		if (neighbors.Length < neighborMinLimit) {
			return Vector3.zero;
		}

		Vector3 force = Vector3.zero;

		Vector3[] directions = new Vector3[neighbors.Length];

		for (int i = 0; i < neighbors.Length; i++) {
			directions[i] = neighbors[i].velocity;
		}

		Vector3 averageDirection = GetAverageVector (boid, directions);

		if (averageDirection != Vector3.zero) {
			force = averageDirection;
		}

		return force.normalized;
	}

	AIComponent[] GetNeighbors (AIComponent boid) {
		return sensor.GetObjects<AIComponent> (boid);
	}

	Vector3 GetAverageVector (AIComponent enemy, Vector3[] positions) {
		Vector3 sum = Vector3.zero;

		// Get the sum of all the positions
		for (int i = 0; i < positions.Length; i++) {
			sum += positions[i];
		}
		return sum / positions.Length;
	}
}