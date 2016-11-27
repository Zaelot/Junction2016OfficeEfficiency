using UnityEngine;
using System.Collections;
using System; //for Guids

/// <summary>
/// Generic class for an occupant (usually person, but we should take pets and robots into account later).
/// To track the users of the rooms (office workers, cleaners etc).
/// </summary>
/// <remarks>Actually with the new and andvanced systems like NEST we should allow individual climate control.</remarks>
/// <creator>@Zaelot at Junction2016</creator>
public enum OccupantType {
	Visitor,
	Employee,
	Robot,
	Pet
} 

public class Occupant {
	public Guid occupantGuid = new Guid();
	public OccupantType occupantType = OccupantType.Visitor;
	public string occupantName = "New occupant";		//could add separate first and last name
	public float occupantWeight = 70.0f; 				//in kg
	public Room occupantCurrentRoom;
	public float desiredTemperature = 21f;				//in °C

//	public Occupant() //everything is default, in the other constructor, so this one's irrelevant
//	{
//		
//	} //End.Occupant() - constructor

	public Occupant( OccupantType oType = OccupantType.Visitor, string name = "New occupant", 
		float weight = 70f, Room room = null )
	{
		occupantGuid = new Guid();
		occupantType = oType;
		occupantName = name;
		occupantWeight = weight;
		occupantCurrentRoom = room;
	} //End.Occupant() - constructor overload

} //End.Occupant{}
