using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Agent))]
public class DashStatus : MonoBehaviour {

    public bool IsDashing = false;

    public int DashFrameLimit;

    public int DashingFrames;

    private Agent agent;

    private void Awake() {
        this.agent = GetComponent<Agent>();
    }

    // Update is called once per frame
    void Update() {
        if (!this.IsDashing) return;

        if (!this.agent.IsGround) {
            this.IsDashing = false;
            return;
        }

        if(this.DashingFrames > this.DashFrameLimit) {
            this.IsDashing = false;
            return;
        }

        this.DashingFrames++;
    }

    public void OnDash() {
        this.IsDashing = true;
        this.DashingFrames = 0;
    }
}
