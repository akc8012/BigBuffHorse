// Benjamin Gordon, 2016

/// This is a basic mover, but does not use physics
/// As such, it will not collide with other objects.

using UnityEngine;

[AddComponentMenu("_Medly/Movement/Basic Mover")]

public class BasicMover : AbstractPlayerMover 
{
    protected Transform myTransform;    // Reference to gameobject's transform

    protected void Start (){
        myTransform = GetComponent<Transform>();
	}

    protected override void Movement(){
        //Vector3 tempPos = myTransform.position;
        Vector3 tempDir = new Vector3(H, 0, V);

        if (!swapZWithY)
            myTransform.Translate(tempDir * moveSpeed, Space.World);
        else
            myTransform.Translate(new Vector3(tempDir.x, tempDir.z, 0) * moveSpeed, Space.World);
    }

    protected override void Rotate(){
        Quaternion tempRot = myTransform.rotation;
        tempRot.SetLookRotation(new Vector3(H, 0, V), Vector3.up);

        if (smoothRotation)
            // Smoothly rotate our player to face the direction it moves to drastically improve aesthetics.
            myTransform.rotation = Quaternion.Lerp(myTransform.rotation, tempRot, rotationSmooth * Time.deltaTime);
        else
            myTransform.rotation = tempRot;
    }
}
