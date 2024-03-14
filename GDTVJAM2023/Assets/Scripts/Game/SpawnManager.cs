using System;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.XR;

[Serializable]
public class WaveData
{
    public GameObject enemyToSpawn;
    public int spawnProbabilities;
    public bool isGroundEnemy = false;
}


public class SpawnManager : MonoBehaviour
{

    public enum Wave
    {
        Wave1,
        Wave2,
        Wave3,
        Wave4,
        Wave5,
        Wave6,
        Wave7,
        Wave8,
        Wave9
    }

    [Header("Choose Wave")]
    public Wave wave;

    [Header("Spawn Elements")]
    public WaveData[] waveData;

    [Header("Spawn Settings")]
    public int maxWaveEnemys;
    public float minSpawnDistance = 8f;
    public float maxSpawnDistance = 10f;
    public float spawnInterval = 2f;

    private GameManager gameManager;
    private Transform playerTransform;
    private SpawnDistrictList spawnDistrictList;


    /* **************************************************************************** */
    /* LIFECYCLE METHODEN---------------------------------------------------------- */
    /* **************************************************************************** */
    private void Start()
    {
        // find gameobjects
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnDistrictList = gameManager.GetComponent<SpawnDistrictList>();

        playerTransform = GameObject.Find("NewPlayer").GetComponent<Transform>();
        playerTransform = GameObject.Find("NewPlayer").GetComponent<Transform>();

        // start spawning
        InvokeRepeating("SpawnObject", 3f, spawnInterval);
    }

    private void OnDestroy()
    {
        // stop spawning
        CancelInvoke("SpawnObject");
    }



    /* **************************************************************************** */
    /* Runtime Funcions------------------------------------------------------------ */
    /* **************************************************************************** */

    private void SpawnObject()
    {
        if (!gameManager.dimensionShift && gameManager.curretEnemyCounter < maxWaveEnemys)
        {
            // Randomly select an object to spawn based on probabilities
            int randomIndex = GetRandomWeightedIndex();
            GameObject objectToSpawn = waveData[randomIndex].enemyToSpawn;
            Vector3 spawnPosition = new Vector3(0, 0, 0);

            // Generate a random position outside the camera's view
            if (waveData[randomIndex].isGroundEnemy == false)
            {
                spawnPosition = GetRandomSpawnPositionFromPlayer();
                gameManager.UpdateEnemyCounter(1);
            }
            else
            {
                // Choose a District
                int i = 0;
                i = UnityEngine.Random.Range(0, gameManager.districtNumber);

                // Spawn in District X
                spawnPosition = GetRandomSpawnPointOverDistrict(spawnDistrictList.districtList[i].transform, 18f);
            }
            

            // Spawn the object at the generated position
            switch (wave)
            {
                case Wave.Wave1:
                    ObjectPoolManager.SpawnObject(objectToSpawn, spawnPosition, Quaternion.identity, ObjectPoolManager.PoolType.Wave1);
                    break;
                case Wave.Wave2:
                    ObjectPoolManager.SpawnObject(objectToSpawn, spawnPosition, Quaternion.identity, ObjectPoolManager.PoolType.Wave2);
                    break;
                case Wave.Wave3:
                    ObjectPoolManager.SpawnObject(objectToSpawn, spawnPosition, Quaternion.identity, ObjectPoolManager.PoolType.Wave3);
                    break;
                case Wave.Wave4:
                    ObjectPoolManager.SpawnObject(objectToSpawn, spawnPosition, Quaternion.identity, ObjectPoolManager.PoolType.Wave4);
                    break;
                case Wave.Wave5:
                    ObjectPoolManager.SpawnObject(objectToSpawn, spawnPosition, Quaternion.identity, ObjectPoolManager.PoolType.Wave5);
                    break;
                case Wave.Wave6:
                    ObjectPoolManager.SpawnObject(objectToSpawn, spawnPosition, Quaternion.identity, ObjectPoolManager.PoolType.Wave6);
                    break;
                case Wave.Wave7:
                    ObjectPoolManager.SpawnObject(objectToSpawn, spawnPosition, Quaternion.identity, ObjectPoolManager.PoolType.Wave7);
                    break;
                case Wave.Wave8:
                    ObjectPoolManager.SpawnObject(objectToSpawn, spawnPosition, Quaternion.identity, ObjectPoolManager.PoolType.Wave8);
                    break;
                case Wave.Wave9:
                    ObjectPoolManager.SpawnObject(objectToSpawn, spawnPosition, Quaternion.identity, ObjectPoolManager.PoolType.Wave9);
                    break;
            }
        }
    }

    private int GetRandomWeightedIndex()
    {
        // Calculate the total weight of all probabilities
        int totalWeight = 0;

        for (int i = 0; i < waveData.Length; i++)
        {
            totalWeight += waveData[i].spawnProbabilities;
        }

        // Generate a random value between 0 and the total weight
        float randomValue = UnityEngine.Random.Range(0f, totalWeight);

        // Iterate through the probabilities and find the corresponding index based on the random value
        float cumulativeWeight = 0f;
        for (int i = 0; i < waveData.Length; i++)
        {
            cumulativeWeight += waveData[i].spawnProbabilities;
            if (randomValue <= cumulativeWeight)
            {
                return i;
            }
        }

        // Default to the last index if no valid index is found
        return 0;
    }

    private Vector3 GetRandomSpawnPositionFromPlayer()
    {
        // Zuf�llig w�hle einen Punkt innerhalb eines Sph�renbereichs um den Spieler
        Vector3 spawnDirection = UnityEngine.Random.onUnitSphere;
        float spawnDistance = UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 spawnPosition = playerTransform.position + (spawnDirection * spawnDistance) + new Vector3(5,0,5);
        spawnPosition.y = 6f;
        return spawnPosition;
    }

    private Vector3 GetRandomSpawnPointOverDistrict(Transform centerObject, float maxDistance)
    {
        // Zuf�llig w�hle einen Punkt innerhalb eines Sph�renbereichs um das Zentrum des Objekts
        Vector3 spawnDirection = UnityEngine.Random.onUnitSphere;
        float spawnDistance = UnityEngine.Random.Range(4f, maxDistance); 
        Vector3 spawnPosition = centerObject.position + spawnDirection * spawnDistance;
        spawnPosition.y = 6f;

        return spawnPosition;
    }
}