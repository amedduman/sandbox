using Services;
using UnityEngine;

[DefaultExecutionOrder(-10000)]
public class AutoRegister : MonoBehaviour
{    
    [SerializeField] bool _isSingleton;

    [SerializeField] Component _cmp;
    
    [ShowIf(nameof(_isSingleton), false)]
    [SerializeField] string _tag;
    
    void Awake()
    {
        if (_isSingleton)
        {
            ServiceLocator.Instance.Register(_cmp); 
        }
        else
        {
            ServiceLocator.Instance.Register(_cmp, _tag); 
        }
    }
}
