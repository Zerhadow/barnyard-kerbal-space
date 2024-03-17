using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public PlayerController playerController;
    
    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource SFXSource;
    [Header("Audio Clips")]
    public AudioClip mainMenu;
    public AudioClip buildTheme;
    public AudioClip playThemeBuildUp;
    public AudioClip playTheme1;
    public AudioClip playTheme2;
    public AudioClip resultSound;

    // [Header("SFX")]
    // public AudioSource confirm;
    // public AudioSource deny;

    public string startSong;

    private void Awake() {
        if(startSong != null) {
            if (startSong == "Main Menu") {
                PlayMusic("Main Menu");
                // StartCoroutine(StartFade(mainMenu, 2f, 1f));
            }
        }
    }

    public void PlayMusic(string musicName) {
        switch (musicName)
        {
            case "Main Menu":
                // Handle main menu music
                Debug.Log("Main Menu music selected.");
                musicSource.clip = mainMenu;
                musicSource.Play();
                break;
            case "Build Music":
                // Handle build theme music
                Debug.Log("Build Theme music selected.");
                musicSource.clip = buildTheme;
                musicSource.Play();
                break;
            case "Build Up Music":
                // Handle play theme build-up music
                Debug.Log("Play Theme Build-Up music selected.");
                musicSource.clip = playThemeBuildUp;
                musicSource.Play();
                break;
            case "Play Music 1":
                // Handle play theme 1 music
                Debug.Log("Play Theme 1 music selected.");
                musicSource.clip = playTheme1;
                musicSource.Play();
                break;
            case "Play Music 2":
                // Handle play theme 2 music
                Debug.Log("Play Theme 2 music selected.");
                musicSource.clip = playTheme2;
                musicSource.Play();
                break;
            case "Result Sound Music":
                // Handle result sound
                Debug.Log("Result Sound selected.");
                musicSource.PlayOneShot(resultSound);
                break;
            default:
                // Handle default case (when musicName does not match any of the above cases)
                Debug.LogWarning("Unknown music name: " + musicName);
                break;
        }
    }

    public void PlaySFX(AudioClip clip) {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayCountDownIntroHelper() {
        PlayMusic("Build Up Music");
        StartCoroutine(PlayCountDownIntro());
    }
    
    public IEnumerator PlayCountDownIntro() {
        yield return new WaitForSeconds(3.2f);
        PlayMusic("Play Music 1");
        playerController.Launch(); // forces ship to launch after
    }

    public static IEnumerator StartFade(AudioSource music, float duration, float targetVol){
        float currentTime = 0;
        float start = music.volume;

        while(currentTime < duration){
            currentTime += Time.deltaTime;
            music.volume = Mathf.Lerp(start, targetVol, currentTime/duration);
            yield return null;
        }
        yield break;
    }
}
