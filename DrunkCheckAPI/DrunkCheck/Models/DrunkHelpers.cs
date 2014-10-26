using System;
using DrunkCheck.APIs;

namespace DrunkCheck.Models
{
    public static class DrunkHelpers
    {
        public static DrunkCheckResponse Read(
            User user, bool notifySupervisor = false, bool textSelf = false, bool notifyIce = false)
        {
            if (user == null)
                throw new Exception("User not found.");

            DrunkCheckResponse response = DrunkCheckInterface.Read(user);
            using (DrunkCheckerContext db = new DrunkCheckerContext())
            {
                db.Readings.Add(new Reading
                {
                    UserId = user.Id,
                    DateTime = DateTime.Now,
                    Value = response.value
                });

                db.SaveChanges();
            }

            if (IsTooDrunk(response.value))
            {
                if (notifySupervisor && user.SupervisorId >= 0)
                {
                    User supervisor = User.Get(user.SupervisorId);

                    //nope.avi
                    String message = "Hi " + supervisor.Name + ", " + user.Name +
                                     " is trying to commit code while in the state of '" +
                                     response.drunkLevel + "'. What a tit";

                    ClockWorkSms.SendMessage(supervisor.Mobile, message);
                }

                if (textSelf)
                    ClockWorkSms.SendMessage(user.Mobile, "STAPPPP");

                if (notifyIce)
                {
                    ClockWorkSms.SendMessage(user.EmergancyContact,
                        string.Format("Erm...{0}, has tried to do stupid stuff while drunk. Please stop them.",
                            user.Name));
                }
            }

            return response;
        }

        public static void Donate(User user)
        {
            if (user == null)
                throw new Exception("User not found.");

            DrunkStripe.PayRandomCharity(user);

            using (DrunkCheckerContext db = new DrunkCheckerContext())
            {
                user.OverrideUntil = DateTime.Now.AddHours(1);
                db.SaveChanges();
            }
        }

        private static bool IsTooDrunk(int value)
        {
            return value > 400;
        }
    }
}