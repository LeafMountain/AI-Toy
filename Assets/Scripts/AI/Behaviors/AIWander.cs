using UnityEngine;

[CreateAssetMenu(menuName ="AI/Behaviors/Wander")]
public class AIWander : AIBeahvior {
	System.Random random = new System.Random();

	float RandomFloat { get { return (float)random.Next(-100, 100) / 200f; } }	
	Vector3 RandomVector { get { return new Vector3(RandomFloat, 0, RandomFloat); } }	

	public override Vector3 DoBehavior(AIComponent boid){
		Vector3 force = Vector3.zero;

		if (boid.velocity == Vector3.zero) 
		{
			force += RandomVector;
		} 
		else 
		{
			float angleOffset = 45;
			Vector3 offset = Quaternion.Euler (0, RandomFloat * angleOffset, 0) * boid.velocity;

			force = boid.velocity + offset;
		}

		return force.normalized;
		
	}
}
