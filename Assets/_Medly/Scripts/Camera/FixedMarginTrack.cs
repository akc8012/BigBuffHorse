// Benjamin Gordon, 2016

using UnityEngine;

[AddComponentMenu("_Medly/Camera/Fixed Margin Track")]
public class FixedMarginTrack : FixedTrack 
{
    [Header("Margin Tracking")]
    [SerializeField] private Vector2 margin;                        // The amount of space the player will have in the center of the screen before the camera moves.
    [SerializeField] private Vector2 minXAndZ;                      // Tells the camera the lowest value it can move in.
    [SerializeField] private Vector2 maxXAndZ;                      // Tells the camera the highest value it can move in.

    protected override void TrackTarget(){
        // Set up a temporary position for the camera. This is 2 separate floats because you cannot modify a vector 3 directly. Im leaving out Y because we dont want to modify that.
        float tempX = transform.position.x;
        float tempZ = transform.position.z;

        // Check the margins
        if (OutOfXMargin()){
            // if the target's position is less than -margin...
            if (target.position.x - transform.position.x < -margin.x)
                tempX = target.position.x + margin.x;
            else if (target.position.x - transform.position.x > margin.x)
                tempX = target.position.x - margin.x;
        }

        if (OutOfZMargin()){
            // If the target's position is positive, relative to middle
            if (target.position.z > transform.position.z - offset.z + margin.y){
                //Debug.Log("target @ top");
                tempZ = (target.position.z - margin.y) + offset.z;
            }
            else{
                //Debug.Log("target @ bottom");
                tempZ = (target.position.z + margin.y) + offset.z;
            }
        }

        // TODO: Clamp the camera's position within the min and max X and Y, so the camera does not venture outside of the level

        // Set the camera's new position, only using our smooth value based on our toggle.
        if (smoothTracking)
            transform.position = Vector3.Lerp(transform.position, new Vector3(tempX, target.position.y + offset.y, tempZ), SmoothValue * Time.deltaTime);
        else
            transform.position = new Vector3(tempX, target.position.y + offset.y, tempZ);
    }

    private bool OutOfXMargin(){
        // If the target's X position is outside of margins, track it.
        if (Mathf.Abs(transform.position.x - target.position.x) > margin.x){
            return true;
        }
        return false;
    }

    private bool OutOfZMargin(){
        // If the target's Z position is outside of margins, track it.
        // In 3D, Z is forward and back, Y is up and down. Its a bit confusing because vector 2 variables are X and Y, but Im using Z instead of Y.
        // Transform.pos.z - Zoffset gets us back to the middle (a relative 0), and then we subtract the target's position to see if it is outside of the margin.
        if (Mathf.Abs(transform.position.z - offset.z - target.position.z) > margin.y){
            return true;
        }
        return false;
    }
}
