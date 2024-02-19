using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExistsWhenHidden : VisibilityObject
{
    public override void setVisible() {
        if(visible) return;
        base.setVisible();
        phaseOut();
    }
    public override void setHidden() {
        if(!visible) return;
        base.setHidden();
        phaseIn();
    }
    public override void drop() {
        base.drop();
        phaseOut();
    }
}
