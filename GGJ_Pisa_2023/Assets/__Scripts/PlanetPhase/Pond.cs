using UnityEngine;
public class Pond : MonoBehaviour
{
    public int numberOfTimeUsed;
    public int MaxUses;
    public Root TRoot;
    public void checkIfEmpty()
    {
        if (numberOfTimeUsed>=MaxUses)
        {
            
            Destroy(this.gameObject);
        }
    }
}
