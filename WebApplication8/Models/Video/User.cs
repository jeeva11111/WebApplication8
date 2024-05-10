using Nexmo.Api.Pricing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication8.Models.ExFile;
using WebApplication8.Models.FileManager;
using WebApplication8.Models.Message;
using WebApplication8.Models.Notes;
using WebApplication8.Models.Notify;
using WebApplication8.Models.SkillsAssignments;

namespace WebApplication8.Models.Video
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? About { get; set; }
        public string? Categories { get; set; }
        public byte[]? ProfileImage { get; set; }

        // User has many Chennels
        public ICollection<Chennel>? Chennels { get; set; }
        public ICollection<Models.Notify.Notify> Notify { get; set; }

        public ICollection<Models.Video.Subscribes> Subscribers { get; set; }
        public ICollection<Models.Video.Audio> Audio { get; set; }

        //public ICollection<Models.ExFile.ImageUploadModel> ImageUploads { get; set; }

        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public string? Roles { get; set; } = "user";
        public ICollection<Models.FileManager.FileManager> fileManagers { get; set; }

        // Adding country, state, and city properties
        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country? Country { get; set; }

        public int? StateId { get; set; }
        [ForeignKey("StateId")]
        public State? State { get; set; }

        public int? CityId { get; set; }
        [ForeignKey("CityId")]
        public City? City { get; set; }

        public ICollection<ImageUpload>? ImageUploads { get; set; }

        public ICollection<Notes.NotePads>? NotePads { get; set; }

        public ICollection<Message.Message>? Messages { get; set; }
        public ICollection<SkillsAssignmentsModel>? SkillsAssignments { get; set; }
        public ICollection<Admin.AdminTeams>? AdminTeams { get; set; }

    }



    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class State
    {
        [Key]
        public int Id { get; set; }
        public string? StateName { get; set; }
        [ForeignKey("CountryId")]
        public Country? country { get; set; }
        public int CountryId { get; set; }

    }

    public class City
    {
        [Key]
        public int Id { get; set; }
        public string? CityName { get; set; }
        [ForeignKey("stateId")]
        public State? state { get; set; }
        public int stateId { get; set; }
    }

}
