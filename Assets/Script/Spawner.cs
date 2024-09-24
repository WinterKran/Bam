using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable] // Corrected typo
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] spawnObject;

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void OnEnable()
    {
        // Start spawning when enabled
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        // Stop spawning when disabled
        CancelInvoke();
    }

    private void Spawn()
    {
        // Random value to determine which object to spawn
        float spawnChance = Random.value;

        // Loop through each spawnable object
        foreach (var obj in spawnObject)
        {
            // Check if this object should be spawned
            if (spawnChance < obj.spawnChance)
            {
                // Instantiate the prefab at the spawner's position
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += transform.position;

                break; // Spawn only one object and break the loop
            }

            // Reduce the chance for the next object in line
            spawnChance -= obj.spawnChance;
        }

        // Invoke the next spawn event with a random delay
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
