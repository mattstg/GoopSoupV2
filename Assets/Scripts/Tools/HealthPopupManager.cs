using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPopupManager  {
    #region Singleton
    private static HealthPopupManager instance;

    private HealthPopupManager() { }

    public static HealthPopupManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HealthPopupManager();
            }
            return instance;
        }
    }
    #endregion

    public Dictionary<Transform, HealthModPopup> healthPopupDict = new Dictionary<Transform, HealthModPopup>();

    public void ModHP(Transform target, float amt)
    {
        //ModHp, if first time called, create a new healthModPopup, otherwise, update the existing one
        if(healthPopupDict.ContainsKey(target))
            healthPopupDict[target].ModValue(amt);
        else
            healthPopupDict.Add(target, HealthModPopup.CreateHealthModPopup(target, amt));
    }

    public void RemovePopup(Transform toRemoveKey)
    {
        healthPopupDict.Remove(toRemoveKey);
    }
}
