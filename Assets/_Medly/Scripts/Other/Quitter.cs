// Benjamin Gordon, 2016
// Is it even worth putting my name on this??

using UnityEngine;

public class Quitter : MonoBehaviour {

    // Lets us quit the game when this is on an object, without worrying about writing this stupidly simple script.
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
	}
}
