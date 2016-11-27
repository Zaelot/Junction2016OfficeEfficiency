using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DisplayRoomStats : MonoBehaviour {
	public Room thisRoom; 
	public string roomName1{ get { return thisRoom.roomName; } }
	public int roomFloor1{ get { return thisRoom.roomFloor; } }
	public Guid roomGuid1{ get { return thisRoom.roomGuid; } }
	public List<Occupant> roomOccupants1{ get { return thisRoom.roomOccupants; } }
	public List<RoomReservation> roomReservations1{ get { return thisRoom.roomReservations; } }
	public TimeSpan roomAvailability1{ get { return thisRoom.roomAvailability; } } //active hours of the room
	public int roomCapacity1{ get{ return thisRoom.roomCapacity; } }
	public int roomNumber1{ get { return thisRoom.roomNumber; } } //future proofing - I'll go with the name and guid for now.
	public Temperature temperature1{ get{ return thisRoom.roomTemperature; } }
	//those didn't display in inspector

	public string roomName;
	public int roomFloor;
	public Guid roomGuid;
	public List<Occupant> roomOccupants;
	public List<RoomReservation> roomReservations;
	public TimeSpan roomAvailability;
	public int roomCapacity;
	public int roomNumber;
	public Temperature temperature;

	// Use this for initialization
	void Start () {
		if( thisRoom == null ) {
			Debug.LogWarning ("Room was null");
			return;
		}

		roomName = thisRoom.roomName;
		roomFloor = thisRoom.roomFloor;
		roomGuid = new Guid();
		roomOccupants = new List<Occupant>();
		roomReservations = new List<RoomReservation>();
		temperature = thisRoom.roomTemperature;
		roomAvailability = thisRoom.roomAvailability;
		roomCapacity = thisRoom.roomCapacity;
		roomNumber = thisRoom.roomNumber;

	} //End.Start()
	

} //End.DisplayRoomStats{}
