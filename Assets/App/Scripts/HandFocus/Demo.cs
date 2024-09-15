using System.Threading;
using TofAr.V0.Hand;

using UnityEngine;

public class Demo : MonoBehaviour
{
    public GameObject objectOnHandRight;
    public GameObject objectOnHandLeft;

    private SynchronizationContext syncContext;

    void Start()
    {
        syncContext = SynchronizationContext.Current;

        TofArHandManager.OnFrameArrived += (s) =>
        {
            syncContext.Post((s) =>
            {
                objectOnHandRight.transform.localPosition = TofArHandManager.Instance.HandData.Data.featurePointsRight[(int)HandPointIndex.HandCenter];
                objectOnHandLeft.transform.localPosition = TofArHandManager.Instance.HandData.Data.featurePointsLeft[(int)HandPointIndex.HandCenter];
            }, this);
        };
    }
}
