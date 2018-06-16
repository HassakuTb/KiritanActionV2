using UnityEngine;

namespace GameScene.Agents.Actions {
    /// <summary>
    /// 地上ダッシュ
    /// </summary>
    public class Dash : Action {

        public float Speed;
        public float MinimumSpeed;

        public int DashFrameLimit;

        private EightDirection inputDirection;
        private int dashingFrames;
        private float defaultGravityScale;

        public bool IsDashing { get; private set; }
        public EightDirection DashingDirection { get; private set; }

        private void Start() {
            this.defaultGravityScale = this.Agent.RigidbodyCache.gravityScale;
        }


        protected override bool Trigger() {
            if (Input.GetButtonDown("Dash")) {
                this.inputDirection = EightDirectionExtensions.InputToDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                return true;
            }
            return false;
        }

        protected override void OnTrigger() {
            if (this.inputDirection == EightDirection.None) {
                this.inputDirection = this.Agent.Direction == Agent.AgentDirection.Right ? EightDirection.Right : EightDirection.Left;
            }
            //  地上で下方向ダッシュはできなくする
            if(this.inputDirection == EightDirection.Down || this.inputDirection == EightDirection.DownLeft || this.inputDirection == EightDirection.DownRight) {
                if (this.Agent.IsGround) return;
            }

            //  速度変化
            Vector2 directionVector = this.inputDirection.ToVector2();
            this.Agent.RigidbodyCache.velocity = directionVector * this.Speed;

            //  ダッシュの開始
            this.IsDashing = true;
            this.dashingFrames = 0;
            this.DashingDirection = inputDirection;
        }

        private new void Update() {
            base.Update();

            if (this.IsDashing) {
                //  ダッシュ時間が終わるとダッシュ状態をやめる
                if (this.dashingFrames > this.DashFrameLimit) {
                    this.IsDashing = false;
                }
                this.dashingFrames++;

                //  減速処理
                float expectedSpeed = this.Speed * (1 - this.dashingFrames / (float)this.DashFrameLimit);
                if (expectedSpeed < MinimumSpeed) expectedSpeed = MinimumSpeed; //  TODO:   MinimumSpeedでダッシュ状態が続かないように変更する
                this.Agent.RigidbodyCache.velocity = this.Agent.RigidbodyCache.velocity.normalized * expectedSpeed;
            }

            //  ダッシュ中は重力の影響を受けないようにする
            this.Agent.RigidbodyCache.gravityScale = this.IsDashing ? 0 : this.defaultGravityScale;
        }
    }
}