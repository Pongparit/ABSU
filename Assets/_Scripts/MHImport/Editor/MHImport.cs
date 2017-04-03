using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
namespace MHImportUtils
{
    public class MHImport : EditorWindow
    {
        GameObject gobj;
        //GameObject template;
        bool processFurther = true;
        List<Material> doubleMats = new List<Material>();

        [MenuItem("Window/MHImport")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(MHImport));
        }

        void OnGUI()
        {
            EditorGUILayout.LabelField("Import file");
            gobj = EditorGUILayout.ObjectField(gobj, typeof(GameObject), false) as GameObject;
            //EditorGUILayout.LabelField("Template file (optional, copies avatar definition from a source model)");

            //template = EditorGUILayout.ObjectField(template, typeof(GameObject), false) as GameObject;

            processFurther = EditorGUILayout.ToggleLeft("Copy export file to [mesh_name]_proc and process meshes further (usually want to enable this)", processFurther);
         
            if (GUILayout.Button("Process MakeHuman"))
            {
                ReImport();
            }
        }
        void ReImport()
        {
            if (gobj == null)
            {
                return;
            }
            string path = AssetDatabase.GetAssetPath(gobj);
            if (!path.EndsWith(".fbx"))
            {
                Debug.LogWarning("\"" + path + "\" does not end in \".fbx\". Ending process.");
                return;
            }


            ModelImporter modelImporter = AssetImporter.GetAtPath(path) as ModelImporter;
            if (modelImporter == null)
            {
                Debug.LogWarning("Asset at \"" + path + "\" does not have a ModelImporter attached to it. Please reimport and try again.");
                return;
            }

            /*if (template != null)
            {
                ModelImporter templateImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(template)) as ModelImporter;

                if (templateImporter == null)
                {
                    Debug.LogWarning("Template at \"" + path + "\" does not have a ModelImporter attached to it. Please reimport and try again.");
                    return;
                }
                modelImporter.animationType = templateImporter.animationType;
                modelImporter.humanDescription = templateImporter.humanDescription;

                modelImporter.SaveAndReimport();
            }*/
            processMats(path);

            if (processFurther)
            {
                var newpath = path.Substring(0, path.LastIndexOf(".fbx")) + "_proc";
                processMeshes(path, newpath);
            }
        }

        void processMeshes(string path, string newpath)
        {
            var asset = AssetDatabase.LoadMainAssetAtPath(path) as GameObject;

            AssetDatabase.DeleteAsset(newpath);
            AssetDatabase.CreateFolder(path, Path.GetFileName(newpath));

            GameObject parent = PrefabUtility.CreatePrefab(newpath + ".prefab",asset);

            { 
                List<Material> copyMats = new List<Material>();
                List<SkinnedMeshRenderer> smrs = new List<SkinnedMeshRenderer>();
                int eyeind = -1;
                for (int i = 0; i < parent.transform.childCount; ++i)
                {
                    GameObject child = parent.transform.GetChild(i).gameObject;
                    if (child.GetComponent<SkinnedMeshRenderer>() != null)
                    {
                        var smr = child.GetComponent<SkinnedMeshRenderer>();
                        var mesh = smr.sharedMesh;
                        /*var m = new Mesh();
                        m = Instantiate(mesh);

                        m.name = mesh.name + "_proc";
                        AssetDatabase.CreateAsset(m, newpath + "/" + m.name + ".asset");
                        smr.sharedMesh = m;*/
                        string name = smr.sharedMaterial.name;
                        if (name.IndexOf("eye") >= 0 || Array.Find(MHImportSettings.hairs, s => s == name) != null
                            || Array.Find(MHImportSettings.clothes, s => s == name) != null)
                        {
                            if (name.IndexOf("eye") > 0)
                                eyeind = smrs.Count;

                            smrs.Add(smr);
                            //meshes.Add(m);

                            Material copyMat = Instantiate(smr.sharedMaterial);
                            copyMat.name = name + "_proc";
                            copyMat.SetFloat("_Cutoff", 0.9f);
                            copyMats.Add(copyMat);
                        }
                    }
                }
                for (int i = 0; i < copyMats.Count; ++i)
                {
                    //doubleSide(meshes[i]);
                    if (i == eyeind)
                    {
                        copyMats[i].shader = Shader.Find("MHImport/Eye");
                    }
                    else if (Array.Find(MHImportSettings.clothes, s => s == name) == null)
                    {
                        copyMats[i].shader = Shader.Find("MHImport/Hair");
                    }
                    else
                    {
                        copyMats[i].shader = Shader.Find("MHImport/Clothes");
                    }
                    smrs[i].material = copyMats[i];
                    smrs[i].enabled = false;
                    smrs[i].enabled = true;
                    AssetDatabase.CreateAsset(copyMats[i], newpath + "/" + copyMats[i].name + ".mat");
                }
            }

            AssetDatabase.SaveAssets();
            
        }

        void subCopy(Mesh m, bool isHair = false)
        {

            var oinds = new List<int>(m.triangles).ToArray();

            if (!isHair)
            {
                m.subMeshCount = 2;

                m.SetIndices(oinds, MeshTopology.Triangles, 0);
                m.SetIndices(oinds, MeshTopology.Triangles, 1);
            }
            else
            {
                m.subMeshCount = 2;

                m.SetIndices(oinds, MeshTopology.Triangles, 0);
                m.SetIndices(oinds, MeshTopology.Triangles, 1);
            }

            m.RecalculateBounds();
            m.UploadMeshData(false);
        }

        void doubleSide(Mesh m, bool isClothes = false)
        {
            var oinds = m.triangles;
            var ninds = new int[oinds.Length * 2];
            if (isClothes)
            {

                for (int i = 0; i < oinds.Length; ++i)
                {
                    ninds[i] = oinds[i];
                    ninds[ninds.Length - i - 1] = oinds[i];
                }

                m.triangles = ninds;
                m.RecalculateBounds();
                m.UploadMeshData(false);
                return;
            }

            for (int i = 0; i < oinds.Length; ++i)
            {
                ninds[i] = oinds[i];
                ninds[ninds.Length - i - 1] = oinds[i] + m.vertexCount;
                
            }

            var nVerts = new List<Vector3>(m.vertices);
            nVerts.AddRange(m.vertices);
            var nUvs = new List<Vector2>(m.uv);
            nUvs.AddRange(m.uv);
            var nTans = new List<Vector4>(m.tangents);
            nTans.AddRange(m.tangents);
            var nNorms = new List<Vector3>(m.normals);
            for (int i = 0; i < m.normals.Length; ++i)
                nNorms.Add(-m.normals[i]);
            var nBones = new List<BoneWeight>(m.boneWeights);
            nBones.AddRange(m.boneWeights);

            m.Clear();

            m.vertices = nVerts.ToArray();
            m.normals = nNorms.ToArray();
            m.tangents = nTans.ToArray();
            m.uv = nUvs.ToArray();
            m.boneWeights = nBones.ToArray();

            m.triangles = ninds;
            m.RecalculateBounds();
            m.UploadMeshData(false);
        }

        void processMats(string path)
        {
            var matdir = Path.GetDirectoryName(path) + "/Materials";
            var texdir = Path.GetDirectoryName(path) + "/Textures";
            var matpaths = Directory.GetFiles(matdir, "*.mat");
            var texpaths = Directory.GetFiles(texdir, "*.png");

            var mats = new Material[matpaths.Length];

            for (int i = 0; i < matpaths.Length; ++i)
            {
                Material mat = AssetDatabase.LoadAssetAtPath<Material>(matpaths[i].Replace('\\', '/'));
                //if (mode == 0 || !mat.IsKeywordEnabled("_ALPHATEST_ON"))
                {
                    {
                        /*
                        mat.SetFloat("_Mode", 1.0f);
                        mat.EnableKeyword("_ALPHATEST_ON");*/
                        mat.SetFloat("_Cutoff", 0.5f);
                    }
                    mat.SetFloat("_Glossiness", MHImportSettings.gloss);
                }
                mats[i] = mat;
            }

            for (int i = 0; i < texpaths.Length; ++i)
            {
                string name = Path.GetFileName(texpaths[i]);
                if (name.Contains("normal"))
                {
                    TextureImporter tImporter = AssetImporter.GetAtPath(texpaths[i].Replace('\\', '/')) as TextureImporter;
                    if (!tImporter.normalmap)
                    {
                        tImporter.textureType = TextureImporterType.NormalMap;
                        tImporter.normalmap = true;
                        tImporter.SaveAndReimport();
                    }
                    int closest = 0;
                    int ind = -1;
                    Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(texpaths[i].Replace('\\', '/'));
                    for (int j = 0; j < mats.Length; ++j)
                    {
                        int test = 0;
                        for (int k = 0; k < mats[j].name.Length && k < tex.name.Length; ++k)
                        {
                            if (mats[j].name[k] == tex.name[k])
                                ++test;
                            else
                                break;
                        }
                        if (test > closest)
                        {
                            closest = test;
                            ind = j;
                        }
                    }
                    if (ind >= 0)
                    {
                        mats[ind].SetTexture("_BumpMap", tex);
                        mats[ind].EnableKeyword("_NORMALMAP");
                    }
                }
            }
        }
    }
}