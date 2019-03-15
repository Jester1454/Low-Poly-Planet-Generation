using System;
using System.IO;
using UnityEngine;

namespace Utilities
{
    public class LoadObjectFromJSON 
    {
        public static T Load<T>(string pathObject)
        {  
            T loadObj = default(T);
			
            if (File.Exists(pathObject))
            {
                try
                {
                    loadObj = JsonUtility.FromJson<T>(File.ReadAllText(pathObject));
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }

            return loadObj;
        }

        public static T LoadFromResources<T>(string pathObject)
        {
            TextAsset targetFile = Resources.Load<TextAsset>(pathObject);
            T loadObj = default(T);

            try
            {
                loadObj = JsonUtility.FromJson<T>(targetFile.text);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }

            return loadObj;
        }
    }
}
