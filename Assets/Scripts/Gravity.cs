using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    /* class for manipulating gravity direction
     * directions for gravity is as follows:
     * 1 ~ 6 for each direction, starting from right up diagonal, counterclockwise
     *    2
     * 3     1
     * 4     6
     *    5
     */
    [SerializeField]
    private float strength = 9.8f;
    public Vector2 Direction { get; private set; }
    public float DirectionRadian { get; private set; }
    public float DirectionDegree { get; private set; }

    private int directionNum;
    public int DirectionNum
    {
        get { return directionNum; }
        set
        {
            directionNum = value;
            DirectionRadian = Mathf.PI * (value * 2 - 1) / 6;
            DirectionDegree = 30 * (value * 2 - 1);
            Direction = new Vector2(Mathf.Cos(DirectionRadian), Mathf.Sin(DirectionRadian));
            Physics2D.gravity = Direction * strength;
        }
    }
}
