using System;

public abstract class Locatable
{
    // if they are locatable, they may associate with region
    int regionLabel;

    public int getRegionLabel() {
        return regionLabel;
    }

    public void setRegion(int pLabel) {
        this.regionLabel = pLabel;

    }
    

}
