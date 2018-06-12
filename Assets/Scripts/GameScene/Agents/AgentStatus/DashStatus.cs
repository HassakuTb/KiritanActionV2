using UnityEngine;
using GameScene.Agents.Actions;

namespace GameScene.Agents.AgentStatus {

    [RequireComponent(typeof(Agent))]
    public class DashStatus : MonoBehaviour {

        public Dash DashReference;

        public bool IsDashing = false;
        public bool IsAirial = false;
        public EightDirection Direction = EightDirection.None;

        public int DashingFrames;

        private Agent agent;

        private void Awake() {
            this.agent = GetComponent<Agent>();
        }

        // Update is called once per frame
        void Update() {
            if (!this.IsDashing) return;

            if (this.DashingFrames > this.DashReference.DashFrameLimit) {
                this.IsDashing = false;
                return;
            }

            this.DashingFrames++;
        }

        public void OnDash() {
            this.IsDashing = true;
            this.DashingFrames = 0;
            this.IsAirial = false;
        }

        public void OnAirDash(EightDirection direction) {
            this.IsDashing = true;
            this.DashingFrames = 0;
            this.IsAirial = true;
            this.Direction = direction;
        }
    }
}
