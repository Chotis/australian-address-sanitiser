using australian_address_sanitiser;
using System.IO;
using System.Runtime.ConstrainedExecution;

namespace australia_address_sanitiser_tests
{
    public class SanitiserTests
    {
        private readonly AddressSanitiser _sut = new AddressSanitiser();

        [Theory]
        [InlineData("123 Test Highway, Ferntree Gully, VIC 3156", "123 Test Hwy, Ferntree Gully, VIC 3156")]
        [InlineData("123 Test Rd, Coopers Plains, QLD 4108", "123 test Road, Coopers Plains, QLD 4108")]
        [InlineData("123 Test road, Croydon, NSW 2132", "123 Test Road, Croydon, NSW 2132")]
        [InlineData("123/456 Test Rd, Oakleigh, VIC 3166", "123/456 Test Rd, Oakleigh, VIC 3166")]
        [InlineData("123-456 Test    Rd,   Hallam, VIC 3803", "123-456 Test Rd, Hallam, VIC 3803")]
        [InlineData("10 - 20 Test Drive, Tweed Heads South, NSW 2486", "10-20 Test Drive, Tweed Heads South, NSW 2486")]
        [InlineData("123 / 456 Test Rd, Oakleigh, VIC 3166", "123/456 Test Rd, Oakleigh, VIC 3166")]
        [InlineData("1 / 234 - 567 Test Rd, Braeside, VIC 3195", "1 /  234  -   567 Test Rd, Braeside, VIC 3195")]
        [InlineData("1­--5 Test Street, Tamworth, NSW 2340", "1­ 5 Test Street, Tamworth, NSW 2340")]
        [InlineData("Building 2,70 Test Street, Braybrook, VIC 3019", "Building 2,70 Test St, Braybrook, VIC 3019")]
        [InlineData("Cnr Test Street and Test Street, Heidelberg, VIC 3084", "Corner Test Street and Test Street, Heidelberg, VIC 3084")]
        [InlineData("P.O.Box 123, Test, QLD 4557", "POBox 123, Test, QLD 4557")]
        [InlineData("Yards 1,2,3,4.  567 Test Avenue, Minchinbury, NSW 2770", "Yards 1.2.3,4. 567 Test Avenue, Minchinbury, NSW 2770")]
        public void SanitiseAddress_SimilarAddress_Match(string address, string similarAddress)
        {
            //Act
            var firstAddress = _sut.SanitizeAddress(address);
            var secondAddress = _sut.SanitizeAddress(similarAddress);

            //Assert
            Assert.Equal(firstAddress, secondAddress);
        }

        [Theory]
        [InlineData("123 Test Highway, Ferntree Gully, VIC 3156", "123 Test Hwy, Ferntree Gully, VIC 3156")]
        [InlineData("123 Test Rd, Coopers Plains, QLD 4108", "123 test Road, Coopers Plains, QLD 4108")]
        [InlineData("123 Test road, Croydon, NSW 2132", "123 Test Road, Croydon, NSW 2132")]
        [InlineData("123/456 Test Rd, Oakleigh, VIC 3166", "123/456 Test Rd, Oakleigh, VIC 3166")]
        [InlineData("123-456 Test    Rd,   Hallam, VIC 3803", "123-456 Test Rd, Hallam, VIC 3803")]
        [InlineData("10 - 20 Test Drive, Tweed Heads South, NSW 2486", "10-20 Test Drive, Tweed Heads South, NSW 2486")]
        [InlineData("123 / 456 Test Rd, Oakleigh, VIC 3166", "123/456 Test Rd, Oakleigh, VIC 3166")]
        [InlineData("1 / 234 - 567 Test Rd, Braeside, VIC 3195", "1 /  234  -   567 Test Rd, Braeside, VIC 3195")]
        [InlineData("1­--5 Test Street, Tamworth, NSW 2340", "1­ 5 Test Street, Tamworth, NSW 2340")]
        [InlineData("Building 2,70 Test Street, Braybrook, VIC 3019", "Building 2,70 Test St, Braybrook, VIC 3019")]
        [InlineData("Cnr Test Street and Test Street, Heidelberg, VIC 3084", "Corner Test Street and Test Street, Heidelberg, VIC 3084")]
        [InlineData("P.O.Box 123, Test, QLD 4557", "POBox 123, Test, QLD 4557")]
        [InlineData("Yards 1,2,3,4.  567 Test Avenue, Minchinbury, NSW 2770", "Yards 1.2.3,4. 567 Test Avenue, Minchinbury, NSW 2770")]
        public void SanitiseAddress_SimilarAddressAdditionalParameters_Match(string address, string similarAddress)
        {
            //Act
            var firstAddress = _sut.SanitizeAddress(address, false, false, true);
            var secondAddress = _sut.SanitizeAddress(similarAddress, false, false, true);

            //Assert
            Assert.Equal(firstAddress, secondAddress);
        }
    }
}