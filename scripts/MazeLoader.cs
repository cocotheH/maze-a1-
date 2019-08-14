using UnityEngine;
using System.Collections;
//using System;

public class MazeLoader : MonoBehaviour
{
    private int mazeRows = 16, mazeColumns = 16;
    //private int keynumber = 3;
    public GameObject wall;
    public GameObject Exit;
    public GameObject floor1;
    public GameObject floor2;
    public GameObject tfloor;
    public GameObject tfloor2;
    public GameObject Door;
    private float size = 6f;
    public GameObject Coin;
    public GameObject Enemy;
    public GameObject Stair;
    public GameObject Montain;
    public GameObject Hole;
    public GameObject Helper;
    public GameObject Boundary;
    public GameObject Boundary2;
    public GameObject TBoundary;
    public GameObject House;
    public GameObject Statue;
    public GameObject Path;
    public GameObject Rock;
    public GameObject Gold;
    public GameObject Player;

    private MazeCell[,] mazeCells;
    private MazeCell[,] terrianCells;
    //private Cell[,] cell;
    public Keys key1;
    public Keys key2;
    public Keys key3;

    // Use this for initialization
    void Start()
    {
        //System.Console.WriteLine(Global.keycounter);
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
        InitializeMaze();
        MazeAlgorithm ma = new HuntAndKillMazeAlgorithm(mazeCells);
        ma.CreateMaze();
        Camera.main.clearFlags = CameraClearFlags.Depth;
        Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
        
        //add keys
        InitializeKey();
        InitializeStairs();
        InitializeStairsExit();
        InitializeTerrian();
        GameObject player = Instantiate(Player, new Vector3(34.5f, 88f, -136f), Quaternion.identity) as GameObject;
        player.name = "player";
        InitializeMazecells();

    }

    // Update is called once per frame
    void Update()
    {

        if (Global.keycounter ==3) {
            Destroy(mazeCells[15, 14].southWall);
            Destroy(GameObject.Find("TFloor2 17,33"));
            Destroy(GameObject.Find("TFloor2 17,34"));
        }
        if (Global.keycounter == 3 && Global.goldcounter == 1)
        {
            Application.Quit();
        }

        if (Global.EnterRoom == 1) {
            GameObject enemy1 = Instantiate(Enemy, new Vector3(1f * size, -1f, 14f * size), Quaternion.identity) as GameObject;
            enemy1.name = "enemy1";
            Global.EnterRoom = 0;
        }

        if (Global.EnterRoom == 2)
        {
            GameObject enemy2 = Instantiate(Enemy, new Vector3(14f * size, -1f, 1f * size), Quaternion.identity) as GameObject;
            enemy2.name = "enemy2";
            Global.EnterRoom = 0;
        }

        if (Global.EnterRoom == 3)
        {
            GameObject enemy3 = Instantiate(Enemy, new Vector3(14f * size, -1f, 14f * size), Quaternion.identity) as GameObject;
            enemy3.name = "enemy3";
            Global.EnterRoom = 0;
        }

        if (Global.flagEnterPatch == 1) {
            GameObject.Find("TFloor2 1,3").GetComponent<Renderer>().material.color = Color.red;
            GameObject.Find("TFloor2 1,4").GetComponent<Renderer>().material.color = Color.red;
        }

        if (Global.flagEnterPatch == 2)
        {
            GameObject.Find("TFloor2 1,3").GetComponent<Renderer>().material.color = Color.blue;
            GameObject.Find("TFloor2 1,4").GetComponent<Renderer>().material.color = Color.blue;
        }

        if (Global.flagEnterPatch == 3)
        {
            Destroy(GameObject.Find("TFloor2 1,3"));
            Destroy(GameObject.Find("TFloor2 1,4"));
        }


    }
    private void InitializeMazecells() {
        
        for (int r1 = 0; r1 < 16; r1++)
        {
            for (int c1 = 0; c1 < 16; c1++)
            {
                Global.cell[r1, c1] = new Cell();
                Global.cell[r1, c1].row = r1;
                Global.cell[r1, c1].column = c1;
            }
        }
        for (int r = 0; r < 16; r++)
        {
            for (int c = 0; c < 16; c++)
            {
                Global.cell[r, c].neigbour = new Cell[2, 2];
                if (r == 0)
                {
                    Global.cell[r, c].neigbour[0, 0] = null;

                }
                else {
                    if (!(mazeCells[r - 1, c].southWall != null))
                    {
                        Global.cell[r, c].neigbour[0, 0] = Global.cell[r - 1, c];
                    }
                    else {
                        Global.cell[r, c].neigbour[0, 0] = null;
                    }
                }
                if (r==15) {
                    Global.cell[r, c].neigbour[0, 1] = null;
                }
                else {
                    if (!(mazeCells[r, c].southWall != null))
                    {
                        Global.cell[r, c].neigbour[0, 1] = Global.cell[r + 1, c];
                    }
                    else {
                        Global.cell[r, c].neigbour[0, 1] = null;
                    }
                }

                if (c == 0)
                {
                    Global.cell[r, c].neigbour[1, 0] = null;
                }
                else {
                    if (!(mazeCells[r, c-1].westWall != null))
                    {
                        Global.cell[r, c].neigbour[1, 0] = Global.cell[r, c - 1];
                    }
                    else {
                        Global.cell[r, c].neigbour[1, 0] = null;
                    }
                }
                if (c == 15)
                {
                    Global.cell[r, c].neigbour[1, 1] = null;
                }
                else {
                    if (mazeCells[r, c + 1].westWall == null)
                    {
                        Global.cell[r, c].neigbour[1, 1] = Global.cell[r, c + 1];
                    }
                    else {
                        Global.cell[r, c].neigbour[1, 1] = null;
                    }
                }
                //if (!(mazeCells[r-1, c].southWall != null)) {
                //    if (r != 0) {
                //        Global.cell[r, c].neigbour[0, 0] = Global.cell[r-1, c];
                //    }
                //    else {
                //        Global.cell[r, c].neigbour[0, 0] = null;
                //    }
                //}
                //if (!(mazeCells[r, c].southWall != null))
                //{
                //    if (r != 15)
                //    {
                //        Global.cell[r, c].neigbour[0, 1] = Global.cell[(r+1), c];
                //    }
                //    else {
                //        Global.cell[r, c].neigbour[0, 1] = null;
                //    }
                //}
                //if (!(mazeCells[r, c].westWall != null))
                //{
                //    if (c != 0)
                //    {
                //        Global.cell[r, c].neigbour[1, 0] = Global.cell[r, (c-1)];
                //    }
                //    else {
                //        Global.cell[r, c].neigbour[1, 0] = null;
                //    }
                //}
                //if (!(mazeCells[r, c].eastWall != null))
                //{
                //    if (c != 15)
                //    {
                //        Global.cell[r, c].neigbour[1, 1] = Global.cell[r, (c + 1)];
                //    }
                //    else {
                //        Global.cell[r, c].neigbour[1, 1] = null;
                //    }
                //}
            }
        }
    }


    private void InitializeKey() {
        key1 = new Keys();
        key1.element = Instantiate(Coin, new Vector3(1f * size, -2.6f, 14f * size), Quaternion.identity) as GameObject;
        key1.element.name = "key1";
        key1.pickup = 0;
        key2 = new Keys();
        key2.element = Instantiate(Coin, new Vector3(14f * size, -2.6f, 1f * size), Quaternion.identity) as GameObject;
        key2.element.name = "key2";
        key2.pickup = 0;
        key3 = new Keys();
        key3.element = Instantiate(Coin, new Vector3(14f * size, -2.6f, 14f * size), Quaternion.identity) as GameObject;
        key3.element.name = "key3";
        key3.pickup = 0;
    }

    //stairs to go down
    private void InitializeStairs() {
        for (int stcounter = 0; stcounter <= 3; stcounter++)
        {
            GameObject hall = Instantiate(Stair, new Vector3(0, -(size / 2f), -4.5f-stcounter*3f), Quaternion.identity) as GameObject;
            hall.name = "hall " + stcounter;
            hall.transform.Rotate(Vector3.right, 90f);
        }

        for (int stcounter = 0 ; stcounter <= 62; stcounter++) {
            GameObject stair = Instantiate(Stair, new Vector3(0, -3f + 1f * stcounter, -4.5f -6f - 1.5f * stcounter), Quaternion.identity) as GameObject;
            stair.name = "stiair " + stcounter;
            stair.transform.Rotate(Vector3.right, 90f);
        }

        for (int stcounter = 66; stcounter <= 76; stcounter++)
        {
            GameObject stair = Instantiate(Stair, new Vector3(0, -3f + 1f * 63f, -4.5f - 9f - 1.5f * 71f - 3f*(stcounter -71f)), Quaternion.identity) as GameObject;
            stair.name = "stiair " + stcounter;
            stair.transform.Rotate(Vector3.right, 90f);
        }
    }

    //stairs to go up
    private void InitializeStairsExit()
    {
        for (int stcounter = 0; stcounter <= 1; stcounter++)
        {
            GameObject hall = Instantiate(Stair, new Vector3(4.5f + (15f * size) + (stcounter) * 3f, -(size / 2f), 14f * size), Quaternion.identity) as GameObject;
            hall.name = "hall2 " + stcounter;
            hall.transform.Rotate(new Vector3(90f, 90f, 0f));
        }
        for (int stcounter = 0; stcounter <= 80; stcounter++)
        {
            GameObject stair2 = Instantiate(Stair, new Vector3((15f * size) + 6f, -3f + 1f * stcounter, 13f * size + 1.5f - 1.5f * stcounter), Quaternion.identity) as GameObject;
            stair2.name = "stiair2 " + stcounter;
            stair2.transform.Rotate(Vector3.right, 90f);
        }
    }
   
    //built montain house every thing should on terrain layer
    private void InitializeTerrian()
    {
        terrianCells = new MazeCell[45, 45];
        for (int r = 0; r <= 19; r++)
        {
            for (int c = 0; c <= 36; c++)
            {
                terrianCells[r, c] = new MazeCell();
                if (c == 0)
                {
                    terrianCells[r, c].westWall = Instantiate(TBoundary, new Vector3(-6f + r * 6f, 81.5f, -139.5f + c * 3f - 1.55f), Quaternion.identity) as GameObject;
                    terrianCells[r, c].westWall.name = "TWestWall " + r + "," + c;
                }
                if (r == 0)
                {
                    terrianCells[r, c].southWall = Instantiate(TBoundary, new Vector3(-6f + r * 6f - 3.05f, 81.5f, -139.5f + c * 3f), Quaternion.identity) as GameObject;
                    terrianCells[r, c].southWall.name = "tsouthwall " + r + "," + c;
                    terrianCells[r, c].southWall.transform.Rotate(Vector3.up * 90f);
                }
                if (c == 36)
                {
                    terrianCells[r, c].eastWall = Instantiate(TBoundary, new Vector3(-6f + r * 6f, 81.5f, -139.5f + c * 3f + 1.55f), Quaternion.identity) as GameObject;
                    terrianCells[r, c].eastWall.name = "TEastWall " + r + "," + c;
                }
                if (r == 19)
                {
                    terrianCells[r, c].northWall = Instantiate(TBoundary, new Vector3(-6f + r * 6f + 3.05f, 81.5f, -139.5f + c * 3f), Quaternion.identity) as GameObject;
                    terrianCells[r, c].northWall.name = "TNorthWall " + r + "," + c;
                    terrianCells[r, c].northWall.transform.Rotate(Vector3.up * 90f);
                }

                if ((r == 1 && c == 3) || (r == 17 && c == 33) || (r == 1 && c == 4) || (r == 17 && c == 34))
                {
                    terrianCells[r, c].floor = Instantiate(tfloor2, new Vector3(-6f + r * 6f, 78.45f, -139.5f + c * 3f), Quaternion.identity) as GameObject;
                    terrianCells[r, c].floor.name = "TFloor2 " + r + "," + c;
                    terrianCells[r, c].floor.transform.Rotate(Vector3.right, 90f);
                }
                else {
                    terrianCells[r, c].floor = Instantiate(tfloor, new Vector3(-6f + r * 6f, 78.45f, -139.5f + c * 3f), Quaternion.identity) as GameObject;
                    terrianCells[r, c].floor.name = "TFloor " + r + "," + c;
                    terrianCells[r, c].floor.transform.Rotate(Vector3.right, 90f);
                }

            }
        }
        GameObject montain = Instantiate(Montain, new Vector3(-9f,78.1f,-108f), Quaternion.identity) as GameObject;
        montain.name = "montain";
        GameObject hole = Instantiate(Hole, new Vector3(48.9f,78.5f,-96.3f), Quaternion.identity) as GameObject;
        hole.name = "hole";
        hole.transform.Rotate(Vector3.right, 90f);
        GameObject helper = Instantiate(Helper, new Vector3(-6f + 1f * 6f, 78.45f, -138f + 3f * 3f), Quaternion.identity) as GameObject;
        helper.name = "helper";
        GameObject boundary1 = Instantiate(Boundary, new Vector3(3.1f, 27.1f, -81.1f), Quaternion.identity) as GameObject;
        boundary1.name = "boundary1";
        GameObject boundary2 = Instantiate(Boundary, new Vector3(-3.6f, 27.1f, -70.7f), Quaternion.identity) as GameObject;
        boundary2.name = "boundary2";
        GameObject boundary3 = Instantiate(Boundary, new Vector3(99.7f, 27.1f, 12.1f), Quaternion.identity) as GameObject;
        boundary3.name = "boundary3";
        GameObject boundary4 = Instantiate(Boundary2, new Vector3(93.5f, 39.2f, 12.1f), Quaternion.identity) as GameObject;
        boundary4.name = "boundary4";
        GameObject boundary5 = Instantiate(Boundary2, new Vector3(0, -3f + 1f * 71f -28.5f, -4.5f - 6f - 1.5f * 71f - 3f * (76f- 71f) - 2.5f), Quaternion.identity) as GameObject;
        boundary5.name = "boundary5";
        boundary5.transform.Rotate(Vector3.up, 90f);
        GameObject house = Instantiate(House, new Vector3(36f, 88.5f, -141f), Quaternion.identity) as GameObject;
        house.name = "house";
        house.transform.Rotate(Vector3.up, -90f);
        GameObject statue = Instantiate(Statue, new Vector3(5f, 42.75f, -222f), Quaternion.identity) as GameObject;
        statue.name = "statue";
        GameObject path = Instantiate(Path, new Vector3(3.325f, 43.283f, -221.834f), Quaternion.identity) as GameObject;
        path.name = "path";
        GameObject rock = Instantiate(Rock, new Vector3(3.325f, 43.283f, -221.834f), Quaternion.identity) as GameObject;
        rock.name = "house";
        GameObject gold = Instantiate(Gold, new Vector3(3.325f, 43.283f, -142.65f), Quaternion.identity) as GameObject;
        gold.name = "statue";
    }

    //initilize maze  builf wall and floor 
    private void InitializeMaze()
    {

        mazeCells = new MazeCell[mazeRows, mazeColumns];

        for (int r = 0; r < mazeRows; r++)
        {
            for (int c = 0; c < mazeColumns; c++)
            {
                mazeCells[r, c] = new MazeCell();

                if (c == 0 && r != 0)
                {
                    mazeCells[r, c].westWall = Instantiate(wall, new Vector3(r * size, -1, (c * size) - (size / 2f)), Quaternion.identity) as GameObject;
                    mazeCells[r, c].westWall.name = "West Wall " + r + "," + c;
                }

                if (!((r == 0 || r == 1 || r == 2 || r == 3 || r == 12 || r == 13 || r == 14 || r == 15) && (c == 12 || c == 13 || c == 14))
                     && !((r == 12 || r == 13 || r == 14 || r == 15) && (c == 0 || c == 1 || c == 2)) && !(r==1 && c==11))
                {
                    mazeCells[r, c].eastWall = Instantiate(wall, new Vector3(r * size, -1, (c * size) + (size / 2f)), Quaternion.identity) as GameObject;
                    mazeCells[r, c].eastWall.name = "East Wall " + r + "," + c;
                }
                if (r == 1 && c == 11) {
                    mazeCells[r, c].eastWall = Instantiate(Door, new Vector3(r * size, -1, (c * size) + (size / 2f)), Quaternion.identity) as GameObject;
                    mazeCells[r, c].eastWall.name = "Door " + r + "," + c;
                }

                if (r == 0)
                {
                    mazeCells[r, c].northWall = Instantiate(wall, new Vector3((r * size) - (size / 2f), -1, c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].northWall.name = "North Wall " + r + "," + c;
                    mazeCells[r, c].northWall.transform.Rotate(Vector3.up * 90f);
                }

               if (!((r==15)&&(c==14)) && !((r == 0 || r == 1 || r == 2 || r == 12 || r == 13 || r == 14) && (c == 12 || c == 13 ||c == 14 || c == 15)) 
                    && !((r == 12 || r == 13 || r == 14) && (c == 0 || c == 1 || c == 2 || c == 3)) && !((r == 11) && (c == 1)) && !((r == 11) && (c == 14)))
                {
                    mazeCells[r, c].southWall = Instantiate(wall, new Vector3((r * size) + (size / 2f), -1, c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].southWall.name = "South Wall " + r + "," + c;
                    mazeCells[r, c].southWall.transform.Rotate(Vector3.up * 90f);
                }

                if (((r == 15) && (c == 14))) {
                    mazeCells[r, c].southWall = Instantiate(Exit, new Vector3((r * size) + (size / 2f), -1, c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].southWall.name = "South Wall " + r + "," + c;
                    mazeCells[r, c].southWall.transform.Rotate(Vector3.up * 90f);
                }

                if(((r == 11) && (c == 1)) || ((r == 11) && (c == 14)))
                {
                    mazeCells[r, c].southWall = Instantiate(Door, new Vector3((r * size) + (size / 2f), -1, c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].southWall.name = "Door " + r + "," + c;
                    mazeCells[r, c].southWall.transform.Rotate(Vector3.up * 90f);
                }
                if ((r == 0 || r == 1 || r == 2 || r == 3) && (c == 12 || c == 13 || c == 14 || c == 15))
                {
                    mazeCells[r, c].visited = 3;
                    mazeCells[r, c].floor = Instantiate(floor2, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].floor.name = "Floor " + r + "," + c;
                    mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);
                }else if ((r == 12 || r == 13 || r == 14 || r == 15) && (c == 0 || c == 1 || c == 2 || c == 3 || c == 12 || c == 13 || c == 14 || c == 15))
                {
                    mazeCells[r, c].visited = 3;
                    mazeCells[r, c].floor = Instantiate(floor2, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].floor.name = "Floor " + r + "," + c;
                    mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);
                }else {
                    mazeCells[r, c].floor = Instantiate(floor1, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].floor.name = "Floor " + r + "," + c;
                    mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);

                }
            }
        }
    }
}
