using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace backend.WebCore
{
    public class InicializeDB
    {
        public static void Initialize(BackendContext context)
        {
            context.Database.EnsureCreated();
            //context.Database.Migrate();
        }
    }
}
