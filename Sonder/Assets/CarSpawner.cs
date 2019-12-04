using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carSpawn;

    public CarSpawner otherSpawner;

    public bool hasSpawned = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnCar(GameObject car, Quaternion rotation)
    {
        print(carSpawn.transform);
        //Instantiate(car, carSpawn.transform, true);
        Instantiate(car, carSpawn.transform.position, rotation);
        hasSpawned = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "SpawnedCar")
        {
            Destroy(collision.gameObject);
            otherSpawner.hasSpawned = false;
        }
    }
}
