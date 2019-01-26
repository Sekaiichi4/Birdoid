using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public bool birdsCaged; //A Booleon value to decide if you do or don't want to keep the birds within the skyRadius.    
    public int cageRadius = 15;   //Sky radius for the birds to flock in.    
    
    public GameObject birdObj;  //Prefab of the Bird
    static int birdAmount = 60; //Initial Amount of birds
    public GameObject[] allBirds; //List containing the Birds

    public bool showUI; //A Booleon value to decide if you do or don't want to show the UI Controls.
    public GameObject UIControlsObj;
    
    void Start()
    {
        allBirds = new GameObject[birdAmount];

        for (int i = 0; i < birdAmount; i++)
        {
            Vector3 mPos = new Vector3( Random.Range(-cageRadius, cageRadius),
                                        Random.Range(-cageRadius, cageRadius),
                                        Random.Range(-cageRadius, cageRadius));
            allBirds[i] = (GameObject) Instantiate(birdObj, mPos, Quaternion.identity, this.transform);
            allBirds[i].GetComponent<FlockBehaviour>().isCaged = birdsCaged;
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
        if(birdsCaged)
        {
            //Show the BoundaryBox
            Debug.DrawLine(new Vector3(-cageRadius, -cageRadius, -cageRadius), new Vector3(cageRadius, -cageRadius, -cageRadius), Color.green);
            Debug.DrawLine(new Vector3(cageRadius, -cageRadius, -cageRadius), new Vector3(cageRadius, cageRadius, -cageRadius), Color.green);
            Debug.DrawLine(new Vector3(cageRadius, cageRadius, -cageRadius), new Vector3(cageRadius, cageRadius, cageRadius), Color.green);
            Debug.DrawLine(new Vector3(cageRadius, cageRadius, cageRadius), new Vector3(-cageRadius, cageRadius, cageRadius), Color.green);
            Debug.DrawLine(new Vector3(-cageRadius, cageRadius, cageRadius), new Vector3(-cageRadius, -cageRadius, cageRadius), Color.green);
            Debug.DrawLine(new Vector3(-cageRadius, -cageRadius, cageRadius), new Vector3(-cageRadius, -cageRadius, -cageRadius), Color.green);
            Debug.DrawLine(new Vector3(cageRadius, -cageRadius, cageRadius), new Vector3(-cageRadius, -cageRadius, cageRadius), Color.green);
            Debug.DrawLine(new Vector3(cageRadius, -cageRadius, cageRadius), new Vector3(cageRadius, cageRadius, cageRadius), Color.green);
            Debug.DrawLine(new Vector3(-cageRadius, cageRadius, -cageRadius), new Vector3(-cageRadius, cageRadius, cageRadius), Color.green);
            Debug.DrawLine(new Vector3(-cageRadius, cageRadius, -cageRadius), new Vector3(cageRadius, cageRadius, -cageRadius), Color.green);
            Debug.DrawLine(new Vector3(-cageRadius, cageRadius, -cageRadius), new Vector3(-cageRadius, -cageRadius, -cageRadius), Color.green);
            Debug.DrawLine(new Vector3(cageRadius, -cageRadius, -cageRadius), new Vector3(cageRadius, -cageRadius, cageRadius), Color.green);
        }
        
        // for (int i = 0; i < allBirds.Length; i++)
        // {

        // }
    }
    public void SetRangeFor(int _valueID, float _value)
    {
        switch (_valueID)
        {
            case 0:
            for (int i = 0; i < allBirds.Length; i++)
            {
                allBirds[i].GetComponent<FlockBehaviour>().alignmentDist = _value;
            }
            break;

            case 1:
            for (int i = 0; i < allBirds.Length; i++)
            {
                allBirds[i].GetComponent<FlockBehaviour>().cohesionDist = _value;
            }
            break;
            case 2:
            for (int i = 0; i < allBirds.Length; i++)
            {
                allBirds[i].GetComponent<FlockBehaviour>().separationDist = _value;
            }
            break;
        }  
    }
}
