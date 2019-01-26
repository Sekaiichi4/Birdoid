using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlockManager : MonoBehaviour
{
    private Toggle toggle;
    public bool birdsCaged; //A Booleon value to decide if you do or don't want to keep the birds within the skyRadius.    
    public int cageRadius = 15;   //Sky radius for the birds to flock in.    
    
    public GameObject birdObj;  //Prefab of the Bird
    static int birdAmount = 60; //Initial Amount of birds
    public GameObject[] allBirds; //List containing the Birds

    public bool showUI; //A Booleon value to decide if you do or don't want to show the UI Controls.
    public GameObject UIControlsObj;
    
    void Start()
    {
        SpawnBirds();

        ToggleUIControls(showUI);

        toggle = GetComponentInChildren<Toggle>();
        toggle.isOn = birdsCaged;
    }

    public void SpawnBirds()
    {
        if(allBirds.Length != 0)
        {
            KillBirds();
        }
        
        allBirds = new GameObject[birdAmount];

        for (int i = 0; i < birdAmount; i++)
        {
            Vector3 mPos = new Vector3( Random.Range(-cageRadius, cageRadius),
                                        Random.Range(-cageRadius, cageRadius),
                                        Random.Range(-cageRadius, cageRadius));
            allBirds[i] = (GameObject) Instantiate(birdObj, mPos, Quaternion.identity, this.transform);
            allBirds[i].GetComponent<FlockBehaviour>().isCaged = birdsCaged;
        }
    }

    public void KillBirds()
    {
        for (int i = 0; i < allBirds.Length; i++)
        {
            Destroy(allBirds[i]); 
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
            case 3:
            for (int i = 0; i < allBirds.Length; i++)
            {
                birdAmount = (int) _value;
                SpawnBirds();
            }
            break;
            case 4:
            for (int i = 0; i < allBirds.Length; i++)
            {
                allBirds[i].GetComponent<FlockBehaviour>().rotVel = _value;
            }
            break;
            case 5:
            for (int i = 0; i < allBirds.Length; i++)
            {
                allBirds[i].GetComponent<FlockBehaviour>().minVel = _value;
                if(allBirds[i].GetComponent<FlockBehaviour>().transVel < _value)
                {
                    allBirds[i].GetComponent<FlockBehaviour>().transVel = _value;
                }
            }
            break;
            case 6:
            for (int i = 0; i < allBirds.Length; i++)
            {
                allBirds[i].GetComponent<FlockBehaviour>().maxVel = _value;
                if(allBirds[i].GetComponent<FlockBehaviour>().transVel > _value)
                {
                    allBirds[i].GetComponent<FlockBehaviour>().transVel = _value;
                }
            }
            break;
        }  
    }

    public void ToggleUIControls(bool _activate)
    {
        UIControlsObj.SetActive(_activate);
    }

    public void ToggleCaging()
    {
        birdsCaged = toggle.isOn;
        for (int i = 0; i < birdAmount; i++)
        {
            allBirds[i].GetComponent<FlockBehaviour>().isCaged = birdsCaged;
        }
    }
}
