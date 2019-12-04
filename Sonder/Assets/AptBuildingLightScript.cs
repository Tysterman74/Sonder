using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AptBuildingLightScript : MonoBehaviour
{
    private List<GameObject> lights;
    public float secondsUntilLightTrigger = 50.0f;

    private float timer = 0.0f;
    private int lastLightIndex = -1;
    public int maxLightOnCount = 5;
    private int currentLightOnCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = secondsUntilLightTrigger;
        lights = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.tag == "BuildingLight")
            {
                lights.Add(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0.0f)
        {
            int lightIndex = Random.Range(0, lights.Count - 1);
            GameObject light = lights[lightIndex];
            bool active = light.activeSelf;

            if (lightIndex != lastLightIndex)
            {
                if (currentLightOnCount <= maxLightOnCount && !active)
                {
                    light.SetActive(true);
                    currentLightOnCount++;
                    lastLightIndex = lightIndex;
                }
                else if (active)
                {
                    light.SetActive(false);
                    currentLightOnCount--;
                    lastLightIndex = lightIndex;
                }
            }
            timer = secondsUntilLightTrigger;
        }
    }
}
