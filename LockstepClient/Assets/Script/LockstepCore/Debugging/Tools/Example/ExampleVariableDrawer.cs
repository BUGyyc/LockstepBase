using DebugTools;
using System.Text;
using UnityEngine.UI;

public class ExampleVariableDrawer : MonoDrawer
{

    public Text outputText;


    // Use this for initialization
    void Start()
    {
        drawerName = "exampleDrawer";
        RegisterDrawer();
        DrawVariable("", (object)"This is a test string");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void DrawVariable(string key, object message)
    {
        outputText.text = message.ToString() + "\n" + outputText.text;
    }

    public override void DrawVariable(string key, string format, params object[] args)
    {
        if (args.Length <= 0)
        {
            DrawVariable(key, (object)format);
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(format, args);
            DrawVariable(key, sb);
        }
    }
}
