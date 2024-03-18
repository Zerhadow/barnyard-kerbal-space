using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public AudioController _controller;
    [Header("Video Source")]
    public VideoPlayer videoSource;
    [Header("Video Clips")]
    public VideoClip intro;
    public VideoClip loseCat;
    public VideoClip loseChicken;
    public VideoClip loseCow;
    public VideoClip winCat;
    public VideoClip winChicken;
    public VideoClip winCow;
    public VideoClip credits;
    
    public void PlayVideo(string clip)
    {
        // Switch case to handle different video clips
        switch (clip)
        {
            case "intro":
                // Play the intro video
                Debug.Log("Playing intro video");
                videoSource.clip = intro;
                videoSource.Play();
                break;
            case "loseCat":
                // Play the loseCat video
                Debug.Log("Playing loseCat video");
                videoSource.clip = loseCat;
                videoSource.Play();
                break;
            case "loseChicken":
                // Play the loseChicken video
                Debug.Log("Playing loseChicken video");
                videoSource.clip = loseChicken;
                videoSource.Play();
                break;
            case "loseCow":
                // Play the loseCow video
                Debug.Log("Playing loseCow video");
                videoSource.clip = loseCow;
                videoSource.Play();
                break;
            case "winCat":
                // Play the winCat video
                Debug.Log("Playing winCat video");
                videoSource.clip = winCat;
                videoSource.Play();
                break;
            case "winChicken":
                // Play the winChicken video
                Debug.Log("Playing winChicken video");
                videoSource.clip = winChicken;
                videoSource.Play();
                break;
            case "winCow":
                // Play the winCow video
                Debug.Log("Playing winCow video");
                videoSource.clip = winCow;
                videoSource.Play();
                break;
            default:
                // Handle default case (if the provided video clip name does not match any case)
                Debug.LogWarning("Unknown video clip: " + clip);
                break;
        }
    }

    public void PlayCredits() {
        _controller.musicSource.Pause();
        videoSource.clip = credits;
        videoSource.Play();
    }
}
