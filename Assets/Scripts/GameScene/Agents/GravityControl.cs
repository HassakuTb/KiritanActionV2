using GameScene.Agents.AgentStatus;
using UnityEngine;

namespace GameScene.Agents {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(DashStatus))]
    public class GravityControl : MonoBehaviour{

        private DashStatus dashStatus;
        private Rigidbody2D rigidbody;

        private void Awake() {
            this.dashStatus = GetComponent<DashStatus>();
            this.rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            this.rigidbody.bodyType = dashStatus.IsDashing ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
        }
    }
}
