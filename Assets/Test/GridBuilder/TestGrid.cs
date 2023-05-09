using UnityEngine;

namespace Test.GridBuilder
{
	public class TestGrid : MonoBehaviour
	{
		[SerializeField] private BoaNeoTools.UI.GridBuilder _grid;
		[SerializeField] private TestCell _cellPrefab;

		void Awake()
		{
			_grid.BeginUpdate();
			for (int i = 0; i < 9; i++)
				_grid.AddCell(_cellPrefab, cell => cell.Setup($"Cell {i}"), i % 3 == 2);
			_grid.EndUpdate();
		}
	}
}