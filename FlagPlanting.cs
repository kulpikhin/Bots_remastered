using UnityEngine;

public class FlagPlanting : MonoBehaviour
{
    private Base _selectedBase;
    private bool _isTowerSelected;

    private void Update()
    {
        SelectTower();
        SelectFlagPlace();
    }

    private void SelectTower()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _selectedBase = hit.transform.GetComponent<Base>();

                _isTowerSelected = _selectedBase != null;
            }
        }
    }

    private void SelectFlagPlace()
    {
        if (_isTowerSelected && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<Terrain>())
                {
                    _selectedBase.SetFlagPosition(hit.point);
                }
            }
        }
    }
}
