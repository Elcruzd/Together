using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseUI;
    // public GameObject pauseUI2;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider MusicSlider;

    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        // pauseUI2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    void Resume() {
        pauseUI.SetActive(false);
        // pauseUI2.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        FindObjectOfType<AudioManager>().Pause("title_screen");
    }

    void Pause() {
        pauseUI.SetActive(true);
        // pauseUI2.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        FindObjectOfType<AudioManager>().Play("title_screen");
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ExitButton() {
        LoadMenu();
    }

    public void ContinueButton() {
        Resume();
    }

    public void ChangeSFXVolume() {
        // AudioListener.volume = SFXSlider.value;
        FindObjectOfType<AudioManager>().ChangeSFXVolume(SoundType.SFX, SFXSlider.value);
    }

    public void ChangeMusicVolume() {
        FindObjectOfType<AudioManager>().ChangeSFXVolume(SoundType.Music, MusicSlider.value);
        // AudioListener.volume = SFXSlider.value;
    }
}
