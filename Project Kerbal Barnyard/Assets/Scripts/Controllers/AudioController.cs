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
    public AudioClip winSound;
    [Header("PlayState Dependencies")]
    private bool playVariant = true;

    [Header("SFX")]
    public AudioClip construction;
    // public AudioSource deny;

    public bool inMainMenu;

    private void Awake() {
        if (inMainMenu) {
            PlayMusic("Main Menu");
        }
    }

    public void PlayMusic(string musicName) {
        switch (musicName)
        {
            case "Main Menu":
                // Handle main menu music
                Debug.Log("Main Menu music selected.");
                musicSource.clip = mainMenu;
                StartCoroutine(StartFade(musicSource, 2f, 1f));
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
                StartCoroutine(FadeOut(musicSource, 2.2f, 1f));
                break;
            case "Result Sound Music":
                // Handle result sound
                Debug.Log("Result Sound selected.");
                musicSource.PlayOneShot(resultSound);
                break;
            case "Win Theme":
                // Handle result sound
                Debug.Log("Result Sound selected.");
                musicSource.PlayOneShot(winSound);
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

    public void PlayVariant() {
        if(playVariant) {
            PlayMusic("Play Music 2");
            playVariant = false;
        }
    }
    
    public IEnumerator PlayCountDownIntro() {
        yield return new WaitForSeconds(3.2f);
        PlayMusic("Play Music 1");
        playerController.Launch(); // forces ship to launch after
    }

    public void PlaySFX(string soundBite) {
        switch(soundBite) {
            case "construction":
            Debug.Log("Constructions");
            SFXSource.PlayOneShot(construction);
            break;
        }
    }

    public static IEnumerator StartFade(AudioSource music, float duration, float targetVol){
        music.Play();
        float currentTime = 0;
        float start = music.volume;

        while(currentTime < duration){
            currentTime += Time.deltaTime;
            music.volume = Mathf.Lerp(start, targetVol, currentTime/duration);
            yield return null;
        }
        yield break;
    }

    public IEnumerator FadeOut(AudioSource music, float duration, float duration2){
        float currentTime = 0;
        float start = music.volume;

        while(currentTime < duration){
            currentTime += Time.deltaTime;
            music.volume = Mathf.Lerp(start, 0, currentTime/duration);
            yield return null;
        }

        musicSource.clip = playTheme2;
        currentTime = 0;
        start = music.volume;
        music.Play();

        while(currentTime < duration2){
            currentTime += Time.deltaTime;
            music.volume = Mathf.Lerp(start, 1f, currentTime/duration2);
            yield return null;
        }
        
    }
}
