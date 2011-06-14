﻿namespace Nancy.Conventions
{
    using System;
    using System.Collections.Generic;
    using ViewEngines;

    /// <summary>
    /// 
    /// </summary>
    public class DefaultViewLocationConventions : IConvention
    {
        /// <summary>
        /// Initialise any conventions this class "owns"
        /// </summary>
        /// <param name="conventions">Convention object instance</param>
        public void Initialise(NancyConventions conventions)
        {
            ConfigureViewLocationConventions(conventions);
        }

        /// <summary>
        /// Asserts that the conventions that this class "owns" are valid
        /// </summary>
        /// <param name="conventions">Conventions object instance</param>
        /// <returns>Tuple containing true/false for valid/invalid, and any error messages</returns>
        public Tuple<bool, string> Validate(NancyConventions conventions)
        {
            if (conventions.ViewLocationConventions == null)
            {
                return Tuple.Create(false, "The view conventions cannot be null.");
            }

            return conventions.ViewLocationConventions.HasConventions() ? 
                Tuple.Create(true, string.Empty) :
                Tuple.Create(false, "The view conventions cannot be empty.");
        }

        private static void ConfigureViewLocationConventions(NancyConventions conventions)
        {
            conventions.ViewLocationConventions = new ViewLocationConventions(new Func<string, object, ViewLocationContext, string>[]
            {
                (viewName, model, viewLocationContext) => {
                    return string.Concat("/", viewName);
                },

                (viewName, model, viewLocationContext) => {
                    return string.Concat("/views/", viewName);
                },

                (viewName, model, viewLocationContext) => {
                    return string.Concat("/views", viewLocationContext.ModulePath, "/", viewName);
                }
            });
        }
    }
}