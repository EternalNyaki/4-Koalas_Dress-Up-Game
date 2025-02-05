using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ClothingImporter.asset", menuName = "Dress-Up/Clothing Importer")]
public class ClothingImporter : ScriptableObject
{
    [Tooltip("Path from Assets to Excel File")]
    public string excelFilePath = "Editor/ClothingData.xlsx";

    public void Import()
    {
        float taskStart = Time.realtimeSinceStartup;

        var excel = new ExcelImporter(excelFilePath);

        var items = DataHelper.GetAllAssetsOfType<ClothingItem>();

        ImportItems("ClothingItems", excel, items);

        Debug.Log($"Clothing Items import succeeded in {(Time.realtimeSinceStartup - taskStart) * 1000}ms");
    }

    private void ImportItems(string category, ExcelImporter excel, Dictionary<string, ClothingItem> items)
    {
        if (!excel.TryGetTable(category, out var table))
        {
            Debug.LogError($"Could not find {category} table");
            return;
        }

        for (int row = 1; row <= table.RowCount; row++)
        {
            string name = table.GetValue<string>(row, "Name");
            if (string.IsNullOrWhiteSpace(name)) { continue; }

            var item = DataHelper.GetOrCreateAsset(name, items, category);

            if (table.TryGetEnum<ClothingType>(row, "Type", out var type))
            {
                item.type = type;
            }
        }
    }
}


//Editor class for the clothing importer
//Creates an Import button in the inspector
[CustomEditor(typeof(ClothingImporter))]
public class ClothingImporterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var editorObject = (ClothingImporter)target;

        if (EditorGUILayout.LinkButton("Import"))
        {
            editorObject.Import();
        }
    }
}
