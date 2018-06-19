using UnityEngine;

namespace GameScene.Agents.Actions {

    [RequireComponent(typeof(IAgent))]
    public abstract class Action : MonoBehaviour {

        private bool isTriggeredAtUpdate;

        protected IAgent Agent { get; private set; }

        protected void Awake() {
            this.Agent = GetComponent<IAgent>();
        }

        public void Update() {
            this.isTriggeredAtUpdate = this.Trigger();
        }

        public void FixedUpdate() {
            if (this.isTriggeredAtUpdate) {
                this.OnTrigger();
                this.isTriggeredAtUpdate = false;
            }
        }

        protected abstract bool Trigger();

        protected abstract void OnTrigger();
    }
}
