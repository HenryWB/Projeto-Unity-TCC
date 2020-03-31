using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ModifyWindows : EditorWindow
{
    private static DataBase database;
    private static EditorWindow window;

    private static Item databaseItem;

    private static Item newItem;
    private GUILayoutOption[] options = { GUILayout.MaxWidth(150.0f), GUILayout.MinWidth(20.0f) }; 

    public static void ShowItemWidow(DataBase db, Item item)
    {
        database = db;
        window = GetWindow<ModifyWindows>();

        window.maxSize = new Vector2(300, 350);
        window.minSize = new Vector2(300, 350);

        databaseItem = item;

        newItem = new Item();

        newItem.id = item.id;
        newItem.name = item.name;
        newItem.itemImage = item.itemImage;
        newItem.itemType = item.itemType;
        newItem.isStackable = item.isStackable;
        newItem.descricao = item.descricao;
        newItem.status = item.status;
    }

    public void OnGUI()
    {
        DisplayItem(newItem);
        if (GUILayout.Button("Confirm"))
        {
            ModifyItem();
        }
    }

    private bool shouldDisable;
    private void DisplayItem(Item item)
    {
        GUIStyle TextAreaStyle = new GUIStyle(GUI.skin.textArea);
        TextAreaStyle.wordWrap = true;

        GUIStyle valueStyle = new GUIStyle(GUI.skin.label);
        valueStyle.wordWrap = true;

        valueStyle.alignment = TextAnchor.MiddleLeft;
        valueStyle.fixedWidth = 50;
        valueStyle.margin = new RectOffset(0, 50, 0, 0);

        EditorGUILayout.BeginVertical("Box");
     
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name: ");
        item.name = EditorGUILayout.TextField(item.name, options);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Item Image: ");
        item.itemImage = (Sprite)EditorGUILayout.ObjectField(item.itemImage, typeof(Sprite), false, options);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Type: ");
        item.itemType = (Item.ItemType)EditorGUILayout.EnumPopup(item.itemType, options);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Stackable: ");
        item.isStackable = EditorGUILayout.Toggle(item.isStackable, options);
        EditorGUILayout.EndHorizontal();

        //EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Descrição: ");
        item.descricao = EditorGUILayout.TextArea(item.descricao, TextAreaStyle, GUILayout.MinHeight(100));
        //EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Cost: ");
        item.status.cost = EditorGUILayout.IntField(item.status.cost, options);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Selling cost: ");
        item.status.sellCost = EditorGUILayout.IntField(item.status.sellCost, options);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage: ");
        item.status.damage = EditorGUILayout.IntField(item.status.damage, options);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Defesa: ");
        item.status.defense = EditorGUILayout.IntField(item.status.defense, options);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Health: ");
        item.status.health = EditorGUILayout.IntField(item.status.health, options);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Mana: ");
        item.status.mana = EditorGUILayout.IntField(item.status.mana, options);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();

    }

    private void ModifyItem()
    {
        Undo.RecordObject(database, "Item Modify");
        databaseItem.name = newItem.name;
        databaseItem.itemImage = newItem.itemImage;
        databaseItem.itemType = newItem.itemType;
        databaseItem.isStackable = newItem.isStackable;
        databaseItem.descricao = newItem.descricao;
        databaseItem.status = newItem.status;
        EditorUtility.SetDirty(database);
        window.Close();
    }
}
