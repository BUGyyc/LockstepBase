using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BM;
using ET;
using System.Security.Cryptography;

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

        if (GlobalSetting.QuickStartAPP)
        {
            OnClick();
        }
    }

    private void initView()
    {
        this.clientTg.isOn = DebugLockStepMode.Client == this.mode;
        this.clientAndServerTg.isOn = DebugLockStepMode.ClientAndServer == this.mode;
        this.pureServerTag.isOn = DebugLockStepMode.PureServer == this.mode;

#if UNITY_EDITOR
        if (GUIUtility.systemCopyBuffer.Contains("Lockstep.Random.Port") == false)
        {
            port = (int)Random.Range(5000, 20000);

            GUIUtility.systemCopyBuffer = "Lockstep.Random.Port#" + port;
        }
        else
        {
            var str = GUIUtility.systemCopyBuffer;
            string[] strs = str.Split("#");
            if (strs.Length > 1)
            {
                port = int.Parse(strs[1]);
            }
        }

#endif

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

                if (GlobalSetting.DebugLockTwoPlayer) 
                {
                    playerIF.text = "2";

                    //port = (int)Random.Range(5000, 20000);
                    //GUIUtility.systemCopyBuffer = "Lockstep.Random.Port#" + port;
                    //this.portIF.text = port.ToString();
                }



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

                StartClient().Coroutine();
                break;

            case DebugLockStepMode.ClientAndServer:

                StartClientAndServer();
                break;

            case DebugLockStepMode.PureServer:
                StartPureServer().Coroutine();
                break;

            default:
                break;
        }
    }

    private async ETTask StartPureServer()
    {
        string scenePath = "Assets/Scenes/Server/PureServer.unity";
        string sceneName = "PureServer";

#if UNITY_EDITOR
        if (AssetComponentConfig.AssetLoadMode == AssetLoadMode.Develop)
        {
            LoadSceneParameters parameters = new LoadSceneParameters()
            {
                loadSceneMode = LoadSceneMode.Single,
                localPhysicsMode = LocalPhysicsMode.None
            };
            LoadSceneHandler _loadHandler = await AssetComponent.LoadSceneAsync(scenePath);
            AsyncOperation _operation =
                UnityEditor.SceneManagement.EditorSceneManager.LoadSceneAsyncInPlayMode(
                    scenePath,
                    parameters
                );
            _operation.completed += asyncOperation =>
            {
                Debug.Log("场景加载完成  " + sceneName);
            };
            return;
        }
        else
        {
            LoadSceneParameters _parameters = new LoadSceneParameters()
            {
                loadSceneMode = LoadSceneMode.Single,
                localPhysicsMode = LocalPhysicsMode.None
            };
            AsyncOperation operation =
               UnityEditor.SceneManagement.EditorSceneManager.LoadSceneAsyncInPlayMode(
                   scenePath,
                   _parameters
               );
            operation.completed += asyncOperation =>
            {
                Debug.Log("场景加载完成  " + scenePath);
            };
            return;
        }

#endif


        SceneManager.LoadScene(sceneName);

    }

    private void StartClientAndServer()
    {
        port = int.Parse(portIF.text);

        if (port < 0 || port > 65535)
            return;

        PlayerNumber = (uint)int.Parse(playerIF.text);

        //SceneManager.LoadScene("ClientAndServer");

        AsyncLoadClientAndServerScene().Coroutine();

        NetSetting.ServerIp = SetGet_str_ipAddress;
        NetSetting.ServerPort = (uint)port;

        NetSetting.PlayerNumber = PlayerNumber;

        //Debug.Log($"启动Server  IP {SetGet_str_ipAddress} Port {port}  PlayerNumber {PlayerNumber}  ");
    }

    private async ETTask StartClient()
    {
        port = int.Parse(portIF.text);
        var ip = ipIF.text;

        NetSetting.ServerIp = ip;
        NetSetting.ServerPort = (uint)port;

        //Debug.Log($"开始连接Server IP {NetSetting.ServerIp} Port {NetSetting.ServerPort}  ");

        //SceneManager.LoadScene(GameSceneSetting.BattleTestScene);
        await AsyncLoadClientScene();


        await AsyncLoadBattleScene();
    }

    private async ETTask AsyncLoadClientAndServerScene()
    {
        string scenePath = "Assets/Scenes/Debug/0.launch/ClientAndServer.unity";
        string sceneName = "ClientAndServer";

#if UNITY_EDITOR
        if (AssetComponentConfig.AssetLoadMode == AssetLoadMode.Develop)
        {
            LoadSceneParameters parameters = new LoadSceneParameters()
            {
                loadSceneMode = LoadSceneMode.Single,
                localPhysicsMode = LocalPhysicsMode.None
            };
            LoadSceneHandler _loadHandler = await AssetComponent.LoadSceneAsync(scenePath);
            AsyncOperation _operation =
                UnityEditor.SceneManagement.EditorSceneManager.LoadSceneAsyncInPlayMode(
                    scenePath,
                    parameters
                );
            _operation.completed += asyncOperation =>
            {
                Debug.Log("场景加载完成  " + sceneName);
            };
            return;
        }

#endif

        LoadSceneHandler loadHandler = await AssetComponent.LoadSceneAsync(scenePath);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.completed += asyncOperation =>
        {
            Debug.Log("场景加载完成  " + scenePath);
        };
    }

    private async ETTask AsyncLoadClientScene()
    {
        string scenePath = "Assets/Scenes/Debug/0.launch/LaunchClient.unity";
        string sceneName = "LaunchClient";

#if UNITY_EDITOR
        if (AssetComponentConfig.AssetLoadMode == AssetLoadMode.Develop)
        {
            LoadSceneParameters parameters = new LoadSceneParameters()
            {
                loadSceneMode = LoadSceneMode.Single,
                localPhysicsMode = LocalPhysicsMode.None
            };
            LoadSceneHandler _loadHandler = await AssetComponent.LoadSceneAsync(scenePath);
            AsyncOperation _operation =
                UnityEditor.SceneManagement.EditorSceneManager.LoadSceneAsyncInPlayMode(
                    scenePath,
                    parameters
                );
            _operation.completed += asyncOperation =>
            {
                Debug.Log("场景加载完成  " + sceneName);
            };
            return;
        }

#endif

        LoadSceneHandler loadHandler = await AssetComponent.LoadSceneAsync(scenePath);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.completed += asyncOperation =>
        {
            Debug.Log("场景加载完成  " + scenePath);
        };
    }

    private async ETTask AsyncLoadBattleScene()
    {
        string scenePath = "Assets/Scenes/Debug/1.battle/Battle.unity";
        string sceneName = "Battle";
#if UNITY_EDITOR
        if (AssetComponentConfig.AssetLoadMode == AssetLoadMode.Develop)
        {
            LoadSceneParameters parameters = new LoadSceneParameters()
            {
                loadSceneMode = LoadSceneMode.Single,
                localPhysicsMode = LocalPhysicsMode.None
            };
            LoadSceneHandler _loadHandler = await AssetComponent.LoadSceneAsync(scenePath);
            AsyncOperation _operation =
                UnityEditor.SceneManagement.EditorSceneManager.LoadSceneAsyncInPlayMode(
                    scenePath,
                    parameters
                );
            _operation.completed += asyncOperation =>
            {
                Debug.Log("场景加载完成  " + scenePath);
            };
            return;
        }

#endif


        LoadSceneHandler loadHandler = await AssetComponent.LoadSceneAsync(scenePath);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.completed += asyncOperation =>
        {
            Debug.Log("场景加载完成  " + scenePath);
        };
    }



    public enum DebugLockStepMode
    {
        Client,
        ClientAndServer,
        PureServer
    }
}
