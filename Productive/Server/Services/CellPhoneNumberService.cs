using System;
namespace Productive.Server.Services
{
	public class CellPhoneNumberService
    {
        /// <summary>
        /// For this case let me use static methods because they are faster to code:
        /// However I could use Dependecy injection and register the validation service
        /// I could also have created a custom phone number validation  annotation
        /// </summary>
        public readonly static string InterNationalCountryCode  = "9725";
        public readonly static string LocalCountryCode  = "05";
        public readonly static  int  InternationalLength  = 12;
        public readonly static int MinimumLenght  = 10;
        /// <summary>
        /// TODO: suppose for some reason the preceeding zero could has been ignored.
        /// </summary>
        

        public static (bool status,string formatedPhoneNumber) Validate(string phoneNumber)
        {
            //checks include length, either 10 or 12
            //check if its in the international standard else fomart it accordingly.

             if (phoneNumber.Length == InternationalLength)
            {
                if (!phoneNumber.StartsWith(InterNationalCountryCode))
                {
                    return (false, "The number provided is invalid.");
                }
                return (true, phoneNumber);
            }
           else if (phoneNumber.Length== MinimumLenght)
            {
                if (!phoneNumber.StartsWith(LocalCountryCode))
                {
                    return (false, "The number provided is invalid.");
                }
                //formart it to the international stanadard
                return (true, $"{InterNationalCountryCode}{phoneNumber.Substring(2, 8)}"); //trim the 05 and add country code
            }
            
            else

            return (false,"The number provided is invalid.");
        }
    }
}

