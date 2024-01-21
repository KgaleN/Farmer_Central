using FarmCentral__task_2_.Models;
using Microsoft.AspNetCore.Mvc;
using Firebase.Auth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FirebaseAdmin.Auth;
using FirebaseAuthException = Firebase.Auth.FirebaseAuthException;

namespace FarmCentral__task_2_.Controllers
{
    public class AuthenticationController : Controller
    {
        FramerCentralDbContext db = new FramerCentralDbContext();
        
        public static string LoginError =null;
        public static string UID= null;
        FirebaseAuthProvider auth;
        

        public AuthenticationController()
        {
            auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDSCfkWVx9aSwPGDN10s_9WhlWxmrpDx2Y"));
        }

        //logs user out
        public IActionResult Logout() 
        {
            LoginError=null;
            SessionVariables.SessionID = null;
            return RedirectToAction("Login");
        }
        //prompts user to login 
        public IActionResult Login()
        {
            ViewBag.LoggedIn = null;
            ViewBag.LoginError = LoginError;
            return View();
        }

        //authenticating users by verifying their email and passwords
        public async Task<IActionResult> SignIn(string password, string email)
        {
           try
            {
                //log in an existing user
                var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(email, password);
                UID = fbAuthLink.FirebaseToken;
                
                //save the token to a session variable
                if (UID != null)
                {
                    var userRecord = auth.GetUserAsync(UID).Result;
                    SessionVariables.SessionID = userRecord.LocalId.ToString();
                    var farmer = db.Farmers.FirstOrDefault(x => x.FarmerId == SessionVariables.SessionID);
                    int empCount = db.Employees.Where(x=>x.EmpId == SessionVariables.SessionID).Count();  
                    int farmerCount = db.Farmers.Where(x => x.FarmerId == SessionVariables.SessionID).Count();

                    if (empCount>0)
                    {
                        
                        return RedirectToAction("DisplayFarmerProfiles", "EmployeeDisplay");
                    }
                    //authorise access if account is active
                    if (farmerCount>0 && farmer.Active=="Y") 
                    {
                     
                        return RedirectToAction("DisplayProduce", "FarmerDisplay");
                    }
                    //authorise access if account is not active
                    if (farmerCount > 0 && farmer.Active=="N") 
                    {
                       
                        LoginError = "This account was disabled";
                        return RedirectToAction("Login");
                    }

                    return RedirectToAction("Login");
                }
            }

            //executes if password is incorrect 
            catch (FirebaseAuthException ex)
            {
                LoginError = "The password or email which you entered is incorrect.";
                return RedirectToAction("Login");
            }

            return View();
        }
    }
}
