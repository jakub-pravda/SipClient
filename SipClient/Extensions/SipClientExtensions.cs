using SipClient.Models;
using Javor.SipSerializer;

namespace SipClient.Extensions
{
    public static class SipClientExtensions
    {
        public static Result ClientRegistration(this ISipClient sipClient)
        {
            SipSession sipSession = new SipSession();

            return null;
        }

        public static SipRequestMessage CreateRegisterMessage(this ISipClient sipClient, SipSession sipSession)
        {
            SipRequestMessage toReturn = new SipRequestMessage(RequestMethods.Register, sipClient.Account.Registrar, sipSession.SequenceNumber);


            return toReturn;
        }
    }
}