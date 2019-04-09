using UnityEngine;

[System.Serializable]
public abstract class AIBeahvior : ScriptableObject {
    public virtual Vector3 DoBehavior (AIComponent boid) {
        return Vector3.zero;
    }
}