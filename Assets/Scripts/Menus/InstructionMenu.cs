﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionMenu : MonoBehaviour {

	
    public void Continue()
    {
        SceneManager.LoadScene(2);
        
    }
}
