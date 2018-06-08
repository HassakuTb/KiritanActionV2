using UnityEngine;
using System.Collections;

namespace ConcleteAction {

    /// <summary>
    /// 地上移動
    /// </summary>
    [CreateAssetMenu(fileName = "Walk", menuName = "ScriptableObject/Action/Walk")]
    public class Walk : Action {

        public float Accel;

        public float VelocityLimit;

        public override bool Trigger() {
            if (this.Agent.IsGround && this.FixedInputController.InputButtonTable["Left"].PressedFrame > 0) return true;
            if (this.Agent.IsGround && this.FixedInputController.InputButtonTable["Right"].PressedFrame > 0) return true;
            return false;
        }

        public override void OnTrigger() {
            Vector2 velocity = this.Agent.RigidbodyCache.velocity;
            if(this.FixedInputController.InputButtonTable["Left"].PressedFrame > 0) {
                if (velocity.x > -this.VelocityLimit) {
                    this.Agent.RigidbodyCache.velocity = new Vector2(velocity.x - this.Accel, velocity.y);
                    if(this.Agent.RigidbodyCache.velocity.x < -VelocityLimit) {
                        this.Agent.RigidbodyCache.velocity = new Vector2(-VelocityLimit, velocity.y);
                    }
                }
            }
            else {
                if (velocity.x < this.VelocityLimit) {
                    this.Agent.RigidbodyCache.velocity = new Vector2(velocity.x + this.Accel, velocity.y);
                    if (this.Agent.RigidbodyCache.velocity.x > VelocityLimit) {
                        this.Agent.RigidbodyCache.velocity = new Vector2(VelocityLimit, velocity.y);
                    }
                }
            }
        }
    }

}