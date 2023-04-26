/*
 * @Author: delevin.ying 
 * @Date: 2023-04-26 17:25:59 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-26 17:31:10
 */


using UnityEngine;


[CreateAssetMenu(fileName = "BuildApplicationConfig.asset", menuName = "BuildPipeline/BuildApplicationConfig")]
public class BuildApplicationConfig : ScriptableObject
{
    public string companyName;

    public string appName;

    public int key;
}