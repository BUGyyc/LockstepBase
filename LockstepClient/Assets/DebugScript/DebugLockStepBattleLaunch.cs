using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

using LiteNetLib;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System;

public class DebugLockStepBattleLaunch : MonoBehaviour
{
    public Toggle serverTg;
    public Toggle clientTg;

    public Text IpText;
    public InputField portIF;
    public InputField ipIF;

    public Button startBtn;

    public Image uiParent;

    public DebugLockStepMode mode = DebugLockStepMode.Server;


    public GameObject GameManagerObj;

    private string localIpAddress;

    private GameObject containerObj;

    public string SetGet_str_ipAddress
    {
        set { localIpAddress = value; }
        get
        {
            localIpAddress = IP(address.IPv4);
            return localIpAddress;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //chooseModeGroup
        startBtn.onClick.AddListener(OnClick);
        serverTg.onValueChanged.AddListener(OnServerTg);
        clientTg.onValueChanged.AddListener(OnClientTg);
        initView();
        UpdateView();

        ShowLocalIP();
    }

    private void initView()
    {
        switch (mode)
        {
            case DebugLockStepMode.Server:
                this.clientTg.isOn = false;
                this.serverTg.isOn = true;
                break;
            case DebugLockStepMode.Client:
                this.clientTg.isOn = true;
                this.serverTg.isOn = false;
                break;
        }
    }

    private void OnServerTg(bool val)
    {
        this.mode = val ? DebugLockStepMode.Server : DebugLockStepMode.Client;

        UpdateView();
    }

    private void OnClientTg(bool val)
    {
        this.mode = val == false ? DebugLockStepMode.Server : DebugLockStepMode.Client;

        UpdateView();
    }

    private void UpdateView()
    {
        switch (mode)
        {
            case DebugLockStepMode.Server:
            default:
                this.IpText.gameObject.SetActive(true);
                this.ipIF.gameObject.SetActive(false);
                break;
            case DebugLockStepMode.Client:
                this.ipIF.gameObject.SetActive(true);
                this.IpText.gameObject.SetActive(false);

                this.ipIF.text = SetGet_str_ipAddress;

                break;
        }

        this.portIF.text = "5000";
    }



    private void ShowLocalIP()
    {

        this.IpText.text = "本机IP：" + SetGet_str_ipAddress;
    }

    private void OnClick()
    {
        switch (mode)
        {
            case DebugLockStepMode.Client:

                StartClient();
                break;

            case DebugLockStepMode.Server:

                StartServer();
                break;

            default:
                break;
        }
    }

    private void StartServer()
    {
        int port = int.Parse(portIF.text);

        if (port < 0 || port > 65535) return;

        if (containerObj != null) return;
        else containerObj = new GameObject("ServerContainer");
        if (containerObj.GetComponent<LockstepBattleServer>() != null) return;


        var gs = containerObj.AddComponent<LockstepBattleServer>();
        //gs._serverBall = serverObj;

        Debug.Log($"启动Server  IP {SetGet_str_ipAddress} Port {port}  ");

        gs.StartServer(port);

        uiParent.gameObject.SetActive(false);
    }

    private void StartClient()
    {
        int port = int.Parse(portIF.text);

        string targetIp = ipIF.text;

        if (port < 0 || port > 65535) return;

        if (containerObj != null) return;
        else containerObj = new GameObject("ClientContainer");
        if (containerObj.GetComponent<LockstepBattleClient>() != null) return;


        var gc = containerObj.AddComponent<LockstepBattleClient>();

        //gc._clientBall = clientObj;
        //gc._clientBallInterpolated = clientIntObj;

        Debug.Log($"启动client  客户端IP {SetGet_str_ipAddress} ");

        Debug.Log($"client 发起链接  IP {targetIp} Port {port}  ");

        gc.StartClient(port);

        uiParent.gameObject.SetActive(false);

        GameManagerObj.SetActive(true);
    }




    public enum DebugLockStepMode
    {
        Server,
        Client
    }


    public enum address
    {
        IPv4, IPv6
    }
    /// <summary>
    /// 获取本机IP
    /// </summary>
    /// <param name="Addfam">要获取的IP类型</param>
    /// <returns></returns>
    public string IP(address fam)
    {
        if (fam == address.IPv6 && !Socket.OSSupportsIPv6)
        {
            return null;
        }
        string output = "";
        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
            NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

            if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    if (fam == address.IPv4)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                    else if (fam == address.IPv6)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
        }
        return output;
    }
}
