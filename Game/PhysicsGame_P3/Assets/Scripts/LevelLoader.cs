using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
	private Texture2D levelMap;
	public bool loadFromBytes;
	public string levelFileName;

	public List<ColorToPrefab> entities = new List<ColorToPrefab>();

	public void LoadLevel()
	{
		EmptyMap(); //Reset before starting the load process
		
		if(levelMap == null) Debug.LogError("No map loaded");

		if (loadFromBytes)
		{
			//Loads file as bytes from StreamingAssets folder (Maybe change to be the only possible way?)
			var filePath = Application.dataPath + "/StreamingAssets/" + levelFileName;
			byte[] bytes = System.IO.File.ReadAllBytes(filePath);
			levelMap.LoadImage(bytes);
		}

		var allPixels = levelMap.GetPixels32();
		var width = levelMap.width;
		var height = levelMap.height;

		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				SpawnAt(allPixels[x + (y * width)], x, y);
			}
		}
	}

	public void EmptyMap()
	{
		while (transform.childCount > 0)
		{
			var c = transform.GetChild(0);
			c.SetParent(null);
			Destroy(c.gameObject);
		}
	}

	public void SetMap(Texture2D map)
	{
		levelMap = map;
	}

	private void SpawnAt(Color32 color, int x, int y)
	{
		if (color == Color.black) return;
		
		//Find the right color in our map
		foreach (var ctp in entities)
		{
			if(!color.Equals(ctp.color)) continue;
			
			//Spawn prefab at right location according to map 
			//Important! The last part of input in the below variable determines rotation!
			var obj = Instantiate(ctp.prefab, new Vector3(x, y, 0), Quaternion.identity);
			obj.transform.SetParent(transform);
			
			return;
		}
		
		//Did not find matching prefab to input color
		Debug.LogError("No prefab found for " + color);
	}
}

[System.Serializable]
public struct ColorToPrefab
{
	public Color32 color;
	public GameObject prefab;
}
