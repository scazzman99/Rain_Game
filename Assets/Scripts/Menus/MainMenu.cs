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
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
            menuToggle = false;
            return;
        }
        else
        {
            settingsMenu.SetActive(true);
            mainMenu.SetActive(false);
            brightS.value = dirlight.intensity;
            ambientS.value = RenderSettings.ambientIntensity;

            ResSetup();
            menuToggle = true;
        }
    }

    private void ResSetup()
    {
        resolutions = Screen.resolutions;
        List<string> myResList = new List<string>();
        int resIndexCur = 0; //needs to be zero to use in dropdown.value
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "X" + resolutions[i].height;
            myResList.Add(option);
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
