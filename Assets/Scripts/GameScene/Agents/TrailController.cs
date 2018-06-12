using UnityEngine;
using GameScene.Agents.AgentStatus;

namespace GameScene.Agents {
    [RequireComponent(typeof(SpriteTrailEmitter))]
    public class TrailController : MonoBehaviour{

        public DashStatus DashStatus;
        private SpriteTrailEmitter emitter;

        private void Awake() {
            this.emitter = GetComponent<SpriteTrailEmitter>();
        }

        private void Update() {
            this.emitter.enabled = DashStatus.IsDashing;
        }

    }
}
