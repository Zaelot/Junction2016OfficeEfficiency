using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


/// <summary>
/// A generic class for a room, should probably be inherited by more specific ones.
/// Instantiate these for the building as needed.
/// </summary>
/// <creator>@Zaelot at Junction2016</creator>
public class Room {
	public string roomName;
	public int roomFloor;
	public Guid roomGuid;
	public List<Occupant> roomOccupants;
	//public List<Occupant> roomOccupants { get; set; } //was considering private/public combo
	public Dictionary<DateTime, List<Occupant>> roomReservations; //hmm, wonder how to include the ending time
	public GameObject roomGO;
	public float roomTemperature; //current temperature
	//public DateTime roomReservationEnding; //when the current resrvation ends. Still wondering how to list this. :|
	public TimeSpan roomAvailability; //active hours of the room

	public Room( List<Occupant> occupants, string name = "", int floor = 0, 
		Dictionary<DateTime, List<Occupant>> reservations = null, GameObject room = null, float temperature = 0f )
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
		if( reservations != null )
			roomReservations = reservations;
		else
			roomReservations = new Dictionary<DateTime, List<Occupant>>();
		if( room )
			roomGO = room;
		else
			roomGO = GameObject.Instantiate( new GameObject( roomName ) ); //I think just using new GameObject() is enough
		roomTemperature = temperature;										// but this can be used for Resources.Load(prefab) 
	} //End.Room() - constructor

	public Room()
	{
		roomName = "New room";
		roomFloor = 0;
		roomGuid = new Guid();
		roomOccupants = new List<Occupant>();
		roomReservations = new Dictionary<DateTime, List<Occupant>>();
		roomGO = new GameObject( roomName );
		roomTemperature = 25f;
	} //End.Room() - constructor overload

	public Room( int floor )
	{
		new Room(); //hmm, looks iffy, hope it works
		roomFloor = floor;
	} //End.Room() - constructor overload

	public static Room GetFreeRoomFor( int participating, DateTime starting, DateTime ending )
	{
		//TODO pull current existing rooms from MainManager, and check which one are free of reservations on time.
	} //End.GetFreeRoomFor()

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
		if( participants == null || participants.Count <= 0 || starting == null )
			throw new Exception( "Can't make a reservation without all of the information" );
		if( starting < DateTime.Now )
			throw new Exception( "The reservation starting time already passed." );
		
		participating = participants;
		timeStarting = starting;
		timeEnding = ending;
		participatingAmount = amount;
		reserVariotionRoom = room; //guess it's good to have the room here as well, though this is mostly pulled from the Room item
	} //End.RoomReservation() - constructor

	//Just want a free room, don't care about which one.
	public RoomReservation( List<Occupant> participants, DateTime starting, DateTime ending, int amountParticipating = 0 ) 
	{
		if( participants == null || participants.Count <= 0 || starting == null )
			throw new Exception( "Can't make a reservation without all of the information" );
		if( starting < DateTime.Now )
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
