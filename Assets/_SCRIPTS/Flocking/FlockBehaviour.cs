using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockBehaviour : MonoBehaviour
{
    public float transVel; //Velocity of the Translation
    private float rotVel; //Velocity of the Rotation
    Vector3 desiredDirection; 
    Vector3 currentDirection;
    public float neighbourDist; //Distance to decide if your neighbour enters the sub-group;
    public bool outofBounds;

    void Start()
    {
        transVel = Random.Range(2.5f, 6.5f);
        rotVel = 1.5f;
        neighbourDist = 4.0f;
        outofBounds = false;
    }

    void Update()
    {
        StayInBound();
        if(!outofBounds)
        {
            Align();
        } 
        
        this.transform.Translate(.0f, .0f, Time.deltaTime * transVel);
    }

    void Align()
    {
        GameObject[] mBirds;
        mBirds = FlockManager.allBirds;

        float mDist;

        foreach (GameObject mBird in mBirds)
        {
            if(mBird != this.gameObject)
            {
                mDist = Vector3.Distance(this.transform.position, mBird.transform.position);
                if(mDist <= neighbourDist)
                {
                    FlockBehaviour mOtherFlockBehaviour = mBird.GetComponent<FlockBehaviour>();

                    this.transform.rotation = Quaternion.Slerp( this.transform.rotation, 
                                                        mBird.transform.rotation,
                                                        rotVel * Time.deltaTime);
                }
            }
        }
        
        currentDirection = this.transform.TransformVector(.0f, .0f, Time.deltaTime * transVel).normalized * neighbourDist; 
        Debug.DrawLine(transform.localPosition, transform.localPosition + currentDirection, Color.magenta); 
    }

    void Cohese()
    {

    }

    void Separate()
    {

    }

    void StayInBound()
    {
        int mSkyRadius = FlockManager.skyRadius;
        Vector3 mDirection = Vector3.zero;

        if(Vector3.Distance(this.transform.position, Vector3.zero) >= FlockManager.skyRadius)
        {
            outofBounds = true;
            if(this.transform.position.x >= mSkyRadius)
            {
                mDirection = new Vector3(0, this.transform.position.y, this.transform.position.z) - this.transform.position;
            }
            else if(this.transform.position.x <= -mSkyRadius)
            {
                mDirection = new Vector3(0, this.transform.position.y, this.transform.position.z) - this.transform.position;
            }
            if(this.transform.position.y >= mSkyRadius)
            {
                mDirection = new Vector3(this.transform.position.x, 0, this.transform.position.z) - this.transform.position;
            }
            else if(this.transform.position.y <= -mSkyRadius)
            {
                mDirection = new Vector3(this.transform.position.x, 0, this.transform.position.z) - this.transform.position;
            }
            if(this.transform.position.z >= mSkyRadius)
            {
                mDirection = new Vector3(this.transform.position.x, this.transform.position.y, 0) - this.transform.position;
            }
            else if(this.transform.position.z <= -mSkyRadius)
            {
                mDirection = new Vector3(this.transform.position.x, this.transform.position.y, 0) - this.transform.position;
            }
        }
        else 
        {
            outofBounds = false;
        }

        if(outofBounds && (mDirection != Vector3.zero))
        {
            this.transform.rotation = Quaternion.Slerp( this.transform.rotation, 
                                                        Quaternion.LookRotation(mDirection),
                                                        rotVel * Time.deltaTime);
            transVel = Random.Range(2.5f, 6.5f);

            Debug.DrawLine(transform.localPosition, transform.localPosition + mDirection, Color.yellow); 
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
