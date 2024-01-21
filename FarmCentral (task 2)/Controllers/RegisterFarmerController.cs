using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using Firebase.Auth;
using FarmCentral__task_2_.Models;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace FarmCentral__task_2_.Controllers
{
    public class RegisterFarmerController : Controller
    {
        FramerCentralDbContext db = new FramerCentralDbContext();
        Farmer farmer = new Farmer();
        FirebaseAuthProvider auth;
        public static string RegError;

        public RegisterFarmerController() 
        {
            auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDSCfkWVx9aSwPGDN10s_9WhlWxmrpDx2Y"));
        }

        //prompt user to register farmer
        public IActionResult AddFarmer()
        {
            ViewBag.LoggedIn = SessionVariables.SessionID;
            ViewBag.RegError = RegError;
            return View();
        }


        //employee registers farmers  
        public async Task<IActionResult> SaveFarmer(string email,string password,string name, string surname,string phone, string address)
        {
            try
            {
                //create the user using firebase
                var fbAuthLink = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
                string UID = fbAuthLink.FirebaseToken;
                var userRecord = auth.GetUserAsync(UID).Result;
                string farmerId = userRecord.LocalId.ToString();
                FarmerDb(farmerId, email, name, surname, phone, address);  
            }
            catch (FirebaseAuthException ex)
            {
                RegError = "User Already exixts";
                ViewBag.RegError = RegError;
                return RedirectToAction("AddFarmer");
            }
              return RedirectToAction("DisplayFarmerProfiles", "EmployeeDisplay");
        }
        //saves farmer to database
        public void FarmerDb(string id, string email, string name, string surname, string phone, string address) 
        {
            farmer.FarmerId = id;
            farmer.Name = name;
            farmer.Surname = surname;
            farmer.Active = "Y";
            farmer.Tel=phone;
            farmer.Address = address;
            farmer.Email = email;
            farmer.EmpId = SessionVariables.SessionID;
            db.Farmers.Add(farmer);
            db.SaveChanges();
        }
    }
}
