using UnityEngine;

[DefaultExecutionOrder(-10000)]
public class AutoRegister : MonoBehaviour
{
    [SerializeField] bool _isSingleton;

    [SerializeField] Component _cmp;
    
    // [ShowIf(nameof(_isSingleton), false)]
    // [SerializeField] string _tag;
    
    [ShowIf(nameof(_isSingleton), false)]
    [SearchableEnum]
    [SerializeField] SerLocID _id;
    
    void Awake()
    {
        if (_isSingleton)
        {
            ServiceLocator.Register(_cmp); 
        }
        else
        {
            ServiceLocator.Register(_cmp, _id); 
        }
    }
}
