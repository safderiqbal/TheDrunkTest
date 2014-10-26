namespace DrunkCheck
{
    public static class DrunkChecker
    {
        public static DrunkLevel HowDrunk(int value)
        {
            if (value < 200)
            {
                return DrunkLevel.Sober;
            }
            else if (value < 300)
            {
                return DrunkLevel.Tipsy;
            }
            else if (value < 400)
            {
                return DrunkLevel.GettingThere;
            }
            else if (value < 500)
            {
                return DrunkLevel.Drunk;
            }
            else if (value < 600)
            {
                return DrunkLevel.Fooked;
            }
            else
            {
                return DrunkLevel.JonDrunk;
            }
        }
    }
}