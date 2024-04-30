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
    
    public int planeysToShootDown = 4;
    private bool Lost = false;
    private bool killed = false;
    private int curretPlanet = 0;
    // Start is called before the first frame update
    void Start()

    {
        
        PlanetSpawner = GetComponent<PlanetSpawner>();
        textofPlanetToKill = PlanetSpawner.PlanetToKill;
        Debug.Log(PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[0]]);
        Debug.Log(textofPlanetToKill.GetComponent<TextMeshPro>().text);
        textofPlanetToKill.GetComponent<TextMeshPro>().text = PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[curretPlanet]];
        
        
        Debug.Log(curretPlanet);
        
        //PlanetSpawner.PlanetToKill.GetComponent<Text>().text = PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[0]];
    }

    // Update is called once per frame
    void Update()
    {

        // if (MainGameController.timeRemaining <= 0)
        // {
        //     MinigameBroadcaster.MinigameFailed();
        // }
        
        if (planetKilled == true )
        {

            if (curretPlanet < 4)
            {
                if (nameOfPlanet.Equals(
                        PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[curretPlanet]] + "(Clone)"))
                {



                    curretPlanet++;
                    Debug.Log(curretPlanet + nameOfPlanet);

                    planeysToShootDown--;
                    if (curretPlanet < 4)
                    {


                        textofPlanetToKill.GetComponent<TextMeshPro>().text =
                            PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[curretPlanet]];
                        planetKilled = false;
                    }

                }
                else
                {

                    Debug.Log("LOST");
                    planetKilled = false;
                }
            }
        }
        //if(PlanetSpawner.planetNames)
        // if (MainGameController.timeRemaining <= 0)
        // {
        //     MinigameBroadcaster.MinigameFailed();
        // }
        // else if (ballonsToShootDown == 0)
        // {
        //     MinigameBroadcaster.MinigameCompleted();
        // }
    }
}
