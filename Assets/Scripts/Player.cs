using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpForce;
    public float MoveSpeed;
    public Transform GroundCheck;
    public Transform SpawnPoint;
    public LayerMask mask;
    public Vector3 Velocity;

    public Vector3 AngularVelocity;
    public GameObject Cylinder;



    private bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {

        float verticalAxis = Input.GetAxisRaw("Vertical") * MoveSpeed ;
        float horizontalAxis = Input.GetAxisRaw("Horizontal") * MoveSpeed ;
        
        Vector3 v = new Vector3(verticalAxis, 0f, horizontalAxis);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        if (Cylinder == null)
            return;
        // Pegar o vetor com a  direção do cilidro para a o player
        Vector3 d = Cylinder.transform.position - transform.position;
        // calculo da velocidade angular 
        Velocity = AngularVelocity * Mathf.Deg2Rad * d.magnitude;
        // velocidade linear
        v += Vector3.Cross(d.normalized, Vector3.up) * Velocity.y;
        v += Vector3.Cross(d.normalized, Vector3.forward) * Velocity.z;
        v += Vector3.Cross(d.normalized, Vector3.right) * Velocity.x;
        rb.velocity = new Vector3(v.x, rb.velocity.y, v.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("EndPoint"))
        {
            transform.position = SpawnPoint.position;
        }
        isGrounded = true;
        Cylinder = other.gameObject;
        var cylinder = other.collider.GetComponent<CylinderVelocty>();
        if (cylinder != null) this.AngularVelocity = cylinder.AngularVelocity;
    }
    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        Cylinder = null;
        this.AngularVelocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(GroundCheck.position, -GroundCheck.up * 0.5f);
    }



}
