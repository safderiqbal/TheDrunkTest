namespace DrunkCheck.Models
{
    public class DrunkCheckResponse
    {
        // ReSharper disable once InconsistentNaming
        protected bool success;

        // ReSharper disable once InconsistentNaming
        protected int value;
        
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