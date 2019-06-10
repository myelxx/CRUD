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

        //password format -> 1 upper, low, numeric
        /**
        (?=.*[A-Z])(?=.*[a-z])(?=.*\d).+$
 
        ^             start of string
        (?=.*[A-Z])   at least one upper case letter exists
        (?=.*[a-z])   at least one lower case letter exists
        (?=.*\d)      at least one digit exists
        .+            match all other characters
        $             end of string
        
        **/
     
        /**
         public void CheckIfUsernameIsValid(string username, bool isValid)
        {
            bool actualResult = UserManager.GetUserManager().IsUsernameValid(username);
            Assert.Equal(isValid, actualResult);
        }
        **/
        
        /**
         public void CheckIfPasswordIsValid(string username, bool isValid)
        {

        }
        **/
        
        //check if lastname, firstname is not numeric
     
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

            //int expected = userList.GetAllUser().GetSizeOfList() + 1; //create singleton
            int expected = userList.GetSizeOfList() + 1;
            int actual = userList.GetSizeOfList();

            Assert.Equal(expected, actual);
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
        
        [Theory]
        [InlineData("Myelxx", "Mel", "Meji")]
        public void UpdateUser_ShouldNotAcceptExistingUsername(string userName, string firstName, string lastName)
        {
            User newUser = new User { Username = userName, FirstName = firstName, LastName = lastName };
            Assert.Throws<ArgumentException>("", () => UserControl.UpdateUser(userList, newUser));
        }
        
        //update should fail
        //if not valid username, firstname, lastname, password, email
        //if empty
        
        //retrieve function
        //should work if have values -> list.Count < 0
        //should fail if empty -> list.Count > 0 
        
        //delete
        //should work
        [Theory]
        [InlineData(2)]
        public void DeleteUser_ShouldWork(int userId)
        {
            User newUser = new User { UserId = userId };
            bool isExist = UserControl.IsUserExist(userList, newUser);
            
            //UserControl.DeleteUser(userList, newUser);
            Assert.True(isExist);
        }
        
        //should fail
        [InlineData(2)]
        public void DeleteUser_ShouldWork(int userId)
        {
            User newUser = new User { UserId = userId };
            bool isExist = UserControl.IsUserExist(userList, newUser);
            
            Assert.True(!isExist);
            Assert.Throws<ArgumentException>(param, () => UserControl.DeleteUser(userList, newUser));
            
        }

        /**
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
        **/

    }
}
