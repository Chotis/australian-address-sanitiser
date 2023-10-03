# Australian Address Sanitiser

The Australian Address Sanitiser is a utility class designed to sanitise Australian addresses. It provides a method to remove commas, full stops, hyphens and extra spaces from the input address. Additionally, it can abbreviate states and address suffixes, as well as capitalize the entire address if required.

## Usage

### Installation

No installation is necessary. Simply include the nuget package https://www.nuget.org/packages/australian-address-sanitiser in your C# project.

### Example Usage

```csharp
using AustralianAddressSanitiser;

class Program
{
    static void Main()
    {
        string inputAddress = "123 Example St, Sydney, NSW.";
        
        // Sanitise the address without abbreviating state and address suffix, and capitalize it
        string sanitizedAddress = AddressSanitiser.SanitizeAddress(inputAddress, abbreviateState: false, abbreviateAddressSuffix: false, capitalise: true);

        Console.WriteLine("Sanitised Address: " + sanitizedAddress);
    }
}
```
### Method: 'SanitizeAddress'
```csharp
public static string SanitizeAddress(string address, bool abbreviateState = true, bool abbreviateAddressSuffix = true, bool capitalise = false)
```

### Parameters

- `address` (*string*): The input address to be sanitised.
- `abbreviateState` (*bool*, optional): Set to `true` to abbreviate the state names (e.g., "New South Wales" becomes "NSW"). Default is `true`.
- `abbreviateAddressSuffix` (*bool*, optional): Set to `true` to abbreviate address suffixes (e.g., "Street" becomes "St"). Default is `true`.
- `capitalise` (*bool*, optional): Set to `true` to convert the entire address to uppercase. Default is `false`.

### Returns

- (*string*): The sanitised address.