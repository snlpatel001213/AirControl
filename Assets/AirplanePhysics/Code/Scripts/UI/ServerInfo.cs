using UnityEngine;
using TMPro;
using Commons;
using System.Net;
using System.Net.Sockets;

/// <summary>
/// It displays the IP and Port of the client 
/// </summary>
public class ServerInfo : MonoBehaviour
{
    public TextMeshProUGUI IpText;
    public TextMeshProUGUI PortText;
    private float deltaTime;
    
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        Debug.Log("No network adapters with an IPv4 address in the system!");
        return "";
    }

    void Start () {
        IpText.text = "IP : "+GetLocalIPAddress();
        PortText.text = "Port : "+CommonFunctions.serverPort;
    }
}