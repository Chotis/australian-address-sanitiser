using australian_address_sanitiser;

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
        public void SanitiseAddress_SimilarAddress_Match(string address, string similarAddress)
        {
            
            //Act
            var firstAddress = _sut.SanitizeAddress(address);
            var secondAddress = _sut.SanitizeAddress(similarAddress);

            //Assert
            Assert.Equal(firstAddress, secondAddress);
        }
    }
}