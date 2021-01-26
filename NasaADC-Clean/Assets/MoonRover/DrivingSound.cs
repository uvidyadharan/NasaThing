using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DrivingSound : MonoBehaviour
{
    public AudioClip highAccelClip;
    public float pitchMultiplier = 1f;                                          // Used for altering the pitch of audio clips
    public float lowPitchMin = 1f;                                              // The lowest possible pitch for the low sounds
    public float lowPitchMax = 6f;                                              // The highest possible pitch for the low sounds
    public float highPitchMultiplier = 0.25f;                                   // Used for altering the pitch of high sounds
    public float maxRolloffDistance = 500;                                      // The maximum distance where rollof starts to take place
    public float dopplerLevel = 1;                                              // The mount of doppler effect used in the audio
    public bool useDoppler = true;
    private bool m_StartedSound;
    private AudioSource m_HighAccel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float camDist = (Camera.main.transform.position - transform.position).sqrMagnitude;
        if (m_StartedSound && camDist > maxRolloffDistance * maxRolloffDistance)
        {
            stopSound();
            Debug.Log("stopped sound");
        }
        if (!m_StartedSound && camDist < maxRolloffDistance * maxRolloffDistance)
        {
            Debug.Log("started");
            startSound();
        }
        if (m_StartedSound)
        {
            float pitch = ULerp(lowPitchMin, lowPitchMax, (GetComponent<RoverControl>().currentSpeed / 3.6f)/20f);
            pitch = Mathf.Min(lowPitchMax, pitch);
            m_HighAccel.pitch = pitch * pitchMultiplier * highPitchMultiplier;
            m_HighAccel.dopplerLevel = useDoppler ? dopplerLevel : 0;
            m_HighAccel.volume = 1f;
        }
        if (Mathf.Round(GetComponent<RoverControl>().currentSpeed / 3.6f) == 0)
        {
            m_HighAccel.volume = 0f;
        }
       // Debug.Log(GetComponent<RoverControl>().currentSpeed / 3.6f);
    }
    private void startSound()
    {
        m_HighAccel = SetUpEngineAudioSource(highAccelClip);
        m_StartedSound = true;
    }
    private void stopSound()
    {
        //Destroy all audio sources on this object:
        foreach (var source in GetComponents<AudioSource>())
        {
            Destroy(source);
        }

        m_StartedSound = false;
    }
    private AudioSource SetUpEngineAudioSource(AudioClip clip)
    {
        // create the new audio source component on the game object and set up its properties
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = 0;
        source.loop = true;

        // start the clip from a random point
        source.time = Random.Range(0f, clip.length);
        source.Play();
        source.minDistance = 5;
        source.maxDistance = maxRolloffDistance;
        source.dopplerLevel = 0;
        return source;
    }
    private static float ULerp(float from, float to, float value)
    {
        return (1.0f - value) * from + value * to;
    }
}
