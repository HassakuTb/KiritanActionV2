using UnityEngine;

namespace GameScene.Agents.Actions {

    /// <summary>
    /// ジャンプ
    /// </summary>
    [RequireComponent(typeof(Dash))]
    public class Jump : Action {

        public float Speed;
        public float JumpingLimitSpeed;

        public bool IsJumping { get; private set; }

        private Dash dash;

        private new void Awake() {
            base.Awake();
            this.dash = GetComponent<Dash>();
        }

        protected override bool Trigger() {
            return this.Agent.IsGround && Input.GetButtonDown("Jump");
        }

        protected override void OnTrigger() {
            this.DoJump();
        }

        private void DoJump() {
            this.Agent.RigidbodyCache.velocity = this.Agent.RigidbodyCache.velocity + Vector2.up * Speed;

            this.IsJumping = true;
            this.dash.CancelDash();
        }

        private new void Update() {
            base.Update();

            if (this.IsJumping) {
                //  一度ジャンプしてからy速度が一定以下になったらジャンプ状態でなくなる
                if (this.Agent.RigidbodyCache.velocity.y < this.JumpingLimitSpeed) this.IsJumping = false;
            }

        }
    }
}