using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManControl : MonoBehaviour
{
    public GameObject UIShow;
    public GameObject ButtomStop;
    public AudioSource audioSource;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Start()
    {
        UIShow.SetActive(false);
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0) { Time.timeScale = 0f; UIShow.SetActive(true); }
            else { Time.timeScale = 1f; UIShow.SetActive(false); }
        }
    }
    public void Close()
    {
        Application.Quit();
    }
    public void Show()
    {
        UIShow.SetActive(true);
        Time.timeScale = 0;
        ButtomStop.SetActive(false);
        audioSource.Pause();
    }
    public void jixu()
    {
        UIShow.SetActive(false);
        Time.timeScale = 1f;
        ButtomStop.SetActive(true);
        audioSource.UnPause();
    }
}
