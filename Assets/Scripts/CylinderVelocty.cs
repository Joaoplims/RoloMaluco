
using UnityEngine;

public class CylinderVelocty : MonoBehaviour
{
    public Vector3 AngularVelocity;

    private Vector3 angles;

    private void Start()
    {
        angles = transform.eulerAngles;
    }
    void Update()
    {
        angles += AngularVelocity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(angles);
    }
}
