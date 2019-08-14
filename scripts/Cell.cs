using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell {

    public int row;
    public int column;
    public int visited = 0;
    public int predirection = 0;
    public int numberofN = 4;
    public Cell[,] neigbour;
}
