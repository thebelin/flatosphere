using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewControls : MonoBehaviour
{

    // A vertical axle for rotation
    public Transform verticalRotator;
    public Transform horizontalRotator;

    public void Tilt( float angle )
    {
        // Rotate the camera around the vertical axle
        verticalRotator.Rotate( Vector3.right, angle );
    }

    public void Pan( float angle )
    {
        // Rotate the camera around the vertical axle
        horizontalRotator.Rotate( Vector3.up, angle );
    }
}
