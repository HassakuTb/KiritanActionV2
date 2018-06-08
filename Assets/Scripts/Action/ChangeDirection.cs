using UnityEngine;
using System.Collections;

namespace ConcleteAction {

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