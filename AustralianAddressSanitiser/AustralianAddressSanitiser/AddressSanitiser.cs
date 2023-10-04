using System.Text.RegularExpressions;

namespace AustralianAddressSanitiser
{
    public static class AddressSanitiser
    {
        public static string SanitizeAddress(string address, bool abbreviateState = true, bool abbreviateAddressSuffix = true, bool capitalise = false)
        {
            string sanitisedAddress = "";

            sanitisedAddress = address.ToLower();
            
            sanitisedAddress = RemoveCommas(sanitisedAddress);

            sanitisedAddress = RemoveFullStops(sanitisedAddress);

            sanitisedAddress = RemoveHyphens(sanitisedAddress);

            sanitisedAddress = RemoveForwardSlashes(sanitisedAddress);

            sanitisedAddress = RemoveExtraSpaces(sanitisedAddress);

            sanitisedAddress = ReturnState(sanitisedAddress, abbreviateState);

            sanitisedAddress = ReturnAddressSuffix(sanitisedAddress, abbreviateAddressSuffix);

            if (capitalise)
            {
                sanitisedAddress = sanitisedAddress.ToUpper();
            }

            return sanitisedAddress;
        }

        // Replace all commas with an empty string
        private static string RemoveCommas(string addressInput)
        {
            
            string removedCommas = addressInput.Replace(",", "");

            return removedCommas;
        }

        // Replace all full stops with an empty string
        private static string RemoveFullStops(string addressInput)
        {
            string removedFullStops = addressInput.Replace(".", "");

            return removedFullStops;
        }

        private static string RemoveHyphens(string addressInput)
        {
            // Remove hyphens from the input string
            return addressInput.Replace("-", " ");
        }

        private static string RemoveForwardSlashes(string addressInput)
        {
            // Remove hyphens from the input string
            return addressInput.Replace("/", " ");
        }

        // Remove additional spaces
        private static string RemoveExtraSpaces(string addressInput)
        {
            string removedExtraSpaces = Regex.Replace(addressInput, @"\s+", " ").Trim();

            return removedExtraSpaces;
        }

        private static string ReturnState(string addressInput, bool abbreviateState)
        {
            var lowercaseDictionary = Constants.Constants.StateCodesAbbreviations.ToDictionary(entry => entry.Key.ToLower(), entry => entry.Value.ToLower());

            string[] words = addressInput.Split(' ');

            // Iterate over the words and check if any word is a key in the dictionary
            foreach (var kvp in lowercaseDictionary)
            {
                // Check if any word in the input address matches a key or value in the dictionary
                for (int i = 0; i < words.Length; i++)
                {
                    if (abbreviateState)
                    {
                        if (words[i] == kvp.Key)
                        {
                            words[i] = kvp.Value;
                            break;
                        }
                    }
                    else
                    {
                        if (words[i] == kvp.Value)
                        {
                            words[i] = kvp.Key;
                            break;
                        }
                    }
                }
            }

            // Join the words back into a string
            string modifiedAddressState = string.Join(" ", words);
            return modifiedAddressState;
        }

        private static string ReturnAddressSuffix(string addressInput, bool abbreviateAddressSuffix)
        {
            var lowercaseDictionary = Constants.Constants.AddressSuffixAbbreviations.ToDictionary(entry => entry.Key.ToLower(), entry => entry.Value.ToLower());

            string[] words = addressInput.Split(' ');

            // Iterate over the words and check if any word is a key in the dictionary
            foreach (var kvp in lowercaseDictionary)
            {
                // Check if any word in the input address matches a key or value in the dictionary
                for (int i = 0; i < words.Length; i++)
                {
                    if (abbreviateAddressSuffix)
                    {
                        if (words[i] == kvp.Key)
                        {
                            words[i] = kvp.Value;
                            break;
                        }
                    }
                    else{
                        if (words[i] == kvp.Value)
                        {
                            words[i] = kvp.Key;
                            break;
                        }
                    }
                }
            }

            // Join the words back into a string
            string modifiedAddressSuffix = string.Join(" ", words);
            return modifiedAddressSuffix;

        }
    }
}