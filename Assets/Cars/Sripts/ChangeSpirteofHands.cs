using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpirteofHands : MonoBehaviour
{
    public Sprite[] BlueSprites;
    public Sprite[] OrangeSprites;
    Image nowButton;
    public GameObject blue;
    public GameObject orange;
    public Sprite[] ButtonSprites;
    public GameObject soundbutton;
    public GameObject musicbutton;
    public GameObject pausebutton;
    
    // Start is called before the first frame update

    void Start()
    {
        foreach(var item in BlueSprites)
        {
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        var spriteofblue = blue.GetComponent<SpriteRenderer>();
        spriteofblue.sprite = BlueSprites[PlayerMove.priorityofBlue];
        var spriteoforange = orange.GetComponent<SpriteRenderer>();
        spriteoforange.sprite = OrangeSprites[PlayerMove.priorityofOrange];
        MusicButton();
        SoundButton();
        PauseButton(); 
    }
    public void MusicButton()
    {

        var nowButton = musicbutton.GetComponent<Image>();
        if (GameController.IsMusicOff)
        {
            nowButton.sprite = ButtonSprites[1];
           // GameController.IsMusicOff = false;
            return; 
        }
        else
        {
            nowButton.sprite = ButtonSprites[0];
           // GameController.IsMusicOff = true;
            return;
        }
    }
    public void SoundButton()
    {

        var nowButton = soundbutton.GetComponent<Image>();
        if (GameController.IsSoundOff)
        {
            nowButton.sprite = ButtonSprites[2];
         //   GameController.IsSoundOff = false;
            return;
        }
        else
        {
            nowButton.sprite = ButtonSprites[3];
          //  GameController.IsSoundOff = true;
            return;
        }
    }
    public void PauseButton()
    {

        var nowButton = pausebutton.GetComponent<Image>();
        if (GameController.IsGamePause)
        {
            nowButton.sprite = ButtonSprites[4];
          //GameController.IsGamePause = true;
            return;
        }
        else
        {
            nowButton.sprite = ButtonSprites[5];
          //GameController.IsGamePause = false;
            return;
        }
    }
}
