using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public Text score_text;
    public Text score;

    void Start()
    {
        int high_score = PlayerPrefs.GetInt("save");
        int incoming_score = PlayerPrefs.GetInt("score_save");

        score_text.text="High Score: "+high_score;
        score.text = "Score:" + incoming_score;
    }

     
    void Update()
    {
        
    }

    public void play()
    {
        SceneManager.LoadScene("level");
    }

    public void exit()
    {
        Application.Quit();
    }
}
