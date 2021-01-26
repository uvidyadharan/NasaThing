using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;
	public Sound[] backgrounds;
    void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
		foreach (Sound s in backgrounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}
    private void Update()
    {
		System.Random random = new System.Random();
		if (!MusicPlaying())
		{
			int index = random.Next(0, backgrounds.Length);
			Play(backgrounds[index].name);
			Debug.Log("Playing: " + backgrounds[index].name);
		}
	}
    private bool MusicPlaying()
    {
		AudioSource[] sources = GetComponents<AudioSource>();
		foreach(AudioSource s in sources)
        {
			if(s.isPlaying)
            {
				return true;
            }
        }
		return false;

	}
    public void Play(string sound)
	{
		Sound s = Array.Find(backgrounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume;
		s.source.pitch = s.pitch;

		s.source.Play();
	}

}
