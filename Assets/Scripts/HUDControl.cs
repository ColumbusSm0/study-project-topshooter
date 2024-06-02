using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDControl : MonoBehaviour
{
    private PlayerControl scriptPlayerControl;
    public  Slider SliderPlayerLife;
<<<<<<< Updated upstream
=======
    public IconDisplayControl IconHolderControl;
    public TextMeshProUGUI MagazineText;
    public TextMeshProUGUI AmmoText;
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
=======
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

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======
    public void UpdateWeaponValuesAndIcon(GunScriptableObject gun, int magazine, int ammo)
    {
        Debug.Log("UpdateText");

        MagazineText.SetText(magazine.ToString());
        AmmoText.SetText("/ " + ammo.ToString());

        IconHolderControl.ChangeIconDisplayed(gun.IconName);
    }
>>>>>>> Stashed changes

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