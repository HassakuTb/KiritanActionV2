using UnityEngine;
using System.Collections;

public abstract class Action : ScriptableObject {

    private bool isTriggeredAtUpdate;

    protected Agent Agent { get; private set; }

    public virtual void Init(Agent agent) {
        this.Agent = agent;
    }

    public void OnUpdate() {
        isTriggeredAtUpdate = Trigger();
    }

    public void OnFixedUpdate() {
        if(isTriggeredAtUpdate) {
            OnTrigger();
            isTriggeredAtUpdate = false;
        }
    }

    protected abstract bool Trigger();

    protected abstract void OnTrigger();
}
