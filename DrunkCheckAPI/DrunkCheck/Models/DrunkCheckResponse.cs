namespace DrunkCheck.Models
{
    public class DrunkCheckResponse
    {
        // ReSharper disable once InconsistentNaming
        public bool success;

        // ReSharper disable once InconsistentNaming
        public int value;

        // ReSharper disable once InconsistentNaming
        public string username;
        
        public DrunkCheckResponse()
        {
            success = false;
        }

        public DrunkCheckResponse(int reading)
        {
            value = reading;
            success = true;
        }

        public DrunkCheckResponse(string username, int reading)
        {
            this.username = username;
            value = reading;
        }
    }
}