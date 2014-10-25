using System.Web.Mvc;
using DrunkCheckAPI.Models;

namespace DrunkCheckAPI.Controllers
{
    public class HomeController : Controller
    {
        private const string PortName = "COM3";
        
        public JsonResult Read()
        {
            string portData;
            
            using (ArduinoInterface arduinoInterface = new ArduinoInterface(PortName))
            {
                portData = arduinoInterface.Read();
            }

            return Json(portData);
        }
    }
}