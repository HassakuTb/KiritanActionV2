using UnityEngine;
using System.Collections;

/// <summary>
/// 方向転換
/// </summary>
[CreateAssetMenu(fileName ="ChangeDirection", menuName ="ScriptableObject/Action/ChangeDirection")]
public class ChangeDirection : Action {

    public override bool Trigger() {
        if (this.FixedInputController.InputButtonTable["Left"].PressedFrame > 0) return true;
        if (this.FixedInputController.InputButtonTable["Right"].PressedFrame > 0) return true;
        return false;        
    }

    public override void OnTrigger() {
        this.Agent.SetDirection(
            this.FixedInputController.InputButtonTable["Left"].PressedFrame > 0 ?
            Agent.AgentDirection.Left : Agent.AgentDirection.Right);
    }
}
