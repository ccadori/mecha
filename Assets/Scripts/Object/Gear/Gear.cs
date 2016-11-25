using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gear : MonoBehaviour {

	internal Rigidbody2D body;
	internal bool active;
	private bool inGame;

	public GearArea gearArea;
	public List<GameObject> gearAreaObjects = new List<GameObject>();
	public GearFile file;

	internal virtual void Awake(){
		body = GetComponent<Rigidbody2D> ();
	}

	public virtual void SetFile(GearFile newFile){
		file = newFile;
		UpdateTransform ();
	}

	internal virtual void Update(){
		
		if (inGame) {
			if (Input.GetKey (file.activeKey)) {
				Active (true);
			} else {
				Active(false);
			}
		}
	}

	internal virtual void Active(bool state){
		active = state;
	}

	public virtual void SetInGame(){
		inGame = true;
		body.isKinematic = false;
	}

	public void UpdateTransform(){
		transform.localPosition = new Vector2 (file.x, file.y);
		transform.Rotate(new Vector3 (0,0, file.rotation));
	}

	//Editor
	public virtual void SetGearArea(GearArea newGearArea){
		gearArea = newGearArea;
		foreach(Vector2 pos in gearArea.addArea){
			Vector2 newPos = SetNewPosition(pos);
			GameObject gearAreaObject = Instantiate (StoreController.singleton.addAreaPrefab) as GameObject;
			SetAreaObject (gearAreaObject, newPos);
			gearAreaObjects.Add (gearAreaObject);
		}
		foreach(Vector2 pos in gearArea.blockArea){
			Vector2 newPos = SetNewPosition(pos);
			GameObject gearAreaObject = Instantiate (StoreController.singleton.blockAreaPrefab) as GameObject;
			SetAreaObject (gearAreaObject, newPos);
			gearAreaObjects.Add (gearAreaObject);
		}
	}

	Vector2 SetNewPosition(Vector2 pos){
		if (file.rotation == 90) {
			pos = new Vector2 (-pos.y + file.x, -pos.x + file.y);
		} else if (file.rotation == 180) {
			pos = new Vector2 (-pos.x + file.x, -pos.y + file.y);
		} else if (file.rotation == 270) {
			pos = new Vector2 (pos.y + file.x, pos.x + file.y);
		} else {
			pos = new Vector2 (pos.x + file.x, pos.y + file.y);
		}
		return pos;
	}

	public void Sell(){
		if (file.type == 0)
			return;
		if (EditorController.singleton.Sell (file.type)) {

			foreach (GameObject gearAreaObject in gearAreaObjects) {
				Destroy (gearAreaObject);
			}

			Destroy (gameObject);
		}
	}

	public void SetAreaObject(GameObject gearAreaObject, Vector2 newPos){
		gearAreaObject.transform.SetParent(transform.parent);
		gearAreaObject.transform.localPosition = new Vector2 (newPos.x, newPos.y);
		gearAreaObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
		gearAreaObject.name = ((int)Mathf.Round(newPos.x)).ToString() + ((int)Mathf.Round(newPos.y)).ToString();
	}
}