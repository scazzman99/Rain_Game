using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    Resolution[] resolutions;
    public TMP_Dropdown dropdown;
    public Light dirlight;
    public bool menuToggle;
    public Slider brightS, ambientS;
    public GameObject pauseMenuMain, pauseSettings;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                //makes  sure we return to main pause screen on pause
                menuToggle = true;
                Resume();
            } 
            else
            {
                Pause();
            }
        }
	}
    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ExitToMain()
    {
        
        //reload the main menu
        SceneManager.LoadScene(0);
        //So the game doesnt not run on reload
        Time.timeScale = 1;

    }

    public void OptionToggle()
    {
        if (menuToggle)
        {
            pauseMenuMain.SetActive(true);
            pauseSettings.SetActive(false);
            menuToggle = false;
            return;
        } else
        {
            pauseMenuMain.SetActive(false);
            pauseSettings.SetActive(true);
            brightS.value = dirlight.intensity;
            ambientS.value = RenderSettings.ambientIntensity;
            ResSetup();
            menuToggle = true;
            return;

        }
    }

    public void Brightness(float val)
    {
        dirlight.intensity = val;
    }

    public void Ambient(float val)
    {
        RenderSettings.ambientIntensity = val;
    }

    public void ChangeRes(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreen);
    }

    public void SetFullscreen(bool input)
    {
        Screen.fullScreen = input;
    }


    private void ResSetup()
    {
        //Get all resolutions of current monitor
        resolutions = Screen.resolutions;
        //Make list of strings to hold string counterparts of available resolutions
        List<string> myResList = new List<string>();
        int resIndexCur = 0; //needs to be zero to use in dropdown.value
        for (int i = 0; i < resolutions.Length; i++)
        {
            //get string counterpart of resolutions
            string option = resolutions[i].width + "X" + resolutions[i].height;
            myResList.Add(option);
            //if the current resolution of the screen matches the resolution in current point of array, Set dropdown index to current resolutions index
            if (Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height) //if our resolution matches one in the array set dropdown index to that entry
            {
                resIndexCur = i;
            }
        }
        dropdown.AddOptions(myResList);
        dropdown.value = resIndexCur;
        dropdown.RefreshShownValue();
    }

}
