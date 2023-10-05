using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class StarUI : MonoBehaviour
{
    private Image starImage;
    public Sprite blueStarSprite;
    public string blueStarName;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        starImage = GetComponent<Image>();
        starImage.color = new Color32(255, 255, 255, 0);

        if (PlayerPrefs.GetInt(blueStarName) == 1)
        {
            BlueStarGotten();
        }
    }

    public void BlueStarGotten()
    {
        starImage.color = new Color32(255, 255, 255, 255);
    }
    public void BlueStarLost()
    {
        if (PlayerPrefs.GetInt(blueStarName) == 0)
        {
            anim.SetBool("BlueStarLost", true);
        }
    }
}
