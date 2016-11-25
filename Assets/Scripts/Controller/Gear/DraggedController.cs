using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DraggedController : MonoBehaviour {
	
	private bool followMouse;
	private bool canDrop;
	private SpriteRenderer spriteRenderer;
	private Sprite icon;
	private GearArea gearArea;
	private List<Transform> blockArea;

	public void SetDragged(GearArea newGearArea, Sprite newIcon){
		gearArea = newGearArea;
		SetGearArea ();
		spriteRenderer = gameObject.AddComponent<SpriteRenderer> ();
		spriteRenderer.sortingLayerName = "Gear";
		spriteRenderer.sortingOrder = 10;
		icon = newIcon;
		followMouse = true;
		spriteRenderer.sprite = icon;
	}

	void SetGearArea(){
		blockArea = new List<Transform> ();
		foreach(Vector2 pos in gearArea.blockArea){
			GameObject gearAreaObject = Instantiate (StoreController.singleton.blockAreaPrefab) as GameObject;
			gearAreaObject.transform.SetParent(transform);
			gearAreaObject.transform.localPosition = new Vector2 (pos.x, pos.y);
			blockArea.Add (gearAreaObject.transform);
		}
	}

	void FixedUpdate(){
		if (followMouse){
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePosition = Calculate.NormalizePosition (mousePosition);
			if (mousePosition != (Vector2)transform.position) {
				transform.position = new Vector2 ((int)mousePosition.x, (int)mousePosition.y);
				CheckColor ();
			}
		}
	}

	void Update(){
		if (Input.GetMouseButtonDown (01)) {
			Rotate ();
		}
		if (Input.GetMouseButtonDown (00)) {
			Click ();
		}
	}

	void Cancel(){
		Destroy (gameObject);
		EditorController.singleton.CancelDrag ();
	}

	void CheckColor(){
		if (GearControllerEditor.singleton.CheckTileIsBlank ((Vector2)transform.position)) {
			spriteRenderer.color = Color.white;
		} else if (CheckIsBlocked ()) {
			spriteRenderer.color = Color.red;
		} else if (GearControllerEditor.singleton.CheckTileIsOverJustAdd ((Vector2)transform.position)) {
			spriteRenderer.color = Color.green;
		} else {
			spriteRenderer.color = Color.red;
		}
	}

	bool CheckIsBlocked(){
		foreach(Transform block in blockArea){
			if (GearControllerEditor.singleton.CheckTileIsBlocked ((Vector2)block.position)) {
				return true;
			}
		}
		return false;
	}

	void Click(){
		if (GearControllerEditor.singleton.CheckTileIsBlank ((Vector2)transform.position)) {
			Cancel ();
		} else if (!GearControllerEditor.singleton.CheckTileIsBlocked((Vector2)transform.position) && !CheckIsBlocked()){
			if (GearControllerEditor.singleton.CheckTileIsOverJustAdd ((Vector2)transform.position)) {
				if (EditorController.singleton.Buy ()) {
					EditorController.singleton.Drop ();
					Cancel ();
				}
			}
		}
	}

	void Rotate(){
		transform.Rotate(new Vector3 (0,0,90));
		CheckColor ();
	}
}