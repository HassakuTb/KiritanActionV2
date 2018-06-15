using UnityEngine;

namespace GameScene.Agents.Actions {
    /// <summary>
    /// 地上ダッシュ
    /// </summary>
    public class Dash : Action {

        public float Velocity;

        public int DashFrameLimit;

        private EightDirection inputDirection;

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
            Vector2 directionVector = this.inputDirection.ToVector2();
            this.Agent.RigidbodyCache.velocity = directionVector * this.Velocity;

            this.Agent.DashStatus.OnDash(this.inputDirection);
        }
    }
}