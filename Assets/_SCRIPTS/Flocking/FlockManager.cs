using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject birdObj;  //Prefab of the Bird
    static int birdAmount = 20; //Initial Amount of birds
    public static GameObject[] allBirds = new GameObject[birdAmount]; //List containing the Birds
    
    public static int skyRadius = 20;   //Sky radius for the birds to flock in.
    public static Vector3 goalPos = Vector3.zero; //Goal position for the birds to flock towards.
    
    public bool showUI; //A Booleon value to decide if you do or don't want to show the UI Controls.
    public GameObject UIControlsObj;
    
    void Start()
    {
        for (int i = 0; i < birdAmount; i++)
        {
            Vector3 mPos = new Vector3(  Random.Range(-skyRadius, skyRadius),
                                        Random.Range(-skyRadius, skyRadius),
                                        Random.Range(-skyRadius, skyRadius));
            allBirds[i] = (GameObject) Instantiate(birdObj, mPos, Quaternion.identity);
        }

        if(showUI)
        {
            UIControlsObj.SetActive(true);
        }
        else
        {
            UIControlsObj.SetActive(false);
        }
    }

    void Update()
    {
        Debug.DrawLine(new Vector3(-skyRadius, -skyRadius, -skyRadius), new Vector3(skyRadius, -skyRadius, -skyRadius), Color.green);
        Debug.DrawLine(new Vector3(skyRadius, -skyRadius, -skyRadius), new Vector3(skyRadius, skyRadius, -skyRadius), Color.green);
        Debug.DrawLine(new Vector3(skyRadius, skyRadius, -skyRadius), new Vector3(skyRadius, skyRadius, skyRadius), Color.green);
        Debug.DrawLine(new Vector3(skyRadius, skyRadius, skyRadius), new Vector3(-skyRadius, skyRadius, skyRadius), Color.green);
        Debug.DrawLine(new Vector3(-skyRadius, skyRadius, skyRadius), new Vector3(-skyRadius, -skyRadius, skyRadius), Color.green);
        Debug.DrawLine(new Vector3(-skyRadius, -skyRadius, skyRadius), new Vector3(-skyRadius, -skyRadius, -skyRadius), Color.green);
        Debug.DrawLine(new Vector3(skyRadius, -skyRadius, skyRadius), new Vector3(-skyRadius, -skyRadius, skyRadius), Color.green);
        Debug.DrawLine(new Vector3(skyRadius, -skyRadius, skyRadius), new Vector3(skyRadius, skyRadius, skyRadius), Color.green);
        Debug.DrawLine(new Vector3(-skyRadius, skyRadius, -skyRadius), new Vector3(-skyRadius, skyRadius, skyRadius), Color.green);
        Debug.DrawLine(new Vector3(-skyRadius, skyRadius, -skyRadius), new Vector3(skyRadius, skyRadius, -skyRadius), Color.green);
        Debug.DrawLine(new Vector3(-skyRadius, skyRadius, -skyRadius), new Vector3(-skyRadius, -skyRadius, -skyRadius), Color.green);
        Debug.DrawLine(new Vector3(skyRadius, -skyRadius, -skyRadius), new Vector3(skyRadius, -skyRadius, skyRadius), Color.green);


        for (int i = 0; i < allBirds.Length; i++)
        {
            //Debug.DrawLine(allBirds[i].transform.position, goalPos);
        }

        // if(Random.Range(1, 1000) < 10)
        // {
        //     goalPos = new Vector3(  Random.Range(-skyRadius, skyRadius),
        //                             Random.Range(-skyRadius, skyRadius),
        //                             Random.Range(-skyRadius, skyRadius));
        // }   
    }
    public void SetRange(float value)
    {
        for (int i = 0; i < allBirds.Length; i++)
        {
            allBirds[i].GetComponent<FlockBehaviour>().neighbourDist = value;
        }
    }
}
