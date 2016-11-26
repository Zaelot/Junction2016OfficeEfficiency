using UnityEngine;
using System.Collections;

/// <summary>
/// Adjusts the temperature of the room based on the amount of current and projected occupants.
/// </summary>
public class Temperature : MonoBehaviour {

	//~Z 2016-11-26 | Should probably make a singleton of this too - unless we attach an individual one to each room.
	//				Actually, that sounds smarter.

	string _nodeId; //what to connect to
	public string nodeId { public get { return _nodeId; } private set; }


	public Temperature( string node )
	{
		_nodeId = node;

	} //End.Temperature() - constructor

	// Use this for initialization
	void Start () {
	
	} //End.Start()
	
	// Update is called once per frame
	void Update () {
	
	} //End.Update()
} //End.Temperature{}

/* Notes on API's

Data for heating, water usage, and water cooler electricity usage
Keila_utilities_semicolon.csv	(placed in Assets/Resources)[field separator ; and decimal ,]
Data explanation:
DomesticWater_m3 = Total water usage in the building. Unit cubic meter
WarmTapWater_m3 = Usage of the warm water that comes from tap, Unit cubic meter
DistrictHeating_kWh = Heating energy of the building. Unit kWh
Electricity_of_cooling_machines_kWh = Electricity that cooling system uses. Unit kWh
Total_Electricity_kWh = Total electricity consumption in the building. Unit kWh

Live cencsors for Air Quality:
https://hackathon.720.fi/nodes/

Sample Data:
{
    "data": {
        "nodes": [
            {
                "city": "Espoo",
                "node_id": "0e34909f-f07f-417c-a667-bf7b12757eef",
                "street": "Keilaniemi, Keilalahdentie",
                "sensortypes": [
                    "co2",
                    "relative_humidity_percent",
                    "temperature_celsius",
                    "voc_ch2o_equiv"
                ],
                "country": "Finland",
                "timezone": "Europe/Helsinki",
                "area_name": "7. krs Tieto Oyj, Keilaniemi",
                "node_name": "Avotila It\u00e4"
            }, 
            <... more ...>
        },
    "status": 200
}


https://hackathon.720.fi/nodes/<node_id>/measurements?from=<timestamp>&until=<timestamp>&aggregate=<granularity>
junction_hackathon@720.fi / i<3python

Sample Data:
{
    "data": {
        "measurements": [
            {
                "record_time": "2016-11-23T00:00:00",
                "sensors": {
                    "co2": {
                        "value_avg": 570.0,
                        "value_max": 570.0,
                        "value_min": 570.0,
                        "value_stddev": 0.0
                    },
                    "relative_humidity_percent": {
                        "value_avg": 31.0,
                        "value_max": 31.0,
                        "value_min": 31.0,
                        "value_stddev": 0.0
                    },
                    "temperature_celsius": {
                        "value_avg": 22.4461536407471,
                        "value_max": 22.6,
                        "value_min": 22.4,
                        "value_stddev": 0.0877061365017857
                    },
                    "voc_ch2o_equiv": {
                        "value_avg": 957.5,
                        "value_max": 960.0,
                        "value_min": 950.0,
                        "value_stddev": 4.52267016866645
                    }
                }
            },
            <... more …>
        ]
    },
    "status": 200
}


Data explanation:
<node_id> is a UUID, which can be retrieved using the node API (see previous paragraph)
<timestamp> is a timestamp in the format [YYYY]-[mm]-[dd]T[HH]:[MM]:[SS] (example: 2016-11-25T18:45:00)
<granularity> defines the level of detail for the retrieved measurement aggregates. Possible values are: 
- 1d (for 1-day aggregates)		[maximum of 93d]
- 6h (for 6-hour aggregates)	[maximum of 32d]
- 1h (for 1-hour aggregates)	[maximum of 8d]
- 5m (for 5-minute aggregates)	[maximum of 2h]

*/

/* Idea outline
Temperature Maintenance: 
No heating, cooling or lights in the conference room until someone has booked it. The temperature of the building 
(where people are present) to be maintained at 25 degrees.
If you need the conference room then book it so that the temperature can be adjusted before the meeting.
Sensors on each door will keep track of the number of people in the room and will be used to maintain the temperature 
in the room by counting the number of people.
*/

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
