using UnityEngine;
using System.Collections;
using System.Collections.Generic; //for List<>
using System; //for DateTime
using UnityEngine.UI;

/// <summary>
/// Main manager. Core element that ties together the program structure.
/// </summary>
/// <remarks>It'a very easy for this to turn into a God object. Be careful.</remarks>
/// <creator>@Zaelot at Junction2016</creator>
public class MainManager : MonoBehaviour {

	//TODO ~Z 2016.11.27 | Make UI for Room reservation prompt | While at it, make the logic too
	//TODO ~Z 2016.11.27 | 

	private static MainManager _mainManager; //Singleton. Enables us to call the values from this from everywhere else without
	public static MainManager Instance //	passing an instance first.
	{
		get {
			if (_mainManager == null) {
				_mainManager = FindObjectOfType<MainManager> ();
			}
			return _mainManager;
		}
		private set {
			if (_mainManager == null)
				_mainManager = value;
			else
				Destroy(value);
		}
	} //End.Instance

	public List<Occupant> occupants;
	public List<Room> rooms; //could probably host buildings as well. Personally would prefer if it was all based off BIM's.
	//public List<DateTime> reservations;
	public List<RoomReservation> reservations; //FIXME Z 2016-11-27 | Argh, totally need a better system
	//TODO Send push notification to participants" 1 hour before the reservation starts
	//TODO could send them with additional SMS through 46elks

	public List<Occupant> occupantsOutside; //occupants that have left the building
	public Text textOutput;


	void Awake() {
		Instance = this; //assigning the singleton (or well, destroying the extra)
	} //End.Awake()

	// Use this for initialization
	void Start() {
		occupants = new List<Occupant>();
		rooms = new List<Room>();

		PopulateWithTestData();

		if( textOutput == null )
			textOutput = GameObject.Find("TextTitle").GetComponent<Text>();
	} //End.Start()
	
	// Update is called once per frame
	void Update() {
		
	} //End.Update()

	public void SetTemperature( Room room, float temperature )
	{
		var indx = rooms.IndexOf( room );
		rooms[indx].roomTemperature.SetTemperature( temperature );


	} //End.SetTemperature()

	private void PopulateWithTestData()
	{
		for( int i = 0; i < 5; ++i ) {
			occupants.Add( new Occupant() );
			rooms.Add( new Room( i ) );
		}
	} //End.PopulateWithTestData()

	string temperatureOutput = "Current temperature for room {0}  is {1}°C";
	int simulatedTemperature = 15;
	public void SimulatePolling()
	{
		//just something to debug with...
		rooms[0].roomTemperature.ReceiveTemperatureData( (float)simulatedTemperature++ );
		if( simulatedTemperature > 27 )
			simulatedTemperature = 15;
		textOutput.text = String.Format( temperatureOutput, rooms[0].roomName, simulatedTemperature );
	} //End.SimulatePolling()

	string temperatureSetting = "Adjusting temperature for room {0} to {1}°C";
	float simulatedTemperaturePlan = 18f;
	public void SimulateSettingTemperature()
	{
		SetTemperature( rooms[0], simulatedTemperaturePlan );
		textOutput.text = String.Format( temperatureSetting, rooms [0].roomName, simulatedTemperaturePlan );
	} //End.SimulateSettingTemperature()

	public void DebugDelayedChange( Room roomToChange )
	{
		StartCoroutine( DebugRoomNameChange(roomToChange) );
	} //End.DebugDelayedChange()

	private IEnumerator DebugRoomNameChange( Room assignToRoom )
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate(); //should carry over to next frame
		Debug.Log( "Renaming room with " + assignToRoom.roomName );
		//roomGO.name = roomName;
		var currentRoom = rooms[ rooms.IndexOf(assignToRoom) ];
		if( currentRoom != null && currentRoom.roomGO )
			currentRoom.roomGO.name = currentRoom.roomName;
		yield break;
	} //End.DebugRoomNameChange() - coroutine
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
Google magic (sur:fc):
https://www.google.com/search?q=person&source=lnms&tbm=isch&tbs=sur:fc
*/