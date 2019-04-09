using UnityEngine;

[CreateAssetMenu(menuName ="AI/Behaviors/Constrain")]
public class AIConstrain : AIBeahvior {
    public Vector3 center;
    public Vector3 area;

    public override Vector3 DoBehavior (AIComponent boid) {
        Vector3 force = Vector3.zero;
        Bounds bounds = new Bounds(center, area);

        if(!bounds.Contains(boid.transform.position)) {
            force = bounds.ClosestPoint(boid.position) - boid.position;
        }

        return force.normalized;
    }

}