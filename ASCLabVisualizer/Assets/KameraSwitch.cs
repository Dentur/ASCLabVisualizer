using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraSwitch : MonoBehaviour {

    public GameObject cameraUser;
    public GameObject cameraFly;

    private bool cameraUserInUse = true;
    private bool prellschutz = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Jump") >= 0.7)
        {
            if (!prellschutz)
            {
                prellschutz = true;
                cameraUserInUse = !cameraUserInUse;
                Debug.Log("Switching Camera");
                if (cameraUserInUse)
                {
                    cameraUser.SetActive(false);
                    cameraFly.SetActive(true);

                    Cursor.lockState = CursorLockMode.Confined;
                }
                else
                {
                    cameraUser.SetActive(true);
                    cameraFly.SetActive(false);

                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
        else
            prellschutz = false;
	}
}
