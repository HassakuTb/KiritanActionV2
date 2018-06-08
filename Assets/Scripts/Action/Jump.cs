using UnityEngine;
using System.Collections;

namespace ConcleteAction {

    /// <summary>
    /// ジャンプ
    /// </summary>
    [CreateAssetMenu(fileName = "Jump", menuName = "ScriptableObject/Action/Jump")]
    public class Jump : Action {

        public float Speed;

        public override bool Trigger() {
            return this.Agent.IsGround && this.FixedInputController.InputButtonTable["Jump"].PressedFrame == 1;
        }

        public override void OnTrigger() {
            this.Agent.RigidbodyCache.velocity = this.Agent.RigidbodyCache.velocity + Vector2.up * Speed;
        }
    }

}