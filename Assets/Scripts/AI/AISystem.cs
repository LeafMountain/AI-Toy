// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;

// public class AISystem : MonoBehaviour {

// 	public bool updateList = true;
// 	public Vector3 area;

// 	public Vector3 center;

// 	public Boid[] boids;
// 	Attractor[] attractors;

// 	System.Random random = new System.Random();

// 	public AIBeahvior[] behaviors;

// 	public struct Attractor {
// 		public Transform transform;
// 		public Vector3 position;
// 	}

// 	void Start(){
// 		center = transform.position;

// 		StartCoroutine("LateStart");
// 	}

// 	IEnumerator LateStart(){
// 		yield return new WaitForEndOfFrame();
// 		updateList = true;

// 		CollectBoids ();
// 		CollectAttractors();
// 	}

// 	void FixedUpdate () {
// 		for (int i = 0; i < boids.Length; i++)
// 		{
// 			UpdateData(ref boids[i]);
// 		}

// 		UpdateAttractors();		

// 		for (int i = 0; i < boids.Length; i++)
// 		{
// 			GetWalkDirection (boids[i]);
// 		}
// 	}

// 	void UpdateData(ref Boid enemy) {
// 		enemy.position = enemy.transform.position;
// 		enemy.velocity = enemy.rigidbody.velocity;
// 	}

// 	void CollectBoids () {
// 		AIComponent[] enemies = GameObject.FindObjectsOfType<AIComponent> ();
// 		this.boids = new Boid[enemies.Length];

// 		for (int i = 0; i < enemies.Length; i++) {
// 			enemies[i].transform.name = "Enemy nr." + i;
// 			enemies[i].id = i;
// 			this.boids[i] = new Boid (enemies[i], i, enemies[i].transform, enemies[i].GetComponent<Rigidbody> (), enemies[i].transform.position, Vector3.zero);
// 		}
// 	}

// 	void CollectAttractors(){
// 		ZombieAttractor[] attractors = FindObjectsOfType<ZombieAttractor>();
// 		this.attractors = new Attractor[attractors.Length];

// 		for (int i = 0; i < attractors.Length; i++)
// 		{
// 			this.attractors[i] = new Attractor();
// 			this.attractors[i].transform = attractors[i].transform;
// 			this.attractors[i].position = this.attractors[i].transform.position;
// 		}
// 	}

// 	void UpdateAttractors(){
// 		for (int i = 0; i < attractors.Length; i++)
// 		{
// 			attractors[i].position = attractors[i].transform.position;
// 		}
// 	}

// 	public Vector3 GetFuturePosition (Boid enemy) {
// 		return enemy.position + enemy.velocity;
// 	}

// 	Vector3 GetNearbyAttractor(Vector3 position, float distance){
// 		float neighborDistanceSqrt = distance * distance;

// 		for (int i = 0; i < attractors.Length; i++){
// 			if(Vector3.SqrMagnitude(position - attractors[i].position) < neighborDistanceSqrt){
// 				return attractors[i].position;
// 			}
// 		}

// 		return Vector3.zero;
// 	}

// 	void GetWalkDirection (Boid boid) {
// 		Vector3 force = Vector3.zero;

// 		Vector3 attractor = GetNearbyAttractor(boid.position, boid.data.neighborDistance);

// 		for (int i = 0; i < behaviors.Length; i++)
// 		{
// 			force += behaviors[i].DoBehavior(boid.data);
// 		}

// 		UnityAction DoStuff = () => {
// 			Seek(boid, force + boid.position);
// 			RotateGameObject(boid);
// 		};

// 		DoStuff.Invoke();
// 	}

// #region Behaviors

// 	void Seek (Boid boid, Vector3 target) {
// 		// The desired direction
// 		Vector3 desired;
// 		// Amount of force to apply
// 		Vector3 steer;

// 		desired = target - GetFuturePosition (boid);
// 		desired = desired.normalized;
// 		desired *= boid.data.maxSpeed;

// 		// Find the force to apply 
// 		steer = desired - boid.velocity;
// 		steer = steer.normalized * boid.data.maxForce;

// 		// Add force to physics system
// 		boid.rigidbody.AddForce (steer);
// 	}

// #endregion

// 	void RotateGameObject (Boid enemy) {
// 		enemy.transform.LookAt (enemy.velocity + enemy.position);
// 	}

// 	public Boid[] GetNeighbors (int id, Vector3 position, float radius) {
// 		return GetNeighbors(id, position, radius, boids);
// 	}

// 	Boid[] GetNeighbors (int id, Vector3 position, float radius, Boid[] boids) {
// 		List<Boid> neighbors = new List<Boid> ();

// 		float radiusSqr = radius * radius;

// 		for (int i = 0; i < boids.Length; i++) 
// 		{
// 			if(boids[i].id == id){
// 				continue;
// 			}

// 			float sqrMagnitude = Vector3.SqrMagnitude (this.boids[i].position - position);

// 			if (sqrMagnitude < radiusSqr) 
// 			{
// 				neighbors.Add (boids[i]);
// 			}
// 		}

// 		// Collider[] stuff = new Collider[30];

// 		// Physics.OverlapSphereNonAlloc(position, radius, stuff);

// 		// for (int i = 0; i < stuff.Length; i++)
// 		// {
// 		// 	if(stuff[i]){
// 		// 		AIComponent ai = stuff[i].GetComponent<AIComponent>();

// 		// 		if(ai){
// 		// 			neighbors.Add(boids[ai.id]);
// 		// 		}
// 		// 	}
// 		// }

// 		return neighbors.ToArray ();
// 	}

// 	public Vector3 GetAverageVector (Boid enemy, Vector3[] positions) {
// 		Vector3 sum = Vector3.zero;

// 		// Get the sum of all the positions
// 		for (int i = 0; i < positions.Length; i++) 
// 		{
// 			sum += positions[i];
// 		}
// 		return sum / positions.Length;
// 	}

// 	float RandomFloat { get { return (float)random.Next(-100, 100) / 100f; } }
// 	Vector3 RandomVector { get { return new Vector3(RandomFloat, 0, RandomFloat); } }

// 	void OnDrawGizmosSelected () {
// 		// Display area restriction
// 		Gizmos.DrawWireCube (center, area);
// 	}
// }
