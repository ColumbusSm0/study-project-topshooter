using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDControl : MonoBehaviour
{
    private PlayerControl scriptPlayerControl;
    public  Slider SliderPlayerLife;

    public GameObject GameOverPanel;

    public int AnimationSpeedMultiplier = 8;

    float timer = 0f;
    float actualValue = 0f; // the goal
    float startValue = 0f; // animation start value
    float displayValue = 0f; // value during animation

    // Start is called before the first frame update
    void Start()
    {
        scriptPlayerControl = GameObject.FindWithTag(Tags.Player).GetComponent<PlayerControl>();
        SliderPlayerLife.maxValue = scriptPlayerControl.myPlayerStats.Life;
        UpdateSliderPlayerLife();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * AnimationSpeedMultiplier;
        displayValue = Mathf.Lerp(startValue, actualValue, timer);
        SliderPlayerLife.value = displayValue;
    }
    public void UpdateSliderPlayerLife() 
    {
        actualValue = scriptPlayerControl.myPlayerStats.Life;
        startValue = SliderPlayerLife.value; // remember amount at animation start
        timer = 0f; // reset timer.
        
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void restartGame()
    {
        SceneManager.LoadScene("game");
    }
}