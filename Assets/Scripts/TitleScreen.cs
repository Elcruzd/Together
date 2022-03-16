using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    [HideInInspector]
    public GameObject OptionsPages;
    [HideInInspector]
    public GameObject SettingsPage;
    [HideInInspector]
    public GameObject ControlsPage;

    public Slider SFXSlider;
    public Slider MusicSlider;
    public TMP_Dropdown ResolutionDropdown;

    public static int GameResolutionIndex = -1;

    Resolution[] resolutions;

    void Awake() {
        OptionsPages = GameObject.Find("OptionsPages");
        SettingsPage = GameObject.Find("SettingsPage");
        ControlsPage = GameObject.Find("ControlsPage");

        // SFXSlider = UI.Find("SFX_Slider");
        // MusicSlider = GameObject.Find("Music_Slider");

        OptionsPages.SetActive(false);
        SFXSlider.value = FindObjectOfType<AudioManager>().GetVolume(SoundType.SFX);
        MusicSlider.value = FindObjectOfType<AudioManager>().GetVolume(SoundType.Music);
    }

    void Start() {
        resolutions = Screen.resolutions;

        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (i == GameResolutionIndex) {
                setResolution(GameResolutionIndex);
                currentResolutionIndex = i;
            }
            else if (GameResolutionIndex == -1 &&
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

    public void setResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        GameResolutionIndex = resolutionIndex;
        Debug.Log("in Title: GameResolutionIndex = " + GameResolutionIndex);
    }

    public void setSFXVolume(float volume) {
        FindObjectOfType<AudioManager>().ChangeVolume(SoundType.SFX, volume);
    }

    public void setMusicVolume(float volume) {
        FindObjectOfType<AudioManager>().ChangeVolume(SoundType.Music, volume);
    }
    public void ButtonClicked() {
        FindObjectOfType<AudioManager>().Play("UI_select");
    }

    public void OptionsButtonClicked() {
        ButtonClicked();
        OptionsPages.SetActive(true);
        SettingsPage.SetActive(true);
        ControlsPage.SetActive(false);
    }

    public void SettingsButtonClicked() {
        ButtonClicked();
        SettingsPage.SetActive(true);
        ControlsPage.SetActive(false);
    }

    public void ControlsButtonClicked() {
        ButtonClicked();
        SettingsPage.SetActive(false);
        ControlsPage.SetActive(true);
    }

    public void OptionsExitButtonClicked() {
        ButtonClicked();
        OptionsPages.SetActive(false);
    }

    public void LoadCreditSceen() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credit");
    }

    public void MainExitButtonClicked() {
        ButtonClicked();
        Application.Quit();
        // LoadCreditSceen();
    }
}
