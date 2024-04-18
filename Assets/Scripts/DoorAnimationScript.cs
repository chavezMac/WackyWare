using UnityEngine;

public class DoorAnimationController : MonoBehaviour
{
    public Animator doorAnimator;

    void Start()
    {
        Play();
    }

    public void Play()
    {
        doorAnimator.Play("hello_door_animation", 0, 0f); // Play from the start
        doorAnimator.speed = 1; // Set animation speed to 1
    }

    // Function to pause the door animation
    public void PauseAnimationAtMiddle()
    {
        doorAnimator.speed = 0; // Set animation speed to 0 to pause
    }

    // Function to resume the door animation
    public void ResumeAnimation()
    {
        doorAnimator.speed = 1; // Set animation speed to 1 to resume
    }
}