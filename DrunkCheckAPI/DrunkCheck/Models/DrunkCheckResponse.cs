namespace DrunkCheck.Models
{
    public class DrunkCheckResponse
    {
        // ReSharper disable InconsistentNaming
        public bool success;
        public int value;
        public User user;
        public DrunkLevel drunkLevel;
        // ReSharper restore InconsistentNaming

        public bool isTooDrunk { get { return value >= 400; } }

        public DrunkCheckResponse(int reading)
        {
            value = reading;
            drunkLevel = DrunkChecker.HowDrunk(reading);
            success = true;
        }

        public DrunkCheckResponse(User user, int reading)
        {
            this.user = user;
            value = reading;
            drunkLevel = DrunkChecker.HowDrunk(reading);
            success = true;
        }
    }
}