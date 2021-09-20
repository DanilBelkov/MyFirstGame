using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spuwn2 : MonoBehaviour
{
    public const int count_room = 6;//кол-во комнат
    public static GameObject[,] right = new GameObject[count_room, count_room];
    public static GameObject[,] back = new GameObject[count_room, count_room];


    // Start is called before the first frame update
    void Start()
    {
        int width = 2;//ширина стен
        float size = GetComponent<Renderer>().bounds.size.x;//размер куба, берется из обьекта на который повешан скрипт
        Material my_material = GetComponent<MeshRenderer>().material;//митериал этого обьекта

        int many, count, item_in_many;

        for (int k = 0; k < count_room; k++)// Z
        {
            count = many = 0;
            item_in_many = -1;
            for (int i = 0; i < count_room; i++)// X
            {
                if (i == 0)
                {
                    many = Random.Range(1, count_room + 1);
                    item_in_many = Random.Range(1, many + 1);
                }

                if (k == 0)
                {
                    back[k, i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    back[k, i].transform.position = new Vector3(-5 * size / (2 * count_room) + i * size / count_room, size / (2 * count_room), 3 * size / count_room - k * size / count_room);
                    back[k, i].transform.localScale = new Vector3(size / count_room, size / count_room, width);
                    back[k, i].GetComponent<MeshRenderer>().material = my_material;
                }
                else if (k != 0 && i != count + item_in_many - 1)
                {
                    back[k, i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    back[k, i].transform.position = new Vector3(-5 * size / (2 * count_room) + i * size / count_room, size / (2 * count_room), 3 * size / count_room - k * size / count_room);
                    back[k, i].transform.localScale = new Vector3(size / count_room, size / count_room, width);
                    back[k, i].GetComponent<MeshRenderer>().material = my_material;
                }


                if (i == count + many - 1)
                {

                    right[k, i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    right[k, i].transform.position = new Vector3(-2 * size / count_room + i * size / count_room, size / (2 * count_room), 5 * size / (2 * count_room) - k * size / count_room);
                    right[k, i].transform.localScale = new Vector3(width, size / count_room, size / count_room);
                    right[k, i].GetComponent<MeshRenderer>().material = my_material;

                    count += many;
                    many = Random.Range(1, count_room - i);
                    item_in_many = Random.Range(1, many + 1);
                }

                
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
