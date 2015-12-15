using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsHandler : MonoBehaviour {

	public Button musicButton;
	public Button sfxButton;
	public Sprite musicOnSprite;
	public Sprite musicOffSprite;
	public Sprite sfxOnSprite;
	public Sprite sfxOffSprite;
	public bool music = true;
	public bool sfx = true;

	AudioSource musicFile;

	public void ToggleMusic () {
		music = !music;

		if (music) {
			musicButton.image.sprite = musicOnSprite;
		} else {
			musicButton.image.sprite = musicOffSprite;
		}
	}

	public void ToggleSFX () {
		sfx = !sfx;

		if (sfx) {
			sfxButton.image.sprite = sfxOnSprite;
		} else {
			sfxButton.image.sprite = sfxOffSprite;
		}
	}

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		musicFile = GetComponent<AudioSource>();
	}

	void OnLevelWasLoaded(int level) {
		if (level == 0) {
			musicButton = GameObject.FindWithTag("MusicButton").GetComponent<Button>();
			sfxButton = GameObject.FindWithTag("SFXButton").GetComponent<Button>();
			if (music) {
				musicButton.image.sprite = musicOnSprite;
			} else {
				musicButton.image.sprite = musicOffSprite;
			}

			if (sfx) {
				sfxButton.image.sprite = sfxOnSprite;
			} else {
				sfxButton.image.sprite = sfxOffSprite;
			}
		}

		if (level == 1) {
			if (music) {
				musicFile.Play();
			}
		}
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
