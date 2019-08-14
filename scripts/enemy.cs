using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
    private GameObject wall;
    private int currentR;
    private int currentC;
    private Vector3 position = new Vector3();
    private List<List<int>> path = new List<List<int>>();
    private int count =0;
    // Use this for initialization
    void Start () {
        position = gameObject.transform.position;
        currentR = (int)System.Math.Ceiling((position.x) / 6f);
        currentC = (int)System.Math.Ceiling((position.x) / 6f);
        Global.cell[currentR, currentC].visited = 1;
        List<int> a = new List<int>();
        a.Add(Global.cell[currentR, currentC].row);
        a.Add(Global.cell[currentR, currentC].column);
        //rigidbody.isKinematic = true;
        //makelist(Global.cell);
    }

    // Update is called once per frame
    void Update()
    {
        //int direction = UnityEngine.Random.Range(1, 5);
        position = gameObject.transform.position;
        currentR = (int)System.Math.Ceiling((position.x) / 6f);
        currentC = (int)System.Math.Ceiling((position.x) / 6f);
        //go north 
        if ( Global.cell[currentR, currentC].neigbour[0, 0] != null
            && Global.cell[currentR, currentC].neigbour[0, 0].visited == 0)
        {
            //int distance = new Vector3();
            Global.cell[currentR, currentC].neigbour[0, 0].predirection = 1;
            gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.right * -6f;
            Global.cell[currentR, currentC].neigbour[0, 0].visited = 1;
            //gameObject.transform.position = transform.forward * -6f * Time.deltaTime;
            position = gameObject.transform.position;
            currentR = (int)System.Math.Ceiling((position.x ) / 6f);
            currentC = (int)System.Math.Ceiling((position.x ) / 6f);

            //gameObject.transform.position = transform.forward * -6f * Time.deltaTime;
        }
        // gameObject.transform.position += transform.right * -6f;
        //}
        else if (Global.cell[currentR, currentC].neigbour[0, 1] != null
            && Global.cell[currentR, currentC].neigbour[0, 1].visited == 0)
        {
            Global.cell[currentR, currentC].neigbour[0, 1].predirection = 2;
            //currentR++;
            // gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.right * 6f;
            gameObject.transform.position += transform.right * 6f * Time.deltaTime;
            Global.cell[currentR, currentC].neigbour[0, 1].visited = 1;
            position = gameObject.transform.position;
            currentR = (int)System.Math.Ceiling((position.x) / 6f);
            currentC = (int)System.Math.Ceiling((position.x) / 6f);

            //gameObject.transform.position += transform.right *Time.deltaTime;
        }
        else if (Global.cell[currentR, currentC].neigbour[1, 0] != null
            && Global.cell[currentR, currentC].neigbour[1, 0].visited == 0)
        {
            Global.cell[currentR, currentC].neigbour[1, 0].predirection = 3;
            //currentC--;
            gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * -6f;
            Global.cell[currentR, currentC].neigbour[1, 0].visited = 1;
            position = gameObject.transform.position;
            currentR = (int)System.Math.Ceiling((position.x) / 6f);
            currentC = (int)System.Math.Ceiling((position.x) / 6f);

            //gameObject.transform.position += transform.forward * -6f ;
        }
        else if (Global.cell[currentR, currentC].neigbour[1, 1] != null
            && Global.cell[currentR, currentC].neigbour[1, 1].visited == 0)
        {
            Global.cell[currentR, currentC].neigbour[1, 1].predirection = 4;
            //currentC++;
            //gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 6f;
            gameObject.transform.position += transform.forward * 6f * Time.deltaTime;
            position = gameObject.transform.position;
            currentR = (int)System.Math.Ceiling((position.x) / 6f);
            currentC = (int)System.Math.Ceiling((position.x) / 6f);
            Global.cell[currentR, currentC].neigbour[1, 1].visited = 1;

            
            //gameObject.transform.position += transform.forward * 6f ;
        }
        else {

            bool hasFreeNei = Route(currentR, currentC);
            if (hasFreeNei && Global.cell[currentR, currentC].predirection == 1)
            {
                gameObject.transform.position += transform.right * 6f * Time.deltaTime;
                position = gameObject.transform.position;
                currentR = (int)System.Math.Ceiling((position.x) / 6f);
                currentC = (int)System.Math.Ceiling((position.x) / 6f);
                //gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.right * 6f;

            }
            if (hasFreeNei && Global.cell[currentR, currentC].predirection == 2)
            {
                gameObject.transform.position += transform.right * -6f * Time.deltaTime;
                position = gameObject.transform.position;
                currentR = (int)System.Math.Ceiling((position.x) / 6f);
                currentC = (int)System.Math.Ceiling((position.x) / 6f);
                //gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.right * -6f;
                //gameObject.transform.position += transform.right * -6f ;
            }
            if (hasFreeNei && Global.cell[currentR, currentC].predirection == 3)
            {
                gameObject.transform.position += transform.forward * 6f * Time.deltaTime;
                position = gameObject.transform.position;
                currentR = (int)System.Math.Ceiling((position.x) / 6f);
                currentC = (int)System.Math.Ceiling((position.x) / 6f);
                //gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 6f;
                //gameObject.transform.position += transform.forward * 6f ;

            }
            if (hasFreeNei && Global.cell[currentR, currentC].predirection == 4)
            {
                gameObject.transform.position += transform.forward * -6f * Time.deltaTime;
                position = gameObject.transform.position;
                currentR = (int)System.Math.Ceiling((position.x - 3f) / 6f);
                currentC = (int)System.Math.Ceiling((position.x - 3f) / 6f);
                //gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * -6f;
                //gameObject.transform.position += transform.forward * -6f ;
            }
        }
    }

    //}

private bool Route(int row, int column)
{
    int availableRoutes = 0;

    if (row > 0 && Global.cell[row - 1, column].visited == 0)
    {
        availableRoutes++;
    }

    if (row < 15 && Global.cell[row + 1, column].visited == 0)
    {
        availableRoutes++;
    }

    if (column > 0 && Global.cell[row, column - 1].visited == 0)
    {
        availableRoutes++;
    }

    if (column < 15 && Global.cell[row, column + 1].visited == 0)
    {
        availableRoutes++;
    }

    return (availableRoutes > 0);
}

    void OnCollisionEnter(Collision otherCol)
    {
       // otherCol.rigidbody.isKinematic = true;
        count = 1;
        if (otherCol.gameObject.name  == "player")
        {
            Destroy(otherCol.gameObject);
        }
    }

}
