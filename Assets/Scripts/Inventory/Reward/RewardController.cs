using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour
{
    public InventoryObject inventory;
    public ItemObject rewardItem;

    private void Start()
    {
        if (rewardItem != null && !inventory.inventoryList.Contains(rewardItem))
            RewardPopup.Instance.OpenRewardPopup(rewardItem);
    }
}
