using System;
using Microsoft.EntityFrameworkCore;

namespace ProniaApp.DAL
{
	public class ApplicationDbContext:DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }
	}
}

