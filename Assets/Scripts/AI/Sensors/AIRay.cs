using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

[CreateAssetMenu(menuName = "AI/Sensors/Ray")]
public class AIRay:AISensor 
{
	public enum RelativeTo { self, world }

	public float range = 1; 
	public RelativeTo relativeTo; 
	public Vector3 direction = Vector3.forward;

	Collider[] colliders = new Collider[1];

	public override Collider[] GetObjects(AIComponent boid)
    {
		colliders[0] = null; 
		Vector3 dir = direction; 

		if (relativeTo == RelativeTo.self)
        {
			dir = boid.transform.TransformDirection(dir);
		}

		Ray ray = new Ray(boid.position, direction); 
		RaycastHit hit; 

		if (Physics.Raycast(ray, out hit, range, layerMask))
        {
			colliders[0] = hit.collider; 
		}

		return colliders; 
	}
}
