using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GearControllerGame : GearController {

	public static GearControllerGame singleton;

	void Awake(){
		singleton = this;
	}

	public override void InstantiateGear (List<GearFile> files)
	{
		base.InstantiateGear (files);

		foreach(GearFile file in files){
			GearType type = StoreController.singleton.gearDb.GetGearTypeById(file.type);
			GameObject newGear = Instantiate (type.prefab) as GameObject;
			newGear.transform.SetParent (transform);
			newGear.name = file.x.ToString() + file.y.ToString();
			Gear gear = newGear.GetComponent<Gear> ();
			gear.SetFile (file);
		}

		SetJoints ();
	}

	public void SetJoints(){
		foreach (Transform gear in transform) {
			Gear currentGear = gear.GetComponent<Gear> ();
			if (currentGear) {
				Vector2 top = new Vector2 (currentGear.file.x, currentGear.file.y+1);
				Vector2 right = new Vector2 (currentGear.file.x +1, currentGear.file.y);
				GameObject topObject = GameObject.Find (((int)Mathf.Round(top.x)).ToString() + ((int)Mathf.Round(top.y)).ToString());
				GameObject rightObject = GameObject.Find (((int)Mathf.Round(right.x)).ToString() + ((int)Mathf.Round(right.y)).ToString());
				if (topObject != null) {
					Rigidbody2D currentBody = topObject.GetComponent<Rigidbody2D> ();
					if (currentBody != null) {
						FixedJoint2D joint = gear.gameObject.AddComponent<FixedJoint2D> ();
						joint.connectedBody = currentBody;
						int rot = (int)topObject.transform.rotation.eulerAngles.z;
						if (rot == 0)
							joint.connectedAnchor = new Vector2 (0,-1);
						else if (rot == 270)
							joint.connectedAnchor = new Vector2 (-1,0);
						else if (rot == 180)
							joint.connectedAnchor = new Vector2 (0, 1);
						else if (rot == 90)
							joint.connectedAnchor = new Vector2 (1,0);
					}
				}
				if (rightObject != null) {
					Rigidbody2D currentBody = rightObject.GetComponent<Rigidbody2D> ();
					if (currentBody != null) {
						FixedJoint2D joint = gear.gameObject.AddComponent<FixedJoint2D> ();
						joint.connectedBody = currentBody;
						int rot = (int)rightObject.transform.rotation.eulerAngles.z;
						if (rot == 0)
							joint.connectedAnchor = new Vector2 (-1,0);
						else if (rot == 270)
							joint.connectedAnchor = new Vector2 (0,-1);
						else if (rot == 180)
							joint.connectedAnchor = new Vector2 (1, 0);
						else if (rot == 90)
							joint.connectedAnchor = new Vector2 (0,1);
					}
				}
			}
		}
		ActiveGears ();
	}

	public void ActiveGears(){
		foreach (Transform gear in transform) {
			Gear currentGear = gear.GetComponent<Gear> ();
			if (currentGear) {
				currentGear.SetInGame ();
			}
		}
	}
}
