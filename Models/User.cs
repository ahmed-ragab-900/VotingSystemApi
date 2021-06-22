using System;
using System.Collections.Generic;

#nullable disable

namespace VotingSystemApi.Models
{
    public partial class User
    {
        public User()
        {
            Candidates = new HashSet<Candidate>();
            Complaints = new HashSet<Complaint>();
            PostComments = new HashSet<PostComment>();
            PostLikes = new HashSet<PostLike>();
            Posts = new HashSet<Post>();
            StudentUnions = new HashSet<StudentUnion>();
            VoteUsers = new HashSet<Vote>();
            VoteVoters = new HashSet<Vote>();
        }

        public string Id { get; set; }
        public int? No { get; set; }
        public string Name { get; set; }
        public string AcademicNumber { get; set; }
        public string IdentityId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Image { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public bool? IsAuthorized { get; set; }
        public int? Year { get; set; }

        public virtual UserRole Role { get; set; }
        public virtual ICollection<Candidate> Candidates { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<StudentUnion> StudentUnions { get; set; }
        public virtual ICollection<Vote> VoteUsers { get; set; }
        public virtual ICollection<Vote> VoteVoters { get; set; }
    }
}
