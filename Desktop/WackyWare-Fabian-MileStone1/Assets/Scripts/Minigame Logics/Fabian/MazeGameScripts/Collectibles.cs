using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerMovement _playerMovement;
    private GameObject player;
    public int itemsCollected = 0;
    public Transform teleportSpot;
    
    void Start()
    {
        player = GameObject.Find("Skeleton_Warrior");
        _playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (itemsCollected == 2 && MainGameController.timeRemaining > 0  )
        {
            MinigameBroadcaster.MinigameCompleted();

        }
        else if ( itemsCollected < 2 && MainGameController.timeRemaining <= 0 )
        {
            MinigameBroadcaster.MinigameFailed();
        }
        
    }

}
