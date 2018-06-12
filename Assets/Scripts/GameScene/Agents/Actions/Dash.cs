using UnityEngine;

namespace GameScene.Agents.Actions {
    /// <summary>
    /// 地上ダッシュ
    /// </summary>
    [CreateAssetMenu(fileName = "Dash", menuName = "ScriptableObject/Action/Dash")]
    public class Dash : Action {

        public float Velocity;

        public int DashFrameLimit;

        private bool isPressedHorizontal;
        Agent.AgentDirection direction;

        protected override bool Trigger() {
            if (this.Agent.IsGround && Input.GetButtonDown("Dash")) {
                if (Input.GetAxis("Horizontal") > 0.1) {
                    isPressedHorizontal = true;
                    direction = Agent.AgentDirection.Right;
                }
                else if (Input.GetAxis("Horizontal") < -0.1) {
                    isPressedHorizontal = true;
                    direction = Agent.AgentDirection.Left;
                }
                else {
                    isPressedHorizontal = false;
                }
                return true;
            }
            return false;
        }

        protected override void OnTrigger() {
            Vector2 velocity = this.Agent.RigidbodyCache.velocity;
            Agent.AgentDirection actualDirection = this.Agent.Direction;
            if (isPressedHorizontal) actualDirection = direction;


            if (actualDirection == Agent.AgentDirection.Left) {
                if (velocity.x > -Velocity) {
                    this.Agent.RigidbodyCache.velocity = new Vector2(-Velocity, velocity.y);
                }
            }
            else {
                if (velocity.x < Velocity) {
                    this.Agent.RigidbodyCache.velocity = new Vector2(Velocity, velocity.y);
                }
            }
            this.Agent.SetDirection(actualDirection);

            this.Agent.DashStatus.OnDash();
        }
    }
}