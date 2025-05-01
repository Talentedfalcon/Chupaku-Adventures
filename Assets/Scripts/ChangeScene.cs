using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void start_game(){
        SceneManager.LoadScene("Main Game");
    }

    public void main_menu(){
        SceneManager.LoadScene("Main Menu");
    }

    public void toggle_info(){
        Transform child=transform.Find("HowToPlayCard");
        if(child.gameObject.activeSelf){
            child.gameObject.SetActive(false);
        }
        else{
            child.gameObject.SetActive(true);
        }
    }
}
