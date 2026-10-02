using UnityEditor;
using UnityEngine;

public class FixFBXMaterials
{
    [MenuItem("Tools/Fix All FBX Material Locations")]
    static void FixAllFBX()
    {
        string[] guids = AssetDatabase.FindAssets("t:Model");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;

            if (importer != null)
            {
                importer.materialImportMode = ModelImporterMaterialImportMode.ImportStandard;
                importer.materialLocation = ModelImporterMaterialLocation.InPrefab;
                importer.SaveAndReimport();
            }
        }

        Debug.Log("All FBX material locations fixed.");
    }
}