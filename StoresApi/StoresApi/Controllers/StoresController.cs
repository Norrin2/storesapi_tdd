using Microsoft.AspNetCore.Mvc;
using StoresApi.Models;
using StoresApi.Repositories;

namespace StoresApi.Controllers
{
    [Route("company/{companyId}/stores")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;

        public StoresController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateStore(int companyId, Store store)
        {
            store.CompanyId = companyId;
            var createdStore = _storeRepository.Add(store);
            return CreatedAtAction(nameof(CreateStore), createdStore);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Store), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetStore(int companyId, int id)
        {
            var store = _storeRepository.FindById(id, companyId);
            if (store == null)
            {
                return NotFound();
            }
            return Ok(store);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Store), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateStore(int companyId, int id, Store store)
        {
            store.CompanyId = companyId;
            store.Id = id;
            var updatedStore = _storeRepository.Update(store);
            if (updatedStore == null)
            {
                return NotFound();
            }

            return Ok(updatedStore);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteStore(int companyId, int id)
        {
            var deleted = _storeRepository.Delete(id, companyId);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
