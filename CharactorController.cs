using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorController : MonoBehaviour {

    private float speed = 12f;
    private float sensitivity = 4f;
    private CharacterController player;
    public GameObject Eyes;
    public GameObject Bullit;
    public Transform bulletSpawn;

    float moveFB;
    float moveLR;
    float rotx;
    float roty;


    // Use this for initialization
    void Start()
    {
        player = GetComponent<CharacterController>();
       
    }

    // Update is called once per frame
    void Update()
    {   
        //GetComponent<Rigidbody>().AddForce(Physics.gravity, ForceMode.Acceleration);
        moveFB = Input.GetAxis("Vertical") * speed;
        moveLR = Input.GetAxis("Horizontal") * speed;
        rotx = Input.GetAxis("Mouse X") * sensitivity;
        roty = Input.GetAxis("Mouse Y") * sensitivity;

        Vector3 movement = new Vector3(moveLR, 0, moveFB);
        transform.Rotate(0, rotx, 0);
        Eyes.transform.Rotate(-roty, 0, 0);
        movement = transform.rotation * movement;
        movement.y -= 9.8f * Time.deltaTime * 2000000;
        player.Move(movement * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && Global.flag1 ==1)
        {
            Fire();
            Global.flag1 = 0;
        }
    }


    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            Bullit,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speed*2f;
    }



    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.name == "key2" || collider.gameObject.name == "key1" || collider.gameObject.name == "key3") {
            Destroy(collider.gameObject);
            Global.keycounter = Global.keycounter + 1;
        }
        if (collider.gameObject.name == "gold")
        {
            Global.EnterRoom = 1;
            Global.goldcounter = Global.goldcounter + 1;
        }
        if (collider.gameObject.name == "Door 1,11")
        {
            Global.EnterRoom = 1;
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.name == "Door 11,1")
        {
            Global.EnterRoom = 2;
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.name == "Door 11,14")
        {
            Global.EnterRoom = 3;
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.name == "helper")
        {
            Global.flagEnterPatch++;
            if (Global.flagEnterPatch == 3) {
                Destroy(collider.gameObject);
            }
        }
    }



}
