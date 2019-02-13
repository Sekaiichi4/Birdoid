using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockBehaviour : MonoBehaviour
{
    private FlockManager fManager;
    public float transVel, minVel, maxVel; //Velocity of the Translation
    public float rotVel; //Velocity of the Rotation
    Vector3 currentDirection;
    Vector3 desiredDirection; 
    public float alignmentDist; //Distance to decide if to copy the neighbour's direction.
    public float cohesionDist; //Distance to decide if to close by on the neighbour;
    public float separationDist; //Distance to decide if to back off from the neighbour;
    public bool outofBounds;
    [SerializeField]
    private bool isFollowing, isAvoiding;
    public bool isCaged;

    void Start()
    {
        minVel = 1.5f;
        maxVel = 6f;
        transVel = Random.Range(minVel, maxVel);
        rotVel = 1f;
        alignmentDist = 4.0f;
        cohesionDist = 6.0f;
        separationDist = 3.0f;
        outofBounds = false;
        isFollowing = false;
        isAvoiding = false;
        fManager = gameObject.GetComponentInParent<FlockManager>();
    }

    void Update()
    {
        if(isCaged)
        {
            StayInBounds();
        }
 
        if(!isAvoiding && !outofBounds)
        {
            Align();
            Cohese();
        } 
        Separate();
        
        this.transform.Translate(.0f, .0f, Time.deltaTime * transVel);
        Debug.DrawLine(transform.localPosition, transform.localPosition + desiredDirection, Color.cyan); 
    }

    void Align()
    {
        GameObject[] mBirds;
        mBirds = fManager.allBirds;

        float mDist;

        foreach (GameObject mBird in mBirds)
        {
            if(mBird != this.gameObject)
            {
                mDist = Vector3.Distance(this.transform.localPosition, mBird.transform.localPosition);
                float alignPower = mDist;

                if(mDist <= alignmentDist)
                {
                    FlockBehaviour mOtherFlockBehaviour = mBird.GetComponent<FlockBehaviour>();

                    this.transform.rotation = Quaternion.Slerp( this.transform.rotation, 
                                                        mBird.transform.rotation,
                                                        rotVel * alignPower * Time.deltaTime);

                    desiredDirection = mBird.transform.TransformVector(.0f, .0f, Time.deltaTime * transVel).normalized;
                }
            }
        }

        currentDirection = this.transform.TransformVector(.0f, .0f, Time.deltaTime * transVel).normalized * alignmentDist; 
        Debug.DrawLine(transform.localPosition, transform.localPosition + currentDirection, Color.magenta); 
    }

    void Cohese()
    {
        GameObject[] mBirds;
        mBirds = fManager.allBirds;

        float mDist;

        foreach (GameObject mBird in mBirds)
        {
            if(mBird != this.gameObject)
            {
                mDist = Vector3.Distance(this.transform.localPosition, mBird.transform.localPosition);
                if(mDist <= cohesionDist)
                {
                    isFollowing = true;
                    FlockBehaviour mOtherFlockBehaviour = mBird.GetComponent<FlockBehaviour>();

                    Vector3 mDirection = mBird.transform.localPosition - this.transform.localPosition;
                    float attractPower = mDist;

                    if(this.transVel < mOtherFlockBehaviour.transVel)
                    {
                        this.transVel = Mathf.Lerp( this.transVel, 
                                                mOtherFlockBehaviour.transVel,
                                                Time.deltaTime);

                        if(mDirection != Vector3.zero)
                        {
                            this.transform.rotation = Quaternion.Slerp( this.transform.rotation, 
                                                                        Quaternion.LookRotation(mDirection),
                                                                        rotVel * attractPower *  Time.deltaTime);
                        }
                    }

                    desiredDirection = (desiredDirection + mDirection).normalized;
                    Debug.DrawLine(this.transform.localPosition, mBird.transform.localPosition, Color.red);
                }
                else
                {
                    isFollowing = false;
                }
            }
        }
    }

    void Separate()
    {
        GameObject[] mBirds;
        mBirds = fManager.allBirds;

        float mDist;

        foreach (GameObject mBird in mBirds)
        {
            if(mBird != this.gameObject)
            {
                mDist = Vector3.Distance(this.transform.localPosition, mBird.transform.localPosition);
                if(mDist <= separationDist)
                {
                    isAvoiding = true;
                    FlockBehaviour mOtherFlockBehaviour = mBird.GetComponent<FlockBehaviour>();

                    Vector3 mDirection = this.transform.localPosition - mBird.transform.localPosition;
                    float avoidPower = 10/mDist;

                    if(mDirection != Vector3.zero)
                    {
                        this.transform.rotation = Quaternion.Slerp( this.transform.rotation, 
                                                                    Quaternion.LookRotation(mDirection),
                                                                    rotVel * avoidPower * Time.deltaTime);
                    }
                    

                    desiredDirection = (desiredDirection + mDirection).normalized;
                    Debug.DrawLine(transform.localPosition, transform.localPosition + mDirection, Color.white); 
                }
                else
                {
                    isAvoiding = false;
                }
            }
        }
    }

    void StayInBounds()
    {
        int mSkyRadius = fManager.cageRadius;
        Vector3 mDirection = Vector3.zero;
        
        if(this.transform.localPosition.x >= mSkyRadius)
        {
            outofBounds = true;
            mDirection = new Vector3(0, this.transform.localPosition.y, this.transform.localPosition.z) - this.transform.localPosition;
        }
        else if(this.transform.localPosition.x <= -mSkyRadius)
        {
            outofBounds = true;
            mDirection = new Vector3(0, this.transform.localPosition.y, this.transform.localPosition.z) - this.transform.localPosition;
        }
        else if(this.transform.localPosition.y >= mSkyRadius)
        {
            outofBounds = true;
            mDirection = new Vector3(this.transform.localPosition.x, 0, this.transform.localPosition.z) - this.transform.localPosition;
        }
        else if(this.transform.localPosition.y <= -mSkyRadius)
        {
            outofBounds = true;
            mDirection = new Vector3(this.transform.localPosition.x, 0, this.transform.localPosition.z) - this.transform.localPosition;
        }
        else if(this.transform.localPosition.z >= mSkyRadius)
        {
            outofBounds = true;
            mDirection = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0) - this.transform.localPosition;
        }
        else if(this.transform.localPosition.z <= -mSkyRadius)
        {
            outofBounds = true;
            mDirection = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0) - this.transform.localPosition;
        }
        else
        {
            outofBounds = false;
        }

        if(outofBounds && (mDirection != Vector3.zero))
        {
            this.transform.rotation = Quaternion.Slerp( this.transform.rotation, 
                                                        Quaternion.LookRotation(mDirection),
                                                        rotVel * 2 * Time.deltaTime);
            transVel = Random.Range(minVel, maxVel);

            Debug.DrawLine(transform.localPosition, transform.localPosition + mDirection, Color.yellow);
            desiredDirection = mDirection.normalized; 
        }

    }
}
