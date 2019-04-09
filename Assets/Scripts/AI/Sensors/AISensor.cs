using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AISensor : ScriptableObject {

	[SerializeField]
	protected LayerMask layerMask;

	public virtual Collider[] GetObjects(AIComponent boid){
		return null;
	}

	public T[] GetObjects<T>(AIComponent boid){
		Collider[] gos = GetObjects(boid);
		List<T> filteredGameObjects = new List<T>();

		for (int i = 0; i < gos.Length; i++)
		{
			if(gos[i])
			{
				T component = gos[i].GetComponent<T>();

				if(component != null){
					filteredGameObjects.Add(component);
				}
			}
		}
		return filteredGameObjects.ToArray();
	}
}
