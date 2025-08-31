using UnityEngine;

public class Item
{
    public enum ItemType {
        fullHealthPotion,
        halfHealthPotion,
        quarterHealthPotion,
        magnet,
        speedRun
    }

public static int GetCost(ItemType itemType) {
    switch (itemType) {
        default:
        case ItemType.fullHealthPotion:
            return 100;
        case ItemType.halfHealthPotion:
            return 50;
        case ItemType.quarterHealthPotion:
            return 25;
    }
}

public static int GetSpecialCost(ItemType itemType) {
    switch (itemType) {
        default:
        case ItemType.magnet:
            return 5;
        case ItemType.speedRun:
            return 5;
    }
}

public static Sprite GetSprite(ItemType itemType) {
    switch (itemType) {
        default:
        case ItemType.fullHealthPotion:
            return GameAssets.i.s_fullHealthPotion;
        case ItemType.halfHealthPotion:
            return GameAssets.i.s_halfHealthPotion;
        case ItemType.quarterHealthPotion:
            return GameAssets.i.s_quarterHealthPotion;
        case ItemType.magnet:
            return GameAssets.i.s_magnet;
        case ItemType.speedRun:
            return GameAssets.i.s_speedRun;
        }
    }
}



