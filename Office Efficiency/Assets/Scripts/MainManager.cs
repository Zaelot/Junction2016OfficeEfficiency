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
http://www.tieto.com/junction2016-data
xmp: https://tieto.iottc.tieto.com/measurement/measurements?pageSize=5&source=12223
junction_hacker / e*@ND_2foa

https://hackathon.720.fi/nodes/<node_id>/measurements?from=<timestamp>&until=<timestamp>&aggregate=<granularity>
junction_hackathon@720.fi / i<3python

I find it odd, that Tieto thinks BIM (Building Information Model) is too cumbersome, when in fact their own internal
database of their building is a sort of BIM on its own.

> Hey! I added the building floor plan image to the shared folder at https://drive.google.com/drive/folders/0BxzyscJdNAlhOENobkxiS0tMeXM 
> for people who wanted to do some visualizations. The floor plan image's width and height both are 72.53m. The objects 
> at http://sprunge.us/FiBj and the employee location data both have x and y coordinates available in meters, with 
> their origin (0, 0) at the lower left corner.


> the file has vector outlines for rooms and one for the floor itself. the format is very simple. the outline-field 
> contains a list of points on the map that are connected by walls. the first and last point should be connected to 
> close the shape.

> Floor height is 3(m)


*/


/* Off topic notes
Resources (icons):
noun project , fontawesome or flaticon
http://freebiesbug.com/psd-freebies/ui-kits/
Images:
https://unsplash.com/
https://www.pexels.com/
*/