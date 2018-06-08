using UnityEngine;
using System.Collections;

namespace ConcleteAction {

    /// <summary>
    /// 地上ダッシュ
    /// </summary>
    [CreateAssetMenu(fileName = "Dash", menuName = "ScriptableObject/Action/Dash")]
    public class Dash : Action {

        public float Velocity;

        public override bool Trigger() {
            return this.Agent.IsGround && this.FixedInputController.InputButtonTable["Dash"].PressedFrame > 0;
        }

        public override void OnTrigger() {
            Vector2 velocity = this.Agent.RigidbodyCache.velocity;
            if(this.Agent.Direction == Agent.AgentDirection.Left) {
                if (velocity.x > -Velocity) {
                    this.Agent.RigidbodyCache.velocity = new Vector2(-Velocity, velocity.y);
                }
            }
            else {
                if (velocity.x < Velocity) {
                    this.Agent.RigidbodyCache.velocity = new Vector2(Velocity, velocity.y);
                }
            }
        }
    }

}