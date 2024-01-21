using FarmCentral__task_2_;
using FarmCentral__task_2_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Firebase.Auth;
using FirebaseAdmin.Auth;

namespace FarmCentral__task_2_.Controllers
{
    public class EmployeeDisplayController : Controller
    {
        FramerCentralDbContext db = new FramerCentralDbContext();
        FirebaseAuthProvider auth;
        public static DateTime StartDate;
        public static DateTime EndDate;
        public static string Type;
        public static string FarmrId;
        public EmployeeDisplayController()
        {
            auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDSCfkWVx9aSwPGDN10s_9WhlWxmrpDx2Y"));
        }
        //shows all farmers in the database
        public IActionResult DisplayFarmerProfiles()
        {
            ViewBag.LoggedIn = SessionVariables.SessionID;
            return View(AllFarmers());
        }

        //register farmer
        public IActionResult AddFarmers()
        {
            return RedirectToAction("AddFarmer","RegisterFarmer");
        }

        //Disable selected farmer profile
        public IActionResult Disable(string farmerId)
        {
            var farmer = db.Farmers.FirstOrDefault(x => x.FarmerId == farmerId);
            farmer.Active = "N"; 
            db.SaveChanges();
            return RedirectToAction("DisplayFarmerProfiles");
        }

        //Enable selected farmer profile
        public IActionResult Enable(string farmerId)
        {
            var farmer = db.Farmers.FirstOrDefault(x => x.FarmerId == farmerId);
            farmer.Active = "Y";
            db.SaveChanges();
            return RedirectToAction("DisplayFarmerProfiles");
        }

        //Gives values that produce list will be filtered by 
        public IActionResult Filter(string prodType, DateTime startDate, DateTime endDate) 
        {
            StartDate=startDate;
            EndDate=endDate;
            Type= prodType;
            return RedirectToAction("DisplayFarmerProduce");
        }

        //Removes values that produce list will be filtered by 
        public IActionResult All()
        {
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MinValue;
            Type = null;
            return RedirectToAction("DisplayFarmerProduce");
        }

        //shows selected farmers produce
        public async Task<IActionResult> DisplayFarmerProduce(string farmerId)
        {
            if (farmerId != null) 
            { FarmrId = farmerId;
            }
            var farmer = db.Farmers.FirstOrDefault(x => x.FarmerId == FarmrId);

            ViewBag.Name = farmer.Name;
            ViewBag.Surname = farmer.Surname;
            ViewBag.Email = farmer.Email;
            ViewBag.Address = farmer.Address;
            ViewBag.Tel = farmer.Tel;
            ViewBag.LoggedIn = SessionVariables.SessionID;
            //produce list with no filter values
            if (StartDate.Equals(DateTime.MinValue) && EndDate.Equals(DateTime.MinValue) && Type == null)
            {
                return View(FarmerProduce(FarmrId));
            }
            //produce list with filter values
            else
            {
                return View(filterSearchBar(StartDate, EndDate, FarmrId, Type));
            }
           
        }
        //returns list of all farmer profiles
        public List<Farmer> AllFarmers() 
        {
            List<Farmer> farmersList = new List<Farmer>();
            List<Farmer> orderedFarmersList = new List<Farmer>();
            int noItems = db.Farmers.Count();

            for (int x = 0; x < noItems; x++)
            {
                Farmer farmers = db.Farmers.AsEnumerable().ElementAt(x);
                farmersList.Add(farmers);
            }
            orderedFarmersList = farmersList.OrderBy(x=>x.Active == "Y" ? 0 : 1).ToList();
            return orderedFarmersList;
        }

        //returns list of all the selected farms produce
public List<Produce> FarmerProduce(string farmerId) 
        {
            List<Produce> farmerProdLst = new List<Produce>();
            farmerProdLst = db.Produces.Where(x => x.FarmrId == farmerId).ToList();
           
            return farmerProdLst;
        }

        //returns a filtered list of the selected farms produce
        public List<Produce> filterSearchBar(DateTime startDate,DateTime endDate,string farmerId, string type)
        {
            List<Produce> prodList= new List<Produce>();
            
            
            if (startDate != DateTime.MinValue && endDate != DateTime.MinValue && type==null)
            {
                prodList = db.Produces.Where(obj => obj.PackedDate >= startDate && obj.PackedDate <= endDate && obj.FarmrId == farmerId).ToList();
                return prodList;
            }
            if (type != null && startDate == DateTime.MinValue && endDate == DateTime.MinValue)
            {
                prodList = db.Produces.Where(obj => obj.Producetype == type && obj.FarmrId == farmerId).ToList();
                return prodList;
            }

            if (startDate != DateTime.MinValue && endDate != DateTime.MinValue && type!=null)
            {
                 prodList = db.Produces.Where(obj => obj.PackedDate >= startDate && obj.PackedDate <= endDate && obj.FarmrId == farmerId && obj.Producetype == type).ToList();
                return prodList;
            }

            return prodList;
        }

    }
}
