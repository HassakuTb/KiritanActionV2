using UnityEngine;
using GameScene.Agents.AgentStatus;

namespace GameScene.Agents {
    [RequireComponent(typeof(Agent))]
    public class DashSpeedControl : MonoBehaviour {

        public float MinimumSpeed;

        private Agent agent;

        private void Awake() {
            this.agent = GetComponent<Agent>();
        }

        private void Update() {
            if (!this.agent.DashStatus.IsDashing) return;

            DashStatus dash = this.agent.DashStatus;
            float initialSpeed = dash.DashReference.Velocity;
            float expectedSpeed = initialSpeed * (1 - dash.DashingFrames / (float)dash.DashReference.DashFrameLimit);
            if (expectedSpeed < MinimumSpeed) expectedSpeed = MinimumSpeed;
            this.agent.RigidbodyCache.velocity = this.agent.RigidbodyCache.velocity.normalized * expectedSpeed;
        }
    }
}
