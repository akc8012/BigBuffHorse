// Benjamin Gordon, 2016

/// This script attaches to a camera and causes it
/// to stay a fixed distance away from a target. 
/// This distance is called the offset.

using UnityEngine;

[AddComponentMenu("_Medly/Camera/Fixed Track")]
public class FixedTrack : abstractTargetTrack {

    [Header("Fixed Tracking")]
    [SerializeField] protected Vector3 offset = new Vector3(0, 3, -5);
	[SerializeField] protected bool useSceneOffset;

    [SerializeField] protected bool smoothTracking = false;           // Toggle whether we are smoothing tracking.
    [SerializeField] protected float SmoothValue;                     // A scale amount of lag to give the camera so that it appears smooth when it moves.

    protected void Start(){
		// Make sure we always start in the same position relative to the target
		if (target)
		{
			if (useSceneOffset)
				offset = transform.position - target.position;
			else
				transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);			
		}
    }

    protected override void TrackTarget() {
        if (smoothTracking)
            transform.position = Vector3.Lerp(transform.position, target.position + offset, SmoothValue * Time.deltaTime);
        else
            transform.position = target.position + offset;
    }
}
