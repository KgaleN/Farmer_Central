using FarmCentral__task_2_.Models;
using Microsoft.AspNetCore.Mvc;
namespace FarmCentral__task_2_.Controllers
{
    public class FarmerDisplayController : Controller
    {
        FramerCentralDbContext db = new FramerCentralDbContext();
        Produce produce = new Produce();
        //shows all logged in farmers produce
        public IActionResult DisplayProduce()
        {
            ViewBag.LoggedIn = SessionVariables.SessionID;
            string farmerId = SessionVariables.SessionID;
            return View(FarmerProduce(farmerId));
        } 
        
        //prompts farmer ito add produce
        public IActionResult AddProduce()
        {
            ViewBag.LoggedIn = SessionVariables.SessionID;
            return View();
        }

        //save the farmers produce
        public IActionResult SaveProduce(string prodType, string prodName, string prodQlty, decimal prodAmt, string prodUnit, DateTime packageDate, int shelfLife)
        {
            ProduceDb(prodType, prodName, prodQlty, prodAmt, prodUnit, packageDate, shelfLife);
            return RedirectToAction("DisplayProduce");
        }

        //returns a list of farmers produce
        public List<Produce> FarmerProduce(string farmerId)
        {
            List<Produce> farmerProdLst = new List<Produce>();
            int noItems = db.Produces.Where(x => x.FarmrId == farmerId).Count();
            for (int x = 0; x < noItems; x++)
            {
                Produce farmerProd = db.Produces.Where(x => x.FarmrId == farmerId).AsEnumerable().ElementAt(x);
                farmerProdLst.Add(farmerProd);
            }
            return farmerProdLst;
        }

        //saves produce to database
        public void ProduceDb(string type, string name, string qlty, decimal amt, string unit, DateTime packageDate, int shelfLife)
        {
            produce.FarmrId = SessionVariables.SessionID;
            produce.ProduceName = name;
            produce.Producetype = type;
            produce.Grade = qlty;
            produce.Amount = amt;
            produce.Unit = unit;
            produce.PackedDate = packageDate;
            produce.ShelfLife = shelfLife;
            produce.IsRotten = "N";
            produce.Status = "On sale";

           db.Produces.Add(produce);
            db.SaveChanges();
        }
    }
}
