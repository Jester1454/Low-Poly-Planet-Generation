 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
 
 
 //---------- class SubMeshScript
 
 public class SubMeshScript : MonoBehaviour {
 
     Material[] mat = new Material[2];
 
     Vector3[] vertices    = new Vector3[8];
     Vector2[] uvs        = new Vector2[8];
     Vector2[] uvs2        = new Vector2[8];
 
     Renderer rend;
 
     Mesh mesh;
 
     int[][] triDex = new int[2][];
 
     //---------- Start
 
     void Start () {
 
         mesh = GetComponent<MeshFilter> ().mesh;
         mesh.Clear ();
 
         rend = GetComponent<Renderer> ();
 
         mat [0] = new Material (Shader.Find("Unlit/Texture"));
         mat [1] = new Material (Shader.Find("Unlit/Texture"));
 
         mat[0].mainTexture = Resources.Load ("colorTest") as Texture2D;
         mat[1].mainTexture = Resources.Load ("MyTest") as Texture2D;
 
         rend.materials = mat;
 
         //----------
 
         vertices [0] = new Vector3 (0f, 0f, 0f);
         vertices [1] = new Vector3 (0f, 0f, 8f);
         vertices [2] = new Vector3 (0f, 8f, 0f);
         vertices [3] = new Vector3 (0f, 8f, 8f);
 
         vertices [4] = new Vector3 (8f, 1f, 0f);
         vertices [5] = new Vector3 (2f, 0f, 3f);
         vertices [6] = new Vector3 (8f, 8f, 25f);
         vertices [7] = new Vector3 (0f, 8f, 5f);
 
         //----------
 
         uvs [0] = new Vector2 (0f, 0f);
         uvs [1] = new Vector2 (1f, 0f);
         uvs [2] = new Vector2 (0f, 1f);
         uvs [3] = new Vector2 (1f, 1f);
 
         uvs [4] = new Vector2 (0f, 0f);
         uvs [5] = new Vector2 (1f, 0f);
         uvs [6] = new Vector2 (0f, 1f);
         uvs [7] = new Vector2 (1f, 1f);
      //                   y   mesh0
      //                  7   2 ------ 3
      //mesh1      / |   |          |
      //               /   |   |          |
      //           6       5   0 ------ 1      z
      //          |    /
      //          | /
      //          4
      //       x
 
         mesh.subMeshCount = 2;
 
         mesh.vertices = vertices;
         mesh.uv  = uvs;
 
         triDex [0] = new int[6] { 0, 3, 1, 0, 2, 3 };
         triDex [1] = new int[6] { 4, 7, 5, 4, 6, 7 };
 
         mesh.SetTriangles (triDex[0], 0);
         mesh.SetTriangles (triDex[1], 1);
         mesh.RecalculateNormals();
 
     }//Start
 }//class SubMeshScript