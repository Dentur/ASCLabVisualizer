using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Marker
{
    public int val;
    public int dir;
    public GameObject marker;
    public bool stone;
}

public class CommandExecutioner : MonoBehaviour {
    public GameObject markerPrefab;
    public bool running = false;
    public float timing = 1.0f;
    public List<List<string>> commands;

    List<Marker> markers;

    public float timeSinceLastExecution = 0;

	// Use this for initialization
	void Start () {
        markers = new List<Marker>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!running)//Do nothing if the component is turned of
            return;
        timeSinceLastExecution += Time.deltaTime;
        if (timeSinceLastExecution < timing)//Return if we are still on cooldown
            return;
        timeSinceLastExecution = 0;
        if(commands.Count == 0)
        {
            running=false;
            return;
        }
        List<string> command = commands[0];
        commands.RemoveAt(0);
        int dir;
        Vector3 mv;
        Vector3 rot;
        Marker marker;
        command[0] = command[0].ToUpper();
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
                if (dir == 0)
                    transform.eulerAngles = new Vector3(0, 0, 0);
                else if (dir == 1)
                    transform.eulerAngles = new Vector3(0, 90, 0);
                else if (dir == 2)
                    transform.eulerAngles = new Vector3(0, 180, 0);
                else if (dir == 3)
                    transform.eulerAngles = new Vector3(0, 270, 0);

                break;
            case "TURN":
                dir = int.Parse(command[1]);
                if (dir == 0)
                    transform.eulerAngles = new Vector3(0, 0, 0);
                else if (dir == 1)
                    transform.eulerAngles = new Vector3(0, 90, 0);
                else if (dir == 2)
                    transform.eulerAngles = new Vector3(0, 180, 0);
                else if (dir == 3)
                    transform.eulerAngles = new Vector3(0, 270, 0);
                break;
            case "TELEPORT":
                int telePosX = int.Parse(command[1]);
                int telePosY = int.Parse(command[2]);
                transform.position = new Vector3(telePosX, 0, -telePosY);
                break;
            case "SSTONE":
                //dir = int.Parse(command[1]);
                //mv = new Vector3();
                //if (dir == 0)
                //    mv.y -= 1;
                //else if (dir == 1)
                //    mv.x += 1;
                //else if (dir == 2)
                //    mv.y += 1;
                //else if (dir == 3)
                //    mv.x -= 1;
                //marker = new Marker();
                //marker.stone = true;
                //marker.marker = (GameObject)Instantiate(markerPrefab, mv, new Quaternion());
                //markers.Add(marker);
                break;
            case "SMARKER":
                //dir = int.Parse(command[1]);
                //mv = new Vector3();
                //if (dir == 0)
                //    mv.z += 1;
                //else if (dir == 1)
                //    mv.x += 1;
                //else if (dir == 2)
                //    mv.z -= 1;
                //else if (dir == 3)
                //    mv.x -= 1;
                //marker = new Marker();
                //marker.stone = true;
                //marker.marker = (GameObject)Instantiate(markerPrefab, mv, new Quaternion());
                //
                //markers.Add(marker);
                break; 
            case "DMARKER":

                break;
            case "PRINTF":

                break;
            default:
                Debug.LogWarning("Command: " + command[0] + " not recogniced!");
                break;
        }
	}
}
