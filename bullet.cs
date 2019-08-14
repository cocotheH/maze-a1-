using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if ((gameObject.transform.position.x < -3 || gameObject.transform.position.x > 93) || 
            (gameObject.transform.position.z < -3 || gameObject.transform.position.z > 93))
        {
            Destroy(gameObject);
            Global.flag1 = 1;
        }
		
	}

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.name == "enemy1" || collision.gameObject.name == "enemy2" || collision.gameObject.name == "enemy3")
        {
            int possibility = Random.Range(1, 5);
            if (possibility == 1)
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
                Global.flag1 = 1;
            }
        }
        else {
            Destroy(gameObject);
            Global.flag1 = 1;
        }

    }

 }
