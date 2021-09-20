using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Camera cam;
    public Camera CameraForArrow;
    public Transform target;
    public float speed;
    public float limitY = 60f;
    public float hideDistance;
    public LayerMask cube;
    public LayerMask noPlayer;

    private float _maxDistance;
    private Vector3 _localPosition;
    private Vector3 _vectorRotation;
    private LayerMask _originCam;

    private GameObject Arrow;

    private Vector3 _position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    void Start()
    {
        _localPosition = target.InverseTransformPoint(_position);//точка относительно цели 
        _maxDistance = Vector3.Distance(_position, target.position);
        _originCam = cam.cullingMask;// изначальная маска с камеры

        Arrow = GameObject.FindGameObjectWithTag("Arrow");
    }

    void CameraRotation()
    {
        var mX = Input.GetAxis("Mouse X");
        var mY = -Input.GetAxis("Mouse Y");
        var rZ = -Input.GetAxis("Forward Rotate");

        if (mY != 0)
        {
            // поворот главной камеры
            _vectorRotation = transform.TransformDirection(Vector3.right);
            transform.RotateAround(target.position, _vectorRotation, mY * speed * Time.deltaTime);

            // поворот камеры у напрвляющей стрелки
            _vectorRotation = CameraForArrow.transform.TransformDirection(Vector3.right);
            CameraForArrow.transform.RotateAround(Arrow.transform.position, _vectorRotation, mY * speed * Time.deltaTime);
            
            // поворот персонажа
            target.transform.Rotate(mY * speed * Time.deltaTime, 0, 0);
           
        }
        if (mX != 0)
        {
            // поворот главной камеры
            _vectorRotation = transform.TransformDirection(Vector3.up);
            transform.RotateAround(target.position, _vectorRotation, mX * speed * Time.deltaTime);

            // поворот камеры у напрвляющей стрелки
            _vectorRotation = CameraForArrow.transform.TransformDirection(Vector3.up);
            CameraForArrow.transform.RotateAround(Arrow.transform.position, _vectorRotation, mX * speed * Time.deltaTime);

            // поворот персонажа
            target.transform.Rotate(0, mX * speed * Time.deltaTime, 0);
        }
        if (rZ != 0)
        {
            // поворот главной камеры
            _vectorRotation = transform.TransformDirection(Vector3.forward);
            transform.RotateAround(target.position, _vectorRotation, rZ * speed * Time.deltaTime);

            // поворот камеры у напрвляющей стрелки
            _vectorRotation = CameraForArrow.transform.TransformDirection(Vector3.forward);
            CameraForArrow.transform.RotateAround(Arrow.transform.position, _vectorRotation, rZ * speed * Time.deltaTime);
            
            // поворот персонажа
            target.transform.Rotate(0, 0, rZ * speed * Time.deltaTime);
        }
    }

    void CubeReact()
    {
        var distance = Vector3.Distance(_position, target.position);
        RaycastHit hit;
        //пускаем луч и смотрим, есть ли преграда
        if(Physics.Raycast(target.position, _position - target.position, out hit, _maxDistance, cube))
        {
            _position = hit.point;
        }//отодвигаем камеру назад
        else if(distance < _maxDistance && !(Physics.Raycast(_position, -transform.forward, .1f, cube)))
        {
            _position -= transform.forward * .05f;
        }
    }

    void PlayerReact()
    {
        var distance = Vector3.Distance(_position, target.position);
        //слишком близко к игроку
        if(distance < hideDistance)
        {
            cam.cullingMask = noPlayer;//окружение без него
        }
        else
        {
            cam.cullingMask = _originCam;//изначальное окружение
        }
    }

    void LateUpdate()
    {
        _position = target.TransformPoint(_localPosition);
        CameraRotation();
        CubeReact();
        PlayerReact();
        //transform.LookAt(target);
        _localPosition = target.InverseTransformPoint(_position);
    }
}
