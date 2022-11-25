using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Launch : MonoBehaviour
{
    public Toggle clientTg;
    public Toggle clientAndServerTg;
    public Toggle pureServerTag;

    public Text IpText;
    public InputField portIF;
    public InputField ipIF;
    public Button startBtn;

    public Image uiParent;

    public InputField playerIF;

    public Text playerText;

    public Text portText;

    public DebugLockStepMode mode = DebugLockStepMode.Client;

    public int port = 9050;

    public uint PlayerNumber = 4;



    // [HideInInspector]
    // public GameObject GameManagerObj;

    private string localIpAddress;

    // private GameObject containerObj;

    public string SetGet_str_ipAddress
    {
        set { localIpAddress = value; }
        get
        {
            localIpAddress = LiteNetLib.NetUtils.GetLocalIp(LiteNetLib.LocalAddrType.IPv4);
            return localIpAddress;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //chooseModeGroup
        startBtn.onClick.AddListener(OnClick);

        clientTg.onValueChanged.AddListener(OnClientTg);
        clientAndServerTg.onValueChanged.AddListener(OnClientAndServerTg);
        pureServerTag.onValueChanged.AddListener(OnPureServerTg);

        initView();
        UpdateView();

        ShowLocalIP();
        //       4294967296L
        const long ONE = 4294967296L;
        long a = 26986075409L;
        //       25769803776
        long b = ONE * 6L;
        //       17179869184
        long c = ONE << 2;
        //       34359738368
        long d = ONE << 3;
        //       68719476736
        long e = ONE << 4;
        // False  a = 26986075409  b= 25769803776   1
        // Debug.LogError($" {a == b}  a = {a}  b= {b}  c= {c}  d= {d} e= {e}");

        long target = a;
        long offset = target >> 32;
        long baseVal = 1L << 32;
        long res = baseVal * offset;
        Debug.LogError($" offset {offset} baseVal {baseVal}    res = {res}  target= {target}   {target == res}");



        long x = a;// 26986075409L;
        long y = x >> 31;
        long z = x >> 32;

        Debug.LogError($"   x = {x}  y= {y}  z= {z}   {x >> 32} {x >> 31}");
        //26986075409L

        //4294967296
        long j = (long)1 << 32;
        //8589934592
        long k = (long)1 << 33;
        //2147483648
        long l = (long)1 << 31;

        long val6 = 6;
        long h6 = j * val6;
        long val7 = 7;
        long h7 = j * val7;

        Debug.LogError($" h6 {h6}  {a}   {h6 > a}  {a == h6}  ");
        Debug.LogError($" h7 {h7}  {a}   {h7 > a}  {a == h7}  ");
    }

    private void initView()
    {
        this.clientTg.isOn = DebugLockStepMode.Client == this.mode;
        this.clientAndServerTg.isOn = DebugLockStepMode.ClientAndServer == this.mode;
        this.pureServerTag.isOn = DebugLockStepMode.PureServer == this.mode;


        this.portIF.text = port.ToString();

        this.playerIF.text = PlayerNumber.ToString();
    }

    private void OnPureServerTg(bool val)
    {
        if (val)
        {
            this.mode = DebugLockStepMode.PureServer;
            UpdateView();
        }
    }

    private void OnClientAndServerTg(bool val)
    {
        if (val)
        {
            this.mode = DebugLockStepMode.ClientAndServer;
            UpdateView();
        }
    }

    private void OnClientTg(bool val)
    {
        if (val)
        {
            this.mode = DebugLockStepMode.Client;
            UpdateView();
        }
    }

    private void UpdateView()
    {
        switch (mode)
        {
            case DebugLockStepMode.PureServer:
            case DebugLockStepMode.ClientAndServer:
                this.IpText.gameObject.SetActive(true);
                this.ipIF.gameObject.SetActive(false);
                this.playerText.gameObject.SetActive(true);
                this.playerIF.gameObject.SetActive(true);
                break;
            case DebugLockStepMode.Client:
                this.ipIF.gameObject.SetActive(true);
                this.IpText.gameObject.SetActive(false);

                this.ipIF.text = SetGet_str_ipAddress;
                this.playerText.gameObject.SetActive(false);
                this.playerIF.gameObject.SetActive(false);
                break;

        }


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

            case DebugLockStepMode.ClientAndServer:

                StartClientAndServer();
                break;

            case DebugLockStepMode.PureServer:
                StartPureServer();
                break;

            default:
                break;
        }
    }

    private void StartPureServer()
    {
        SceneManager.LoadScene("PureServer", LoadSceneMode.Single);
    }

    private void StartClientAndServer()
    {
        port = int.Parse(portIF.text);

        if (port < 0 || port > 65535) return;

        PlayerNumber = (uint)int.Parse(playerIF.text);

        SceneManager.LoadScene("ClientAndServer");

        NetSetting.ServerIp = SetGet_str_ipAddress;
        NetSetting.ServerPort = (uint)port;

        NetSetting.PlayerNumber = PlayerNumber;

        //Debug.Log($"启动Server  IP {SetGet_str_ipAddress} Port {port}  PlayerNumber {PlayerNumber}  ");
    }

    private void StartClient()
    {
        port = int.Parse(portIF.text);
        var ip = ipIF.text;

        NetSetting.ServerIp = ip;
        NetSetting.ServerPort = (uint)port;

        //Debug.Log($"开始连接Server IP {NetSetting.ServerIp} Port {NetSetting.ServerPort}  ");

        SceneManager.LoadScene(GameSceneSetting.BattleTestScene);
    }




    public enum DebugLockStepMode
    {
        Client,
        ClientAndServer,
        PureServer
    }

}


