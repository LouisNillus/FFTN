using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Color soundOn;
    public Color soundOff;

    public Color qualityLow;
    public Color qualityhigh;

    public Image muteButton;
    public Image qualityButton;

    public bool muted = false;

    public

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
        muteButton.GetComponentInChildren<TextMeshProUGUI>().text = muted ? "OFF" : "ON";
        muteButton.color = muted ? soundOff : soundOn;
    }

    public void GraphicsSwap()
    {
        QualitySettings.SetQualityLevel(QualitySettings.GetQualityLevel() == 0 ? 1 : 0);

        int currentQuality = QualitySettings.GetQualityLevel();
        qualityButton.color = currentQuality == 0 ? qualityhigh : qualityLow;
        qualityButton.GetComponentInChildren<TextMeshProUGUI>().text = currentQuality == 0 ? "HIGH" : "LOW";
    }
}
