using System;
using static System.Convert;
using System.Collections.Generic;
using UnityEngine;
///Class that generates the Maze
public class MazeGen : MonoBehaviour
{
    public GameObject Floor;
    public GameObject Wall;
    public GameObject Pillar;
    public GameObject Button;
    public static int Width;
    public static int Height;
    public bool Perfect;
    public int Perfectness;
    private System.Random rand = new();
    void Start()
    {
        //Loading the player preferences for the maze and generating it
        Width = PlayerPrefs.GetInt("MazeWidth");
        Height = PlayerPrefs.GetInt("MazeHeight");
        Perfect = ToBoolean(PlayerPrefs.GetInt("MazePerfect"));
        Perfectness = PlayerPrefs.GetInt("MazePerfectness");
        GenerateMaze();
    }
    //The function that generates the maze using non-recursive Random DFS
    void GenerateMaze()
    {
        bool[,] visited = new bool[Width, Height];
        var stack = new Stack<(int x, int y, int n, int m)>();
        (int sx, int sy) = (rand.Next(Width), rand.Next(Height));
        stack.Push((sx, sy, sx, sy));
        while (stack.Count > 0)
        {
            (int x, int y, int n, int m) = stack.Pop();
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                //Placing the wall between the current tile and previously visited tile
                if (visited[x, y])
                {
                    if (Perfect)
                    {
                        Instantiate(Wall, new Vector3((x + n) * 2, 0, (y + m) * 2), Quaternion.Euler(0, y == m ? 90 : 0, 0), transform);
                    }
                    //Placing the wall based on the perfectness level in the non-perfect maze
                    else if (rand.Next(100) < Perfectness)
                    {
                        Instantiate(Wall, new Vector3((x + n) * 2, 0, (y + m) * 2), Quaternion.Euler(0, y == m ? 90 : 0, 0), transform);
                    }
                }
                else
                {
                    visited[x, y] = true;
                    //Placing the floor for each tile
                    Instantiate(Floor, new Vector3(4 * x, 0, 4 * y), Quaternion.identity, transform);
                    int[][] directions = {
                        new int[] {1, 0},
                        new int[] {-1, 0},
                        new int[] {0, 1},
                        new int[] {0, -1}
                    };
                    Shuffle(directions);
                    //Pushing the neighbour tiles except the parent tile into the stack
                    foreach (int[] direction in directions)
                    {
                        if (x + direction[0] != n || y + direction[1] != m)
                        {
                            stack.Push((x + direction[0], y + direction[1], x, y));
                        }
                    }
                }
            }
            //Placing the walls for the maze borders
            else
            {
                Instantiate(Wall, new Vector3((x + n) * 2, 0, (y + m) * 2), Quaternion.Euler(0, y == m ? 90 : 0, 0), transform);
            }
        }
        //Placing the end-point (a pillar with a button)
        Instantiate(Pillar, new Vector3(4 * (Width - 1), 0, 4 * (Height - 1)), Quaternion.identity, transform);
        Instantiate(Button, new Vector3(4 * (Width - 1) - 0.25f, 2, 4 * (Height - 1)), Quaternion.identity, transform);
    }
    //Fisher-Yates shuffle
    void Shuffle(int[][] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }
    }
}