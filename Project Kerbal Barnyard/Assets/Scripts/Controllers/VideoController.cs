using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
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
                break;
            case "loseChicken":
                // Play the loseChicken video
                Debug.Log("Playing loseChicken video");
                break;
            case "loseCow":
                // Play the loseCow video
                Debug.Log("Playing loseCow video");
                break;
            case "winCat":
                // Play the winCat video
                Debug.Log("Playing winCat video");
                break;
            case "winChicken":
                // Play the winChicken video
                Debug.Log("Playing winChicken video");
                break;
            case "winCow":
                // Play the winCow video
                Debug.Log("Playing winCow video");
                break;
            default:
                // Handle default case (if the provided video clip name does not match any case)
                Debug.LogWarning("Unknown video clip: " + clip);
                break;
        }
    }
}
