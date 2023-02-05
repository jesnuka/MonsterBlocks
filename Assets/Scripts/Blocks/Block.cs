using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockGrid _blockGrid;
    public BlockGrid BlockGrid { get { return _blockGrid; } set { _blockGrid = value; } }

    private BlockPosition _blockPosition;
    public BlockPosition BlockPosition { get { return _blockPosition; } set { _blockPosition = value; } }

    [SerializeField] private RectTransform _rectTransform;
    public RectTransform RectTransform { get { return _rectTransform; } }

    [field:SerializeField] private BlockSprite _blockSprite;
    public BlockSprite BlockSprite { get { return _blockSprite; } }

    private bool _isEnabled;
    public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; } }

    // Disabled blocks are never turned back on
    private bool _isDisabled;
    public bool IsDisabled { get { return _isDisabled; } set { _isDisabled = value; } }

    ~Block()
    {

    }

    public void SwapTiles(Block other)
    {
        // Swap the places of two blocks
        int column = BlockPosition.Column;
        int row = BlockPosition.Row;

        BlockPosition.SetPosition(other.BlockPosition.Column, other.BlockPosition.Row);
        other.BlockPosition.SetPosition(column, row);

        // Swap rectTransform position as well
        Vector3 rectTransformPosition = RectTransform.localPosition;
        RectTransform.localPosition = other.RectTransform.localPosition;
        other.RectTransform.localPosition = rectTransformPosition;

    }

    public void SetPosition(int column, int row)
    {
        if (BlockPosition == null)
            BlockPosition = new BlockPosition(column, row);
        else
            BlockPosition.SetPosition(column, row);
    }
    public Vector2 GetSize()
    {
        return new Vector2(RectTransform.sizeDelta.x, RectTransform.sizeDelta.y);
    }

    public void DisableBlock()
    {
        // Turns off BlockSprite completely
        IsEnabled = false;
        IsDisabled = true;
        BlockSprite.DisableBlock();
    }

    public void ToggleBlock(bool value)
    {
        if (IsDisabled)
            return;
        IsEnabled = value;
        BlockSprite.ToggleBlock(value);
    }

}
