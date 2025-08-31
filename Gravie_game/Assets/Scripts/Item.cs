using UnityEngine;

public class Item
{
    public enum ItemType {
        HealthNone,
        fullHealthPotion,
        halfHealthPotion,
        quarterHealthPotion,
        magnet,
        speedRun
        
    }

public static int GetCost(ItemType itemType) {
        switch (itemType)
        {
            default:
            case ItemType.HealthNone:
                return 0;
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
            return 200;
        case ItemType.speedRun:
            return 150;
    }
}

public static Sprite GetSprite(ItemType itemType) {
    switch (itemType) {
        default:
        case ItemType.HealthNone:
            return GameAssets.instance.s_HealthNone;
        case ItemType.fullHealthPotion:
            return GameAssets.instance.s_fullHealthPotion;
        case ItemType.halfHealthPotion:
            return GameAssets.instance.s_halfHealthPotion;
        case ItemType.quarterHealthPotion:
            return GameAssets.instance.s_quarterHealthPotion;
        case ItemType.magnet:
            return GameAssets.instance.s_magnet;
        case ItemType.speedRun:
            return GameAssets.instance.s_speedRun;
        }
    }
}



