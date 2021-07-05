using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioController audioController;
    
    public float sens = 100;

    private int points;
    public int deadCount;
    public float time;
    public int ammo;

    public MMFeedbacks shakeFeedback;
    
    private bool _gameover = false;
    public bool pause = false;
    //private bool mute;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        Application.targetFrameRate = 300;
        
        if (audioController)
        {
            Instantiate(audioController);
            AudioController.Instance.init();
        }
        
        points = 0;
        Time.timeScale = 1;
        //if (SceneManager.GetActiveScene().name == "Start")        
        //    Cursor.lockState = CursorLockMode.None;        
        //else
        //    Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (_gameover)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {        
            if (!pause)
            {
                EnablePause();
            }
            else
            {
                DisablePause();
            }
        }
    }

    public void EnablePause()
    {
        pause = true;
        AudioController.Instance.pauseMute = true;
        //Cursor.lockState = CursorLockMode.None;
        UIController.Instance.optionsPanel.SetActive(true);
        UIController.Instance.whilePlayPanel.SetActive(false);
        ChangeTimeScale();

        if (AudioController.Instance.globalMute)
            return;

        MuteAllSounds();
    }

    public void DisablePause()
    {
        pause = false;
        AudioController.Instance.pauseMute = false;
        //Cursor.lockState = CursorLockMode.Locked;
        UIController.Instance.optionsPanel.SetActive(false);
        UIController.Instance.whilePlayPanel.SetActive(true);
        ChangeTimeScale();

        if (AudioController.Instance.globalMute)
            return;

        MuteAllSounds();
    }

    public void ChangeTimeScale()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void RestartGame()
    {
        var currScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currScene);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void Menu_Button()
    {
        SceneManager.LoadScene("Start");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnGameOver()
    {
        _gameover = true;
        UIController.Instance.SetDeathUI();
        //Cursor.lockState = CursorLockMode.None;
    }

    public void OnDestroyEnemy(int points)
    {
        CountPoints(points);
        PlayFeedbacks();
        UIController.Instance.SetScoreText(points, deadCount);
    }
    public void CountPoints(int _points)
    {
        points += _points;
        deadCount++;
    }

    public void UpdateTimer(float _time)
    {
        time = _time;
        UIController.Instance.timeText.text = "Next wave    \n" + time.ToString("0");
    }

    public void UpdateAmmo(int _ammo)
    {
        ammo = _ammo;
        UIController.Instance.ammoText.text = ammo.ToString() + "  Ammo";
    }

    public void PlayFeedbacks()
    {
        shakeFeedback.PlayFeedbacks();
    }

    public void MuteBgMusic()
    {
        AudioController.Instance.MuteBgMusic();
    }

    public void MuteAllSounds()
    { 
        AudioController.Instance.MuteAllSounds();    
    }

    public void GlobalMute()
    {
        AudioController.Instance.globalMute = !AudioController.Instance.globalMute;  
    }

    public void SetSensetivity()
    {
        sens = UIController.Instance.sensSlider.value;
    }
}
