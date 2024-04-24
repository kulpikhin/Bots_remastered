using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MineralDistrebutor))]
public class MineralDistrebutor : MonoBehaviour
{
    private List<Base> _bases = new List<Base>();
    private List<Mineral> _minerals = new List<Mineral>();

    private int _indexCurentBase = 0;

    public void DistributeMineral(Mineral mineral)
    {
        _minerals.Add(mineral);
        SendMineral();
    }

    public void AddBase(Base newBase)
    {
        _bases.Add(newBase);
    }

    private void SendMineral()
    {
        bool freeWorkers = false;
        Base curentBase;
        Mineral mineral;

        do
        {
            for (int i = _indexCurentBase; i < _bases.Count && _minerals.Count > 0; i++)
            {
                curentBase = _bases[i];
                mineral = _minerals[0];

                if (curentBase.WorkerManagment.HasFreeWorker)
                {
                    curentBase.WorkerManagment.SendMining(mineral);
                    _minerals.RemoveAt(0);
                    freeWorkers = true;
                }
                else
                {
                    freeWorkers = false;
                }
            }
        }
        while (freeWorkers && _minerals.Count > 0);
    }
}
