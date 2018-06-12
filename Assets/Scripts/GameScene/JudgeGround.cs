/**
JudgeGround.cs
Copyright (c) 2016 Hassaku
This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/
using UnityEngine;

namespace GameScene {

    public class JudgeGround : MonoBehaviour {

        //  BoxCastの発生元を少し上にずらす距離
        private const float Epsilon = 0.005f;

        /// <summary>
        /// 接地判定に利用するBoxCollider
        /// </summary>
        public BoxCollider2D Collider;

        /// <summary>
        /// Colliderと地面の距離がこの値より小さければ接地と判定する
        /// </summary>
        public float FloatingDistance = 0.01f;

        /// <summary>
        /// 地形のレイヤーマスク
        /// </summary>
        public LayerMask GroundLayerMask;

        /// <summary>
        /// 接地しているかどうかを取得する
        /// </summary>
        public bool IsGround;

        /// <summary>
        /// 接地しているときのRaycastのコリジョン状態を取得します
        /// </summary>
        public RaycastHit2D RaycastHit { get; private set; }

        protected void Update() {
            //  BoxColliderの真下に短くぶっといRaycast(BoxCast)を撃つ
            //  衝突すれば接地中
            //  すでに地面にめり込んでいる場合は判定されないので少し上から撃つ
            //  FloatingDirectionより地面との距離が小さいならば接地と判定
            RaycastHit = Physics2D.BoxCast(
                origin: new Vector2(Collider.transform.position.x, Collider.transform.position.y) + Collider.offset + new Vector2(0f, Epsilon),
                size: Collider.size,
                angle: 0f,
                direction: Vector2.down,
                distance: FloatingDistance + Epsilon,
                layerMask: GroundLayerMask
                );

            IsGround = RaycastHit;
        }
    }
}