using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Scene_Change : MonoBehaviour
{
    public void Change_Scene(string Scene_Name)
    {
        SceneManager.LoadScene(Scene_Name, LoadSceneMode.Single);
    }    

}
