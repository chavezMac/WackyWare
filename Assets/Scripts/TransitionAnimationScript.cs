using UnityEngine;
using UnityEngine.Serialization;

public class TransitionAnimationController : MonoBehaviour
{
    public Animator transitionAnimator;
    private AudioSource sfx;
    public AudioClip clap;

    void Start()
    {
        transitionAnimator = GetComponent<Animator>();
        sfx = GetComponent<AudioSource>();
        sfx.clip = clap;
    }

    public void init()
    {
        transitionAnimator.Play("ClapperAnimation2", 0, .43f);
        transitionAnimator.speed = 1;
    }

    public void Play()
    {
        transitionAnimator.Play("ClapperAnimation2", 0, 0f); // Play from the start
        transitionAnimator.speed = 1;
    }

    // Function to pause the door animation
    public void PauseAnimationAtMiddle()
    {
        transitionAnimator.speed = 0;
        sfx.Play();
    }

    // Function to resume the door animation
    public void ResumeAnimation()
    {
        transitionAnimator.speed = 1;
    }
}