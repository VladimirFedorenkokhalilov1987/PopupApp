using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePopup1 : MonoBehaviour 
{

	[SerializeField]
	private Button _popupBut1;
	[SerializeField]
	private Button _popupBut2;

	[SerializeField]
	private GameObject _popupCloseBut2;

	public float minSize;
	public float growFactor;
	public float waitTime;

	public RectTransform standartPos;

	[SerializeField]
	private GameObject _popup1;

	[SerializeField]
	private Image _popup1Image;

	private bool buttset = true;

	void Start()
	{
		standartPos = _popup1.GetComponent<RectTransform>();
	}

	// Use this for initialization
	public void ClosePopup () 
	{
		StopAllCoroutines ();
		StartCoroutine(Scale());

		_popupCloseBut2.SetActive (false);

		_popupBut1.interactable = false;
		_popupBut2.interactable = false;

		buttset = false;
	}
	
	IEnumerator Scale()
	{
		float timer = 0;

		while(_popup1.transform.localScale != new Vector3(0.5f, 0.5f, 0.5f)) // this could also be a condition indicating "alive or dead"
		{
			// we scale all axis, so they will have the same value, 
			// so we can work with a float instead of comparing vectors
			while(minSize < _popup1.transform.localScale.x)
			{
				timer += Time.deltaTime;
				_popup1.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
				yield return null;
			}

			yield return new WaitForSeconds(waitTime);

			timer = 0;
			while(_popup1.transform.localPosition.y < 500)
			{
				timer += Time.deltaTime;
				_popup1.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				_popup1.transform.localPosition = Vector3.Lerp(_popup1.transform.localPosition, _popup1.transform.localPosition+new Vector3(0,5,0), 100);

				yield return null;
			}

			timer = 0;
			yield return new WaitForSeconds(waitTime);
		}
	}

	void Update()
	{
		if (_popup1.transform.localPosition.y == 500) 
		{
			StopAllCoroutines ();

			_popup1Image.color = new Color(_popup1Image.color.r,_popup1Image.color.g,_popup1Image.color.b,0);
			_popup1.gameObject.SetActive (false);
		}

		if (_popup1.activeSelf == false && buttset == false) 
		{
			_popupBut1.interactable = true;
			_popupBut2.interactable = true;
			buttset = true;

			StopAllCoroutines ();
		}
	}
}


