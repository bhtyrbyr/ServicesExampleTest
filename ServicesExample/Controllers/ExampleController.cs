using ServicesExample.Attributes;

namespace ServicesExample.Controllers
{
    [Controller]
    public class ExampleController
    {
        public ExampleController() { }

        [Method("Get")]
        public bool Get() { return true; }
        public void Post() { } 
        public void Put() { }
        public void Delete() { }
    }
}
