using UnityEngine;
using System.Collections;

namespace ConcleteAction {

    /// <summary>
    /// 摩擦による減速
    /// </summary>
    [CreateAssetMenu(fileName = "Friction", menuName = "ScriptableObject/Action/Friction")]
    public class Friction : Action {

        public float Accel;

        public float IgnoreFrames;

        public override bool Trigger() {
            return this.Agent.GroundFrameCount > IgnoreFrames;
        }

        public override void OnTrigger() {
            Vector2 velocity = this.Agent.RigidbodyCache.velocity;

            if(velocity.x > 0) {
                this.Agent.RigidbodyCache.velocity = new Vector2(velocity.x - this.Accel, velocity.y);
                if(this.Agent.RigidbodyCache.velocity.x < 0) this.Agent.RigidbodyCache.velocity = new Vector2(0, velocity.y);
            }
            else if(velocity.x < 0) {
                this.Agent.RigidbodyCache.velocity = new Vector2(velocity.x + this.Accel, velocity.y);
                if (this.Agent.RigidbodyCache.velocity.x > 0) this.Agent.RigidbodyCache.velocity = new Vector2(0, velocity.y);
            }
        }
    }

}