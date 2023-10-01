using System.Text.RegularExpressions;

namespace australian_address_sanitiser
{
    public class AddressSanitiser
    {
        public string SanitizeAddress(string address, bool abbreviateState = true, bool abbreviateAddressSuffix = false)
        {
            string sanitisedAddress = "";

            sanitisedAddress = address.ToLower();
            
            sanitisedAddress = RemoveCommas(sanitisedAddress);

            sanitisedAddress = RemoveFullStops(sanitisedAddress);

            sanitisedAddress = RemoveExtraSpaces(sanitisedAddress);

            sanitisedAddress = ReturnState(sanitisedAddress, abbreviateState);

            sanitisedAddress = ReturnAddressSuffix(sanitisedAddress, abbreviateAddressSuffix);

            return sanitisedAddress;
        }

        // Replace all commas with an empty string
        private string RemoveCommas(string addressInput)
        {
            
            string removedCommas = addressInput.Replace(",", "");

            return removedCommas;
        }

        // Replace all full stops with an empty string
        private string RemoveFullStops(string addressInput)
        {
            string removedFullStops = addressInput.Replace(".", "");

            return removedFullStops;
        }

        // Remove additional spaces
        private string RemoveExtraSpaces(string addressInput)
        {
            string removedExtraSpaces = Regex.Replace(addressInput, @"\s+", " ").Trim();

            return removedExtraSpaces;
        }

        private string ReturnState(string addressInput, bool abbreviateState)
        {
            // Regular expression pattern to match state names or abbreviations
            string pattern = @"\b(" + string.Join("|", Constants.Constants.StateCodesAbbreviations.Keys.Select(k => k.ToLower())) + "|" +
                             string.Join("|", Constants.Constants.StateCodesAbbreviations.Values.Select(v => v.ToLower())) + @")\b";

            // Replace state names or abbreviations in the address string
            string sanitizedStateAddress = Regex.Replace(addressInput, pattern, match =>
            {
                string value = match.Value;

                if (abbreviateState)
                {
                    // If the matched value is a state name, replace it with its abbreviation
                    if (Constants.Constants.StateCodesAbbreviations.ContainsKey(value))
                    {
                        return Constants.Constants.StateCodesAbbreviations[value];
                    }
                }
                else
                {
                    // If the matched value is a state abbreviation, replace it with its full name
                    if (Constants.Constants.StateCodesAbbreviations.ContainsValue(value))
                    {
                        return Constants.Constants.StateCodesAbbreviations.FirstOrDefault(x => x.Value == value).Key;
                    }
                }
                
                // If no match is found, return the original value
                return value;
            });

            return sanitizedStateAddress;
        }

        private string ReturnAddressSuffix(string addressInput, bool abbreviateAddressSuffix)
        {
            // Regular expression pattern to match state names or abbreviations
            string pattern = @"\b(" + string.Join("|", Constants.Constants.AddressSuffixAbbreviations.Keys.Select(k => k.ToLower())) + "|" +
                             string.Join("|", Constants.Constants.AddressSuffixAbbreviations.Values.Select(v => v.ToLower())) + @")\b";

            // Replace state names or abbreviations in the address string
            string sanitizedAddressSuffix = Regex.Replace(addressInput, pattern, match =>
            {
                string value = match.Value;

                if (abbreviateAddressSuffix)
                {
                    // If the matched value is a state name, replace it with its abbreviation
                    if (Constants.Constants.AddressSuffixAbbreviations.ContainsKey(value))
                    {
                        return Constants.Constants.AddressSuffixAbbreviations[value];
                    }
                }
                else
                {
                    // If the matched value is a state abbreviation, replace it with its full name
                    if (Constants.Constants.AddressSuffixAbbreviations.ContainsValue(value))
                    {
                        return Constants.Constants.AddressSuffixAbbreviations.FirstOrDefault(x => x.Value == value).Key;
                    }
                }

                // If no match is found, return the original value
                return value;
            });

            return sanitizedAddressSuffix;
        }
    }
}