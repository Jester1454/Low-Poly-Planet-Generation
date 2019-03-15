using System;
using System.IO;
using UnityEngine;

namespace Utilities
{
	public class SaveObjectInJSON 
	{
		public static void Save<T>(string savePath, T saveObject)
		{
			try
			{
				string text = JsonUtility.ToJson(saveObject, true);
				File.WriteAllText
				(
					savePath,
					text
				);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}
	}
}
