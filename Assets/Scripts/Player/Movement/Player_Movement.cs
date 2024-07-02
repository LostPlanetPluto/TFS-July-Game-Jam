using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnTime;
    private Rigidbody rb;

    private Vector3 direction;

    // Turn Smooth variables
    private float turnSmoothVel;

    [Header("Ground Check Properties")]
    [SerializeField] private LayerMask groundCheckLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        Move();

        GroundCheck();
    }

    private void Move()
    {
        if (rb != null)
        {
            if (direction == Vector3.zero) return;

            if (GroundCheck()) rb.AddForce(direction * moveSpeed * Time.deltaTime);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private bool GroundCheck()
    {
        Ray ray = new Ray(transform.position + Vector3.up + transform.forward * 1, -transform.up);
        return Physics.Raycast(ray, 100, groundCheckLayer);
    }

    private void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position + Vector3.up + transform.forward * 1, -transform.up);

        Gizmos.DrawRay(ray);
    }
}
