using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModPopup : MonoBehaviour {

    readonly float HEALTHPOPUP_TIME_MAX = 2; //If recieves no new ModValue calls by this amount of time, it closes off the instance
    readonly float HEALTHPOPUP_FADE_TIME = 1.5f; //Time for text to fade away once launch on anim curve
    readonly float HEALTHPOPUP_LERP_TIME = 1; //Time it should take to lerp to final value

    public AnimationCurve animCurve;
    TextMesh textmesh;
    float finalValue;
    Transform followTarget;
    Vector2 offsetFromTarget;
    float curValue;
    float curTimer;
    bool coroutineIsRunning = false;

    float currentVelo; //Used by smoothdamp function
	
    public void InitializeOnTarget(Transform _followTarget, Vector2 _offsetFromTarget, float initialAmt)
    {
        textmesh = GetComponent<TextMesh>();
        followTarget = _followTarget;
        offsetFromTarget = _offsetFromTarget;
        finalValue = initialAmt;
    }
	
	public void ModValue(float modBy)
    {
        finalValue += modBy;
        if(!coroutineIsRunning)
        {
            currentVelo = 0;
        }
    }

    IEnumerator LerpModValue()
    {
        while (curTimer < HEALTHPOPUP_FADE_TIME)
        {
            while (!(curValue >= finalValue || Mathf.Approximately(curValue, finalValue)))
            {
                curTimer = 0;
                curValue = Mathf.SmoothDamp(curValue, finalValue, ref currentVelo, HEALTHPOPUP_LERP_TIME);
                textmesh.text = curValue.ToString(); //Update the text
                                                     //Update the text color? Lerp to red red I gues
                yield return null;
            }
            curTimer += Time.deltaTime;
            yield return null;
        }
        
        //Create other co-routine
        //Function is done
        //Send to manager that this one is cut off (removes)
        //Ends this function
    }
}
