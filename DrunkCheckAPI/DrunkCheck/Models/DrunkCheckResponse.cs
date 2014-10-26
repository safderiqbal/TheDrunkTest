namespace DrunkCheck.Models
{
    public class DrunkCheckResponse
    {
        // ReSharper disable InconsistentNaming
        public bool success;
        public int value;
        public User user;
        public DrunkLevel drunkLevel; 
        public string errorMessage;
        // ReSharper restore InconsistentNaming
        
        public DrunkCheckResponse(string errorMessage)
        {
            success = false;
            this.errorMessage = errorMessage;
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