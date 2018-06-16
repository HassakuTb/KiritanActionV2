using UnityEngine;

namespace GameScene.Agents.Actions {
    /// <summary>
    /// ダッシュ
    /// </summary>
    public class Dash : Action {

        public float Speed;
        public float MinimumSpeed;

        public int ConstantSpeedFrames = 5; //  ダッシュ開始後5Fは一定速度
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

            this.DoDash(this.inputDirection);
        }

        private void DoDash(EightDirection direction) {

            //  速度変化
            Vector2 directionVector = direction.ToVector2();
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
                    this.CancelDash();
                }
                this.dashingFrames++;

                //  減速処理
                float expectedSpeed = this.Speed;
                if(this.dashingFrames > this.ConstantSpeedFrames) {
                    expectedSpeed = this.MinimumSpeed + (this.Speed - this.MinimumSpeed)  * (1 - (this.dashingFrames - this.ConstantSpeedFrames) / (float)(this.DashFrameLimit - this.ConstantSpeedFrames));
                }
                this.Agent.RigidbodyCache.velocity = this.Agent.RigidbodyCache.velocity.normalized * expectedSpeed;
            }

            //  ダッシュ中は重力の影響を受けないようにする
            this.Agent.RigidbodyCache.gravityScale = this.IsDashing ? 0 : this.defaultGravityScale;
        }

        //  ダッシュをやめる
        public void CancelDash() {
            this.IsDashing = false;
        }
    }
}