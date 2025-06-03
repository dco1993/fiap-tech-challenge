using Bogus;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Unit
{
    public class UserTests
    {
        private readonly Faker<User> _userFaker;

        public UserTests()
        {
            _userFaker = new Faker<User>()
                .RuleFor(u => u.Name, f => f.Person.FullName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Password, "Mudar@123");
        }

        #region[Name Validation]

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void CreateUserNameEmptyOrWhitespaceShouldThrowException(string invalidName)
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(() =>
                _userFaker.RuleFor(u => u.Name, f => invalidName).Generate());

            //Assert
            Assert.Equal("User name is required.", ex.Message);
        }

        [Theory]
        [InlineData("Ana")]
        [InlineData("João")]
        [InlineData("Abc ")]
        public void CreateUserNameLessThan5CharsShouldThrowException(string invalidName)
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(() =>
                _userFaker.RuleFor(u => u.Name, f => invalidName).Generate());

            //Assert
            Assert.Equal("User name must contain at least 5 characters.", ex.Message);
        }

        [Fact]
        public void CreateUserNameAboveMaximumShouldThrowException()
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(() =>
                _userFaker.RuleFor(u => u.Name, f => f.Random.String2(101, 110)).Generate());

            //Assert
            Assert.Equal("User name must contain a maximum of 100 characters.", ex.Message);
        }

        #endregion

        #region[Email Validation]

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CreateUserEmailEmptyOrWhitespaceShouldThrowException(string invalidEmail)
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(() =>
                _userFaker.RuleFor(u => u.Email, f => invalidEmail).Generate());

            //Assert
            Assert.Equal("Email is required.", ex.Message);
        }

        [Theory]
        [InlineData("plainaddress")]
        [InlineData("missing@domain")]
        [InlineData("missing.domain@")]
        [InlineData("@missingusername.com")]
        [InlineData("user@@domain.com")]
        public void CreateUserEmailInvalidShouldThrowException(string invalidEmail)
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(() =>
                _userFaker.RuleFor(u => u.Email, f => invalidEmail).Generate());

            //Assert
            Assert.Equal("Invalid email.", ex.Message);
        }

        [Fact]
        public void CreateUserEmailAboveMaximumhouldThrowException()
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(() =>
                _userFaker.RuleFor(u => u.Email, f => f.Random.String2(95, 95) + "@teste.com").Generate());

            //Assert
            Assert.Equal("User email must contain a maximum of 100 characters.", ex.Message);
        }

        #endregion

        #region[Password Validation]

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void CreateUserPasswordEmptyOrWhitespaceShouldThrowException(string invalidPwd)
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(() =>
               _userFaker.RuleFor(u => u.Password, f => invalidPwd).Generate());

            //Assert
            Assert.Equal("Password is required.", ex.Message);
        }

        [Theory]
        [InlineData("short1!")]
        [InlineData("alllowercase1!")]
        [InlineData("ALLUPPERCASE1!")]
        [InlineData("NoNumber!!")]
        [InlineData("NoSpecialChar1")]
        [InlineData("WayTooLongPassword123!")]
        public void CreateUserPasswordInvalidShouldThrowException(string invalidPwd)
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(() =>
                _userFaker.RuleFor(u => u.Password, f => invalidPwd).Generate());

            //Assert
            Assert.Equal(
                "The password must be between 8 and 16 characters long, contain one uppercase letter, one lowercase letter, one number and one special character.",
                ex.Message);
        }

        [Fact]
        public void CreateUserPasswordValidShouldBeTrimmed()
        {
            //Arrange and Act
            var user = _userFaker.RuleFor(u => u.Password, f => "   Abcdef1!   ").Generate();

            //Assert
            Assert.Equal("Abcdef1!", user.Password);
        }

        #endregion

        [Fact]
        public void CreateValidUserShouldSucceed()
        {
            //Arrange and Act
            var user = _userFaker.Generate();

            //Assert
            Assert.True(user.Status);
        }
    }
}
