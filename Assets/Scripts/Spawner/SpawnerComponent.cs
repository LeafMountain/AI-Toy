using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerComponent : MonoBehaviour
{

    public GameObject prefab;
    public int count;
    public Vector3 spawnArea;

    Vector3 position;

    void Start()
    {
        position = transform.position;

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = RandomPosition(position + spawnArea);
            Quaternion spawnRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 359), 0));
            Instantiate(prefab, transform.position + spawnPosition, spawnRotation, transform);
        }
    }

    Vector3 RandomPosition(Vector3 area)
    {
        float spawnX = Random.Range(-(area.x / 2), area.x / 2);
        float spawnY = Random.Range(-(area.y / 2), area.y / 2);
        float spawnZ = Random.Range(-(area.z / 2), area.z / 2);

        return new Vector3(spawnX, spawnY, spawnZ);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
}
