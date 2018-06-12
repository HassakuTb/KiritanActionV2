using UnityEngine;

namespace GameScene {
    public enum EightDirection {
        None = 0,
        Up = 1,
        UpRight = 2,
        Right = 3,
        DownRight = 4,
        Down = 5,
        DownLeft = 6,
        Left = 7,
        UpLeft = 8,
    }

    public static class EightDirectionExtensions {

        public static Vector2 ToVector2(this EightDirection self) {
            switch (self) {
                case EightDirection.None:
                    return Vector2.zero;
                case EightDirection.Up:
                    return new Vector2(0, 1);
                case EightDirection.UpRight:
                    return new Vector2(1, 1).normalized;
                case EightDirection.Right:
                    return new Vector2(1, 0);
                case EightDirection.DownRight:
                    return new Vector2(1, -1).normalized;
                case EightDirection.Down:
                    return new Vector2(0, -1);
                case EightDirection.UpLeft:
                    return new Vector2(-1, 1).normalized;
                case EightDirection.Left:
                    return new Vector2(-1, 0);
                case EightDirection.DownLeft:
                    return new Vector2(-1, -1).normalized;
                default:
                    return Vector2.zero;

            }
        }

        public static EightDirection InputToDirection(float horizontalAxis, float verticalAxis) {
            if(horizontalAxis > 0.1) {
                if(verticalAxis > 0.1) {
                    return EightDirection.UpRight;
                }
                else if(verticalAxis < -0.1) {
                    return EightDirection.DownRight;
                }
                else {
                    return EightDirection.Right;
                }
            }
            else if (horizontalAxis < -0.1) {
                if (verticalAxis > 0.1) {
                    return EightDirection.UpLeft;
                }
                else if (verticalAxis < -0.1) {
                    return EightDirection.DownLeft;
                }
                else {
                    return EightDirection.Left;
                }
            }
            else if (verticalAxis > 0.1) {
                return EightDirection.Up;
            }
            else if (verticalAxis < -0.1) {
                return EightDirection.Down;
            }
            else {
                return EightDirection.None;
            }
        }
    }
}