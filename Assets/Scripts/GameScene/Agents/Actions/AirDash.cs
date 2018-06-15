using UnityEngine;

namespace GameScene.Agents.Actions {
    /// <summary>
    /// 空中ダッシュ
    /// </summary>
    public class AirDash : Action {
        
        public Dash DashReference;

        private EightDirection inputDirection;

        protected override bool Trigger() {
            if (!this.Agent.IsGround && Input.GetButtonDown("Dash")) {
                this.inputDirection = EightDirectionExtensions.InputToDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                return true;
            }
            return false;
        }

        protected override void OnTrigger() {
            if (this.inputDirection == EightDirection.None) {
                this.inputDirection = this.Agent.Direction == Agent.AgentDirection.Right ? EightDirection.Right : EightDirection.Left;
            }
            Vector2 directionVector = this.inputDirection.ToVector2();
            this.Agent.RigidbodyCache.velocity = directionVector * DashReference.Velocity;

            this.Agent.DashStatus.OnAirDash(this.inputDirection);
        }
    }
}
