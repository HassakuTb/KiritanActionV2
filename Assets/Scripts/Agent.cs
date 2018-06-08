﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JudgeGround))]
public class Agent : MonoBehaviour {

    /// <summary>
    /// キャラの方向
    /// </summary>
    public enum AgentDirection {
        Left = -1,
        Right = 1,
    }

    public AgentDirection Direction = AgentDirection.Right;

    public Action[] Actions;

    public FixedInputController InputController;

    private JudgeGround judgeGround;

    // Use this for initialization
    void Awake() {
        judgeGround = GetComponent<JudgeGround>();

        foreach(Action action in this.Actions){
            action.Init(this, InputController);
        }
    }

    // Update is called once per frame
    void Update() {

        //  Animatorへの状態通知

    }

    private void FixedUpdate() {
        //  アクション処理
        foreach(Action action in this.Actions) {
            if (action.Trigger()) action.OnTrigger();
        }
    }

    public bool IsGround {
        get { return this.judgeGround.IsGround; }
    }

    public void SetDirection(AgentDirection direction) {
        if(this.Direction != direction) {
            var scale = this.transform.localScale;
            this.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            this.Direction = direction;
        }
    }
}
