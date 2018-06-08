using UnityEngine;
using System.Collections;

public abstract class Action : ScriptableObject {

    protected Agent Agent { get; private set; }

    protected FixedInputController FixedInputController { get; private set; }

    public virtual void Init(Agent agent, FixedInputController inputController) {
        this.Agent = agent;
        this.FixedInputController = inputController;
    }

    public abstract bool Trigger();

    public abstract void OnTrigger();
}
