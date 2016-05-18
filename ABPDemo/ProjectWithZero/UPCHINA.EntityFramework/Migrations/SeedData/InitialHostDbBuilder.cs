﻿using UPCHINA.EntityFramework;
using EntityFramework.DynamicFilters;

namespace UPCHINA.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly UPCHINADbContext _context;

        public InitialHostDbBuilder(UPCHINADbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
