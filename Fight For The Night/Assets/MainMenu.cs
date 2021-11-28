using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Sprite soundOn;
    public Sprite soundOff;

    public Sprite qualityLow;
    public Sprite qualityHigh;

    public Image muteButton;
    public Image qualityButton;

    public bool muted = false;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MuteSwap()
    {
        muted = !muted;

        AudioManager.instance.audioSource.volume = muted ? 0f : 1f;
        muteButton.sprite = muted ? soundOff : soundOn;
    }

    public void GraphicsSwap()
    {
        QualitySettings.SetQualityLevel(QualitySettings.GetQualityLevel() == 0 ? 1 : 0);

        int currentQuality = QualitySettings.GetQualityLevel();
        qualityButton.sprite = currentQuality == 0 ? qualityHigh : qualityLow;
    }
}
