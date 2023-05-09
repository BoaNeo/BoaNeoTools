using BoaNeoTools.UI;
using TMPro;
using UnityEngine;

public class TestCell : GridCell
{
	[SerializeField] private TMP_Text _text;

	public void Setup(string s)
	{
		_text.text = s;
	}
}
