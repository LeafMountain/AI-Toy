using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSystem : ComponentSystem<SpawnerComponent> {

	// protected override void OnUpdate(){
	// 	for (int i = 0; i < components.Length; i++)
	// 	{			
	// 		if(components[i].numberSpawned < components[i].amount)
	// 		{
	// 			components[i].timeSinceLastSpawn += Time.deltaTime;

	// 			if(components[i].spawnInterval == 0){
	// 				for (int y = 0; y < components[i].amount; y++)
	// 				{
	// 					Spawn(components[i]);
	// 				}
	// 			}
	// 			else if(components[i].timeSinceLastSpawn > components[i].spawnInterval){
	// 				Spawn(components[i]);
	// 			}
	// 		}
	// 	}
	// }

	// // int failedSpawnPositions;
	// // int maximumFails = 10;

	// void Spawn(SpawnerComponent component){
	// 	Vector2 spawnArea = component.spawnArea;

	// 	Vector3 spawnPosition = RandomPosition(spawnArea);

	// 	Collider[] colliders = Physics.OverlapSphere(spawnPosition, 0);

	// 	for (int i = 0; i < colliders.Length; i++)
	// 	{
	// 		spawnPosition = colliders[i].bounds.ClosestPoint(spawnPosition);
	// 	}
		
	// 	Instantiate(component.prefab, component.transform.position + spawnPosition, Quaternion.Euler(new Vector3(0, Random.Range(0, 359), 0)), component.transform);
	// 	component.numberSpawned++;
	// 	component.timeSinceLastSpawn = 0;
	// }

	// Vector3 RandomPosition(Vector2 area){
	// 	float spawnX = Random.Range(-(area.x / 2), area.x / 2);
	// 	float spawnY = Random.Range(-(area.y / 2), area.y / 2);		

	// 	return new Vector3(spawnX, 20, spawnY);
	// }
}
