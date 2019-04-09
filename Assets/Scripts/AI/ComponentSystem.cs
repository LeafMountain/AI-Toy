using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentSystem<T> : MonoBehaviour where T : UnityEngine.Object {
	protected T[] components;

	void Start(){
		//Populate array with enemies
		UpdateComponentsList();
	}

	void Update(){
		OnUpdate();
	}

	protected virtual void OnUpdate(){

	}

	protected void UpdateComponentsList(){
		components = GameObject.FindObjectsOfType<T>();	
	}
}
