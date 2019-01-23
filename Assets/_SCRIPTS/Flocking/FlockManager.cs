using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject birdObj;  //Prefab of the Bird
    static int birdAmount = 10; //Initial Amount of birds
    public static GameObject[] allBirds = new GameObject[birdAmount]; //List containing the Birds
    
    public static int spawnRange = 5;   //Range radius for spawning the birds.
    public static Vector3 goalPos = Vector3.zero; //Goal position for the birds to flock towards.
    
    void Start()
    {
        for (int i = 0; i < birdAmount; i++)
        {
            Vector3 pos = new Vector3(  Random.Range(-spawnRange, spawnRange),
                                        Random.Range(-spawnRange, spawnRange),
                                        Random.Range(-spawnRange, spawnRange));
            allBirds[i] = (GameObject) Instantiate(birdObj, pos, Quaternion.identity);
        }
    }

    void Update()
    {
        if(Random.Range(1, 1000) < 50)
        {
            goalPos = new Vector3(  Random.Range(-spawnRange, spawnRange),
                                    Random.Range(-spawnRange, spawnRange),
                                    Random.Range(-spawnRange, spawnRange));
        }
    }
}
