using UnityEngine;
using System.Collections;

namespace ConcleteAction {

    /// <summary>
    /// ジャンプ後にジャンプキーを放すとその時点から落下を開始するやつ
    /// </summary>
    [CreateAssetMenu(fileName = "JumpRelease", menuName = "ScriptableObject/Action/JumpRelease")]
    public class JumpRelease : Action {

        private Agent.AgentDirection direction;

        public float Accel;

        public float VelocityLimit;

        protected override bool Trigger() {
            return Input.GetButtonUp("Jump") && this.Agent.JumpStatus.IsJumping;
        }

        protected override void OnTrigger() {
            this.Agent.RigidbodyCache.velocity = new Vector2(this.Agent.RigidbodyCache.velocity.x, 0);
        }
    }

}