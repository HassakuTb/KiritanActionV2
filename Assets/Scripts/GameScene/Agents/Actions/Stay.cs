using UnityEngine;

namespace GameScene.Agents.Actions {
    /// <summary>
    /// 地上で無入力
    /// </summary>
    public class Stay : Action {

        public float Friction;

        protected override bool Trigger() {
            return this.Agent.IsGround && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1;
        }

        protected override void OnTrigger() {
            Vector2 velocity = this.Agent.RigidbodyCache.velocity;
            if (velocity.x > 0) {
                this.Agent.RigidbodyCache.velocity = new Vector2(velocity.x - Friction > 0 ? velocity.x - Friction : 0, velocity.y);
            }
            else if (velocity.x < 0) {
                this.Agent.RigidbodyCache.velocity = new Vector2(velocity.x + Friction < 0 ? velocity.x + Friction : 0, velocity.y);
            }

        }
    }
}
