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
        
        public DrunkCheckResponse()
        {
            success = false;
        }

        public DrunkCheckResponse(int reading)
        {
            value = reading;
            success = true;
        }

        public DrunkCheckResponse(User user, int reading)
        {
            this.user = user;
            value = reading;
            success = true;
        }
    }
}