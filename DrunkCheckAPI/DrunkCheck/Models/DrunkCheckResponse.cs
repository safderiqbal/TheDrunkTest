namespace DrunkCheck.Models
{
    public class DrunkCheckResponse
    {
        // ReSharper disable once InconsistentNaming
        public bool success;

        // ReSharper disable once InconsistentNaming
        public int value;
        
        public DrunkCheckResponse()
        {
            success = false;
        }

        public DrunkCheckResponse(int reading)
        {
            value = reading;
            success = true;
        }
    }
}