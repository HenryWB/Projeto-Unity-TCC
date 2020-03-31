using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Database")]
public class DataBase : ScriptableObject
{
    public List<Item> itens = new List<Item>();

    public Item FindItemInDatabase(int id)
    {
        foreach(Item item in itens) {
            if(item.id == id)
            {
                return item;
            }
        }
        return null;
    }
}
[System.Serializable]
public class Item
{
    public int id;
    public string name;
    [TextArea(5,5)]
    public string descricao;
    public bool isStackable;

    public ItemType itemType;
    public Stats status;
    public Vector2 scrollPos;

    public Sprite itemImage;

    [System.Serializable]
    public struct Stats
    {
        public int cost;
        public int sellCost;
        public int damage;
        public int defense;
        public int health;
        public int mana;
    }

    public enum ItemType {CONSUMABLE, WEAPON, CLOTH, QUEST, MISC}
}