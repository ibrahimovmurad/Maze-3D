using System;
using static System.Convert;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
///Class that handles all things related to menu
public class Menu : MonoBehaviour
{
    public static bool GameRuns = false;
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject PauseMenu;
    public GameObject PlayMenu;
    public TMP_InputField MazeWidth;
    public TMP_InputField MazeHeight;
    public Toggle MazePerfect;
    public TMP_InputField MazePerfectness;
    public TMP_Dropdown ResolutionDropdown;
    Resolution[] resolutions;
    public TextMeshProUGUI Message;
    void Start()
    {
        //Fetching the applicable screen resolutions and applying the current screen resolution as default
        resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();
        List<string> res = new();
        int currentResIndex = 0;
        foreach (Resolution r in resolutions)
        {
            res.Add($"{r.width} x {r.height}");
            if (r.Equals(Screen.currentResolution))
            {
                currentResIndex = Array.IndexOf(resolutions, r);
            }
        }
        ResolutionDropdown.AddOptions(res);
        ResolutionDropdown.value = currentResIndex;
        ResolutionDropdown.RefreshShownValue();
    }
    void Update()
    {
        //Escape key functionality
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenu != null)
            {
                if (GameRuns)
                {
                    Pause();
                }
                else
                {
                    if (PauseMenu.gameObject.activeSelf)
                    {
                        Unpause();
                    }
                    else
                    {
                        OptionsMenu.SetActive(false);
                        PauseMenu.SetActive(true);
                    }
                }
            }
            else
            {
                OptionsMenu.SetActive(false);
                PlayMenu.SetActive(false);
                MainMenu.SetActive(true);
            }
        }
        //Fade effect for the displayed message
        if (PauseMenu == null)
        {
            Message.color = new Color(Message.color.r, Message.color.g, Message.color.b, Message.color.a - 0.005f);
        }
    }
    public void LoadMaze()
    {
        //If the user didn't enter the necessary fields, the warning message is displayed
        if (MazeWidth.text == "" || MazeHeight.text == "" || (!MazePerfect.isOn && MazePerfectness.text == ""))
        {
            Message.text="Please enter the necessary fields!";
            Message.color = new Color(Message.color.r, Message.color.g, Message.color.b, 1f);
        }
        //Storing the player preferences for the maze 
        else
        {
            PlayerPrefs.SetInt("MazeWidth", ToInt32(MazeWidth.text));
            PlayerPrefs.SetInt("MazeHeight", ToInt32(MazeHeight.text));
            PlayerPrefs.SetInt("MazePerfect", ToInt32(MazePerfect.isOn));
            if (!MazePerfect.isOn)
            {
                PlayerPrefs.SetInt("MazePerfectness", ToInt32(MazePerfectness.text));
            }
            SceneUp();
        }
    }
    public void ActiveRev(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }
    public static void SetGameRuns(bool runs){
        GameRuns = runs;
        Cursor.visible = !runs;
        Cursor.lockState = runs ? CursorLockMode.Locked : CursorLockMode.None;
    }
    public static void SceneUp()
    {
        SetGameRuns(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public static void SceneDown()
    {
        SetGameRuns(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void SetRes(int resIndex)
    {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    public void Fullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void Unpause()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        SetGameRuns(true);
    }
    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        SetGameRuns(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}