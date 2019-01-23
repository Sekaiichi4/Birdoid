using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockBehaviour : MonoBehaviour
{
    public float transVel; //Velocity of the Translation
    private float rotVel; //Velocity of the Rotation
    Vector3 avgHeading; 
    Vector3 avgPos;
    private float neighbourDist; //Distance to decide if your neighbour enters the sub-group;
    bool turning;

    void Start()
    {
        transVel = Random.Range(.5f, 1.0f);
        rotVel = 4.0f;
        neighbourDist = 3.0f;
        turning = false;
    }

    void Update()
    {
        if(Vector3.Distance(this.transform.position, Vector3.zero) >= FlockManager.skyRadius)
        {
            turning = true;
        }
        else
        {
            turning = false;
        }

        if(turning)
        {
            Vector3 mDirection = Vector3.zero - this.transform.position;
            this.transform.rotation = Quaternion.Slerp( this.transform.rotation, 
                                                        Quaternion.LookRotation(mDirection),
                                                        rotVel * Time.deltaTime);
            transVel = Random.Range(.5f, 1.0f);
        }
        else
        {
            if(Random.Range(0, 5) < 1)
            {
                ApplyRules();
            }
        }
        
        this.transform.Translate(.0f, .0f, Time.deltaTime * transVel);
    }

    void ApplyRules()
    {
        GameObject[] mBirds;
        mBirds = FlockManager.allBirds;

        Vector3 mCenterVector = Vector3.zero;
        Vector3 mAvoidVector = Vector3.zero;

        int mGroupSize = 0; //Amount of birds within the sub-group within the main flock.
        float mGroupVel = .1f; //Velocity of the sub-group.

        Vector3 mGoalPos = FlockManager.goalPos;

        float mDist;
        
        foreach (GameObject mBird in mBirds)
        {
            if(mBird != this.gameObject)
            {
                mDist = Vector3.Distance(this.transform.position, mBird.transform.position);
                if(mDist <= neighbourDist)
                {
                    mCenterVector += mBird.transform.position;
                    mGroupSize++;

                    if(mDist < 1.0f)
                    {
                        mAvoidVector = mAvoidVector + (this.transform.position - mBird.transform.position);
                    }

                    FlockBehaviour mOtherFlockBehaviour = mBird.GetComponent<FlockBehaviour>();
                    mGroupVel = mGroupVel + mOtherFlockBehaviour.transVel;
                }
            }
        }

        if(mGroupSize > 0)
        {
            mCenterVector = mCenterVector/mGroupSize + (mGoalPos - this.transform.position);
            transVel = mGroupVel/mGroupSize;

            Vector3 mDirection = (mCenterVector + mAvoidVector) - this.transform.position;
            if(mDirection != Vector3.zero)
            {
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                                                           Quaternion.LookRotation(mDirection),
                                                           rotVel * Time.deltaTime);
            } 
        }

    }
}
