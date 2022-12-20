using EventBus;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly IEventBus _eventBus;

        public CatalogController(ILogger<CatalogController> logger, IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }
    }
}