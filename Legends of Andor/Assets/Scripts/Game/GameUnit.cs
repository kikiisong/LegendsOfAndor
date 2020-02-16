using System;
public abstract class GameUnit
{

    int WP;
    int SP;

    public void setWP(int pWP) {
        this.WP = pWP;
    }

    public void setSP(int pSP) {
        this.SP = pSP;
    }

    public int getWP() {
        return this.WP;
    }

    public int getSP() {
        return this.SP;
    }

}

