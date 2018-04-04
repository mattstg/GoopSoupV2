using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModPopup : MonoBehaviour {

    readonly float HEALTHPOPUP_LOCK_TIME = 2; //If recieves no new ModValue calls by this amount of time, it closes off the instance
    readonly float HEALTHPOPUP_FADE_TIME = 1.5f; //Time for text to fade away once launch on anim curve
    readonly float HEALTHPOPUP_LERP_TIME = .5f; //Time it should take to lerp to final value
    readonly float HEALTHPOPUP_LERP_MIN = .5f; //Min amount for lerp per second incase velo drops too low
    readonly Vector2 HEALTHPOPUP_OFFSET = new Vector2(0, .7f);

    public AnimationCurve animCurve;
    TextMesh textmesh;
    float finalValue;
    Transform followTarget;
    Vector3 offsetFromTarget;
    float curValue;
    float curTimer;

    Vector3 popoffStartPoint;

    float currentVelo; //Used by smoothdamp function

    void InitializeOnTarget(Transform _followTarget, float initialAmt)
    {

        textmesh = GetComponent<TextMesh>();
        //Text mesh is a 3D object, only way to draw in front is to change the Z or to access the hidden Renderer on the TextMesh
        textmesh.GetComponent<Renderer>().sortingLayerName = "UIText";
        followTarget = _followTarget;
        offsetFromTarget = HEALTHPOPUP_OFFSET;
        finalValue = initialAmt;
        currentVelo = 0;
        StartCoroutine(LerpModValue());
    }
	
	public void ModValue(float modBy)
    {
        finalValue += modBy;
        currentVelo *= .8f; //Slows it down a bit when new values come in, just for aethestic purposes\
        //could make another co-routine that makes it grow each time it recieves value
    }

    IEnumerator LerpModValue()
    {
        while (curTimer < HEALTHPOPUP_LOCK_TIME)
        {
            while ((curValue < finalValue && !Mathf.Approximately(curValue, finalValue)))
            {
                curTimer = 0;
                float newValue = Mathf.SmoothDamp(curValue, finalValue, ref currentVelo, HEALTHPOPUP_LERP_TIME);
                //Smooth damp sometimes never reaches it's target, since it slows down as it approaches target value
                //so we do a comparision to see if the difference is too small, and have a min dif instead
                float dif = newValue - curValue;   //Find the difference from smooth damp
                curValue += Mathf.Max(dif, HEALTHPOPUP_LERP_MIN * Time.deltaTime); //Add the larger of the min speed or the smoothdamp speed
                curValue = Mathf.Clamp(curValue, 0, finalValue);  //Clamp it within finalValue to prevent overshoot
                textmesh.text = newValue.ToString("0.0"); //Update the text, truncate to 1 decimal place
                transform.position = followTarget.position + offsetFromTarget; //Place the text above the target
                yield return null;
            }
            transform.position = followTarget.position + offsetFromTarget; //Place the text above the target
            curValue = finalValue;
            curTimer += Time.deltaTime;
            yield return null;
        }

        curTimer = HEALTHPOPUP_FADE_TIME; //For next co-routine, it uses the Fade time
        popoffStartPoint = transform.position;
        StartCoroutine(PopOffValue());  //Create other co-routine
        HealthPopupManager.Instance.RemovePopup(followTarget);  //Removes from list so others cant add to it
        //Ends this function
    }

    IEnumerator PopOffValue()
    {
        while(curTimer > 0)
        {
            float percX = 1 - (curTimer / HEALTHPOPUP_FADE_TIME);
            float yPos = animCurve.Evaluate(percX);
            transform.position = popoffStartPoint + new Vector3(percX / 2, yPos,0);
            Color fadedColor = textmesh.color;
            fadedColor.a = Mathf.Lerp(1, 0, percX);
            textmesh.color = fadedColor;
            curTimer -= Time.deltaTime;
            yield return null;
        }
        GameObject.Destroy(gameObject);
    }

    public static HealthModPopup CreateHealthModPopup(Transform _followTarget, float initialAmt)
    { //Static method to create an instance of a healthPopup
        GameObject newInstance = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/HealthModPopup"));
        HealthModPopup toRet = newInstance.GetComponent<HealthModPopup>();
        toRet.InitializeOnTarget(_followTarget, initialAmt);
        return toRet;
    }
}
