using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CollectableManager : MonoBehaviour
{
    public int coinsCollected;
    public TMP_Text coinText;
    public TMP_Text totalCoinText;
    void Start()
    {
        coinsCollected = 0;
        if (coinText != null)
        {
            coinText.text = ": " + coinsCollected.ToString();
        }
        if (totalCoinText != null)
        {
            totalCoinText.text = "Total Coins: " + coinText;
        }
    }
    // Update is called once per frame
    void Update()
    {
        coinText.text = ": " + coinsCollected.ToString();
        if (totalCoinText != null)
        {
            totalCoinText.text = "Total Coins: " + coinsCollected.ToString();
        }
    }
    
   
}
