using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Launch : MonoBehaviour
{
    public Toggle serverTg;
    public Toggle clientTg;

    public Text IpText;
    public InputField portIF;
    public InputField ipIF;
    public Button startBtn;

    public Image uiParent;

    public DebugLockStepMode mode = DebugLockStepMode.Server;

    public int port = 9050;

    // [HideInInspector]
    // public GameObject GameManagerObj;

    private string localIpAddress;

    // private GameObject containerObj;

    public string SetGet_str_ipAddress
    {
        set { localIpAddress = value; }
        get
        {
            localIpAddress = NetTool.IP(address.IPv4);
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

        this.portIF.text = port.ToString();
    }



    private void ShowLocalIP()
    {

        this.IpText.text = "当前IP：" + SetGet_str_ipAddress;
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
        port = int.Parse(portIF.text);

        if (port < 0 || port > 65535) return;

        SceneManager.LoadScene("SampleServer");

        GameSetting.ServerIp = SetGet_str_ipAddress;
        GameSetting.ServerPort = port;

        Debug.Log($"启动Server  IP {SetGet_str_ipAddress} Port {port}  ");

        // gs.StartServer(port);

        // uiParent.gameObject.SetActive(false);
    }

    private void StartClient()
    {
        port = int.Parse(portIF.text);
        var ip = ipIF.text;

        GameSetting.ServerIp = ip;
        GameSetting.ServerPort = port;

        Debug.Log($"开始连接Server IP {GameSetting.ServerIp} Port {GameSetting.ServerPort}  ");

        SceneManager.LoadScene(GameSceneSetting.BattleTestScene);
    }




    public enum DebugLockStepMode
    {
        Server,
        Client
    }

}
