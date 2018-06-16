using UnityEngine;

namespace GameScene.Agents.Actions {

    /// <summary>
    /// ジャンプ後にジャンプキーを放すとその時点から落下を開始するやつ
    /// </summary>
    public class JumpRelease : Action {

        public Jump JumpReference;

        protected override bool Trigger() {
            return Input.GetButtonUp("Jump") && this.JumpReference.IsJumping;
        }

        protected override void OnTrigger() {
            this.Agent.RigidbodyCache.velocity = new Vector2(this.Agent.RigidbodyCache.velocity.x, 0);
        }
    }
}