using Catalog_Service.Infrastructure;
using Catalog_Service.Model;
using Catalog_Service.ViewModel;
using EventBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Catalog_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _catalogContext;

        public CatalogController(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        // GET api/v1/[controller]/items/type/1/brand[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items/type/{catalogTypeId}/brand/{catalogBrandId:int?}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<CatalogItem>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsViewModel<CatalogItem>>> ItemsByTypeIdAndBrandIdAsync(int catalogTypeId, int? catalogBrandId, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<CatalogItem>)_catalogContext.CatalogItems;

            root = root.Where(ci => ci.CatalogTypeId == catalogTypeId);

            if (catalogBrandId.HasValue)
            {
                root = root.Where(ci => ci.CatalogBrandId == catalogBrandId);
            }

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);
        }
    }
}