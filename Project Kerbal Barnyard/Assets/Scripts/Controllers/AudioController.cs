using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Tracks")]
    public AudioSource mainMenu;
    public AudioSource buildTheme;
    public AudioSource playThemeBuildUp;
    public AudioSource playTheme1;
    public AudioSource playTheme2;

    // [Header("SFX")]
    // public AudioSource confirm;
    // public AudioSource deny;

    public string startSong;

    private void Awake() {
        if(startSong != null) {
            if (startSong == "Main Menu") {
                mainMenu.Play();
                // StartCoroutine(StartFade(mainMenu, 2f, 1f));
            }
        }
    }

    public void PlayCountDownIntroHelper() {
        StartCoroutine(PlayCountDownIntro());
    }
    
    public IEnumerator PlayCountDownIntro() {
        playThemeBuildUp.Play();
        yield return new WaitForSeconds(3f);
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
