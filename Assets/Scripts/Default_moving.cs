using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default_moving : MonoBehaviour
{
    public float speed = 1;

    private Rigidbody _rigidbody;
    private float _forward;
    private float _right;
    private float _jump;
    private Vector3 _vectorForward;
    private Vector3 _vectorMove;

   
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {

        _forward = Input.GetAxis("Vertical");
        _right = Input.GetAxis("Horizontal");
        _jump = Input.GetAxis("Jump");

        if(_jump != 0)
        {
            _rigidbody.AddForce(Vector3.up * speed * 4);
        }
        _vectorForward = transform.TransformDirection(Vector3.forward) * _forward;
        _vectorMove = _vectorForward + transform.TransformDirection(Vector3.right) * _right;

        _rigidbody.AddForce(_vectorMove * speed);
        
    }
}
