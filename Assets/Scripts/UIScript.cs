using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class UIScripttest : MonoBehaviour {

	//declare default scene to load
	public string sceneToLoad = "CombRunner";

	public GameObject btn1;
	public GameObject btn2;
	public GameObject btn3;
	public GameObject char1;
	public GameObject char2;
	public GameObject char3;
	//private Transform btnPressed;

	public static readonly Vector3 lvlEntryPoint = new Vector3(0f,-2.2f,0f);

	public static readonly Vector3 roomEntryPoint = new Vector3(0f,1f,0f);

	private Vector3 btnPos1;
	private Vector3 btnPos2;
	private Vector3 btnPos3;
	private Vector3 charPos1;
	private Vector3 charPos2;
	private Vector3 charPos3;


  	//objects to transform
	public GameObject menuWrapper;  

	//private Transform cam;  
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;
	private Vector3 menuMarker;
	private Vector3 roomMarker;


	// Use this for initialization
	void Start () {	
		//declare main cam position
		//cam = Camera.main.transform;    //get camera transform
		menuWrapper = GameObject.Find("uiWrapper/menuWrapper"); 
		menuMarker = menuWrapper.transform.position;   //get starting position;
		roomMarker = menuWrapper.transform.position + new Vector3(0,-7,0);   //get starting position;
		Debug.Log("start");
		Debug.Log(menuMarker);
		Debug.Log(roomMarker);

		btnPos1 = btn1.transform.position;
		btnPos2 = btn2.transform.position;
		btnPos3 = btn3.transform.position;

		//Debug.Log (btnPos1);
		//Debug.Log (btnPos2);
		//Debug.Log (btnPos3);
	}


	// Update is called once per frame
	void Update () {
		
	}
	 
	// Load Scene
	public void LoadStage(string sceneName)  {
		//load scene
		SceneManager.LoadScene (sceneName);
	}

	// Load Lobby Menu Scene
	public void LoadMenu()  {
		//return to menu
		SceneManager.LoadScene ("Menu");
		//set default scene to load
		sceneToLoad = "CombRunner";
	}

	// Load Lobby Menu Scene
	public void ScrollToRoom()  {
		StartCoroutine(ScrollRoom(menuWrapper, roomMarker, .05f));

	}

	// Load Lobby Menu Scene
	public void ScrollToMenu()  {
		StartCoroutine(ScrollRoom(menuWrapper, menuMarker, .05f));

	}


	/*
	*  Rotate scenes making center positioned ready to load
	*/

	public void selectScene()  {
		//var tmp = sceneToLoad;
		//sceneToLoad = EventSystem.current.currentSelectedGameObject.name;
		//EventSystem.current.currentSelectedGameObject.name = tmp;

		//current clicked button
		var btnPressed = EventSystem.current.currentSelectedGameObject;

		Vector3 relativeLocation = btnPressed.transform.position;
		Vector3 targetLocation1 = lvlEntryPoint + relativeLocation;
		Vector3 targetLocation2 = btnPos2 + relativeLocation;
		Vector3 targetLocation3 = btnPos3 + relativeLocation;
		//Vector3 targetLocation = btn.transform.position + relativeLocation;
		float timeDelta = 0.05f;


		if (btnPressed.transform.position == lvlEntryPoint) {
			Debug.Log (btnPressed.transform.position);
			Debug.Log (btnPressed.name);

		} else if (btnPressed.name == btn1.name) {
			Debug.Log (btnPressed.name);

		} else if (btnPressed.name == btn2.name) {
			Debug.Log (btnPressed.name);


 			StartCoroutine(SmoothMove(btn1, targetLocation3, btn2, targetLocation1, btn3, targetLocation2, timeDelta));
 

		} else if (btnPressed.name == btn3.name) {
			Debug.Log (btnPressed.name);
			StartCoroutine(SmoothMove(btn1, targetLocation2, btn2, targetLocation3, btn3, targetLocation1, timeDelta));

		}




	}
	IEnumerator ScrollRoom(GameObject obj1, Vector3 target1, float delta)
	{
		// Will need to perform some of this process and yield until next frames
		float closeEnough = 0.2f;
		float distance1 = (obj1.transform.position - target1).magnitude;

		// GC will trigger unless we define this ahead of time
		WaitForEndOfFrame wait = new WaitForEndOfFrame();

		// Continue until we're there
		while(distance1 >= closeEnough )
		{
			// Confirm that it's moving
			Debug.Log("Executing ScrollRoom Movement");
			Debug.Log(obj1.name );

			// Move a bit then  wait until next  frame
			transform.position = Vector3.Slerp(transform.position, target1, delta);
			yield return wait;

			// Check if we should repeat
			distance1 = (obj1.transform.position - target1).magnitude;

		}

		// Complete the motion to prevent negligible sliding
		obj1.transform.position = target1;

		// Confirm  it's ended
		Debug.Log ("Movement ScrollRoom Complete");
	}

	IEnumerator SmoothMove(GameObject obj1, Vector3 target1,GameObject obj2, Vector3 target2,GameObject obj3, Vector3 target3, float delta)
	{
		// Will need to perform some of this process and yield until next frames
		float closeEnough = 0.2f;
		float distance1 = (obj1.transform.position - target1).magnitude;
		float distance2 = (obj2.transform.position - target2).magnitude;
		float distance3 = (obj3.transform.position - target3).magnitude;

		// GC will trigger unless we define this ahead of time
		WaitForEndOfFrame wait = new WaitForEndOfFrame();

		// Continue until we're there
		while(distance1 >= closeEnough && distance2 >= closeEnough && distance3 >= closeEnough)
		{
			// Confirm that it's moving
			Debug.Log("Executing Movement");

			// Move a bit then  wait until next  frame
			transform.position = Vector3.Slerp(transform.position, target1, delta);
			transform.position = Vector3.Slerp(transform.position, target2, delta);
			transform.position = Vector3.Slerp(transform.position, target3, delta);
			yield return wait;

			// Check if we should repeat
			distance1 = (obj1.transform.position - target1).magnitude;
			distance2 = (obj2.transform.position - target2).magnitude;
			distance3 = (obj3.transform.position - target3).magnitude;
		}

		// Complete the motion to prevent negligible sliding
		obj1.transform.position = target1;
		obj2.transform.position = target2;
		obj3.transform.position = target3;

		// Confirm  it's ended
		Debug.Log ("Movement Complete");
	}
}
