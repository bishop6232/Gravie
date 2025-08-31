using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Button = UnityEngine.UI.Button; 

public class UI_Shop : MonoBehaviour

{
    private Transform container;
    private Transform shopItemTemplate;
    private IShopCustomer shopCustomer;

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(true);

    }

    private void Start() {
        createItemButton(Item.ItemType.fullHealthPotion, Item.GetSprite(Item.ItemType.fullHealthPotion), "Full Health Potion", Item.GetCost(Item.ItemType.fullHealthPotion), 0);
        createItemButton(Item.ItemType.halfHealthPotion, Item.GetSprite(Item.ItemType.halfHealthPotion), "Half Health Potion", Item.GetCost(Item.ItemType.halfHealthPotion), 1);
        createItemButton(Item.ItemType.quarterHealthPotion, Item.GetSprite(Item.ItemType.quarterHealthPotion), "Quarter Health Potion", Item.GetCost(Item.ItemType.quarterHealthPotion), 2);
        createItemButton(Item.ItemType.magnet, Item.GetSprite(Item.ItemType.magnet), "Magnet", Item.GetSpecialCost(Item.ItemType.magnet), 3);
        createItemButton(Item.ItemType.speedRun, Item.GetSprite(Item.ItemType.speedRun), "Speed Run", Item.GetSpecialCost(Item.ItemType.speedRun), 4);

        Hide();
    }

    private void createItemButton(Item.ItemType itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex) {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 120f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;
        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("itemCost").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        

        shopItemRectTransform.GetComponent<Button>().onClick.AddListener(() => {
            TryBuyItem(itemType);

        });
    }

    private void TryBuyItem(Item.ItemType itemType) {
        if (shopCustomer.TrySpendCoins(Item.GetCost(itemType)))
        {
            shopCustomer.BoughtItem(itemType);
        }
        else if (shopCustomer.TrySpendSpecial(Item.GetSpecialCost(itemType)))
        {
            shopCustomer.BoughtItem(itemType);
        }
    }

    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
