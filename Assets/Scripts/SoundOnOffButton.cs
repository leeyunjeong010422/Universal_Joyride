using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOnOffButton : MonoBehaviour
{
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    [SerializeField] private Button soundToggleButton;

    private Image buttonImage;

    private void Start()
    {
        //게임 씬에 있다가 다시 처음 화면으로 돌아오면 음악이 재생 안 되어서 추가
        SoundManager.Instance.SetStartBGM();
        buttonImage = soundToggleButton.GetComponent<Image>();
        UpdateButtonImage();
        soundToggleButton.onClick.AddListener(ToggleSound);
    }

    private void ToggleSound()
    {
        SoundManager.Instance.ButtonToggle();
        UpdateButtonImage();
    }

    private void UpdateButtonImage()
    {
        //현재 사운드에 따라 이미지 변경
        if (SoundManager.Instance.IsSoundMuted())
        {
            buttonImage.sprite = soundOffSprite;
        }
        else
        {
            buttonImage.sprite = soundOnSprite;
        }
    }
}
