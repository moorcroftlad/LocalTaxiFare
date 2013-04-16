﻿using System.Web.Mvc;

namespace LocalTaxiFare
{
    public interface ICreateTheTaxiControllerUri
    {
        string GetUriForTaxi(UrlHelper url);
    }

    public class CreateTheTaxiControllerUri : ICreateTheTaxiControllerUri
    {
        public string GetUriForTaxi(UrlHelper url)
        {
            return url.Action("Index", "Taxi");
        }
    }
}