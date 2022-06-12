using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Commons;
using Communicator;
 
/// <summary>
/// It displays the IP and Port of the client 
/// </summary>
public class ClientInfo : MonoBehaviour
{
    public TextMeshProUGUI IpText;
    public TextMeshProUGUI PortText;
    private float deltaTime;
 
    void Start () {
        IpText.text = "IP : "+CommonFunctions.clientIP;
        PortText.text = "Port : "+CommonFunctions.clientPort;
    }

    void update(){
        if (StaticClientInfo.IsActive){
            IpText.text = "IP : "+StaticClientInfo.ClientIP;
            PortText.text = "Port : "+StaticClientInfo.ClientPort;
            StaticClientInfo.IsActive=false;
        }
    }

}