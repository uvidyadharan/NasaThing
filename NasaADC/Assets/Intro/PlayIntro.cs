using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayIntro : MonoBehaviour {

    private string movie = "LoadingScreenAnimation.mov";

    void Start () 
    {
        StartCoroutine(streamVideo(movie));
    }

    private IEnumerator streamVideo(string video)
    {
        Handheld.PlayFullScreenMovie(video, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.Fill);
        yield return new WaitForEndOfFrame ();
        SceneManager.LoadScene ("MainScene");
    }
}