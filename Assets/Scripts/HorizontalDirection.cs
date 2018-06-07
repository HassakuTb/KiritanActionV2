using UnityEngine;

public class HorizontalDirection : MonoBehaviour {

    //  1で前向き -1で後ろ向き
    public bool isFront = true;

    // Update is called once per frame
    void FixedUpdate() {

        bool previousDirection = isFront;

        if (Input.GetAxis("Horizontal") > 0.1) {
            isFront = true;
        }
        else if (Input.GetAxis("Horizontal") < -0.1) {
            isFront = false;
        }

        if(isFront != previousDirection) {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
