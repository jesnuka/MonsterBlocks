using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSprite : MonoBehaviour 
{
    [field: SerializeField] private Image _blockImage;
    public Image BlockImage { get { return _blockImage; }}
    [field: SerializeField] private Image _blockEyeImage;
    public Image BlockEyeImage { get { return _blockEyeImage; } }

    public BlockSprite()
    {

    }
}
