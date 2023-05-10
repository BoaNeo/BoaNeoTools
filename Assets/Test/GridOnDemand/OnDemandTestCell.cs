using BoaNeoTools.UI;
using TMPro;
using UnityEngine;

namespace Test.GridOnDemand
{
	public class OnDemandTestCell : GridCell
	{
		[SerializeField] private TMP_Text _text;

		public void Setup(string s)
		{
			_text.text = s;
		}
	}
}
