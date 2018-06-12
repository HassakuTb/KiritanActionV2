using UnityEngine;

namespace GameScene.Agents.Actions {

    /// <summary>
    /// ジャンプ
    /// </summary>
    [CreateAssetMenu(fileName = "Jump", menuName = "ScriptableObject/Action/Jump")]
    public class Jump : Action {

        public float Speed;

        protected override bool Trigger() {
            return this.Agent.IsGround && Input.GetButtonDown("Jump");
        }

        protected override void OnTrigger() {
            this.Agent.RigidbodyCache.velocity = this.Agent.RigidbodyCache.velocity + Vector2.up * Speed;

            this.Agent.JumpStatus.OnJump();
        }
    }
}