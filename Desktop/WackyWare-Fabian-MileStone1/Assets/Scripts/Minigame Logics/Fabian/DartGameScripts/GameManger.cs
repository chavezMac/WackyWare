using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour

{
    private PlanetSpawner PlanetSpawner;
    public bool planetKilled = false;
    private GameObject textofPlanetToKill;
    [NonSerialized]
    public string nameOfPlanet = " ";
    

    public bool over;
    public int planeysToShootDown;
    public GameObject fireworks;
    private bool Lost = false;
    private bool killed = false;
    private int curretPlanet = 0;
    // Start is called before the first frame update
    void Start()

    {
        fireworks.SetActive(true);
        over = true;
        planeysToShootDown = 4;
        PlanetSpawner = GetComponent<PlanetSpawner>();
        textofPlanetToKill = PlanetSpawner.PlanetToKill;
       // Debug.Log(PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[0]]);
       // Debug.Log(textofPlanetToKill.GetComponent<TextMeshPro>().text);
        textofPlanetToKill.GetComponent<TextMeshPro>().text = PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[curretPlanet]];
        
        
        Debug.Log(planeysToShootDown);
        
        //PlanetSpawner.PlanetToKill.GetComponent<Text>().text = PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[0]];
    }

    // Update is called once per frame
    void Update()
    {
        if (planetKilled)
        {
            if (curretPlanet < 4)
            {
                if (nameOfPlanet.Equals(PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[curretPlanet]] + "(Clone)"))
                {
                    curretPlanet++;
                    if (curretPlanet < 4)
                    {
                        textofPlanetToKill.GetComponent<TextMeshPro>().text = PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[curretPlanet]];
                        planetKilled = false;
                    }
                }
                else
                {
                    MinigameBroadcaster.MinigameFailed();
                    Debug.Log("LOST");
                    planetKilled = false;
                }
            }
        }

        // Check if all planets are shot down
        if (planeysToShootDown == 0)
        {
            over = true;

            // Check if time has not expired
            if (MainGameController.timeRemaining > 0)
            {
                MinigameBroadcaster.MinigameCompleted();
            }
        }

        // Check if time has expired
        if (MainGameController.timeRemaining <= 0)
        {
            MinigameBroadcaster.MinigameFailed();
        }
    }


}
