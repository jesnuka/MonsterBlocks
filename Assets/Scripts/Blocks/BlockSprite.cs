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

    [field: SerializeField] private Image _blockEmptyImage;
    public Image BlockEmptyImage { get { return _blockEmptyImage; } }

    public void ActivateBlock()
    {
        BlockImage.gameObject.SetActive(true);
        BlockEyeImage.gameObject.SetActive(true);
        BlockEmptyImage.gameObject.SetActive(false);
    }

    public void DeactivateBlock()
    {
        BlockImage.gameObject.SetActive(false);
        BlockEyeImage.gameObject.SetActive(false);
        BlockEmptyImage.gameObject.SetActive(true);
    }


}
