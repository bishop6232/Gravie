using UnityEngine;
using System.Reflection;


public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public Sprite s_fullHealthPotion;
    public Sprite s_halfHealthPotion;
    public Sprite s_quarterHealthPotion;
    public Sprite s_magnet;
    public Sprite s_speedRun;

    public static GameAssets i {
        get {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }
}
