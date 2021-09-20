using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public LayerMask ColumnLayer;
    public GameObject plane;

    private Light _lantern;
    private Cam cam;
    private GameObject _raycastGO;
    private GameObject _oldRaycastGO = null;
    private Material _oldGO_material;
    private Material _selectedGO_matrial;
    private Spawn_labirint S_P;
    private bool flag;

    void Start()
    {
        _lantern = GetComponent<Light>();
        //_lantern.enabled = false;
        _selectedGO_matrial = Instantiate(Resources.Load("Materials/Hight_material_cube") as Material);
        S_P = plane.GetComponent<Spawn_labirint>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            _lantern.enabled = !_lantern.enabled;
            //if(_lantern.type == LightType.Point)
            //{
            //    _lantern.type = LightType.Spot;
            //}
            //else
            //{
            //    _lantern.type = LightType.Point;
            //}

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            flag = !flag;
        }

        if(flag)
            skill_create();


    }
    
    public void skill_create()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        //пускаем луч и смотрим, есть ли преграда
        if (Physics.Raycast(ray, out hit, 300.0f, ColumnLayer))
        {
            //hit.collider.transform.position = new Vector3(0, 0, 0);
            _raycastGO = hit.collider.gameObject;

            if (_oldRaycastGO == null)
            {
                _oldRaycastGO = _raycastGO;
                _oldGO_material = _raycastGO.GetComponent<Renderer>().material;
            }
            if (_raycastGO != _oldRaycastGO)
            {
                _oldRaycastGO.GetComponent<Renderer>().material = _oldGO_material;
                _oldGO_material = _raycastGO.GetComponent<Renderer>().material;
                _raycastGO.GetComponent<Renderer>().material = _selectedGO_matrial;
                _oldRaycastGO = _raycastGO;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                create_Wall(_raycastGO);
            }

            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                Destroy(_raycastGO);
            }

        }
    }
    public void create_Wall(GameObject column)
    {
        GameObject wall = Instantiate(Resources.Load("Objects/wall_Labirint") as GameObject);
        wall.name = "create_new_" + column.name;
        wall.transform.localScale = new Vector3(S_P.size_room - S_P.thickness_wall, S_P.size_room - S_P.thickness_wall, S_P.thickness_wall);
        wall.transform.position = (column.transform.parent.position + column.transform.forward * S_P.size_room/2);

        wall.transform.rotation = column.transform.rotation;
        wall.transform.Rotate(0, 90, 0);

    }
}
