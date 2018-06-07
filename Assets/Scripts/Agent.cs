using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JudgeGround))]
public class Agent : MonoBehaviour {

    private JudgeGround judgeGround;

	// Use this for initialization
	void Awake () {
        judgeGround = GetComponent<JudgeGround>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("Horizontal") > 0.1) {
            if (judgeGround.IsGround) {
            }
            else {
                //  左歩行
            }
        }
        else if( Input.GetAxis("Horizontal") < -0.1) {
            //  
        }

        if (Input.GetButtonDown("Jump") && judgeGround.IsGround) {
            //  ジャンプ
        }

        if (Input.GetButtonDown("Dash") && judgeGround.IsGround) {
            //  ダッシュ
        }


        //  Animatorへの状態通知

    }
}
