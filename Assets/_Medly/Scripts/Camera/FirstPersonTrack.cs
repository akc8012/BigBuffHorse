using UnityEngine;

public class FirstPersonTrack : abstractTargetTrack {

    [Header("Mouse Tracking")]
    [SerializeField] protected Transform head;

    [SerializeField] protected float xSpeed = 120.0f;
    [SerializeField] protected float ySpeed = 120.0f;

    [SerializeField] protected float yMinLimit = -20f;
    [SerializeField] protected float yMaxLimit = 80f;

    protected float x = 0.0f;
    protected float y = 0.0f;

	void Start () {
	    if(head == null) {
            head = GameObject.Find("PlayerHead").transform;
            Debug.Log("You should probably assign your player a head.");
        }
        if(head != null) {
            target = head;
        }

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        transform.position = target.position;
        transform.rotation = target.rotation;
	}

    protected override void TrackTarget() {
        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y + Mathf.Abs(target.rotation.eulerAngles.x), x, 0);

        transform.rotation = rotation;
        transform.position = target.position;
    }

    public float ClampAngle(float angle, float min, float max){
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
