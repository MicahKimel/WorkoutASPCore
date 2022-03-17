using APIDataManager.Library.DataAccess;
using APIDataManager.Library.Models;
using APIDATAManagerCore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace APIDATAManagerCore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("apiuser")]
    [EnableCors]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;

        public UserController(IConfiguration config)
        {
            _config = config;
        } 

        [HttpGet]
        [Route("user")]
        // GET: User
        public UserModel GetById(string id)
        {
            string user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserData data = new UserData(_config);
            return data.GetUserById(id);
        }

        [HttpPost]
        [Route("createuser")]
        public bool CreateUser(CreateUser user)
        {
            UserData data = new UserData(_config);
            return data.CreateUser(user);
        }

        [HttpGet]
        // GET: UserWorkouts
        [Route("Workouts")]
        public List<UserWorkout> GetUserWorkouts(string id)
        {
            UserData workout = new UserData(_config);
            return workout.GetUserWorkouts(id);
        }

        [HttpGet]
        // GET: RecentUserExercies
        [Route("RecentExercies")]
        public IActionResult GetRecentUserExercise()
        {
            string user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserData data = new UserData(_config);
            return Ok(data.GetRecentUserExercise(user));
        }

        [HttpPost]
        // GET: UserExercies
        [Route("Exercies")]
        public IActionResult GetUserExercise(Login id)
        {
            UserData data = new UserData(_config);
            return Ok(data.GetUserExercise(id.username));
        }

        [HttpPost]
        // GET: UserExercies
        [Route("ExploreExercies")]
        public IActionResult GetExploreExercise(ExploreExercies filter)
        {
            UserData data = new UserData(_config);
            string username = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(data.GetExploreExercise(filter, username));
        }

        [HttpGet]
        // GET: UserWorkoutExercies
        [Route("WorkoutExercies")]
        public List<String> GetUserWorkoutExercise(string id, string exid)
        {
            UserData data = new UserData(_config);
            return data.GetUserWorkoutExercise(id, exid);
        }

        [HttpPost]
        [Route("CreateWorkout")]
        public int CreateWorkout(string id, string Title, bool Public)
        {
            UserWorkout workout = new UserWorkout();
            string UserId = id;
            workout.AuthId = UserId;
            workout.Title = Title;
            workout.Public = Public;
            workout.CreateTime = DateTime.Now;
            UserData data = new UserData(_config);
            return data.CreateWorkout(workout);
        }

        [HttpPost]
        [Route("CreateSet")]
        public int CreateExercise(ExerciseSubmit ex)
        {
            //!!! VERIFY EXERCISE TYPE OR THROW ERROR
            string user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserExercies exercies = new UserExercies();
            exercies.AuthId = user;
            exercies.ExerciseTypeId = ex.ExerciseType;
            exercies.MetricType = ex.MetricType;
            exercies.Sets = ex.sets;
            exercies.Reps = ex.reps;
            exercies.StartTime = DateTime.Now;
            exercies.EndTime = DateTime.Now;
            exercies.ExerciesCreateTime = DateTime.Now;
            exercies.SetUpdateTime = DateTime.Now;
            exercies.Weight = ex.Weight;
            UserData data = new UserData(_config);
            return data.CreateExerciseSet(exercies);
        }

        /*[HttpPost]
        [Route("CreateExerciseSet")]
        public int CreateExerciseSet(string id, int workoutId, int exerciseId, int sets, int reps, DateTime Time)
        {
            UserExercies exercies = new UserExercies();
            exercies.AuthId = id;
            exercies.workoutId = workoutId;
            exercies.id = exerciseId;
            exercies.Sets = sets;
            exercies.Reps = reps;
            exercies.Time = Time;
            exercies.Time = DateTime.Now;
            exercies.ExerciesCreateTime = DateTime.Now;
            exercies.SetUpdateTime = DateTime.Now;
            UserData data = new UserData(_config);
            return data.CreateExerciseSet(exercies);

        }*/

    }
}
