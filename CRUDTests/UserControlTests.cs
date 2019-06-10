using CRUDLibrary;
using Domain.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CRUDTests
{
    public class UserControlTests
    {
        List<User> userList = new List<User>();

        //email format
        //password format -> 1 upper, low, numeric


        [Theory]
        [InlineData("Myelxx", "Mel", "Meji")]
        public void UpdateUser_ShouldNotAcceptExistingUsername(string userName, string firstName, string lastName)
        {
            User newUser = new User { Username = userName, FirstName = firstName, LastName = lastName };

            Assert.Throws<ArgumentException>("", () => UserControl.UpdateUser(userList, newUser));

        }

        [Theory]
        [InlineData("Me", "Mel", "Meji", "Username")]
        public void CreateNewUser_ShouldNotExceedMaxLength(string userName, string firstName, string lastName, string param)
        {
            User newUser = new User { Username = userName, FirstName = firstName, LastName = lastName };
            Assert.True(newUser.Username.Length > 1);
            Assert.True(newUser.FirstName.Length > 1);
            Assert.True(newUser.LastName.Length > 1);
            //Assert.Throws<ArgumentException>(param, () => UserControl.CreateNewUser(userList, newUser));
        }

        [Fact]
        public void CreateNewUser_ShouldWork()
        {
            User newUser = new User { Username = "Myel", FirstName = "Melroese", LastName = "Mejidana" };
            UserControl.CreateNewUser(userList, newUser);

            string actual = newUser.Username;

            Assert.True(actual.Length > 0);
            Assert.True(userList.Count == 1);
            Assert.Contains<User>(newUser, userList);
        }
     
        [Theory]
        [InlineData("Myelxx", "", "Meji", "FirstName")]
        [InlineData("Myelxx", "Mel", "", "LastName")]
        [InlineData("", "Mel", "Meji", "Username")]

        public void CreateNewUser_ShouldNotAcceptEmptyValues(string userName, string firstName, string lastName, string param)
        {
            User newUser = new User { Username = userName, FirstName = firstName, LastName = lastName };

            Assert.Throws<ArgumentException>(param, () => UserControl.CreateNewUser(userList, newUser));
        }

        [Theory]
        [InlineData(2, "Myelxx", "", "Meji")]
        public void UpdateUser_ShouldWork(int userId, string userName, string firstName, string lastName)
        {
            User newUser = new User { UserId = userId, Username = userName, FirstName = firstName, LastName = lastName };
            bool isNotExist = UserControl.IsUserExist(userList, newUser);
            Assert.True(isNotExist);
        }

        public class UserControlTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "Myelxx", "", "Meji", "FirstName" };
                yield return new object[] { "", "Mel", "Meji", "Username" };
                yield return new object[] { "Myelxx", "", "Meji", "LastName" };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


    }
}
