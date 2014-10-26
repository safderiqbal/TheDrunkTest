namespace DrunkCheck
{
    public static class DrunkChecker
    {
        public static DrunkLevel HowDrunk(int value)
        {
            if (value < 200)
                return DrunkLevel.Sober;
            if (value < 300)
                return DrunkLevel.Tipsy;
            if (value < 400)
                return DrunkLevel.GettingThere;
            if (value < 500)
                return DrunkLevel.Drunk;
            if (value < 600)
                return DrunkLevel.Fooked;
            return DrunkLevel.JonDrunk;
        }
    }
}