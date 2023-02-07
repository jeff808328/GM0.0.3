using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonDetect : MonoBehaviour
{
    [Header("Àð»P¦aªO°»´ú")]
    public LayerMask Ground;
    public LayerMask Wall;

    protected Vector2 LeftWallBoxPos;
    protected Vector2 RightWallBoxPos;
    protected Vector2 GroundBoxPos;

    protected Vector2 WallBoxSize;
    protected Vector2 GroundBoxSize;

    protected Rigidbody2D LeftWallDetect;
    protected Rigidbody2D RightWallDetect;
    protected Rigidbody2D GroundDetect;

    public float WallBoxHeight;
    public float WallBoxWidth;
    public float WallBoxHeightOffset;
    public float WallBoxWidthOffset;

    public float GroundBoxHeight;
    public float GroundBoxWidth;
    public float GroundBoxHeightOffset;
    public float GroundBoxWidthOffset;

    protected CommonState CommonState;

    protected void BoxUpdate()
    {
        WallBoxSize = new Vector2(transform.lossyScale.x * WallBoxWidth, transform.lossyScale.y * WallBoxHeight);
        GroundBoxSize = new Vector2(transform.lossyScale.x * GroundBoxWidth, transform.lossyScale.y * GroundBoxHeight);

        LeftWallBoxPos = new Vector2(this.transform.position.x - WallBoxWidthOffset, this.transform.position.y + WallBoxHeightOffset);
        RightWallBoxPos = new Vector2(this.transform.position.x + WallBoxWidthOffset, this.transform.position.y + WallBoxHeightOffset);
        GroundBoxPos = new Vector2(this.transform.position.x + GroundBoxWidthOffset, this.transform.position.y + GroundBoxHeightOffset);

        CommonState.GroundTouching = Physics2D.OverlapBox(GroundBoxPos, GroundBoxSize, 0, Ground);
        CommonState.WallTouching = (Physics2D.OverlapBox(LeftWallBoxPos, WallBoxSize, 0, Ground) || (Physics2D.OverlapBox(RightWallBoxPos, WallBoxSize, 0, Ground)) ? true : false);
    }

}
