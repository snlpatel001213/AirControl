using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CreateDBScript : MonoBehaviour {

	// public Text DebugText;

	// Use this for initialization
	void Start () {
		StartSync();
	}

    private void StartSync()
    {
        var ds = new DataService("tempDatabase.sqlite");
        ds.CreateDB();
        
        var people = ds.GetPersons ();
        ToConsole (people);
        people = ds.GetPersonsNamedVishnu ();
        ToConsole("Searching for Vishnu ...");
        ToConsole (people); 
    }
	
	private void ToConsole(IEnumerable<Person> people){
		foreach (var person in people) {
			ToConsole(person.ToString());
		}
	}
	
	private void ToConsole(string msg){
		// DebugText.text += System.Environment.NewLine + msg;
		Debug.Log (msg);
	}
}
