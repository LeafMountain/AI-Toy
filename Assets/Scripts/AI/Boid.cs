
using UnityEngine;

public struct Boid {
	public AIComponent data;
	public int id;
	public Transform transform;
	public Rigidbody rigidbody;
	public Vector3 position;
	public Vector3 velocity;

	public Boid (AIComponent enemy, int id, Transform transform, Rigidbody rigidbody, Vector3 position, Vector3 velocity) {
		this.data = enemy;
		this.id = id;
		this.transform = transform;
		this.rigidbody = rigidbody;
		this.position = position;
		this.velocity = velocity;
	}
}