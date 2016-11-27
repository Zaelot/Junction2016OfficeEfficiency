using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

/// <summary>
/// Networking - to handle the API communication.
/// </summary>
/// <creator>@Zaelot at Junction2016</creator>
public class Communication : MonoBehaviour {

	//TODO ~Z 2016.11.27 | Still needs JSON deserialization, receiving data etc.
	//TODO ~Z 2016.11.27 | We also need our own custom Template.

	public void TestData()
	{
		MainManager.Instance.SetTemperature();
	} //End.TestData()

	private void ReceiveData( string response )
	{
		if( string.IsNullOrEmpty(response) ) {
			Debug.LogWarning( "Received empty data." );
			return;
		}
		Debug.Log( "Received: \n" + response ); //debug

		//should probably put in try/catch
		var data = JsonUtility.FromJson( response ); //FIXME geez, this requires an actual type....
		if( data != null ) {
			Debug.Log( data );
		}
			
	} //End.ReceiveData

	// Use this for initialization
//	void Start () {
//	
//	}
	
	// Update is called once per frame
//	void Update () {
//	
//	}

} //End.Communication{}
