﻿using System;
using System.Web.Mvc;
using Geography;
using LocalTaxiFare.Models;

namespace LocalTaxiFare.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ICreateResultViewModels _resultsViewModelFactory;
        private readonly ICreateLocations _createLocations;

        public ResultsController(ICreateResultViewModels resultsViewModelFactory, ICreateLocations createLocations)
        {
            _resultsViewModelFactory = resultsViewModelFactory;
            _createLocations = createLocations;
        }

        public ViewResult Index(string from, string to, string fromlatlong, string tolatlong)
        {
            var resultsViewModel = new ResultsViewModel();
            try
            {
                var startingPoint = new StartingPoint(_createLocations.GetLocation(fromlatlong), from);
                var destination = new Destination(_createLocations.GetLocation(tolatlong), to);
                
                resultsViewModel = _resultsViewModelFactory.Create(Url, startingPoint, destination);

                return View("Index", resultsViewModel);
            }
            catch (Exception e)
            {
                resultsViewModel.Error = e;
            }

            return View("Error", resultsViewModel);
        }

        public ViewResult Fight()
        {
            return View();
        }
    }
}