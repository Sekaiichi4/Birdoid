using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject birdObj;  //Prefab of the Bird
    static int birdAmount = 40; //Initial Amount of birds
    public static GameObject[] allBirds = new GameObject[birdAmount]; //List containing the Birds
    
    public static int skyRadius = 20;   //Sky radius for the birds to flock in.
    public static Vector3 goalPos = Vector3.zero; //Goal position for the birds to flock towards.
    
    public bool showUI; //A Booleon value to decide if you do or don't want to show the UI Controls.
    public GameObject UIControlsObj;
    
    void Start()
    {
        for (int i = 0; i < birdAmount; i++)
        {
            Vector3 pos = new Vector3(  Random.Range(-skyRadius, skyRadius),
                                        Random.Range(-skyRadius, skyRadius),
                                        Random.Range(-skyRadius, skyRadius));
            allBirds[i] = (GameObject) Instantiate(birdObj, pos, Quaternion.identity);
        }

        if(showUI)
        {
            Instantiate(UIControlsObj, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
        }
    }

    void Update()
    {
        if(Random.Range(1, 1000) < 50)
        {
            goalPos = new Vector3(  Random.Range(-skyRadius, skyRadius),
                                    Random.Range(-skyRadius, skyRadius),
                                    Random.Range(-skyRadius, skyRadius));
        }
    }
}
