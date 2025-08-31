using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shop : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        createItemButton(Item.GetSprite(Item.ItemType.fullHealthPotion), "Full Health Potion", Item.GetCost(Item.ItemType.fullHealthPotion), 0);
        createItemButton(Item.GetSprite(Item.ItemType.halfHealthPotion), "Half Health Potion", Item.GetCost(Item.ItemType.halfHealthPotion), 1);
        createItemButton(Item.GetSprite(Item.ItemType.quarterHealthPotion), "Quarter Health Potion", Item.GetCost(Item.ItemType.quarterHealthPotion), 2);
        createItemButton(Item.GetSprite(Item.ItemType.magnet), "Magnet", Item.GetSpecialCost(Item.ItemType.magnet), 3);
        createItemButton(Item.GetSprite(Item.ItemType.speedRun), "Speed Run", Item.GetSpecialCost(Item.ItemType.speedRun), 4);
    }

    private void createItemButton(Sprite itemSprite, string itemName, int itemCost, int positionIndex) {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        float shopItemHeight = 30f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;
        shopItemTransform.Find("itemName").GetComponent<TextMeshPro>().SetText(itemName);
        shopItemTransform.Find("itemCost").GetComponent<TextMeshPro>().SetText(itemCost.ToString());
    }
}
