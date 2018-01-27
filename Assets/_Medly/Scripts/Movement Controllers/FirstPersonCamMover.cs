// Benjamin Gordon, 2016
// 
// jhocking, 2014, at: http://gamedev.stackexchange.com/questions/82048/how-can-i-make-my-player-move-in-the-direction-the-camera-faces
//      Helped with rotating player relative to camera direction. 
//
// AlwaysSunny, 2015, at http://answers.unity3d.com/questions/1003293/how-to-set-velocity-of-rigidbody-without-changing.html
//      Helped with formula for Kinematic equations and a fix for loss of acceleration due to gravity when setting rigidbody velocity.
//
// AlwaysSunny, 2011, at http://forum.unity3d.com/threads/first-person-controller-walking-twice-as-fast-when-walking-diagonally.120296/
//      Also helped with normalizing magnitude of input vectors so that diagnonal movement was not faster than lateral movement.

/// A player controller script that moves the player relative to the rotation of the camera. Based on my third person camera controller.
/// This controller normalizes movement input, so that diagonal movement is not faster than lateral movement.

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]   // Makes sure this object has a rigidbody. If one is not found, it is automatically created. Thanks Unity!
[AddComponentMenu("_Medly/Movement/First Person Cam Mover")]

public class FirstPersonCamMover : AbstractPlayerMover {

    protected Transform cam;
    protected Rigidbody rigidBody;
    [SerializeField] protected Animator HeadAnimator;
    protected int speedParam;

    void Awake() {
        cam = Camera.main.transform;

        // An error check to make sure that the player has a rigidbody.
        // Unity has default error messages, but this one will stop Awake() from asking for a rigidbody that isn't there.
        if (gameObject.GetComponent<Rigidbody>() == null) {
            Debug.LogError("Hey, your player does not have a rigidbody!");
            return;
        }

        rigidBody = GetComponent<Rigidbody>();
        speedParam  = Animator.StringToHash("speedParam");
    }

    protected override void ProcessInput() {
        // Rotate all the time.
        if (useRotation)
            Rotate();

        if (Mathf.Abs(V) > gamepadError || Mathf.Abs(H) > gamepadError) {
            Movement();
            
            //if (Mathf.Abs(J) > gamepadError)
            //    Jump();
        }else if (HeadAnimator.GetFloat(speedParam) != 0){
            HeadAnimator.SetFloat(speedParam, 0);
        }
    }

    protected override void Movement() {

        // Get direction of movement.
        Vector3 input = new Vector3(H, 0, V);

        // Adjust the length of input vectors so that moving diagonal does not make you faster.
        if (input.sqrMagnitude > 1)
            input.Normalize();

        HeadAnimator.SetFloat(speedParam, input.magnitude);

        Vector3 movement = (input * moveSpeed);

        // Alter movement vector so that it is relative to camera rotation.
        movement = transform.TransformDirection(movement);

        // Find velocity displacement in -y to add gravity back into this weird ass setup.
        float d = rigidBody.velocity.y + Physics.gravity.y * Time.fixedDeltaTime;

        // Add the final force, depending on the orientation of the controller.
        rigidBody.velocity = new Vector3(movement.x, d, movement.z);
    }

    protected override void Rotate() {
        Quaternion tempRot = cam.rotation; // Get the rotation we want from the camera.

        tempRot.x = 0; // Make sure the player's rotation stays locked on the X-Z plane.
        tempRot.z = 0;

        rigidBody.freezeRotation = true;                    // Stop player from tumbling around.

        // Rotate the player with final rotation values.
        if (smoothRotation) {
            //Smoothly rotate our player to face the direction it moves to drastically improve aesthetics.
            rigidBody.rotation = Quaternion.Lerp(rigidBody.rotation, tempRot, rotationSmooth * Time.deltaTime);
            transform.rotation = rigidBody.rotation;
        }
        else
            rigidBody.rotation = tempRot;
    }
}
