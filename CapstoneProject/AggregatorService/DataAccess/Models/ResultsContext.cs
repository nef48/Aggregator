using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatorService.DataAccess
{
    public class ResultsContext : DbContext
    {
        public ResultsContext(DbContextOptions<ResultsContext> options)
            : base(options)
        {
        }
    }
}
