using UnityEngine.Audio;
using UnityEngine;
using System;

[System.Serializable]
public class AudioManager : MonoBehaviour {
	
	public Sound[] sounds;

	void Awake () {
		foreach (Sound sound in sounds) {
			sound.source = gameObject.AddComponent<AudioSource>();
			sound.source.clip = sound.clip;
		}
	}

	public void Play(string name) {
	 	Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Play();
	}
}
// FindObjectOfType<AudioManager>().Play();