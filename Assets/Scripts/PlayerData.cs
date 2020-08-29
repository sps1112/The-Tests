using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : Stats
{
    public float jumpForce;

    public float groundDetectRange;

    public float jumpTimeLag;

    public float gravityMultiplier;

    public float dashForce;

    public float dashTimePeriod;

    public float impactForce;

    public Color dashColor;

    public Material dashMat;

    public float wallDetectRange;

    public float hurtTimePeriod;

    public bool canDashInAir;

}
