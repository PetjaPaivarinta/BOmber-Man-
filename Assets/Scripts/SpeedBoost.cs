using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    void Update()
{
    // Rotate the object around its Z-axis at 1 degree per second
    transform.Rotate(new Vector3(0, 0, Time.deltaTime * 180));
}
}
