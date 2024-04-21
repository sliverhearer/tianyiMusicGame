using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartControl : MonoBehaviour
{
    public string songPath;
    public Text currentMusic;
    public void StartGame()
    {
        songPath = "";
        
    }
    public void getsong()
    {
        songPath = "MusicSheetJson";
        currentMusic.text = "current Song:Yiren";
    }
    public void selectSong(string musicSheetName)
    {
        PlayerPrefs.SetString("SelectedMusicSheet", musicSheetName);
        PlayerPrefs.Save();
    }
    public void into()
    {
        selectSong(songPath);
        SceneManager.LoadScene("SampleScene");
    }
}
