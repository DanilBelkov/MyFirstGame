using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour
{
    public float speed = 1;

    //private CharacterController _characterController;
    private Rigidbody _rigidbody;
    private float _forward;
    private float _right;
    private Vector3 _vectorForward;
    private Vector3 _vectorMove;

    void Start()
    {
        //_characterController = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    //}

    void FixedUpdate()
    {

        _forward = Input.GetAxis("Vertical");
        _right = Input.GetAxis("Horizontal");


        //_vectorForward = Camera.main.transform.TransformDirection(Vector3.forward) * _forward;
        //_vectorMove = _vectorForward + Camera.main.transform.TransformDirection(Vector3.right) * _right;
        
        _vectorForward = transform.TransformDirection(Vector3.forward) * _forward;
        _vectorMove = _vectorForward + transform.TransformDirection(Vector3.right) * _right;
        
        _rigidbody.AddForce(_vectorMove * speed);
        //_characterController.SimpleMove(_vectorForward * _forward * speed);
    }
   
}