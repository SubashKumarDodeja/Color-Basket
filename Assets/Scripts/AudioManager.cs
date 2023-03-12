using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;


	public AudioSource basketSound;
	public AudioSource failSound;
	public AudioSource tapSound;
	private void Awake()
	{


		if (Instance == null)
		{
			Instance = this;
		}

		else if (Instance != this)
		{
			Destroy(gameObject);
		}


		DontDestroyOnLoad(gameObject);
	}

	public void PlayBasketSound()
	{
		basketSound.Play();
	}
	public void PlayFailSound()
	{
		failSound.Play();
	}
	public void PlayTapSound()
	{
		tapSound.Play();
	}
}
