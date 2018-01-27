// Benjamin Gordon, 2016
// Veli V, 2015, MouseOrbitImproved.cs http://wiki.unity3d.com/index.php?title=MouseOrbitImproved

/// This script allows a mouse to control a camera. The camera will
/// orbit its target when the mouse is moved. It also features zooming
/// with the scroll wheel and linecasting to avoid camera clipping.

using UnityEngine;

[AddComponentMenu("_Medly/Camera/Mouse Track")]

public class MouseTrack : abstractTargetTrack
{
    [Header("Mouse Tracking")]

    [SerializeField] protected float distance = 5.0f;
    [SerializeField] protected float scrollSpeed = 5.0f;
    [SerializeField] protected float xSpeed = 120.0f;
    [SerializeField] protected float ySpeed = 120.0f;

    [SerializeField] protected float yMinLimit = -20f;
    [SerializeField] protected float yMaxLimit = 80f;

    [SerializeField] protected float distanceMin = .5f;
    [SerializeField] protected float distanceMax = 15f;

    protected float x = 0.0f;
    protected float y = 0.0f;

    //private float oldDistance;
    //private bool obstructed = false;

    protected void Start(){
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        //oldDistance = distance;
    }

    protected override void TrackTarget(){
        x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        //if(Input.GetAxis("Mouse ScrollWheel") != 0) {
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * scrollSpeed, distanceMin, distanceMax);
            //oldDistance = distance;
        //}

        // TODO: Make camera return to previous position once obstruction is gone.
        RaycastHit hit;
        if (Physics.Linecast(target.position, transform.position, out hit)){
            //obstructed = true;

            //oldDistance = distance;

            distance -= hit.distance;
        }
        // TODO: Make a ray behind camera that checks for things. If none, revert cam. Maybe watch that old tutorial?
        //else if (Physics.Raycast(obstructed) {
        //    distance = oldDistance;
        //    obstructed = false;
        //}
        //else distance = oldDistance;

        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }

    public float ClampAngle(float angle, float min, float max){
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
