using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance; 
	public AudioMixerGroup mixerGroup;
	public Sound[] sounds;

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
	}
	private void Start()
    {
		EventParam eventParam = new EventParam();
		eventParam.soundstr_ = "BossTheme";
		EventManager.TriggerEvent("PlayThemeMusic", eventParam);
    }

    private void OnEnable()
    {
		EventManager.StartListening("PlayThemeMusic", Play);
		EventManager.StartListening("AchievementEarned", Play);
		EventManager.StartListening("BabySquidSpawned", Play);
		EventManager.StartListening("CarDamaged", Play);
		EventManager.StartListening("BuildingDamaged", Play);
		EventManager.StartListening("SoldierHit", Play);
		EventManager.StartListening("GunFire", Play);
		EventManager.StartListening("IceCreamEaten", Play);
    }

    private void OnDisable()
    {
		EventManager.StopListening("PlayThemeMusic", Play);
		EventManager.StopListening("AchievementEarned", Play);
		EventManager.StopListening("BabySquidSpawned", Play);
		EventManager.StopListening("CarDamaged", Play);
		EventManager.StopListening("BuildingDamaged", Play);
		EventManager.StopListening("SoldierHit", Play);
		EventManager.StopListening("GunFire", Play);
		EventManager.StopListening("IceCreamEaten", Play);
	}

    private void OnApplicationQuit()
    {
		Destroy(this);
		EventManager.StopListening("PlayThemeMusic", Play);
		EventManager.StopListening("AchievementEarned", Play);
		EventManager.StopListening("BabySquidSpawned", Play);
		EventManager.StopListening("CarDamaged", Play);
		EventManager.StopListening("BuildingDamaged", Play);
		EventManager.StopListening("SoldierHit", Play);
		EventManager.StopListening("GunFire", Play);
		EventManager.StopListening("IceCreamEaten", Play);
	}

	public void Play(EventParam eventParam)
	{
		Sound s = Array.Find(sounds, item => item.name == eventParam.soundstr_);
		if (s == null) 
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}
}
