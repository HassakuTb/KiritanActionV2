using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// FixedUpdateで入力を扱うためにずれを軽減する
/// execute order を通常より低くして扱う
/// </summary>
public class FixedInputController : MonoBehaviour {

    public class InputButton {
        private bool isPressedAtUpdate;

        public int PressedFrame = 0;
        public int ReleasedFrame = int.MaxValue;

        public void _OnFixedUpdate(bool throughUpdate) {
            if (throughUpdate) {
                if (isPressedAtUpdate) {
                    if (PressedFrame < int.MaxValue) ++PressedFrame;
                    ReleasedFrame = 0;
                }
                else {
                    if (ReleasedFrame < int.MaxValue) ++ReleasedFrame;
                    PressedFrame = 0;
                }
                isPressedAtUpdate = false;
            }
            else {  //  Updateを通っていない場合
                if (PressedFrame > 0 && PressedFrame < int.MaxValue) ++PressedFrame;
                if (ReleasedFrame > 0 && ReleasedFrame < int.MaxValue) ++ReleasedFrame;
            }
        }

        //  Updateのタイミングで押されていた時呼び出す
        public void _SetPressedAtUpdate(bool isPressed) {
            isPressedAtUpdate = isPressed;
        }
    }

    /// <summary>
    /// 入力状態
    /// </summary>
    public Dictionary<string, InputButton> InputButtonTable { get; private set; }

    private bool throughUpdate = false;

    private void Awake() {
        InputButtonTable = new Dictionary<string, InputButton>();

        InputButtonTable.Add("Left", new InputButton());
        InputButtonTable.Add("Right", new InputButton());
        InputButtonTable.Add("Up", new InputButton());
        InputButtonTable.Add("Down", new InputButton());
        InputButtonTable.Add("Jump", new InputButton());
        InputButtonTable.Add("Dash", new InputButton());
    }

    // 入力はUpdateで拾う
    void Update() {
        throughUpdate = true;

        if (Input.GetAxis("Horizontal") < -0.5f) {
            InputButtonTable["Left"]._SetPressedAtUpdate(true);
        }
        else if (Input.GetAxis("Horizontal") > 0.5f) {
            InputButtonTable["Right"]._SetPressedAtUpdate(true);
        }

        if (Input.GetAxis("Vertical") < -0.5f) {
            InputButtonTable["Down"]._SetPressedAtUpdate(true);
        }
        else if (Input.GetAxis("Vertical") > 0.5f) {
            InputButtonTable["Up"]._SetPressedAtUpdate(true);
        }

        if (Input.GetButton("Jump")) {
            InputButtonTable["Jump"]._SetPressedAtUpdate(true);
        }

        if (Input.GetButton("Dash")) {
            InputButtonTable["Dash"]._SetPressedAtUpdate(true);
        }
    }

    //  FixedUpdate用に変換
    private void FixedUpdate() {
        foreach(InputButton button in InputButtonTable.Values){
            button._OnFixedUpdate(throughUpdate);
        }
        throughUpdate = false;
    }
}
