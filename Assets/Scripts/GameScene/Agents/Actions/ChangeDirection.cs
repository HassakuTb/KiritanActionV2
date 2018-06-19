using UnityEngine;

namespace GameScene.Agents.Actions {

    /// <summary>
    /// 方向転換
    /// </summary>
    public class ChangeDirection : Action {

        private AgentDirection direction;

        protected override void OnTrigger() {
            this.Agent.SetDirection(this.direction);
        }

        protected override bool Trigger() {
            if (this.Agent.DashStatus.IsDashing) return false;
            if (Input.GetAxis("Horizontal") < -0.1) {
                this.direction = AgentDirection.Left;
                return true;
            }
            if (Input.GetAxis("Horizontal") > 0.1) {
                this.direction = AgentDirection.Right;
                return true;
            }
            return false;
        }
    }
}