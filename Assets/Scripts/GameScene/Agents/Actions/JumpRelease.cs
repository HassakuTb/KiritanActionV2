using UnityEngine;

namespace GameScene.Agents.Actions {

    /// <summary>
    /// ジャンプ後にジャンプキーを放すとその時点から落下を開始するやつ
    /// </summary>
    [RequireComponent(typeof(Jump))]
    public class JumpRelease : Action {

        private Jump jump;

        private new void Awake() {
            base.Awake();
            this.jump = GetComponent<Jump>();
        }

        protected override bool Trigger() {
            return Input.GetButtonUp("Jump") && this.jump.IsJumping;
        }

        protected override void OnTrigger() {
            this.Agent.RigidbodyCache.velocity = new Vector2(this.Agent.RigidbodyCache.velocity.x, 0);
        }
    }
}