using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonDetect : MonoBehaviour
{
    public LayerMask Ground;
    public LayerMask Wall;

    private Vector2 LeftWallBoxPos;
    private Vector2 RightWallBoxPos;
    private Vector2 GroundBoxPos;

    private Vector2 WallBoxSize;
    private Vector2 GroundBoxSize;

    private Rigidbody2D LeftWallDetect;
    private Rigidbody2D RightWallDetect;
    private Rigidbody2D GroundDetect;

    public float WallBoxHeight;
    public float WallBoxWidth;
    public float WallBoxHeightAdjust;
    public float WallBoxWidthAdjust;

    public float GroundBoxHeight;
    public float GroundBoxWidth;
    public float GroundBoxHeightAdjust;
    public float GroundBoxWidthAdjust;
}
