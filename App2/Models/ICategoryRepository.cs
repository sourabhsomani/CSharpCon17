using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App2.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
