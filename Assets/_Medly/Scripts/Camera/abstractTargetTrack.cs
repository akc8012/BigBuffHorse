// Benjamin Gordon, 2016

/// A basic starting point for my camera scripts.
/// 
/// It features a single target transform, and several
/// ways to initialize it automatically. 
/// 
/// Tracking behavior is defined by subclasses.
/// 
/// It also contains basic methods to cease tracking
/// at runtime or switch targets.

using UnityEngine;

public abstract class abstractTargetTrack : MonoBehaviour
{
    [Header("Basic Tracking")]
    public Transform target;                                    // Reference to the player's transform. A transform is an XYZ position, scale, and rotation.
    [SerializeField] protected string playerTag = "Player";
    [SerializeField] protected bool useCursor = false;          // Toggles visiblity of cursor.
    protected bool tracking = true;                             // Toggle in case we want to set the camera position without tracking something.

    protected void Awake(){
        // Initialize reference to the player's transform, with proper error checking.
        // I'm using a tag to identify a "player" object.
        if (playerTag == "")
            playerTag = "Player";

        // Set cursor visibilty on awake.
        UseCursor = useCursor;

        if (!GameObject.FindGameObjectWithTag(playerTag))
            Debug.LogError("Hey, why is there no player in your level?");
        else if (!target)
            // Set the target automatically if we have not specified a target and there is a player object in the scene.
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    protected void LateUpdate(){
        // If the camera is supposed to track and we actually have a target, track the target.
        if (tracking && target)
            TrackTarget();

        // Allows cursor to be toggled on and off with the escape key and the mouse. Only on PC.
        //#if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.Escape))
                UseCursor = true;
            if(Input.GetMouseButtonDown(0))
                UseCursor = false;
        //#endif
    }

    protected abstract void TrackTarget();

    // Public accessor for the target, in case we want the camera to focus on something other than the player later on.
    public Transform Target { get { return target; } set { target = value; } }
   
    // Public accessor for tracking, in case we want another object to control tracking on this camera.
    public bool Tracking { set { tracking = value; } }
    
    // Public accessor for cursor visibility. Automatically toggles visibility when "set" is called.
    public bool UseCursor {
        set {
            useCursor = value;
            Cursor.visible = useCursor;

            if(!useCursor)
               Cursor.lockState = CursorLockMode.Locked;
            else Cursor.lockState = CursorLockMode.None;
        }
        get { return useCursor; }
    }
}
