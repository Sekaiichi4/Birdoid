using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject birdObj;
    static int birdAmount = 10;
    public static GameObject[] allBirds = new GameObject[birdAmount];
    public static int spawnRange = 5;
    
    void Start()
    {
        for (int i = 0; i < birdAmount; i++)
        {
            Vector3 pos = new Vector3(  Random.Range(-spawnRange, spawnRange),
                                        Random.Range(-spawnRange, spawnRange),
                                        Random.Range(-spawnRange, spawnRange));
            allBirds[i] = (GameObject) Instantiate(birdObj, pos, birdObj.transform.rotation);
        }
    }

    void Update()
    {
        
    }
}
