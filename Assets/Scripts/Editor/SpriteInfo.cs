using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Object = UnityEngine.Object;

static class UnityObjectExtensions {
    public static Sprite toSprite(this Object obj) {
        if (obj is GameObject) {
            return (obj as GameObject).GetComponent<SpriteRenderer>()?.sprite;
        }
        if (obj is Sprite) {
            return (Sprite)obj;
        }
        return null;
    }

    public static Mesh toMesh(this Sprite sprite) {
        var mesh = new Mesh();
        mesh.SetVertices(Array.ConvertAll(sprite.vertices, c => (Vector3)c).ToList());
        mesh.SetUVs(0, sprite.uv.ToList());
        mesh.SetTriangles(Array.ConvertAll(sprite.triangles, c => (int)c), 0);
        return mesh;
    }

    public static GameObject toGameObject(this Mesh mesh) {
        var obj = new GameObject();
        obj.name = mesh.name;
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshRenderer>();
        obj.AddComponent<MeshCollider>();
        obj.GetComponent<MeshFilter>().sharedMesh = mesh;
        obj.GetComponent<MeshFilter>().sharedMesh.name = mesh.name;
        obj.GetComponent<MeshCollider>().sharedMesh = mesh;
        return obj;
    }
}

public class SpriteInfo : Editor {
    [MenuItem("Tools/Create Mesh from Sprite")]
    public static void Create() {
        foreach (var obj in Selection.objects) {
            var sprite = obj.toSprite();
            if (sprite == null) { continue; }

            var mesh = sprite.toMesh();
            var mesh_filename = $"Assets/Resources/Meshes/{ obj.name }.asset";
            AssetDatabase.CreateAsset(mesh, mesh_filename);

            var gameObj = mesh.toGameObject();
            var gameObj_filename = $"Assets/Resources/Prefabs/{ obj.name }.prefab";
            PrefabUtility.CreatePrefab(gameObj_filename, gameObj);
            AssetDatabase.SaveAssets();
        }
    }
}
