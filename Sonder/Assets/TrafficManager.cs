using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    public GameObject[] carToSpawn;
    public int additionalBSpawnRotation;
    public int additionalASpawnRotation;

    private GameObject spawnAObj;
    private CarSpawner aSpawner;

    private GameObject spawnBObj;
    private CarSpawner bSpawner;

    private float timeToWait = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "SpawnA")
            {
                spawnAObj = child.gameObject;
                aSpawner = child.GetComponent<CarSpawner>();
                foreach (Transform c in spawnAObj.transform)
                {
                    if (c.name == "CarSpawn")
                    {
                        aSpawner.carSpawn = c.gameObject; 
                    }
                }
            }
            else if (child.name == "SpawnB")
            {
                spawnBObj = child.gameObject;
                bSpawner = child.GetComponent<CarSpawner>();
                foreach (Transform c in spawnBObj.transform)
                {
                    if (c.name == "CarSpawn")
                    {
                        bSpawner.carSpawn = c.gameObject;
                    }
                }
            }
        }

        aSpawner.otherSpawner = bSpawner;
        bSpawner.otherSpawner = aSpawner;

        timeToWait = Random.Range(5.0f, 15.0f);
        print("Time to wait: " + timeToWait);
    }

    // Update is called once per frame
    void Update()
    {
        timeToWait -= Time.deltaTime;
        if (timeToWait < 0)
        {
            int spawner = Random.Range(0, 2);
            GameObject selectedCar = carToSpawn[Random.Range(0, carToSpawn.Length)];
            if (spawner == 0)
            {
                if (!bSpawner.hasSpawned)
                {
                    selectedCar.GetComponent<CarEngine>().direction = Vector3.forward;
                    Quaternion rotation = selectedCar.transform.rotation;
                    rotation = Quaternion.Euler(new Vector3(rotation.x, rotation.y + 90 + additionalBSpawnRotation, rotation.z));
                    bSpawner.spawnCar(selectedCar, rotation);
                }
            }
            else
            {
                if (!aSpawner.hasSpawned)
                {
                    selectedCar.GetComponent<CarEngine>().direction = Vector3.forward;
                    Quaternion rotation = selectedCar.transform.rotation;
                    rotation = Quaternion.Euler(new Vector3(rotation.x, rotation.y + additionalASpawnRotation, rotation.z));

                    //citySpawner.spawnCar(selectedCar, rotation);
                    aSpawner.spawnCar(selectedCar, rotation);
                }
            }
            timeToWait = Random.Range(5.0f, 10.0f);
        }
    }
}
