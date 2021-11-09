using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Spawn_labirint : MonoBehaviour
{

    public int countRoom  = 6;//кол-во комнат
    public float size_room = 50.0f;
    public float thickness_wall = 4.0f;

    public GameObject All_Walls;
    public GameObject All_Columns;
    public GameObject All_Joints;

    private int _many, _count, _item_in__many;
    private int _item_in__many_Y;

    private enum cubeOrient {Top, Right, Back};
    private enum typeWall {Glass, Grass, Metal, Portal, Default, Border};

    
    void Start()
    {
        spawn_Target();

        createColumns();
        createWalls();
        
    }

    private void createColumns()
    {
        for(int j = 0; j <= countRoom; j++)
        {
            for(int k = 0; k <= countRoom; k++) 
            {
                for(int i = 0; i <= countRoom; i++)
                {

                    drawJoint(i, j, k);

                    //последний по X
                    if (i == countRoom)
                    {
                        if (j == countRoom) //последний по Y
                        {
                            if (k == countRoom)// последний по Z
                                continue;
                            drawColumn(i, j, k, cubeOrient.Top);
                        }
                        else if (k == countRoom)// последний по Z, то ставим просто балку в углу
                            drawColumn(i, j, k, cubeOrient.Back);
                        else
                        {
                            // рисуем одну правую грань 
                            drawColumn(i, j, k, cubeOrient.Top);
                            drawColumn(i, j, k, cubeOrient.Back);
                        }
                    }
                    else if(k == countRoom)
                    {
                        if (j == countRoom)
                            drawColumn(i, j, k, cubeOrient.Right);
                        else
                        {
                            // рисуем одну переднюю грань 
                            drawColumn(i, j, k, cubeOrient.Right);
                            drawColumn(i, j, k, cubeOrient.Back);
                        }
                    }
                    else if(j == countRoom)
                    {
                        // рисуем одну верхнюю грань 
                        drawColumn(i, j, k, cubeOrient.Top);
                        drawColumn(i, j, k, cubeOrient.Right);
                    }
                    else
                    {
                        drawColumn(i, j, k, cubeOrient.Top);
                        drawColumn(i, j, k, cubeOrient.Right);
                        drawColumn(i, j, k, cubeOrient.Back);
                    }
                }
            }
        }
    }

    private void createWalls()
    {
        for (int j = countRoom; j >= 0; j--)// Y
        {

            for (int k = 0; k <= countRoom; k++)// Z
            {

                _count = 0;
                _many = Random.Range(1, countRoom + 1);
                _item_in__many = Random.Range(1, _many + 1);
                _item_in__many_Y = Random.Range(1, _many + 1);

                for (int i = 0; i <= countRoom; i++)// X
                {

                    if (j != countRoom && k != countRoom)
                    {
                        if (i == 0 || i == countRoom)//задняя стенка
                        {
                            drawWall(i, j, k, cubeOrient.Right);
                        }
                        else if (i == _count + _many)//если закончилось пустое множество, ставим стену и строим новое множество
                        {
                            drawWall(i, j, k, cubeOrient.Right);

                            _count += _many;
                            _many = Random.Range(1, countRoom - i + 1);
                            _item_in__many = Random.Range(1, _many + 1);
                            _item_in__many_Y = Random.Range(1, _many + 1);
                        }
                    }

                    if (i != countRoom)
                    {
                        if (k != countRoom)
                        {
                            if (j == 0 || j == countRoom)//пол/потолок
                            {
                                drawWall(i, j, k, cubeOrient.Top);
                            }
                            else if (i != _count + _item_in__many_Y - 1)//если дошли до индекса, пробиваем отверстие
                            {
                                drawWall(i, j, k, cubeOrient.Top);
                            }
                        }

                        if (j != countRoom)
                        {
                            if (k == 0 || k == countRoom)//задняя стенка
                            {
                                drawWall(i, j, k, cubeOrient.Back);
                            }
                            else if (i != _count + _item_in__many - 1)//если дошли до индекса, сносим стену
                            {
                                drawWall(i, j, k, cubeOrient.Back);
                            }
                        }
                    }

                }

            }

        }
    }

    private void drawWall(int i, int j, int k, cubeOrient cO)
    {
        GameObject one_wall = Instantiate(Resources.Load("Objects/wall_Labirint") as GameObject);
        one_wall.transform.parent = All_Walls.transform;
        one_wall.name = "wall_" + i + "_" + j + "_" + k;
        one_wall.transform.localScale = new Vector3(size_room - thickness_wall, size_room - thickness_wall, thickness_wall);

        switch (cO)
        {
            case cubeOrient.Top:
                one_wall.transform.position = new Vector3(i * size_room + size_room / 2, j * size_room, k * size_room + size_room / 2);
                one_wall.transform.Rotate(90f, 0f, 0f);
                break;
            case cubeOrient.Right:
                one_wall.transform.position = new Vector3(i * size_room, j * size_room + size_room / 2, k * size_room + size_room / 2);
                one_wall.transform.Rotate(0f, 90f, 0f);
                break;
            case cubeOrient.Back:
                one_wall.transform.position = new Vector3(i * size_room + size_room / 2, j * size_room + size_room / 2, k * size_room);
                break;
            default:
                break;
        }
    }

    private void drawColumn(int i, int j, int k, cubeOrient c)
    {
        GameObject column = Instantiate(Resources.Load("Objects/Column") as GameObject);
        column.transform.parent = All_Columns.transform;
        column.name = "column_" + i + "_" + j + "_" + k;
        column.transform.localScale = new Vector3(thickness_wall, size_room, thickness_wall);

        switch(c)
        {
            case cubeOrient.Top:
                column.transform.position = new Vector3(i * size_room, j * size_room, k * size_room + size_room / 2);
                column.transform.Rotate(90f, 0f, 0f);
                break;
            case cubeOrient.Right:
                column.transform.position = new Vector3(i * size_room + size_room / 2, j * size_room, k * size_room);
                column.transform.Rotate(0f, 0f, 90f);
                break;
            case cubeOrient.Back:
                column.transform.position = new Vector3(i * size_room, j * size_room + size_room / 2, k * size_room);
                break;
            default:
                break;
        }

    }
    
    private void drawJoint(int i, int j, int k)
    {
        GameObject joint = Instantiate(Resources.Load("Objects/Joint") as GameObject);
        joint.transform.parent = All_Joints.transform;
        joint.name = "joint_" + i + "_" + j + "_" + k;
        joint.transform.localScale = new Vector3(thickness_wall, thickness_wall, thickness_wall) * 3;
        joint.transform.position = new Vector3(i * size_room, j * size_room, k * size_room);
    }

    private void spawn_Target()
    {
        GameObject Target = GameObject.FindGameObjectWithTag("Finish");

        int pos = Random.Range(0, countRoom);
        float x = size_room / 2 + size_room * pos;
        pos = Random.Range(0, countRoom);
        float y = size_room / 2 + size_room * pos;
        pos = Random.Range(0, countRoom);
        float z = size_room / 2 + size_room * pos;

        Target.transform.position = new Vector3(x, y, z);
        Target.transform.localScale = new Vector3(size_room / 2, size_room / 2, size_room / 2);
    }
}
