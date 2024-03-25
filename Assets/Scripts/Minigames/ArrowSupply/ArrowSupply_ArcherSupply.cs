using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupply_ArcherSupply : MonoBehaviour
{    
    [field: SerializeField] public List<ArrowRecord> Arrows = new List<ArrowRecord>();

    ArrowSupply_AI aiController;

    public bool HasArrows => Arrows.Count > 0;

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        
        if (character != null && character.State.GetType() == new CS_ArrowSupply_Carrying(character, null).GetType())
        {
            ArrowSupply_Arrow arrow = ((CS_ArrowSupply_Carrying)character.State).Arrow;

            character.SetNewState(new CS_ArrowSupply_Delivery(character, this, arrow));
        }
        else if (character != null)
        {
            Debug.LogWarning(character.name + " has entered Archer trigger sphere but not carrying anything");
        }
    }

    public void GiveArrow(ArrowSupply_Arrow arrow, Character owner)
    {
        //aiController.DeliverArrow();
        Debug.Log("dropping off " + arrow.Type.ToString());

        Arrows.Add(new ArrowRecord(arrow.Type, owner));

        Destroy(arrow.gameObject);

        Debug.Log("total arrows = " + Arrows.Count);
        
    }

    public ArrowRecord TakeArrow()
    {
        
        //ArrowSupply_Arrow.EType arrowType = ArrowSupply_Arrow.EType.none;

        ArrowRecord arrow = null;

        if (Arrows.Count > 0)
        {
            arrow = Arrows[0];

            Arrows.RemoveAt(0);
        }
        return arrow;
    }

    public class ArrowRecord
    {
        public ArrowSupply_Arrow.EType Type;

        public Character Owner;

        public ArrowRecord(ArrowSupply_Arrow.EType type, Character owner)
        {
            this.Type = type;

            this.Owner = owner;
        }

        public ArrowRecord(ArrowRecord oldRecord)
        {
            this.Type = oldRecord.Type;

            this.Owner = oldRecord.Owner;
        }
    }
}
