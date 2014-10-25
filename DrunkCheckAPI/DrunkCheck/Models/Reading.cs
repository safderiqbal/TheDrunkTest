using System;

namespace DrunkCheck.Models
{
    public class Reading
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Value { get; set; }

        public DateTime DateTime { get; set; } 

        public Reading()
        {
            
        }
    }
}