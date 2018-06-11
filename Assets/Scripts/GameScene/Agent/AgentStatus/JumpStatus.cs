using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Agent))]
public class JumpStatus : MonoBehaviour {

    public bool IsJumping;

    public float JumpingLimitVelocity;

    private Agent agent;

    private void Awake() {
        this.agent = GetComponent<Agent>();
    }

    // Update is called once per frame
    void Update() {
        if (!this.IsJumping) return;

        //  一度ジャンプしてからy速度が一定以下になったらジャンプ状態でなくなる
        if (this.agent.RigidbodyCache.velocity.y < JumpingLimitVelocity) this.IsJumping = false;
    }

    //  ジャンプしたときに呼び出す
    public void OnJump() {
        this.IsJumping = true;
    }
}
