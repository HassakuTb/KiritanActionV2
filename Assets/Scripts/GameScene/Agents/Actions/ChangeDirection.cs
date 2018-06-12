using UnityEngine;

namespace GameScene.Agents.Actions {

    /// <summary>
    /// 方向転換
    /// </summary>
    [CreateAssetMenu(fileName = "ChangeDirection", menuName = "ScriptableObject/Action/ChangeDirection")]
    public class ChangeDirection : Action {

        private Agent.AgentDirection direction;

        protected override void OnTrigger() {
            this.Agent.SetDirection(this.direction);
        }

        protected override bool Trigger() {
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
    }
}