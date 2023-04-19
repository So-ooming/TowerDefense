using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPlayerHP;
    [SerializeField] private TextMeshProUGUI textPlayerGold;
    [SerializeField] private TextMeshProUGUI textCurrentWave;
    [SerializeField] private PlayerHP playerHP;
    [SerializeField] private PlayerGold playerGold;
    [SerializeField] private WaveSystem currentWave;
    [SerializeField] private WaveViewer waveViewer;

    private void Update()
    {
        textPlayerHP.text = playerHP.CurrentHP + "/" + playerHP.MaxHP;
        textPlayerGold.text = playerGold.CurrentGold.ToString();
        textCurrentWave.text = currentWave.CurrentWaveIndex.ToString() + "/" + currentWave.maxWave.ToString();
        //Debug.Log(waveViewer.chgimage);
    }
}
