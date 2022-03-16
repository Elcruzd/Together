using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseUI;
    // public GameObject pauseUI2;

    public GameObject SettingsPage;
    public GameObject ControlsPage;
    public Slider SFXSlider;
    public Slider MusicSlider;
    
    public TMP_Dropdown ResolutionDropdown;
    Resolution[] resolutions;

    void Start()
    {
        // pauseUI.SetActive(false);

        SFXSlider.value = FindObjectOfType<AudioManager>().GetVolume(SoundType.SFX);
        MusicSlider.value = FindObjectOfType<AudioManager>().GetVolume(SoundType.Music);
        // pauseUI2.SetActive(false);

        resolutions = Screen.resolutions;

        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
         Debug.Log("in Pause: GameResolutionIndex = " + TitleScreen.GameResolutionIndex);
        
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (i == TitleScreen.GameResolutionIndex) {
                Debug.Log("in Pause: i = " + i);
                setResolution(TitleScreen.GameResolutionIndex);
                currentResolutionIndex = i;
            } 
            else if (TitleScreen.GameResolutionIndex == -1 && 
                     resolutions[i].width == Screen.currentResolution.width && 
                     resolutions[i].height == Screen.currentResolution.height) {

                    currentResolutionIndex = i;
                    Debug.Log(option);
            }
        }
        
        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();
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
        FindObjectOfType<AudioManager>().Stop("title_screen");
    }

    void Pause() {
        pauseUI.SetActive(true);
        SettingsPage.SetActive(true);
        ControlsPage.SetActive(false);
        // pauseUI2.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        FindObjectOfType<AudioManager>().Play("title_screen");
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void LoadCreditSceen() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credit");
    }

    public void playSFX() {
        FindObjectOfType<AudioManager>().Play("UI_select");
    }

    public void ExitButton() {
        playSFX();
        Application.Quit();
        // LoadCreditSceen();
    }

    public void MainMenuButton() {
        playSFX();
        LoadMenu();
    }

    public void ContinueButton() {
        playSFX();
        Resume();
    }

    public void setSFXVolume(float volume) {
        FindObjectOfType<AudioManager>().ChangeVolume(SoundType.SFX, volume);
    }

    public void setMusicVolume(float volume) {
        FindObjectOfType<AudioManager>().ChangeVolume(SoundType.Music, volume);
    }

    public void setResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        TitleScreen.GameResolutionIndex = resolutionIndex;
        Debug.Log("in Pause: GameResolutionIndex = " + TitleScreen.GameResolutionIndex);
    }

    public void SettingsButtonClicked() {
        playSFX();
        SettingsPage.SetActive(true);
        ControlsPage.SetActive(false);
    }

    public void ControlsButtonClicked() {
        playSFX();
        SettingsPage.SetActive(false);
        ControlsPage.SetActive(true);
    }

}
