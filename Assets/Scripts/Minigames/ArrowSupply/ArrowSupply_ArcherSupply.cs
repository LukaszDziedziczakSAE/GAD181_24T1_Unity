using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupply_ArcherSupply : MonoBehaviour
{
    [field: SerializeField] public List<ArrowSupply_Arrow.EType> Arrows = new List<ArrowSupply_Arrow.EType>();

    ArrowSupply_AINavigationController aiController;

    public bool HasArrows => Arrows.Count > 0;

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null && character.State.GetType() == new CS_ArrowSupply_Carrying(character, null).GetType())
        {
            ArrowSupply_Arrow arrow = ((CS_ArrowSupply_Carrying)character.State).Arrow;
            character.SetNewState(new CS_ArrowSupply_Delivery(character, this, arrow));
        }
    }

    public void GiveArrow(ArrowSupply_Arrow arrow)
    {
        //aiController.DeliverArrow();
        Debug.Log("dropping off " + arrow.Type.ToString());
        Arrows.Add(arrow.Type);
        Destroy(arrow.gameObject);
        Debug.Log("total arrows = " + Arrows.Count);
        
    }

    public ArrowSupply_Arrow.EType TakeArrow()
    {
        
        ArrowSupply_Arrow.EType arrowType = ArrowSupply_Arrow.EType.none;
        if (Arrows.Count > 0)
        {
            arrowType = Arrows[0];
            Arrows.RemoveAt(0);
        }
        return arrowType;
    }
}
