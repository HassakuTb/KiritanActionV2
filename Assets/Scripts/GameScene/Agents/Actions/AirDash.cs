using UnityEngine;

namespace GameScene.Agents.Actions {
    /// <summary>
    /// 空中ダッシュ
    /// </summary>
    [CreateAssetMenu(fileName = "AirDash", menuName = "ScriptableObject/Action/AirDash")]
    class AirDash : Action {
        
        public Dash DashReference;

        private EightDirection inputDirection;

        protected override bool Trigger() {
            if (!this.Agent.IsGround && Input.GetButtonDown("Dash")) {
                this.inputDirection = EightDirectionExtensions.InputToDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                return true;
            }
            return false;
        }

        protected override void OnTrigger() {
            Vector2 directionVector = this.inputDirection.ToVector2();
            if (inputDirection == EightDirection.None) {
                directionVector = new Vector2((float)this.Agent.Direction, 0);
            }
            this.Agent.RigidbodyCache.velocity = directionVector * DashReference.Velocity;

            this.Agent.DashStatus.OnDash();
        }
    }
}
