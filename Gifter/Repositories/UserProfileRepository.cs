using Gifter.Models;
using Gifter.Utils;
using System.Reflection.PortableExecutable;

namespace Gifter.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }
        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT up.Name, up.Bio, up.Email, up.DateCreated, 
                       up.ImageUrl
                  FROM UserProfile up";

                    var reader = cmd.ExecuteReader();

                    var userProfiles = new List<UserProfile>();
                    while (reader.Read())
                    {
                        userProfiles.Add(new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            Bio = DbUtils.GetString(reader, "Bio"),

                            DateCreated = DbUtils.GetDateTime(reader, "PostDateCreated"),
                            ImageUrl = DbUtils.GetString(reader, "PostImageUrl"),

                        });
                    }

                    reader.Close();

                    return userProfiles;
                }
            }
        }

        public UserProfile GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" 
                   SELECT up.Name, up.Bio, up.Email, up.DateCreated,
                       up.ImageUrl 
                  FROM UserProfile up;
                           WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    UserProfile userProfile = null;
                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = id,
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl")
                        };
                    }

                    reader.Close();

                    return userProfile;
                }
            }
        }

        public void Add(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserProfile (Name, Email,Bio, DateCreated, ImageUrl)
                        OUTPUT INSERTED.ID
                        VALUES (@Name, @Email, @Bio @DateCreated, @ImageUrl)";

                    DbUtils.AddParameter(cmd, "@Name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@Bio", userProfile.Bio);

                    DbUtils.AddParameter(cmd, "@DateCreated", userProfile.DateCreated);
                    DbUtils.AddParameter(cmd, "@ImageUrl", userProfile.ImageUrl);

                }
            }
        }
        public void Update(UserProfile userProfile) 
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE UserProfile
                           SET Name = @Name,
                               Email = @Email,
                               Bio = @Bio     
                               DateCreated = @DateCreated,
                               ImageUrl = @ImageUrl,                                
                                WHERE Id = @Id";


                    DbUtils.AddParameter(cmd, "@Title", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@Caption", userProfile.Email);
                     DbUtils.AddParameter(cmd, "@Bio", userProfile.Bio);
                    DbUtils.AddParameter(cmd, "@DateCreated", userProfile.DateCreated);
                    DbUtils.AddParameter(cmd, "@ImageUrl", userProfile.ImageUrl);

                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Post WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UserProfile GetByIdWithPosts(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT up.Id AS UserProfileId, up.[Name], up.Email, up.Bio, up.DateCreated,
                       up.ImageUrl,

                       p.Id AS PostId, p.Title, p.ImageUrl, p.Caption, p.DateCreated AS PostDateCreated,
                       p.UserProfileId AS PostUserProfileId

                  FROM UserProfile up
                       LEFT JOIN Post p ON p.UserProfileId = up.id
                       WHERE up.Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    UserProfile userProfile = null;
                    while (reader.Read())
                    {
                        if (userProfile == null)
                        {
                            userProfile = new UserProfile()
                            {
                                Id = id,
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                Bio = DbUtils.GetString(reader, "Bio"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                Posts = new List<Post>()
                            };
                        }

                        var postId = DbUtils.GetInt(reader, "PostId");
                        var existingPost = userProfile.Posts.FirstOrDefault(p => p.Id == postId);

                        if (existingPost == null)
                        {
                            existingPost = new Post()
                            {
                                Id = DbUtils.GetInt(reader, "PostId"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Caption = DbUtils.GetString(reader, "Caption"),
                                DateCreated = DbUtils.GetDateTime(reader, "PostDateCreated"),
                                ImageUrl = DbUtils.GetString(reader, "PostImageUrl"),
                                UserProfileId = DbUtils.GetInt(reader, "PostUserProfileId"),
                            };
                            userProfile.Posts.Add(existingPost);
                        }

                    }

                    reader.Close();

                    return userProfile;
                }
            }
        }

    }
}
