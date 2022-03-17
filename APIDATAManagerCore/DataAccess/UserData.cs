using APIDataManager.Library.Internal.DataAccess;
using APIDataManager.Library.Models;
using APIDATAManagerCore.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIDataManager.Library.DataAccess
{
    public class UserData
    {
        private IConfiguration _config;

        public UserData(IConfiguration config)
        {
            _config = config;
        }
        public UserModel GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { Id = Id };
            return sql.LoadData<UserModel, dynamic>("WorkoutData.dbo.spUserLookup", p, "WorkoutData").First();
        }

        public bool Verify(Login user)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { Username = user.username, Email = user.username, Password = user.password };
            return sql.UserExists<string, dynamic>("WorkoutData.dbo.spUserLogin", p, "WorkoutData");
        }

        public bool CreateUser(CreateUser user)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { Username = user.UserName, Email = user.Email, Password = user.Password,
            FirstName = user.FirstName, LastName = user.LastName};
            return sql.CreateUser<string, dynamic>("WorkoutData.dbo.spCreateUser", p, "WorkoutData");
            //SEND VERIFICATION EMAIL!!
        }

        public List<UserWorkout> GetUserWorkouts(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { Id = Id };
            return sql.LoadData<UserWorkout, dynamic>("WorkoutData.dbo.spUserWorkouts", p, "WorkoutData");
        }

        public List<String> GetRecentUserExercise(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { Id = Id };
            return sql.LoadData<String, dynamic>("WorkoutData.dbo.spRecentUserExercies", p, "WorkoutData");
        }

        public List<UserExerciescs> GetUserExercise(string UserId)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { UserId = UserId };
            return sql.LoadData<UserExerciescs, dynamic>("WorkoutData.dbo.spUserExercies", p, "WorkoutData");
        }

        internal List<UserExerciescs> GetExploreExercise(ExploreExercies filter, string username)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { UserId = filter.username,
                          Exercies = filter.exercies,
                          ThisUser = username
                        };
            return  sql.LoadData<UserExerciescs, dynamic>("WorkoutData.dbo.spExploreExercies", p, "WorkoutData");
        }

        public List<String> GetUserWorkoutExercise(string userId, string id)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new
            {
                UserId = userId,
                Id = id
            };
            return sql.LoadData<String, dynamic>("WorkoutData.dbo.spUserWorkoutExercises", p, "WorkoutData");
        }

        public int CreateWorkout(UserWorkout workout)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            return sql.SaveData<UserWorkout>("WorkoutData.dbo.spCreateWorkout", workout, "WorkoutData");
        }

        public int CreateExercies(UserExercies exercies)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            return sql.SaveData<UserExercies>("WorkoutData.dbo.spCreateExercies", exercies, "WorkoutData");
        }

        public int CreateExerciseSet(UserExercies exercies)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            return sql.SaveData<UserExercies>("WorkoutData.dbo.spCreateExerciesSet", exercies, "WorkoutData");
        }
    }
}
