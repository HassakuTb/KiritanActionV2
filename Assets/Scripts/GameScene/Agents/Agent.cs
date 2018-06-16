using UnityEngine;
using GameScene.Agents.Actions;

namespace GameScene.Agents {

    [RequireComponent(typeof(JudgeGround))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Agent : MonoBehaviour {

        /// <summary>
        /// キャラの方向
        /// </summary>
        public enum AgentDirection {
            Left = -1,
            Right = 1,
        }

        public AgentDirection Direction = AgentDirection.Right;

        public Rigidbody2D RigidbodyCache { get; private set; }

        public int GroundFrameCount = 0;

        private JudgeGround judgeGround;

        public Jump JumpStatus { get; private set; }
        public Dash DashStatus { get; private set; }
        public Animator Animator { get; private set; }

        // Use this for initialization
        void Awake() {
            this.judgeGround = GetComponent<JudgeGround>();
            this.RigidbodyCache = GetComponent<Rigidbody2D>();
            this.JumpStatus = GetComponentInChildren<Jump>();
            this.DashStatus = GetComponentInChildren<Dash>();
            this.Animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update() {

            //  Animatorへの状態通知
            this.Animator.SetBool("IsDashing", this.DashStatus.IsDashing);
            this.Animator.SetBool("IsGround", this.IsGround);
            this.Animator.SetFloat("VelocityY", this.RigidbodyCache.velocity.y);
            this.Animator.SetFloat("HorizontalInputAbs", Mathf.Abs(Input.GetAxis("Horizontal")));
            this.Animator.SetInteger("DashingDirection", (int)this.DashStatus.DashingDirection);
        }

        private void FixedUpdate() {
            //  接地フレーム数を計算
            if (this.judgeGround.IsGround) this.GroundFrameCount++;
            else this.GroundFrameCount = 0;
        }

        public bool IsGround {
            get { return this.judgeGround.IsGround; }
        }

        public void SetDirection(AgentDirection direction) {
            if (this.Direction != direction) {
                var scale = this.transform.localScale;
                this.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
                this.Direction = direction;
            }
        }
    }

}