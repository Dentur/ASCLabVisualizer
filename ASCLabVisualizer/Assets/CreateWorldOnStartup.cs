using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class CreateWorldOnStartup : MonoBehaviour {

    public string wayFile;
    public List<List<string>> commands;
    public GameObject FloorPrefab;
    public GameObject WallPrefab;
    public GameObject EndPrefab;
    public GameObject playerObject;
    int startX, startY, endX, endY;

	// Use this for initialization
	void Start () {
        string[] args = Environment.GetCommandLineArgs();
        if (args.Length <= 2)
        {
            Debug.LogWarning("No start parameters set, appling default parameter");
            wayFile = @"C:/Projects/testWay.txt";
        }
        else
        {
            if (args[1] != "-way")
            {
                Debug.LogWarning("No -way in Start options found, using default path");
                wayFile = @"C:/Projects/testWay.txt";
            }
            else
            {
                wayFile = args[2];
            }
        }
        if (!File.Exists(wayFile))
        {
            Debug.LogError("Could not open way file " + wayFile + ", aborting");
            return;
        }
        StreamReader sr = new StreamReader(wayFile);
        string lvlFile = sr.ReadLine();
        //Create the list of commands
        commands = new List<List<string>>();
        while(!sr.EndOfStream)
        {
            List<string> command = new List<string>();
            command.AddRange(sr.ReadLine().Split());
            commands.Add(command);
        }
        sr.Close();
        Debug.Log("Read commands");

        //Load the level
        if(!File.Exists(lvlFile))
        {
            Debug.LogError("Could not load Level File!");
            return;
        }
        Debug.Log("Opening Level File: " + lvlFile);
        sr = new StreamReader(lvlFile);
        string[] lvlSize = sr.ReadLine().Split();
        int lvlXSize = Int32.Parse(lvlSize[0]);
        int lvlYSize = Int32.Parse(lvlSize[1]);
        Vector3 currentPos = new Vector3(0, 0, 0);
        Vector2 startPos;
        int lineNr = 0;
        while(!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            for(int i = 0; i < lvlXSize; i++)
            {
                if (line[i] == ' ' || line[i] == 'S' || line[i] == 'G')
                {
                    GameObject floor = (GameObject)Instantiate(FloorPrefab, currentPos, new Quaternion());
                }
                if(line[i] == 'G')
                {
                    GameObject end = (GameObject)Instantiate(EndPrefab, currentPos, new Quaternion());
                    endX = i;
                    endY = lineNr;
                }
                if(line[i]== '#')
                {
                    GameObject wall = (GameObject)Instantiate(WallPrefab, currentPos, new Quaternion());
                }
                if (line[i] == 'S')
                {
                    startX = i;
                    startY = lineNr;
                    playerObject.transform.position= new Vector3(startX, 0, -startY);//- because the grid is inverted
                }
                currentPos.x += 1;
            }
            currentPos.z -= 1;
            currentPos.x = 0;
            
            lineNr++;
        }
        Debug.Log("Read lvlFile");
        GameObject player = GameObject.Find("User");
        CommandExecutioner ce = (CommandExecutioner)player.GetComponent("CommandExecutioner");
        ce.commands = commands;
        ce.running = true;
	}
	
	// Update is called once per frame
	void Update () {
    }
}
