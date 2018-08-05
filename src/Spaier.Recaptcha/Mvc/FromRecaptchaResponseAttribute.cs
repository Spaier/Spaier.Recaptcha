using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.AspNetCore.Mvc
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class FromRecaptchaResponseAttribute : Attribute, IBindingSourceMetadata, IModelNameProvider
    {
        public static BindingSource Source = new BindingSource("from-recaptcha", "Recaptcha", false, false);

        public string Name { get; }

        public BindingSource BindingSource => Source;
    }
}
