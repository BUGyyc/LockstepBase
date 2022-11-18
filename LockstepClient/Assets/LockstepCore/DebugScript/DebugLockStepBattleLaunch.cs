// using UnityEngine;
// using UnityEngine.UI;


// public class DebugLockStepBattleLaunch : MonoBehaviour
// {
//     public Toggle serverTg;
//     public Toggle clientTg;

//     public Text IpText;
//     public InputField portIF;
//     public InputField ipIF;

//     public Button startBtn;

//     public Image uiParent;

//     public DebugLockStepMode mode = DebugLockStepMode.Server;


//     public GameObject GameManagerObj;

//     private string localIpAddress;

//     private GameObject containerObj;

//     public string SetGet_str_ipAddress
//     {
//         set { localIpAddress = value; }
//         get
//         {
//             localIpAddress = LiteNetLib.NetUtils.GetLocalIp(LiteNetLib.LocalAddrType.IPv4);
//             //NetTool.IP(address.IPv4);
//             return localIpAddress;
//         }
//     }

//     // Start is called before the first frame update
//     void Start()
//     {
//         //chooseModeGroup
//         startBtn.onClick.AddListener(OnClick);
//         serverTg.onValueChanged.AddListener(OnServerTg);
//         clientTg.onValueChanged.AddListener(OnClientTg);
//         initView();
//         UpdateView();

//         ShowLocalIP();

//     }

//     private void initView()
//     {
//         switch (mode)
//         {
//             case DebugLockStepMode.Server:
//                 this.clientTg.isOn = false;
//                 this.serverTg.isOn = true;
//                 break;
//             case DebugLockStepMode.Client:
//                 this.clientTg.isOn = true;
//                 this.serverTg.isOn = false;
//                 break;
//         }
//     }

//     private void OnServerTg(bool val)
//     {
//         this.mode = val ? DebugLockStepMode.Server : DebugLockStepMode.Client;

//         UpdateView();
//     }

//     private void OnClientTg(bool val)
//     {
//         this.mode = val == false ? DebugLockStepMode.Server : DebugLockStepMode.Client;

//         UpdateView();
//     }

//     private void UpdateView()
//     {
//         switch (mode)
//         {
//             case DebugLockStepMode.Server:
//             default:
//                 this.IpText.gameObject.SetActive(true);
//                 this.ipIF.gameObject.SetActive(false);
//                 break;
//             case DebugLockStepMode.Client:
//                 this.ipIF.gameObject.SetActive(true);
//                 this.IpText.gameObject.SetActive(false);

//                 this.ipIF.text = SetGet_str_ipAddress;

//                 break;
//         }

//         this.portIF.text = "5000";
//     }



//     private void ShowLocalIP()
//     {

//         this.IpText.text = "����IP��" + SetGet_str_ipAddress;
//     }

//     private void OnClick()
//     {
//         switch (mode)
//         {
//             case DebugLockStepMode.Client:

//                 StartClient();
//                 break;

//             case DebugLockStepMode.Server:

//                 StartServer();
//                 break;

//             default:
//                 break;
//         }
//     }

//     private void StartServer()
//     {
//         int port = int.Parse(portIF.text);

//         if (port < 0 || port > 65535) return;

//         if (containerObj != null) return;
//         else containerObj = new GameObject("ServerContainer");
//         if (containerObj.GetComponent<LockstepBattleServer>() != null) return;


//         var gs = containerObj.AddComponent<LockstepBattleServer>();
//         //gs._serverBall = serverObj;

//         Debug.Log($"����Server  IP {SetGet_str_ipAddress} Port {port}  ");

//         gs.StartServer(port);

//         uiParent.gameObject.SetActive(false);
//     }

//     private void StartClient()
//     {
//         int port = int.Parse(portIF.text);

//         string targetIp = ipIF.text;

//         if (port < 0 || port > 65535) return;

//         if (containerObj != null) return;
//         else containerObj = new GameObject("ClientContainer");
//         if (containerObj.GetComponent<LockstepBattleClient>() != null) return;


//         var gc = containerObj.AddComponent<LockstepBattleClient>();

//         //gc._clientBall = clientObj;
//         //gc._clientBallInterpolated = clientIntObj;

//         Debug.Log($"����client  �ͻ���IP {SetGet_str_ipAddress} ");

//         Debug.Log($"client ��������  IP {targetIp} Port {port}  ");

//         gc.StartClient(port);

//         uiParent.gameObject.SetActive(false);

//         GameManagerObj.SetActive(true);
//     }




//     public enum DebugLockStepMode
//     {
//         Server,
//         Client
//     }



// }
