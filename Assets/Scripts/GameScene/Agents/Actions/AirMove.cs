using UnityEngine;

namespace GameScene.Agents.Actions {
    /// <summary>
    /// 空中移動制御
    /// </summary>
    public class AirMove : Action {

        private Agent.AgentDirection direction;

        public float Accel;

        public float VelocityLimit;

        protected override bool Trigger() {
            if (this.Agent.IsGround) return false;
            if (this.Agent.DashStatus.IsDashing) return false;
            if (Input.GetAxis("Horizontal") < -0.1) {
                this.direction = Agent.AgentDirection.Left;
                return true;
            }
            if (Input.GetAxis("Horizontal") > 0.1) {
                this.direction = Agent.AgentDirection.Right;
                return true;
            }
            return false;
        }

        protected override void OnTrigger() {
            Vector2 velocity = this.Agent.RigidbodyCache.velocity;
            if (this.direction == Agent.AgentDirection.Left) {
                if (velocity.x > -this.VelocityLimit) {
                    this.Agent.RigidbodyCache.velocity = new Vector2(velocity.x - this.Accel, velocity.y);
                    if (this.Agent.RigidbodyCache.velocity.x < -VelocityLimit) {
                        this.Agent.RigidbodyCache.velocity = new Vector2(-VelocityLimit, velocity.y);
                    }
                }
            }
            else {
                if (velocity.x < this.VelocityLimit) {
                    this.Agent.RigidbodyCache.velocity = new Vector2(velocity.x + this.Accel, velocity.y);
                    if (this.Agent.RigidbodyCache.velocity.x > VelocityLimit) {
                        this.Agent.RigidbodyCache.velocity = new Vector2(VelocityLimit, velocity.y);
                    }
                }
            }
        }
    }
}