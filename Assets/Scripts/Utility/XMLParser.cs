using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;

public static class XMLParser {

	static string dbPath = Application.dataPath + "/DataBase";
	static string gearPath = Application.dataPath + "/DataBase/GearFiles.xml";
	static string settingsPath = Application.dataPath + "/DataBase/PlayerSettings.xml";

	static void CheckComponents(){
		if (!Directory.Exists (dbPath)) {
			Directory.CreateDirectory (dbPath);
		}
	}

	public static bool WritePlayerSettings(PlayerSettings settings){
		string xml = "<PlayerSettings";

		xml += " currentLevel='" + settings.currentLevel + "'";
		xml += " coins='" + settings.coins + "'";
		xml += " name='" + settings.name + "'";
		xml += " ></PlayerSettings>";

		try {	
			using (StreamWriter writer = new StreamWriter(settingsPath)){
				writer.WriteLine(xml);
				writer.Close();
			}
			return true;
		} catch {
			return false;
		}
	}

	public static PlayerSettings GetPlayerSettings(){

		CheckComponents();

		PlayerSettings settings = new PlayerSettings ();
		XmlDocument doc = new XmlDocument ();

		try {
			using (StreamReader reader = new StreamReader(settingsPath)){
			string text = reader.ReadToEnd();
			reader.Close();

				doc.LoadXml(text);

				XmlNode node = doc.FirstChild;
				settings.coins = int.Parse(node.Attributes["coins"].Value);
				settings.currentLevel = int.Parse(node.Attributes["currentLevel"].Value);
			}

		} catch	{

			settings.coins = 400;
			settings.currentLevel = 1;
		}

		return settings;
	}

	public static List<GearFile> GetGearFileFromXML(){

		CheckComponents();

		List<GearFile> files = new List<GearFile> ();
		XmlDocument document = new XmlDocument ();

		try { 
			using (StreamReader reader = new StreamReader(gearPath)){

				string text = reader.ReadToEnd();
				reader.Close();

				document.LoadXml(text); 

				foreach (XmlNode node in document.FirstChild){
					files.Add (new GearFile(){
						x = int.Parse(node.Attributes["x"].Value),
						y = int.Parse(node.Attributes["y"].Value),
						rotation = int.Parse(node.Attributes["rotation"].Value), 
						activeKey = (KeyCode)Enum.Parse(typeof(KeyCode), node.Attributes["activeKey"].Value),
						type = int.Parse(node.Attributes["type"].Value)
					});
				}
			}
		} catch {
			
			files.Add (new GearFile (){ x = 0, y = 0, rotation = 0, type = 0, activeKey = KeyCode.None });
		}

		return files;
	}

	public static bool WriteGearFileOnXML(List<GearFile> files){

		CheckComponents();

		string text = "<gearfiles>" + "\n";

		foreach (GearFile file in files){
			text += "<gearfile";
			text += " x = '" + file.x + "'";
			text += " y = '" + file.y + "'";
			text += " rotation = '" + file.rotation + "'";
			text += " type = '" + file.type + "'";
			text += " activeKey = '" + file.activeKey.ToString() + "'";
			text += "/>";
			text += "\n";
		}

		text += "</gearfiles>";

		try {
			using (StreamWriter writer = new StreamWriter(gearPath)){

				writer.WriteLine(text);
				writer.Close();
			}

			return true;
		} catch {
			return false;
		}
	}
}