using UnityEngine;

namespace GameScene.Agents.Actions {

    /// <summary>
    /// 空中ダッシュキャンセル
    /// </summary>
    [RequireComponent(typeof(Dash))]
    public class DashCancel : Action {

        private Dash dash;

        private new void Awake() {
            base.Awake();
            this.dash = GetComponent<Dash>();
        }

        protected override bool Trigger() {
            return !this.Agent.IsGround && this.dash.IsDashing && Input.GetButtonDown("Jump");
        }

        protected override void OnTrigger() {
            this.dash.CancelDash();
        }
    }
}