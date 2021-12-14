using System.Collections;
using SQLite4Unity3d;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DB_InputClassDefinitions 
{
	/// <summary>
	/// Just [PrimaryKey] is added to the Id Attribute as we only want the updated value and dont want to accumulate it
	/// if one need to auto increment it the Id fielsd can be configured as [PrimaryKey, AutoIncrement]
	/// In this case it will fill up the database. [Not supported]
	/// </summary>
	/// <value></value>
    [PrimaryKey]
	public string direction  { get; set; }
    // public DateTime date  { get; set; }
	public int ActiveCamera { get; set; }
	// public string Surname { get; set; }
	// public int Age { get; set; }

}

