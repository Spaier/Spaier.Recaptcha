using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Spaier.Recaptcha.Mvc
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class FilterFactoryAttribute : Attribute, IFilterFactory
    {
        protected bool _isReusable;

        bool IFilterFactory.IsReusable => _isReusable;

        public abstract IFilterMetadata CreateInstance(IServiceProvider serviceProvider);
    }
}
