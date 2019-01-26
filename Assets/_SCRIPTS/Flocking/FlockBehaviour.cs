using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockBehaviour : MonoBehaviour
{
    private FlockManager fManager;
    public float transVel; //Velocity of the Translation
    private float rotVel; //Velocity of the Rotation
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
        transVel = Random.Range(1.5f, 5.5f);
        rotVel = .25f;
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
                mDist = Vector3.Distance(this.transform.position, mBird.transform.position);
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
                mDist = Vector3.Distance(this.transform.position, mBird.transform.position);
                if(mDist <= cohesionDist)
                {
                    isFollowing = true;
                    FlockBehaviour mOtherFlockBehaviour = mBird.GetComponent<FlockBehaviour>();

                    Vector3 mDirection = mBird.transform.position - this.transform.position;
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
                    Debug.DrawLine(this.transform.position, mBird.transform.position, Color.red);
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
                mDist = Vector3.Distance(this.transform.position, mBird.transform.position);
                if(mDist <= separationDist)
                {
                    isAvoiding = true;
                    FlockBehaviour mOtherFlockBehaviour = mBird.GetComponent<FlockBehaviour>();

                    Vector3 mDirection = this.transform.position - mBird.transform.position;
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
        
        if(this.transform.position.x >= mSkyRadius)
        {
            outofBounds = true;
            mDirection = new Vector3(0, this.transform.position.y, this.transform.position.z) - this.transform.position;
        }
        else if(this.transform.position.x <= -mSkyRadius)
        {
            outofBounds = true;
            mDirection = new Vector3(0, this.transform.position.y, this.transform.position.z) - this.transform.position;
        }
        else if(this.transform.position.y >= mSkyRadius)
        {
            outofBounds = true;
            mDirection = new Vector3(this.transform.position.x, 0, this.transform.position.z) - this.transform.position;
        }
        else if(this.transform.position.y <= -mSkyRadius)
        {
            outofBounds = true;
            mDirection = new Vector3(this.transform.position.x, 0, this.transform.position.z) - this.transform.position;
        }
        else if(this.transform.position.z >= mSkyRadius)
        {
            outofBounds = true;
            mDirection = new Vector3(this.transform.position.x, this.transform.position.y, 0) - this.transform.position;
        }
        else if(this.transform.position.z <= -mSkyRadius)
        {
            outofBounds = true;
            mDirection = new Vector3(this.transform.position.x, this.transform.position.y, 0) - this.transform.position;
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
            transVel = Random.Range(1.5f, 5.5f);

            Debug.DrawLine(transform.localPosition, transform.localPosition + mDirection, Color.yellow);
            desiredDirection = mDirection.normalized; 
        }

    }

    void ApplyRules()
    {
        // GameObject[] mBirds;
        // mBirds = FlockManager.allBirds;

        // Vector3 mCenterVector = Vector3.zero;
        // Vector3 mAvoidVector = Vector3.zero;

        // int mGroupSize = 0; //Amount of birds within the sub-group within the main flock.
        // float mGroupVel = .1f; //Velocity of the sub-group.

        // Vector3 mGoalPos = FlockManager.goalPos;

        // float mDist;
        
        // foreach (GameObject mBird in mBirds)
        // {
        //     if(mBird != this.gameObject)
        //     {
        //         mDist = Vector3.Distance(this.transform.position, mBird.transform.position);
        //         if(mDist <= neighbourDist)
        //         {
        //             mCenterVector += mBird.transform.position;
        //             mGroupSize++;

        //             Debug.DrawLine(this.transform.position, mCenterVector, Color.red);

        //             if(mDist < 2.0f)
        //             {
        //                 mAvoidVector = mAvoidVector + (this.transform.position - mBird.transform.position);
        //             }

        //             FlockBehaviour mOtherFlockBehaviour = mBird.GetComponent<FlockBehaviour>();
        //             mGroupVel = mGroupVel + mOtherFlockBehaviour.transVel;
        //         }
        //     }
        // }

        // if(mGroupSize > 0)
        // {
        //     mCenterVector = mCenterVector/mGroupSize + (mGoalPos - this.transform.position);
        //     transVel = mGroupVel/mGroupSize;

        //     Vector3 mDirection = (mCenterVector + mAvoidVector) - this.transform.position;
        //     if(mDirection != Vector3.zero)
        //     {
        //         this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
        //                                                    Quaternion.LookRotation(mDirection),
        //                                                    rotVel * Time.deltaTime);
        //     } 
        // }

    }
}
