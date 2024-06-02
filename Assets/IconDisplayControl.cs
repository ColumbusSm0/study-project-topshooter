using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IconDisplayControl : MonoBehaviour
{
    [SerializeField] private List<GameObject> icons = new List<GameObject>();

    [SerializeField] private GameObject currentIcon;

    // Start is called before the first frame update
    void Start()
    {
        ChangeIconDisplayed("PISTOL_ICON");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeIconDisplayed(string iconName)
    {
        if (iconName != currentIcon.name || currentIcon == null)
        {
            for (int i = 0; i < icons.Count; i++)
            {
                if (icons[i].name == iconName)
                {
                    icons[i].gameObject.SetActive(true);
                    currentIcon.SetActive(false);
                    currentIcon = icons[i];
                }
            }
        }

    }
}
