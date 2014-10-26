using System;
using DrunkCheck.APIs;

namespace DrunkCheck.Models
{
    public static class DrunkHelpers
    {
        private static readonly string[] Insults = {
            "numpty",
            "tit",
            "idiot",
            "wazzok",
            "noob",
            "utter genius",
            "pillock",
            "goon",
            "stooge",
            "dummy",
            "fool",
            "jive turkey"
        };

        private static readonly string[] StopSayings =
        {
            "STAPPPP",
            "NO! NOOOOOOOOOO! NO GOD NO!",
            "Just step away from the computer device...",
            "STOP! Hammer time!",
            "STOP! Collaborate and listen. Ice is back...",
            "MOVE AWAY FROM THE KEYBOARD"
        };

        private readonly static Random Random = new Random();

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

            if (response.isTooDrunk)
            {
                if (notifySupervisor && user.SupervisorId >= 0)
                {
                    User supervisor = User.Get(user.SupervisorId);

                    //nope.avi
                    String message = string.Format(
                        "Hi {0}, {1} is trying to commit code while in the state of '{2}'. What a {3}",
                    supervisor.Name, user.Name, response.drunkLevel, Insults.GetValue(Random.Next() * Insults.Length));

                    ClockWorkSms.SendMessage(supervisor.Mobile, message);
                }

                if (textSelf)
                    ClockWorkSms.SendMessage(user.Mobile, StopSayings.GetValue(Random.Next() * StopSayings.Length).ToString());

                if (notifyIce)
                {
                    ClockWorkSms.SendMessage(user.EmergancyContact,
                        string.Format("Erm...{0}, has tried to do stupid stuff while drunk. Please stop them.", user.Name));
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
    }
}