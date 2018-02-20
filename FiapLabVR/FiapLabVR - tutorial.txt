First steps:
1) Create new project 3D as FiapLabVR
2) Build Settings > Android > Switch Platform 
In Player Settings:
 - Company Name = FiapMob
 - ProductName = FiapLabVR
 - Other Settings > Package Name =  fiap.mob.vr
 - Other Settings > Minimum API Level = 4.4 Kit Kat
 - XR Settings > Virtual Reality Supported = true
 - Virtual Reality SDKs > Add Cardboard
3) Assets > Import Package > Custom Package
 - Choose file GoogleVRForUnity_1.120.0.unitypackage
 - Press import
4) Create a player
 - Add empty object and rename as player 
 - include main camera inside
	- add Gvr Pointer Physics Raycaster component
 - inside camera add: 
	- GoogleVR > Prefabs > GvrEditorEmulator
	- GoogleVR > Prefabs > EventySystem > GvrEventSystem
	- GoogleVR > Prefabs > Cardboard > GvrReticlePointer
5) Create a terrain
6) Create a cube to interact
 - Add Event Trigger > Pointer Click
 - To something cool, as changing Cube > MeshRenderer.material 
 - Important: The object must to have a collider
7) Test inside Unity (without stereo vision)
8) Build to android platform
 - Android SDK : C:\Android_Config

To add movement to player:
1) Create object as target
 - Add tag = TargetToMove
2) Create C# script calling MovingPlayer

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarPlayer : MonoBehaviour {

    public Camera cameraRayCasting;
    public int distanceToMove = 100;

    private bool isMoving = false;
    private int speed = 10;
    private RaycastHit hit;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private float startTime;
    private float journeyLength;

    void Update() {

        Ray ray = cameraRayCasting.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f));

        if (GvrControllerInput.TouchDown || Input.GetMouseButtonDown (0)) {
            if (Physics.Raycast(ray, out hit, distanceToMove)) {
                if (hit.transform.tag == "TargetToMove") {
                    Debug.Log ("Find somewhere to move"); 

                    startPoint = transform.position;
                    endPoint = hit.transform.position;
                    startTime = Time.time;
                    journeyLength = Vector3.Distance (startPoint, endPoint);
                    isMoving = true;
                }
            }
        }

        if (isMoving) {
            float distanceCovered = (Time.time - startTime) * speed;
            float fracJourney = distanceCovered / journeyLength;
            Vector3 move = Vector3.Lerp (startPoint, endPoint, fracJourney);
            this.transform.position = new Vector3(move.x, this.transform.position.y, move.z);
            isMoving = (this.transform.position != endPoint);
        }
    }
}

3) Add to Player the new script component
 - Associate the camera
4) Test

 

