// Benjamin Gordon, 2016

/// Basic component for my player movement scripts.
/// 
/// Contains values for horizontal and vertical input, as well as movement speed.
/// Additionally, contains value for gamepad stick error, as not all sticks are 100% accurate when untouched.
/// This creates a very small deadzone for the left stick to prevent unintended movement.
/// 
/// Main features include Movement() and Rotate(), which are only called when input is detected.
/// They must be implemented by all subclasses. 
/// 
/// Secondary features include RetrieveInput() and ProcessInput(), which can be overridden in subclasses for special cases,
/// and also allow easier overrides of the Fixed Update loop.

using UnityEngine;

public abstract class AbstractPlayerMover : MonoBehaviour
{
    [Header("Basic Movement")]

    [SerializeField] protected float moveSpeed = 7f;          // This is the force with which to move the player, shown in the inspector so it can be adjusted at runtime.
    [SerializeField] protected float gamepadError = 0.1f;     // Not all sticks work 100% This accomodates faulty controllers.
    //[SerializeField] protected float jumpHeight = 3; 
    [SerializeField] protected float rotationSmooth = 15f;    // The smoothing value for the player's rotation.

    [Header("Basic Settings")]

    [SerializeField] protected bool useRotation = true;       // Toggles player rotation relative to input.
    [SerializeField] protected bool useHorizontal = true;     // Toggles horizontal input.
    [SerializeField] protected bool useVertical = true;       // Toggles vertical input.
    [SerializeField] protected bool smoothRotation = true;    // Toggles rotation smoothing.
    [SerializeField] protected bool swapZWithY = false;       // Allows for up and down control instead of forward and back

    protected float H, V;                                     // Values to represent input commonly used for movement.

    // protected float J;

    // TODO: Add ground detection stuff

    // Using FixedUpdate for consistently timed calls to Movement().
    protected void FixedUpdate() {
        // Called every frame to get input from keyboard or controller.
        RetrieveInput();

        // Called every frame after input is retrieved.
        ProcessInput();
    }

    // Taken out of fixedUpdate and put into method so overriding does not erase this default functionality.
    protected virtual void RetrieveInput() {
        if (useHorizontal)
            H = Input.GetAxis("Horizontal");
        else H = 0;

        if (useVertical)
            V = Input.GetAxis("Vertical");
        else V = 0;

         //J = Input.GetAxis("Jump");
    }

    // Taken out of fixedUpdate and put into method so overriding does not erase this default functionality.
    protected virtual void ProcessInput() {
        if (Mathf.Abs(V) > gamepadError || Mathf.Abs(H) > gamepadError){
            Movement();
            if(useRotation)
                Rotate();
            //if (Mathf.Abs(J) > gamepadError)
            //    Jump();
        }
    }

    // Left abstract so that child scripts create functionality.
    protected abstract void Movement();
    protected abstract void Rotate();
    //protected abstract void Jump();
}
