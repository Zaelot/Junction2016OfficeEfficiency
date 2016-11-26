using UnityEngine;
using System.Collections;

/// <summary>
/// Adjusts the temperature of the room based on the amount of current and projected occupants.
/// </summary>
public class Temperature : MonoBehaviour {

	//~Z 2016-11-26 | Should probably make a singleton of this too - unless we attach an individual one to each room.
	//				Actually, that sounds smarter.

	// Use this for initialization
	void Start () {
	
	} //End.Start()
	
	// Update is called once per frame
	void Update () {
	
	} //End.Update()
} //End.Temperature{}

/* Notes from the first night
   "# 1 prompt: arrange meeting?\n",
    "# 2 meeting time\n",
    "# 3 number of people\n",
    "# 4 before the meeting time automatic reminder to all participants to confirm their presence\n",
    "# 5 if less than original number of people then an alternate meeting room be suggested \n",
    "\n",
    "\n",
    "\n",
    "# Preassign a room based on reservation (at the time the reservation is done).\n",
    "# features of the room: room number, max occupancy\n",
    "# features of occupants: names\n",
    "\n",
    "list_of_attendents = ['A', 'B', 'C', 'D', 'E', 'F', 'G']\n",
    "room = {'1':15, '2':8, '3':4}\n",
    "time = 12.00\n",
    "\n",
    "#  \"Send push notification to participants\" (first message sent immediately, second sent one hour earlier)\n",
    "#  Notification is public\n",
    "def notification(time, list_of_attendents):\n",
    "    #So the attendands in the list_of_attendents should be a class, that has a variable int occupiedRoom, and it this case you should go through them in a loop and compare that they are in the room time and return a new list of those.",
    "    return list_of_actual_attendees\n",
    "\n",
    "# \"If not identified participants, sends a request to the organizer (host) to ask about the number of attending participants\" \n",
    "# 20-min prior to start of the reservation, checks the amount of participants and their location.\n",
    "\n",
    "\n",
    "# Assigns a new room that optimizes the room size,\n",
    "\n",
    "\n",
    "# distance of participants from the room,\n",
    "\n",
    "\n",
    "# and expected energy consumption.\n",
    "\n",
    "\n",
    "# Send push notification of the reserved room to the participants/host.\n",
*/ 
