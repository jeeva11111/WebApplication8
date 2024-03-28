using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models.Account.Profile
{
    public class UserProfile
    {
       
        public int Id { get; set; }
        public int VideoCount { get; set; }
        public int Subscribers { get; set; }
        public int AudioCount { get; set; }
        


    }
}
