// Benjamin Gordon, 2016

/// A basic 2-Axis controller that uses physics for locomotion.
/// 
/// The object this is attached to will move with the default 'Horizontal' and 'Vertical'
/// input axis. The object will also rotate to face the direction it is moving.
/// 
/// Note: Because this script uses physics to move, it forces the object's rigidbody rotation to be frozen.
/// 
/// This script also adds toggled features for specialized controllers, such as the ability to move up and down along Y
/// instead of forward and back along Z.

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]   // Makes sure this object has a rigidbody. If one is not found, it is automatically created. Thanks Unity!
[AddComponentMenu("_Medly/Movement/Basic Physics Mover")]

public class BasicPhysicsMover : AbstractPlayerMover{

    [Header("Physics Settings")]
    [SerializeField] protected bool tumble = false;             // Weird thing that you can do, good for ragdolls

    protected Rigidbody rigidBody;                              // Reference to rigidbody, which is used for physics. 

    protected void Awake(){
        // An error check to make sure that the player has a rigidbody. Unity has default error messages, but I like to put in my own.
        if (gameObject.GetComponent<Rigidbody>() == null){
            Debug.LogError("Hey, your player does not have a rigidbody!");
            return;
        }

        // Initilize rigidbody reference with GetComponent. 'gameobject' is similar to 'this', but it refers to the object that this script is attached to.
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    protected override void Movement(){

        // Get direction of movement.
        Vector3 input = new Vector3(H, 0, V);

        // Adjust the length of input vectors so that moving diagonal does not make you faster.
        if (input.sqrMagnitude > 1)
            input.Normalize();

        Vector3 tempVel = input * moveSpeed;

        // Find velocity displacement in -y to add gravity back into this weird ass setup.
        float d = rigidBody.velocity.y + Physics.gravity.y * Time.fixedDeltaTime;

        if (!swapZWithY)
            rigidBody.velocity = new Vector3(tempVel.x, d, tempVel.z);
        else
            rigidBody.velocity = new Vector3(tempVel.x, tempVel.z, d);
    }

    protected override void Rotate(){
        Quaternion tempRot = rigidBody.rotation;

        tempRot.SetLookRotation(new Vector3(H, 0, V), Vector3.up);

        rigidBody.freezeRotation = !tumble;     // Stop player from tumbling, if you want to.

        if (!tumble)
            rigidBody.freezeRotation = true;
        else {
            // Freeze Rigidbody rotation so it does not fly around oddly. Unless you wanted it to... Looks kinda neat.
            if (Mathf.Abs(H) > gamepadError || Mathf.Abs(V) > gamepadError)
                rigidBody.freezeRotation = true;
            else
                rigidBody.freezeRotation = false;
        }

        if (smoothRotation)
            //Smoothly rotate our player to face the direction it moves to drastically improve aesthetics.
            rigidBody.rotation = Quaternion.Lerp(rigidBody.rotation, tempRot, rotationSmooth * Time.deltaTime);
        else
            rigidBody.rotation = tempRot;
    }
}
