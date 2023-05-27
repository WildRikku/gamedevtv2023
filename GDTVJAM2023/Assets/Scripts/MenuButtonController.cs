
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour
{
    public string gameScene = "GameScene";
    public Slider _musicSlider, _sfxSlider;

    public void Start()
    {
        if (_musicSlider != null)
        {
            MusicVolume();
            SFXVolume();
        }
    }


    //Main Menu
    public void GameStart()
    {
        AudioManager.Instance.PlaySFX("MouseKlick");
        SceneManager.LoadScene(gameScene);
    }
    public void Credits()
    {
        AudioManager.Instance.PlaySFX("MouseKlick");
        SceneManager.LoadScene("CreditScene");
    }

    public void GameQuit()
    {
        AudioManager.Instance.PlaySFX("MouseKlick");
        Application.Quit();
    }

    //Cedit Scene
    public void BacktoMainMenue()
    {
        AudioManager.Instance.PlaySFX("MouseKlick");
        SceneManager.LoadScene("MenueScene");
    }

    public void LevelRestart()
    {
        AudioManager.Instance.PlaySFX("MouseKlick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /*public void SetTimesclae()
    {
        Time.timeScale = 1;
    }*/
    void OnMouseEnter()
    {
        AudioManager.Instance.PlaySFX("MouseHover");
    }


    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
    public void ToggleSfx()
    {
        AudioManager.Instance.ToggleSFX();
    }
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }
}
