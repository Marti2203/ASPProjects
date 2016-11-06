﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class SingletonBoxList
    {
        private static IList<BoxModel> models;
        private SingletonBoxList() { }
        public static IList<BoxModel> Models
        {
            get
            {
                if (models == null)
                {
                    models = new List<BoxModel>();
                    models.Add(new BoxModel
                    {
                        Colour = "Blue",
                        Height = 12,
                        Length = 2,
                        Material = "22222",
                        Weight = 123,
                        Width = 555
                    });
                    models.Add(new BoxModel
                    {
                        Colour = "Red",
                        Height = 12,
                        Length = 3,
                        Material = "2244222",
                        Weight = 153,
                        Width = 666
                    });
                }
                return models;
            }
        }
    }
}