using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;


public static class DB_OutputClassDefinition 
{
    [PrimaryKey, AutoIncrement]
	public static int Id { get; set; }
	public static string Name { get; set; }
	public static string Surname { get; set; }
	public static int Age { get; set; }
}

