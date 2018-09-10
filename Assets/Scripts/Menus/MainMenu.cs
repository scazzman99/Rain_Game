using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    #region Vars
    Resolution[] resolutions;
    public TMP_Dropdown dropdown;
    public Light dirlight;
    public bool menuToggle;
    public GameObject mainMenu, settingsMenu;
    public Slider brightS, ambientS;
    


    #endregion
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        //load the first scene in build
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetBrightness(float val) //Uses dynamic float to handle value grabbing and setting
    {
        dirlight.intensity = val;
    } 

    public void ChangeMenu()
    {
        if (menuToggle)
        {
            //set main menu to true
            mainMenu.SetActive(true);
            //set settings menu to false
            settingsMenu.SetActive(false);
            //change menu toggle for the next call
            menuToggle = false;
            return;
        }
        else
        {
            settingsMenu.SetActive(true);
            mainMenu.SetActive(false);
            //set brightness slider and ambient slider initial value to their current intensities
            brightS.value = dirlight.intensity;
            ambientS.value = RenderSettings.ambientIntensity;
            //run ressetup to get all available resolutions
            ResSetup();
            menuToggle = true;
        }
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

    public void SetAmbient(float val)
    {
        RenderSettings.ambientIntensity = val;
    }

    public void ChangeRes(int index)
    {
        Resolution selectedRes = resolutions[index];
        Screen.SetResolution(selectedRes.width, selectedRes.height, Screen.fullScreen);
    }

    public void FullScreen(bool toggle)
    {

        Screen.fullScreen = toggle;
    }

}
