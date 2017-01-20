public abstract class DNABit
{
    public abstract int getLength();

    public abstract string getTooltipTitleKey();
    public abstract string getTooltipCoreExplanationKey();

    public override string ToString()
    {
        return "DNABit";
    }

    public virtual string getInternalName()
    {
        return ToString();
    }
};

