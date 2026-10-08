using UnityEditor;
using UnityEngine;
using System.IO;

public class AutoFixAtmosphericHouse
{
    [MenuItem("Tools/Atmospheric House/Auto-Fix All Materials")]
    static void AutoFix()
    {
        string root = "Assets/AtmosphericHouse";   // Change if your folder is different

        // 1. Find all textures in the asset folder
        var textureGUIDs = AssetDatabase.FindAssets("t:Texture2D", new[] { root });
        var textures = new System.Collections.Generic.Dictionary<string, Texture2D>();

        foreach (var guid in textureGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

            if (tex != null)
            {
                string key = Path.GetFileNameWithoutExtension(path).ToLower();
                textures[key] = tex;
            }
        }

        Debug.Log($"Found {textures.Count} textures.");

        // 2. Fix all FBX importers
        var modelGUIDs = AssetDatabase.FindAssets("t:Model", new[] { root });

        foreach (var guid in modelGUIDs)
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

        Debug.Log("FBX import settings fixed.");

        // 3. Find all generated materials and auto-assign textures
        var materialGUIDs = AssetDatabase.FindAssets("t:Material", new[] { root });

        foreach (var guid in materialGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat == null) continue;

            // Force URP Lit
            mat.shader = Shader.Find("Universal Render Pipeline/Lit");

            string matName = mat.name.ToLower();

            // Try to match textures by name
            foreach (var kvp in textures)
            {
                string texName = kvp.Key;
                Texture2D tex = kvp.Value;

                if (texName.Contains(matName))
                {
                    // Base color
                    if (texName.Contains("base") || texName.Contains("albedo") || texName.Contains("diff"))
                        mat.SetTexture("_BaseMap", tex);

                    // Normal map
                    if (texName.Contains("normal") || texName.Contains("nrm"))
                    {
                        mat.SetTexture("_BumpMap", tex);
                        mat.EnableKeyword("_NORMALMAP");
                    }

                    // Mask map (metallic/roughness/ao)
                    if (texName.Contains("mask") || texName.Contains("metal") || texName.Contains("rough"))
                        mat.SetTexture("_MaskMap", tex);
                }
            }

            EditorUtility.SetDirty(mat);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Atmospheric House materials fully auto-fixed.");
    }
}
