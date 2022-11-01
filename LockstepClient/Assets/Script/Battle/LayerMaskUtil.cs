using UnityEngine;


public class LayerMaskUtil
{
    public static readonly int LAYER_DEFAULT = LayerMask.NameToLayer("Default");
    public static readonly int LAYER_DEFAULT_MASK = LayerMask.GetMask("Default");



    public static readonly int LAYER_WALL = LayerMask.NameToLayer("Wall");
    public static readonly int LAYER_WALL_MASK = LayerMask.GetMask("Wall");


}


