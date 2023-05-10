namespace Test.GridOnDemand
{
	using UnityEngine;
	using BoaNeoTools.UI;

	public class OnDemandTestGrid : MonoBehaviour
	{
		[SerializeField] private GridOnDemand _grid;
		[SerializeField] private OnDemandTestCell _cellPrefab;

		void Awake()
		{
			_grid.Setup(_cellPrefab, 100, 100, (x, y, cell) =>
			{
				cell.Setup($"Cell {x},{y}");
			});
		}
	}
}