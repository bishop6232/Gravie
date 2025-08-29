using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CollectableManager : MonoBehaviour
{
    public int coinsCollected;
    public TMP_Text coinText;
    public TMP_Text totalCoinText;
    public int diamondsCollected;
    public TMP_Text diamondText;
    void Start()
    {
        coinsCollected = 0;
        diamondsCollected = 0;

        if (coinText != null)
        {
            coinText.text = "" + coinsCollected.ToString();
        }
        if (totalCoinText != null)
        {
            totalCoinText.text = "Total Coins: " + coinsCollected.ToString();
        }
        if (diamondText != null)
        {
            diamondText.text = "" + diamondsCollected.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "" + coinsCollected.ToString();
        diamondText.text = "" + diamondsCollected.ToString();
        if (totalCoinText != null)
        {
            totalCoinText.text = "Total Coins: " + coinsCollected.ToString();
        }
    }
}
