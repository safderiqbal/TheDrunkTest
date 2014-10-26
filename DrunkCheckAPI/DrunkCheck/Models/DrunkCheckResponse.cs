namespace DrunkCheck.Models
{
    public class DrunkCheckResponse
    {
        // ReSharper disable once InconsistentNaming
        public bool success;

        // ReSharper disable once InconsistentNaming
        public int value;

        // ReSharper disable once InconsistentNaming
        public User user;

        // ReSharper disable once InconsistentNaming
        public DrunkLevel drunkLevel; 
        
        public DrunkCheckResponse()
        {
            success = false;
        }

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