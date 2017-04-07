using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App2.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App2.Controllers
{
    public class ItemsController : Controller
    {

        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ItemsController(ICategoryRepository categoryRepository, IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
        }
        public ActionResult List()
        {
            return View(_itemRepository.Items);
        }
    }

}
