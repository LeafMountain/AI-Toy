
using UnityEngine;

[CreateAssetMenu(menuName ="AI/Behaviors/Flock")]
public class AIFlock : AIBeahvior {

	public AISensor sensor;

	public override Vector3 DoBehavior (AIComponent boid) {
		Vector3 force = Vector3.zero;
		Transform[] neighbors = GetNeighbors(boid);

		Vector3[] neighborPositions = new Vector3[neighbors.Length];

		for (int i = 0; i < neighbors.Length; i++)
		{
			neighborPositions[i] = neighbors[i].position;
		}

		Vector3 averagePosition = GetAverageVector (boid, neighborPositions);

		if(averagePosition != Vector3.zero){
			force = averagePosition - boid.position;
		}

		return force.normalized;
	}

	Transform[] GetNeighbors(AIComponent boid){
		return sensor.GetObjects<Transform>(boid);
	}

	Vector3 GetAverageVector (AIComponent enemy, Vector3[] positions) {
		Vector3 sum = Vector3.zero;

		// Get the sum of all the positions
		for (int i = 0; i < positions.Length; i++)
		{
			sum += positions[i];
		}
		return sum / positions.Length;
	}
}