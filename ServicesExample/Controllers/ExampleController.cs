using ServicesExample.Attributes;

namespace ServicesExample.Controllers
{
    [Controller]
    public class ExampleController
    {
        public ExampleController() { }

        [Method("Get")]
        public bool Get() { return true; }

        [Method("Post")]
        public void Post() { }

        [Method("Put")]
        public void Put() { }

        [Method("Delete")]
        public void Delete() { }
    }
}
