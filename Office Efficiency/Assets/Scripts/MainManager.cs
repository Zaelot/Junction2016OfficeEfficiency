using UnityEngine;
using System.Collections;

/// <summary>
/// Main manager. Core element that ties together the program structure.
/// </summary>
public class MainManager : MonoBehaviour {

	public static MainManager Instance;


	// Use this for initialization
	void Start () {
	
	} //End.Start()
	
	// Update is called once per frame
	void Update () {
	
	} //End.Update()
} //End.MainManager{}


/* Generic Unity notes:
 * Resources folder
 	It's always included in the builds, and works similar to a downloaded AssetBundle - ie, it should generally hold 
 	assets that will be instanciated at runtime. (Such as changing graphics or GameObject prefabs.)
 * Plugins folder
 	If we're going to need to include any external DLL's or the like.
*/

/* Project notes
I find it odd, that Tieto thinks BIM (Building Information Model) is too cumbersome, when in fact their own internal
database of their building is a sort of BIM on its own.
*/