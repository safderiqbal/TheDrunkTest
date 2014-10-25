using System.Configuration;
using System.Web.Mvc;
using DrunkCheck.Models;

namespace DrunkCheck.Controllers
{
    public class ArduinoController : Controller
    {
        public JsonResult Read()
        {
            int portData;

            using (ArduinoInterface arduinoInterface = new ArduinoInterface(ConfigurationManager.AppSettings["ComPort"]))
            {
                portData = arduinoInterface.Read();
            }

            return Json(new DrunkCheckResponse(portData), JsonRequestBehavior.AllowGet);
        }
    }
}