using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class TranslateOBJ : MonoBehaviour
{
    public GameObject activeobj = null;
    Controller controller;
    float HandPalmPitch;
    float HandPalmYam;
    float HandPalmRoll;
    float HandWristRot;
    List<Hand> hands;
    bool active = false;
    public void printmes()
    {
        Debug.Log("Kuch Hua");
        active = true;
    }
    public void printmes2()
    {
        Debug.Log("Nahi Hua");
        active = false;
    }
    bool activel = false;
    public void printmesl()
    {
        Debug.Log("Kuch Hua");
        activel = true;
    }
    public void printmes2l()
    {
        Debug.Log("Nahi Hua");
        activel = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        activeobj = GameObject.FindGameObjectsWithTag("Active")[0];
    }

    // Update is called once per frame
    void Update()
    {
        activeobj = GameObject.FindGameObjectsWithTag("Active")[0];
        controller = new Controller();
        Frame frame = controller.Frame();
        hands = frame.Hands;
        
        if (frame.Hands.Count > 0)
        {
            Hand firsthand = hands[0];
        }
        HandPalmPitch = hands[0].PalmNormal.Pitch;
        HandPalmRoll = hands[0].PalmNormal.Roll;
        HandPalmYam = hands[0].PalmNormal.Yaw;

        HandWristRot = hands[0].WristPosition.Pitch;
        
        
        if (hands[0].GrabAngle > 3 && hands[0].IsLeft && frame.Hands.Count==1)
        {

            float s = hands[0].PalmPosition.Magnitude / 150;
            s = 0.005f * s;
            //float s = diffpos.Magnitude/150;
            Debug.Log(s);
            if(s>0.00001)
            activeobj.transform.localScale = new Vector3(s, s, s);
        }
        else if (hands[0].GrabAngle > 3 && hands[0].IsRight && frame.Hands.Count == 1)
        {
            float s = hands[0].PalmPosition.Magnitude / 150;
            activeobj.transform.rotation = hands[0].Rotation.ToQuaternion();
            //Debug.Log("pos " + hands[0].PalmPosition.Magnitude);
        }
        else if (frame.Hands.Count == 2)
        {
            
            if(hands[0].PalmNormal.z > 0 && hands[0].PalmNormal.z > 0)
            {
                activeobj.transform.Translate(new Vector3(0, 0, -1 * Time.deltaTime));
            }
            else if(hands[0].PalmNormal.z < 0 && hands[0].PalmNormal.z < 0){
                activeobj.transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime));
            }
            
            Debug.Log("Forward/Back: " + hands[0].PalmNormal.z + " " + hands[1].PalmNormal.z);
        }
        if (active == true)
        {
            movex();
        }
        else if(activel == true)
        {
            movex2();
        }
    }
    public void movex()
    {
        Debug.Log("Kuch Hua");
        activeobj.transform.Translate(new Vector3(0.5f * Time.deltaTime, 0, 0));
        //hands[0].PalmPosition.x 
    }
    public void movex2()
    {
        Debug.Log("Kuch Hua");
        activeobj.transform.Translate(new Vector3(-0.5f * Time.deltaTime, 0, 0));
        //hands[0].PalmPosition.x 
    }
    public void place()
    {
        Debug.Log("Placed");
        activeobj.gameObject.tag = "Untagged"; 
        activeobj = null;
    }
}
