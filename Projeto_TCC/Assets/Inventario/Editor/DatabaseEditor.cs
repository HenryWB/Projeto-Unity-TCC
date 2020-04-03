using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataBase))]
public class DatabaseEditor : Editor
{
    private DataBase database;

    private string searchString = "";
    private bool shouldSearch;


    private void OnEnable()
    {
        database = (DataBase)target;
    }
    public override void OnInspectorGUI()
    {
        if (database)
        {
            EditorGUILayout.BeginHorizontal("Box");
            GUILayout.Label("Items in Database: " + database.itens.Count);
            EditorGUILayout.EndHorizontal();

            if(database.itens.Count > 0)
            {
                EditorGUILayout.BeginHorizontal("Box");
                GUILayout.Label("Search: ");
                searchString = GUILayout.TextField(searchString);
                EditorGUILayout.EndHorizontal();
            }

            if(GUILayout.Button("Add Item"))
            {
                Debug.Log("Abrir");
                ItemWindows.ShowEmptyWidow(database);
            }

            if (System.String.IsNullOrEmpty(searchString))
            {
                shouldSearch = false;
            }
            else
            {
                shouldSearch = true;
            }

            foreach(Item item in database.itens){

                if (shouldSearch)
                {
                    if(item.name == searchString || item.name.Contains(searchString) || item.id.ToString() == searchString)
                    {
                        DisplayItem(item);
                    }
                }
                else
                {
                    DisplayItem(item);
                }
            }

            if(deletedItem != null)
            {
                database.itens.Remove(deletedItem);
            }
        }
    }

    private Item deletedItem;

    private void DisplayItem(Item item)
    {
        GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.wordWrap = true;

        GUIStyle valueStyle = new GUIStyle(GUI.skin.label);
        valueStyle.wordWrap = true;

        valueStyle.alignment = TextAnchor.MiddleLeft;
        valueStyle.fixedWidth = 50;
        valueStyle.margin = new RectOffset(0, 50, 0, 0);

        EditorGUILayout.BeginVertical("Box");

        Sprite itemSprite = item.itemImage;
        if (itemSprite != null)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Image: " + item.itemImage.ToString());
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("ID: ");
        GUILayout.Label(item.id.ToString(), valueStyle);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name: ");
        GUILayout.Label(item.name, labelStyle);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Type: ");
        GUILayout.Label(item.itemType.ToString(), labelStyle);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Stackable: ");
        GUILayout.Toggle(item.isStackable,GUIContent.none);
        EditorGUILayout.EndHorizontal();

        //EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Descrição: ");
        item.scrollPos = EditorGUILayout.BeginScrollView(item.scrollPos,  GUILayout.MinHeight(3), GUILayout.MaxHeight(70));
        GUILayout.Label(item.descricao, labelStyle);
        EditorGUILayout.EndScrollView();
        //EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Cost: ");
        GUILayout.Label(item.status.cost.ToString(), valueStyle);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Selling cost: ");
        GUILayout.Label(item.status.sellCost.ToString(), valueStyle);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage: ");
        GUILayout.Label(item.status.damage.ToString(), valueStyle);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage: ");
        GUILayout.Label(item.status.defense.ToString(), valueStyle);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Health: ");
        GUILayout.Label(item.status.health.ToString(), valueStyle);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Mana: ");
        GUILayout.Label(item.status.mana.ToString(), valueStyle);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Modify"))
        {
            ModifyWindows.ShowItemWidow(database, item);
        }

        if (GUILayout.Button("Delete"))
        {
            deletedItem = item;
        }
        else
        {
            deletedItem = null;
        }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.EndVertical();

    }
}
