using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExistsWhenSeen : VisibilityObject
{
    public override void setVisible() {
        if(visible) return;
        base.setVisible();
        phaseIn();
    }
    public override void setHidden() {
        if(!visible) return;
        base.setHidden();
        phaseOut();
    }
    public override void drop() {
        base.drop();
        phaseIn();
    }
}
