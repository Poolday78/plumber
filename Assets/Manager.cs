using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject[] pipePrefebs;

    public List<Pipe> pipes;

    public int roww, columnn = 3;

    public string[] pipeTypes = { "STRAIGHT", "ANKLE", "T", "CROSS"};

    private void Start()
    {
        pipes = new List<Pipe>();
        Generate(roww,columnn);
    }

    void Generate(int row, int column)
    {
        pipes.Clear();
        Pipe pipe;
        for (int i = -row; i <= row; i++)
        {
            for (int j = -column; j <= column; j++)
            {
                pipe = new Pipe();
                int type = Random.Range(0, pipeTypes.Length);

                if ((i == -row && j == -column) || (i == -row && j == column) || (i == row && j == -column) || (i == row && j == column))
                {
                    type = 1;
                }
                else if (j == -column || j == column || i == -row || i == row)
                {
                    if (type == 3)
                    {
                        type = Random.Range(0, pipeTypes.Length - 1);
                    }
                }
                
                pipe.SetSides(pipeTypes[type]);
                pipe.go = Instantiate(pipePrefebs[type]);
                pipe.go.transform.position = new Vector2(i, j);

                switch (Random.Range(0,4))
                {
                    case 0:
                        pipe.go.transform.rotation = Quaternion.Euler(0, 0, -90);
                        break;
                    case 1:
                        pipe.go.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case 2:
                        pipe.go.transform.rotation = Quaternion.Euler(0, 0, 90);
                        break;
                    case 3:
                        pipe.go.transform.rotation = Quaternion.Euler(0, 0, 180);
                        break;
                    default:
                        break;
                }

                pipes.Add(pipe);
            }
        }

        UpdateConnected();
    }

    public Pipe PipeMatch(GameObject go)
    {
        foreach (Pipe pipe in pipes)
        {
            if (pipe.go == go)
            {
                return pipe;
            }
        }
        return null;
    }

    public void UpdateConnected()
    {
        for (int i = 0; i < pipes.Count; i++)
        {
            Pipe pipe = pipes[i];
            Sides sides = pipe.GetSides();
            if (i == 0) /// pipe is ankle/elbow on the most left
            {
                if (sides.down || sides.right)
                {
                    if (pipes[i + 1] != null)
                    {

                    }
                }
            }
        }
    }
}

public class Pipe
{
    public GameObject go;
    private Sides sides = new Sides();

    public string pipeType;

    public void SetSides(string pipeType)
    {
        switch (pipeType)
        {
            case "STRAIGHT":
                sides.up = true;
                sides.down = true;
                sides.left = false;
                sides.right = false;
                break;
            case "ANKLE":
                sides.up = true;
                sides.down = false;
                sides.left = true;
                sides.right = false;
                break;
            case "CROSS":
                sides.up = true;
                sides.down = true;
                sides.left = true;
                sides.right = true;
                break;
            case "T":
                sides.up = true;
                sides.down = true;
                sides.left = true;
                sides.right = false;
                break;
            default:
                sides.up = false;
                sides.down = false;
                sides.left = false;
                sides.right = false;
                break;
        }
    }

    public Sides GetSides()
    {
        Sides newSides = sides;
        bool temp;
        switch (go.transform.eulerAngles.z)
        {
            case 0:
                break;
            case 90:
                temp = newSides.right;
                newSides.right = newSides.up;
                newSides.up = newSides.left;
                newSides.left = newSides.down;
                newSides.down = temp;
                break;
            case 180:
                temp = newSides.right;
                newSides.right = newSides.left;
                newSides.left = temp;
                temp = newSides.up;
                newSides.up = newSides.down;
                newSides.down = temp;
                break;
            case -90:
                temp = newSides.left;
                newSides.left = newSides.up;
                newSides.up = newSides.right;
                newSides.right = newSides.down;
                newSides.down = temp;
                break;
            default:
                break;
        }

        return newSides;
    }
}

public class Sides
{
    public bool up;
    public bool down;
    public bool left;
    public bool right;
}