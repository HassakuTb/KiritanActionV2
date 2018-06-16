using UnityEngine;

namespace GameScene.Agents.Actions {

    /// <summary>
    /// ジャンプ
    /// </summary>
    public class Jump : Action {

        public float Speed;
        public float JumpingLimitSpeed;

        public bool IsJumping { get; private set; }

        protected override bool Trigger() {
            return this.Agent.IsGround && Input.GetButtonDown("Jump");
        }

        protected override void OnTrigger() {
            this.Agent.RigidbodyCache.velocity = this.Agent.RigidbodyCache.velocity + Vector2.up * Speed;

            this.DoJump();
        }

        private void DoJump() {
            this.IsJumping = true;
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