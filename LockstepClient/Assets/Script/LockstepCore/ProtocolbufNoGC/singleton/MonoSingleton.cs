using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	private static T m_instance = null;

	public static T instance
    {
        get
        {
			if (m_instance == null)
            {
            	m_instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if (m_instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    m_instance = go.AddComponent<T>();
                    GameObject parent = GameObject.Find("Boot");
                    if (parent != null)
                    {
                        go.transform.parent = parent.transform;
                    }
                }
            }

            return m_instance;
        }
    }

    /*
     * û���κ�ʵ�ֵĺ��������ڱ�֤MonoSingleton��ʹ��ǰ�Ѵ���
     */
    public void Startup()
    {

    }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this as T;
        }

        DontDestroyOnLoad(gameObject);
        Init();
    }
 
    protected virtual void Init()
    {

    }

    public void DestroySelf()
    {
        Dispose();
        MonoSingleton<T>.m_instance = null;
        UnityEngine.Object.Destroy(gameObject);
    }

    public virtual void Dispose()
    {

    }

}