using System;

namespace DrunkCheck.Models
{
    public static class DrunkDonate
    {
        public static void Donate(User user)
        {
            if (user == null)
                throw new Exception("User not found.");

            DrunkStripe.PayRandomCharity(user);

            using (DrunkCheckerContext db = new DrunkCheckerContext())
            {
                user.OverrideEnabled = true;
                db.SaveChanges();
            }
        }
    }
}