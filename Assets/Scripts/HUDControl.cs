using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HUDControl : MonoBehaviour
{
    private PlayerControl scriptPlayerControl;
    public  Slider SliderPlayerLife;
    public IconDisplayControl IconHolderControl;
    public TextMeshProUGUI MagazineText;
    public TextMeshProUGUI AmmoText;

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

    private void OnEnable()
    {
        WeaponControl.ShootHUDEvent += UpdateWeaponValuesAndIcon;
        WeaponControl.ReloadFinishedEvent += UpdateWeaponValuesAndIcon;
    }

    private void OnDisable()
    {
        WeaponControl.ShootHUDEvent -= UpdateWeaponValuesAndIcon;
        WeaponControl.ReloadFinishedEvent -= UpdateWeaponValuesAndIcon;
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
    public void UpdateWeaponValuesAndIcon(GunScriptableObject gun, int magazine, int ammo)
    {
        Debug.Log("UpdateText");

        MagazineText.SetText(magazine.ToString());
        AmmoText.SetText("/ " + ammo.ToString());

        IconHolderControl.ChangeIconDisplayed(gun.IconName);
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