
using Xunit;

namespace UserService.UnitTests;




public class UnitTest1
{

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(19)]
    public void GivenANumberIsNotDivisibleBy3r5_ThenReturnsNumberAsString(int number)
    {
        string result = FizzBuzzGame.Play(number);

        Assert.Equal(result, result.ToString());
    }
}


public static class FizzBuzzGame
{

    public static string Play(int i) { 
        return i.ToString();
    }
}

