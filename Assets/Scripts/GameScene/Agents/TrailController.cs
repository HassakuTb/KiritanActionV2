using UnityEngine;
using GameScene.Agents.Actions;

namespace GameScene.Agents {
    [RequireComponent(typeof(SpriteTrailEmitter))]
    public class TrailController : MonoBehaviour{

        public Dash Dash;
        private SpriteTrailEmitter emitter;

        private void Awake() {
            this.emitter = GetComponent<SpriteTrailEmitter>();
        }

        private void Update() {
            this.emitter.enabled = Dash.IsDashing;
        }

    }
}
