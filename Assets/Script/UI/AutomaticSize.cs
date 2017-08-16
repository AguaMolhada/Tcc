// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutomaticSize.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/AguaMolhada
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;

public class AutomaticSize : MonoBehaviour
{
    [Range(0f,100f)]
    public float ChildHeight = 50f;
    public bool IsHeightInfluent;
    [Range(0f,100f)]
    public float ChildWidth = 50f;
    public bool IsWidthInfluent;
	
    // Use this for initialization
	void Start () {
		AdjustSize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AdjustSize()
    {

        Vector2 size = this.GetComponent<RectTransform>().sizeDelta;
        size.y = IsHeightInfluent ? this.transform.childCount * ChildHeight : ChildHeight;
        size.x = IsWidthInfluent ? this.transform.childCount * ChildWidth : ChildWidth;
        this.GetComponent<RectTransform>().sizeDelta = size;

    }
}
