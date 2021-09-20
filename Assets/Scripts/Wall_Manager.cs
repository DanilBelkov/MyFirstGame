using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Dafault
{
    public int strenght;
    public GameObject object_wall;

    public Wall_Dafault(GameObject obj)
    {
        strenght = 100;
        object_wall = obj;
    } 
}

public class Wall_Glass : Wall_Dafault
{
    public Wall_Glass(GameObject obj) : base(obj)
    {
        strenght = 20;
        object_wall = obj;
    }
}

public class Wall_Metal : Wall_Dafault
{
    public Wall_Metal(GameObject obj) : base(obj)
    {
        strenght = 150;
        object_wall = obj;
    }
}

public class Wall_Border : Wall_Dafault
{
    public Wall_Border(GameObject obj) : base(obj)
    {
        object_wall = obj;
    }
}

public class Wall_Manager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
