using GameScene.Agents.AgentStatus;
using UnityEngine;

namespace GameScene.Agents {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(DashStatus))]
    public class GravityControl : MonoBehaviour{

        private DashStatus dashStatus;
        private Rigidbody2D rigidbody;

        private float defaultGravityScale;

        private void Awake() {
            this.dashStatus = GetComponent<DashStatus>();
            this.rigidbody = GetComponent<Rigidbody2D>();

            this.defaultGravityScale = rigidbody.gravityScale;
        }

        private void Update() {
            this.rigidbody.gravityScale = dashStatus.IsDashing ? 0 : this.defaultGravityScale;
        }
    }
}
