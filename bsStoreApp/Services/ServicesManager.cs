﻿using AutoMapper;
using Repositories.Contracts;
using Services.Contrats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesManager : IServicesManager
    {
        private readonly Lazy<IBookServices> _bookServices;
        public ServicesManager(IRepositoryManager repositoryManager,
            ILoggerService logger,
            IMapper mapper)
        {
            _bookServices= new Lazy<IBookServices>(() =>
            new BookManager(repositoryManager,logger,mapper));
        }

        public IBookServices BookServices => _bookServices.Value;
    }
}
