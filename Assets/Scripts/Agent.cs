using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JudgeGround))]
[RequireComponent(typeof(Rigidbody2D))]
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

    public Action[] Actions;

    public int GroundFrameCount = 0;

    private JudgeGround judgeGround;

    // Use this for initialization
    void Awake() {
        this.judgeGround = GetComponent<JudgeGround>();
        this.RigidbodyCache = GetComponent<Rigidbody2D>();

        foreach(Action action in this.Actions){
            action.Init(this);
        }
    }

    // Update is called once per frame
    void Update() {

        //  アクション処理
        foreach (Action action in this.Actions) {
            action.OnUpdate();
        }

        //  Animatorへの状態通知

    }

    private void FixedUpdate() {
        //  接地フレーム数を計算
        if (this.judgeGround.IsGround) this.GroundFrameCount++;
        else this.GroundFrameCount = 0;

        //  アクション処理
        foreach(Action action in this.Actions) {
            action.OnFixedUpdate();
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
