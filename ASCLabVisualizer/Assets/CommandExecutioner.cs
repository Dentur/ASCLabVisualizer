using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

struct Marker
{
    public int val;
    public int dir;
    public GameObject marker;
    public bool stone;
    public int x, y;
}

public class CommandExecutioner : MonoBehaviour {
    public GameObject markerPrefab;
    public GameObject LookNorth;
    public GameObject LookSouth;
    public GameObject LookEast;
    public GameObject LookWest;
    public GameObject Compass;
    public GameObject DebugText;
    public bool running = false;
    public float timing = 1.0f;
    public List<List<string>> commands;
    public bool lerp;

    Vector3 lastPos, lastRot, newPos, newRot;

    List<Marker> markers;

    public float timeSinceLastExecution = 0;

    private int actualDir;

	// Use this for initialization
	void Start () {
        markers = new List<Marker>();
        //lastRot = transform.eulerAngles;
        //lastPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!running)//Do nothing if the component is turned of
            return;
        timeSinceLastExecution += Time.deltaTime;
        //if (lerp)
        //{
        //    transform.position = Vector3.Lerp(lastPos, newPos, (timeSinceLastExecution / (timing * 0.75f)));
        //    transform.eulerAngles = Vector3.Lerp(lastRot, newRot, (timeSinceLastExecution / (timing * 0.75f)));
        //}
        //else
        //{
        //    lastPos = transform.position;
        //    lastRot = transform.eulerAngles;
        //}
        //if (timeSinceLastExecution <= timing * 0.75f)
        //    lerp = false;
        //Execute
        if (timeSinceLastExecution < timing)//Return if we are still on cooldown
            return;
        timeSinceLastExecution = 0;
        if(commands.Count == 0)
        {
            running=false;
            return;
        }

        //Reset Look

        if (actualDir == 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            Compass.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (actualDir == 1)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            Compass.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (actualDir == 2)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            Compass.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (actualDir == 3)
        {
            transform.eulerAngles = new Vector3(0, 270, 0);
            Compass.transform.eulerAngles = new Vector3(0, 0, 270);
        }

        List<string> command = commands[0];
        commands.RemoveAt(0);
        int dir;
        Vector3 mv;
        Vector3 rot;
        Marker marker;
        command[0] = command[0].ToUpper();
        Debug.Log("Current command: " + command[0]);
        switch(command[0])
        {
            case "STEP":
                dir = int.Parse(command[1]);
                mv = new Vector3();
                if (dir == 0)
                    mv.z += 1;
                else if (dir == 1)
                    mv.x += 1;
                else if (dir == 2)
                    mv.z -= 1;
                else if (dir == 3)
                    mv.x -= 1;
                transform.Translate(mv, Space.World);
                //newPos=transform.position+mv;
                //lerp = true;
                break;
            case "MOVE":
                dir = int.Parse(command[1]);
                mv = new Vector3();
                if (dir == 0)
                    mv.z += 1;
                else if (dir == 1)
                    mv.x += 1;
                else if (dir == 2)
                    mv.z -= 1;
                else if (dir == 3)
                    mv.x -= 1;
                transform.Translate(mv, Space.World);
                //newPos=transform.position+mv;
                //lerp = true;

                if (dir == 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    Compass.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (dir == 1)
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);
                    Compass.transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (dir == 2)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    Compass.transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (dir == 3)
                {
                    transform.eulerAngles = new Vector3(0, 270, 0);
                    Compass.transform.eulerAngles = new Vector3(0, 0, 270);
                }
                actualDir = dir;
                break;
            case "LOOK":
                dir = int.Parse(command[1]);
                if (dir == 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    Compass.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (dir == 1)
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);
                    Compass.transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (dir == 2)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    Compass.transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (dir == 3)
                {
                    transform.eulerAngles = new Vector3(0, 270, 0);
                    Compass.transform.eulerAngles = new Vector3(0, 0, 270);
                }
                break;
            case "TURN":
                dir = int.Parse(command[1]);
                if (dir == 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    Compass.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (dir == 1)
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);
                    Compass.transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (dir == 2)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    Compass.transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else if (dir == 3)
                {
                    transform.eulerAngles = new Vector3(0, 270, 0);
                    Compass.transform.eulerAngles = new Vector3(0, 0, 270);
                }
                actualDir = dir;
                break;
            case "TELEPORT":
                int telePosX = int.Parse(command[1]);
                int telePosY = int.Parse(command[2]);
                transform.position = new Vector3(telePosX, 0, -telePosY);
                newPos = transform.position;
                lastPos = transform.position;
                break;
            case "SSTONE":
                dir = int.Parse(command[1]);
                marker = new Marker();
                marker.stone = true;
                marker.marker = (GameObject)Instantiate(markerPrefab, transform.position, new Quaternion());
                mv = new Vector3();
                if (dir == 0)
                {
                    mv = new Vector3(0, 0, 1);
                }
                else if (dir == 1)
                {
                    mv = new Vector3(1, 0, 0);
                }
                else if (dir == 2)
                {
                    mv = new Vector3(0, 0, -1);
                }
                else if (dir == 3)
                {
                    mv = new Vector3(-1, 0, 0);
                }
                marker.marker.transform.position = (mv+marker.marker.transform.position);
                marker.x = (int)marker.marker.transform.position.x;
                marker.y = (int)marker.marker.transform.position.z;
                Transform stone = marker.marker.transform.FindChild("Stone");
                Debug.Log(stone);
                stone.gameObject.active = true;
                markers.Add(marker);

                break;
            case "SMARKER":
                dir = int.Parse(command[1]);
                int dir2 = int.Parse(command[2]);
                int val = int.Parse(command[3]);
                marker = new Marker();
                marker.stone = true;
                marker.marker = (GameObject)Instantiate(markerPrefab, transform.position, new Quaternion());
                mv = new Vector3();
                if (dir == 0)
                {
                    mv = new Vector3(0, 0, 1);
                }
                else if (dir == 1)
                {
                    mv = new Vector3(1, 0, 0);
                }
                else if (dir == 2)
                {
                    mv = new Vector3(0, 0, -1);
                }
                else if (dir == 3)
                {
                    mv = new Vector3(-1, 0, 0);
                }
                else if (dir == 8)
                {
                    mv = new Vector3();
                }
                marker.marker.transform.position = (mv + marker.marker.transform.position);
                marker.x = (int)marker.marker.transform.position.x;
                marker.y = (int)marker.marker.transform.position.z;
                rot = new Vector3();
                if (dir2 == 0)
                {
                    rot = new Vector3(0, 0, 0);
                }
                else if (dir2 == 1)
                {
                    rot = new Vector3(0, 90, 0);
                }
                else if (dir2 == 2)
                {
                    rot = new Vector3(0, 180, 0);
                }
                else if (dir2 == 3)
                {
                    rot = new Vector3(0, 270, 0);
                }
                marker.marker.transform.eulerAngles = rot;
                Transform text = marker.marker.transform.FindChild("Text");
                text.gameObject.SetActive(true);
                text.GetComponent<TextMesh>().text = val.ToString();
                markers.Add(marker);
                break; 
            case "DMARKER":
                
                dir = int.Parse(command[1]);
                mv = new Vector3();
                if (dir == 0)
                {
                    mv = new Vector3(0, 0, 1);
                }
                else if (dir == 1)
                {
                    mv = new Vector3(1, 0, 0);
                }
                else if (dir == 2)
                {
                    mv = new Vector3(0, 0, -1);
                }
                else if (dir == 3)
                {
                    mv = new Vector3(-1, 0, 0);
                }
                else if(dir == 8)
                {
                    mv = new Vector3();
                }
                int posX = (int)(mv + transform.position).x;
                int posY = (int)(mv + transform.position).z;
                try
                {
                    int index = markers.FindIndex(k => k.x == posX && k.y == posY);
                    Debug.Log("Deleting marker at " + index + " at position " + markers[index].x + "x " + markers[index].y + "y");
                    Destroy(markers[index].marker);
                    markers.RemoveAt(index);
                }
                catch(Exception e)
                { }
                break;
            case "PRINTF":
                string debugText = "";
                for (int i = 1; i < command.Count; i++ )
                {
                    debugText += command[i];
                }
                DebugText.GetComponent<Text>().text = debugText;
                break;
            default:
                Debug.LogWarning("Command: " + command[0] + " not recogniced!");
                break;
        }
	}
}
