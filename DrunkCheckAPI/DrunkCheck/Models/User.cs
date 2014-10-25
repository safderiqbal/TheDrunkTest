using System.Web.Mvc.Html;
using System.Data.Entity;

namespace DrunkCheck.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public User()
        {

        }
    }
}