﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulasWebApi.Infra.Config
{
    public class AppConfiguration
    {
        private readonly IConfiguration _configuration;
        public AppConfiguration(IConfiguration configuration) 
        {
            this._configuration = configuration;
        }
        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("Postgres");
        }
    }
}
