// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutomaticSize.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;

public class AutomaticSize : MonoBehaviour
{
    [Range(0f,100f)]
    public float ChildHeight = 50f;
    public bool IsHeightInfluent;
    public bool UseMaxHeight;
    [Range(0f,100f)]
    public float ChildWidth = 50f;
    public bool IsWidthInfluent;
    public bool UseMaxWidth;
	
    // Use this for initialization
	void Start () {
		AdjustSize();
	}
	
    public void AdjustSize()
    {
        Vector2 size = this.GetComponent<RectTransform>().sizeDelta;

        if (!UseMaxHeight)
        {
            size.y = IsHeightInfluent ? this.transform.childCount * ChildHeight : ChildHeight;
        }
        if (!UseMaxWidth)
        {
            size.x = IsWidthInfluent ? this.transform.childCount * ChildWidth : ChildWidth;
        }
        this.GetComponent<RectTransform>().sizeDelta = size;

    }
}
