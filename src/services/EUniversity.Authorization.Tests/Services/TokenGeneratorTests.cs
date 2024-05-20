using EUniversity.Authorization.Contract.Services;
using EUniversity.Core.Enums;
using EUniversity.Shared.Authentication.Settings;
using Microsoft.Extensions.Options;
using Moq;

namespace EUniversity.Authorization.Tests.Services;

[TestFixture]
internal class TokenGeneratorTests
{
    private TokenGenerator _tokenGenerator;

    [SetUp]
    public void SetUp()
    {
        var optionsMock = new Mock<IOptions<JwtSettings>>();
        optionsMock.Setup(x => x.Value).Returns(new JwtSettings { Key = "ThisIsVeryEasyKey_for_JWT_Authentication" });

        _tokenGenerator = new TokenGenerator(optionsMock.Object);
    }

    [Test]
    public void Check_ThatGenerateAccessTokenReturnsCorrectValue()
    {
        var userId = Guid.NewGuid();
        var email = "lion20914king@gmail.com";
        var roles = new List<Role> { Role.User, Role.FacultyAdmin };
        var permissions = new List<string> { "permission" };

        var res = _tokenGenerator.GenerateAccessToken(userId, email, roles, permissions);

        Assert.That(res, Is.Not.Empty);
    }
}
