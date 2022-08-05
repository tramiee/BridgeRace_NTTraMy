using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant : MonoBehaviour
{
    public const string ANIM_ISRUN = "isRun";
    public const string ANIM_WIN = "Win";
    public const string ANIM_ISFALL = "isFall";

    public const string TAG_FINISH = "Finish";
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_PLAYER = "Player";

    public const string LAYER_BRIDGE = "Bridge";

    public enum BrickTags { Yellow, Red, Blue, Green };
    public enum BridgeTag { BridgeYellow, BridgeRed, BridgeBlue, BridgeGreen };
    public enum BrickType { Blue = 0, Green = 1, Red = 2, Yellow = 3 };
}
