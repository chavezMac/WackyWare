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

    private GameObject planet;
    // Start is called before the first frame update
    void Start()

    {
        Invoke("startMusic", .7f);
        fireworks.SetActive(true);
        over = true;
        planeysToShootDown = 4;
        PlanetSpawner = GetComponent<PlanetSpawner>();
        textofPlanetToKill = PlanetSpawner.PlanetToKill;
        planet = GameObject.Find(PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[curretPlanet]]+"(Clone)");
        planet.transform.Find("Sphere").gameObject.SetActive(true);
        // Debug.Log(PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[0]]);
        // Debug.Log(textofPlanetToKill.GetComponent<TextMeshPro>().text);
        textofPlanetToKill.GetComponent<TextMeshPro>().text = PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[curretPlanet]];
        //PlanetSpawner.PlanetToKill.GetComponent<Text>().text = PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[0]];
    }

    // Update is called once per frame
    void Update()
    {
        // if (!planetKilled)
        // {
        //     textofPlanetToKill = PlanetSpawner.PlanetToKill;
        //     // Debug.Log(PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[0]]);
        //     // Debug.Log(textofPlanetToKill.GetComponent<TextMeshPro>().text);
        //     textofPlanetToKill.GetComponent<TextMeshPro>().text = PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[curretPlanet]];
        //     planet = PlanetSpawner.PlanetsPrefabs[PlanetSpawner.planetNumbers[curretPlanet]];
        //     planet.transform.Find("Sphere").gameObject.SetActive(true);
        //
        //
        //     Debug.Log(planet.transform.Find("Sphere").gameObject.name + planet.name);
        //
        // }
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
                        planet = GameObject.Find(PlanetSpawner.planetNames[PlanetSpawner.planetNumbers[curretPlanet]]+"(Clone)");
                        planet.transform.Find("Sphere").gameObject.SetActive(true);
                        planetKilled = false;
                    }
                }
                else
                {
                    GetComponent<AudioSource>().Stop();
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
                GetComponent<AudioSource>().Stop();
                MinigameBroadcaster.MinigameCompleted();
            }
        }

        // Check if time has expired
        if (MainGameController.timeRemaining <= 0)
        {
            GetComponent<AudioSource>().Stop();
            MinigameBroadcaster.MinigameFailed();
        }
    }

    void startMusic()
    {
        GetComponent<AudioSource>().Play();
    }
}
