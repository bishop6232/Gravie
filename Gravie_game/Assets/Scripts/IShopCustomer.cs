using UnityEngine;

public interface IShopCustomer {

    public int coinsCollected { get; }
    public int diamondsCollected { get; }

    void BoughtItem(Item.ItemType itemType);
    bool TrySpendCoins(int coinsCollected);
    bool TrySpendSpecial(int diamondsCollected);
}

