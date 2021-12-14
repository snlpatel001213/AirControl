using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ExistingDBScript : MonoBehaviour {

	public Text DebugText;

	// Use this for initialization
	void Start () {
		var ds = new DataService ("existing.db");
		//ds.CreateDB ();
		var people = ds.GetPersons ();
		ToConsole (people);

		people = ds.GetPersonsNamedVishnu ();
		ToConsole("Searching for Vishnu ...");
		ToConsole (people);

		ds.CreatePerson ();
		ToConsole("New person has been created");
		var p = ds.GetVishnu ();
		ToConsole(p.ToString());

	}
	
	private void ToConsole(IEnumerable<Person> people){
		foreach (var person in people) {
			ToConsole(person.ToString());
		}
	}

	private void ToConsole(string msg){
		DebugText.text += System.Environment.NewLine + msg;
		Debug.Log (msg);
	}

}
