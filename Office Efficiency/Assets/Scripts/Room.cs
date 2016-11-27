using UnityEngine;
using System.Collections;
using System; //For Guid
using System.Collections.Generic; //for Lists
using System.Linq; //for FirstOrDefault etc Linq
//using UnityEngine.Events; //for coroutines - nvm, not monobehaviour


/// <summary>
/// A generic class for a room, should probably be inherited by more specific ones.
/// Instantiate these for the building as needed.
/// </summary>
/// <creator>@Zaelot at Junction2016</creator>
public class Room { //crumbled under pressure, now Monobehaviour - nope, that does not work for us
	public string roomName;
	public int roomFloor;
	public Guid roomGuid;
	public List<Occupant> roomOccupants;
	//public List<Occupant> roomOccupants { get; set; } //was considering private/public combo
	//public Dictionary<DateTime, List<Occupant>> roomReservations; //hmm, wonder how to include the ending time
	public List<RoomReservation> roomReservations; //new try
	public GameObject roomGO;
	//public float roomTemperature; //current temperature - think this should be in the Temperature
	//public DateTime roomReservationEnding; //when the current resrvation ends. Still wondering how to list this. :|
	public TimeSpan roomAvailability; //active hours of the room
	public int roomCapacity;
	public int roomNumber; //future proofing - I'll go with the name and guid for now.
	private Temperature temperature;
	public Temperature roomTemperature {
		get{ 
			if( temperature == null ) {
				if( roomGO && roomGO.GetComponent<Temperature>() != null )
					temperature = roomGO.GetComponent<Temperature>();
				//else if( MainManager.Instance.rooms.First( r => r.roomGuid == roomGuid ) ) {
				//~Z 2016-11-27 | starting to be too tired for this. don't even know what I'm doing here. >.<
				else if( roomGO )
					temperature = roomGO.AddComponent<Temperature> ();				
			} 
			return temperature;
		}//end.get
		set {
			if( temperature == null )
				temperature = value;
		}
	}//end.roomTemperature



	public Room( List<Occupant> occupants, string name = "", int floor = 0, 
		GameObject room = null, //float temperature = 0f, Dictionary<DateTime, List<Occupant>> reservations = null )
		List<RoomReservation> reservations = null, int capacity = 5, Temperature temp = null, int number = 0 )
	{
		if( !String.IsNullOrEmpty(name) )
			roomName = name;
		else
			roomName = "New room";
		if( occupants != null && occupants.Count > 0 )
			roomOccupants = occupants;
		else
			roomOccupants = new List<Occupant>();
		roomFloor = floor;
		roomGuid = new Guid();
		if (reservations != null)
			roomReservations = reservations;
		else
			roomReservations = new List<RoomReservation>();
//			roomReservations = new Dictionary<DateTime, List<Occupant>>();
		if( room )
			roomGO = room;
		else
			roomGO = GameObject.Instantiate( new GameObject( roomName ) ); //I think just using new GameObject() is enough
		roomTemperature = temp;											   // but this can be used for Resources.Load(prefab)
		roomCapacity = capacity;
		//var tmp = roomGO.AddComponent<Room>();
		//tmp = this;
		if( MainManager.Instance.rooms != null )
			roomNumber = MainManager.Instance.rooms.Count;
		else
			roomNumber = number;
	} //End.Room() - constructor

	public Room()
	{
		roomName = "New room";
		roomFloor = 0;
		roomGuid = new Guid();
		roomOccupants = new List<Occupant>();
//		roomReservations = new Dictionary<DateTime, List<Occupant>>();
		roomReservations = new List<RoomReservation>();
		roomGO = new GameObject( roomName );
		roomGO.AddComponent<DisplayRoomStats>().thisRoom = this;
		//roomTemperature = roomGO.AddComponent<Temperature>();//25f;
		temperature = roomGO.AddComponent<Temperature>();
	} //End.Room() - constructor overload

	public Room( int floor )
	{
		Debug.Log("Creating a new room " + floor);
		new Room(); //hmm, looks iffy, hope it works
		roomFloor = floor;
		roomName = "Room " + floor;
		//roomGO.name = roomName;
		//StartCoroutine( DebugRoomNameChange );
		//can't run a coroutine here, since not a monobehaviour class..
		MainManager.Instance.DebugDelayedChange( this );
	} //End.Room() - constructor overload



	public static Room GetFreeRoomFor( int participating, DateTime starting, DateTime ending )
	{
		//TODO pull current existing rooms from MainManager, and check which one are free of reservations on time.
		var trying = MainManager.Instance.rooms.First( r => r.roomCapacity > participating && r.IsRoomFree(starting, ending) );

		return trying; //don't care if it came out null, need to check for thart on the receiving end.
		//return new Room();
	} //End.GetFreeRoomFor()

	public bool IsRoomFree( DateTime starting, DateTime ending )
	{
//		if( this.roomReservations.ContainsKey() )
		//if( roomReservations.First( r => r.timeStarting.Equals(starting) ) != null )
		//occurs on the same date, and at the same time (whether it started before and ends after, 
		//	or starts before the new one ends) || ~Z 2016-11-27 | hope this works >.<
		if( roomReservations.First( r => (r.timeStarting.Date == starting.Date || r.timeEnding.Date == ending.Date ) && 
			( (r.timeStarting <= starting && r.timeEnding >= ending) || 
				(r.timeStarting >= starting && r.timeStarting <= ending) ) 
		) != null )
			return false;
//		else if( roomAvailability. ) 
//			return false;


		return true;
	} //End.IsRoomFree()

} //End.Room{}


/// <summary>
/// Room reservation.
/// </summary>
/// <remarks>Had to make a helper class to include both starting and ending time of the reservation.</remarks>
public class RoomReservation 
{
	public List<Occupant> participating; //this wasn't this easy, apparently we just need the number of people participating...
	public DateTime timeStarting;
	public DateTime timeEnding;
	private int amount;
	public int participatingAmount { //the one doing the reservation doesn't necessarily know(or want to) fill all participants
		get { 
			if( amount <= 0 || amount < participating.Count )
				return participating.Count;
			else
				return amount;
		}
		set{ amount = value; }
	}//end.participatingAmount
	public Room reserVariotionRoom;

	//want a specific room
	//geez, wanted null for ending, but nope
	public RoomReservation( List<Occupant> participants, DateTime starting, DateTime ending, Room room, int amountParticipating = 0 ) 
	{
		if( participants == null || participants.Count <= 0 ) //|| starting == null ) //apparently DateTime always has a default value
			throw new Exception( "Can't make a reservation without all of the information" );
		//if( starting < DateTime.Now )
		if( ending < DateTime.Now || starting > ending ) //don't actually care whether it already started or not, people can make ongoing reservations
			throw new Exception( "The reservation ending time already passed." );
		
		participating = participants;
		timeStarting = starting;
		timeEnding = ending;
		participatingAmount = amount;
		//TODO ~Z 2016-11-27 | Make a check on whether the room is free during this time.
		reserVariotionRoom = room; //guess it's good to have the room here as well, though this is mostly pulled from the Room item
	} //End.RoomReservation() - constructor

	//Just want a free room, don't care about which one.
	public RoomReservation( List<Occupant> participants, DateTime starting, DateTime ending, int amountParticipating = 0 ) 
	{
		if( participants == null || participants.Count <= 0 ) // || starting == null )
			throw new Exception( "Can't make a reservation without all of the information" );
		if( starting < DateTime.Now || starting > ending )
			throw new Exception( "The reservation starting time already passed." );

		participating = participants;
		timeStarting = starting;
		timeEnding = ending;
		participatingAmount = amount;
		//reserVariotionRoom = MainManager.Instance.GetFreeRoomFor( 
		reserVariotionRoom = Room.GetFreeRoomFor( 
			amountParticipating <= 0 ? participants.Count : ( amount < participants.Count ? participants.Count : amount ),
			starting, ending );
	} //End.RoomReservation() - constructor

} //End.RoomReservation{}
