using System.Linq;
using Stripe;

namespace DrunkCheck.Models
{
    public static class DrunkStripe
    {
        public static void PayRandomCharity(User user)
        {
            using (DrunkCheckerContext db = new DrunkCheckerContext())
            {
                Charity charity = db.Charities.FirstOrDefault();
                PayTheCharity(user, charity);
            }
        }

        public static void PayTheCharity(User user, Charity charity)
        {
            StripeTokenCreateOptions myToken = new StripeTokenCreateOptions
            {
                CardCvc = user.CardCvc,
                CardExpirationMonth = user.CardExpirationMonth,
                CardExpirationYear = user.CardExpirationYear,
                CardName = user.Name,
                CardNumber = user.CardNumber
            };

            StripeTokenService tokenService = new StripeTokenService(charity.ApiKey);
            StripeToken stripeToken = tokenService.Create(myToken);

            StripeChargeCreateOptions myCharge = new StripeChargeCreateOptions
            {
                Amount = 500,
                Currency = "gbp",
                Description = string.Empty,
                TokenId = stripeToken.Id
            };

            StripeChargeService chargeService = new StripeChargeService(charity.ApiKey);
            chargeService.Create(myCharge);
        }
    }
}