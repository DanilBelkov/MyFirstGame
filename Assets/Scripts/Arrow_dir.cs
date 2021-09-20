using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_dir : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    public Camera cameraArrow;

    private Vector3 _tempPosition;
    private Vector3 _vectorRotation;
    private Cam _mainCamera;

    void FixedUpdate()
    {
        _tempPosition = transform.position;

        transform.position = player.transform.position;
        transform.LookAt(target.transform);

        transform.position = _tempPosition;


        //float rX = Input.GetAxis("Mouse X");
        //float rY = -Input.GetAxis("Mouse Y");

        //if (rY != 0)
        //{
        //    _vectorRotation = cameraArrow.transform.TransformDirection(Vector3.right);

        //    cameraArrow.transform.RotateAround(transform.position, _vectorRotation, rY * _mainCamera.speedY * Time.deltaTime);
        //    //target.Rotate(target.right, mY * speedY * Time.deltaTime);
        //}
        //if (rX != 0)
        //{
        //    _vectorRotation = cameraArrow.transform.TransformDirection(Vector3.up);

        //    cameraArrow.transform.RotateAround(transform.position, _vectorRotation, rX * _mainCamera.speedX * Time.deltaTime);
        //    //target.Rotate(target.up, mX * speedX * Time.deltaTime);
        //}
    }
}

